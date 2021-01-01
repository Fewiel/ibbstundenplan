using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//
// Erstellt duch Phillip Weitkamp
// Dieses Werk ist lizenziert unter einer Creative Commons Namensnennung 
// - Nicht-kommerziell - Weitergabe unter gleichen Bedingungen 4.0 International Lizenz.
//
namespace USIT2020Stundenpläne
{
    public partial class FrmMain : Form
    {
        public Settings Settings { get; set; }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public string archivepath;


        public FrmMain()
        {
            InitializeComponent();
            TrayMenuContext();
            Settings = Settings.Load(Path.Combine(Environment.CurrentDirectory, "settings.json"));
            if (Settings.Minimiert)
                WindowState = FormWindowState.Minimized;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            DateTime dateUpdateInfo = new DateTime(2021, 1, 4);
            if (DateTime.Now < dateUpdateInfo)
            {
                MessageBox.Show("Fröhliches neues Jahr! Mit dem Update habe ich einen fehler behoben welcher bei Kalenderwochen unter kw10 auftrat." +
                    " Damit ihr die neuen Stundenpläne erhaltet, " +
                    "bitte den Neusten Stundenplan von kw01 hinzufügen da sich das Jahr im Namen geändert hat!", "Stundenplan Tool - Happy new year! - Updateinfo! Wichtig!");  
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));

            if (!Directory.Exists(Environment.CurrentDirectory + "/Stundenpläne"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Stundenpläne");
            foreach (var f in Directory.GetFiles(Environment.CurrentDirectory + "/Stundenpläne"))
            {
                lbSp.Items.Add(Path.GetFileName(f));
            }

            cbAutoupdate.Checked = Settings.Autoupdate;
            cboxMinuten.Text = Settings.Aktualisierung.ToString();
            cbAutostart.Checked = Settings.Autostart;
            cbMinimiert.Checked = Settings.Minimiert;
            cbArchiv.Checked = Settings.Archivieren;
            archivepath = Settings.ArchivPath;
            var time = Convert.ToInt32(cboxMinuten.Text) * 60 * 1000;
            timer1.Interval = time;
            if (Settings.Minimiert)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
            else
            {
                notifyIcon1.Visible = false;
            }
            UpdateKurse();
            UpdateStundenpläne();
            timer1.Start();
            timer2.Start();
        }



        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var kurs = tbAdd.Text;
            Regex rgx = new Regex("abKW[0-9]{1,2}.pdf$");
            if (rgx.IsMatch(kurs))
            {
                Settings.AddKurs(kurs);
                UpdateKurse();
                UpdateStundenpläne();
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (lbKurse.SelectedItem == null)
                return;
            Settings.RemoveKurs((string)lbKurse.SelectedItem);
            UpdateKurse();
            UpdateStundenpläne();
        }

        private void UpdateKurse()
        {
            lbKurse.Items.Clear();
            lbKurse.Items.AddRange(Settings.Kurse.ToArray());
        }

        private void UpdateStundenpläne()
        {
            var kw0 = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(-14), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var kw1 = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(-7), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var kw2 = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var kw3 = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(7), CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var kw0str = "" + kw0;
            if (kw0 < 10) { kw0str = "0" + kw0; }
            var kw1str = "" + kw1;
            if (kw1 < 10) { kw1str = "0" + kw1; }
            var kw2str = "" + kw2;
            if (kw2 < 10) { kw2str = "0" + kw2; }
            var kw3str = "" + kw3;
            if (kw3 < 10) { kw3str = "0" + kw3; }

            foreach (var k in Settings.Kurse)
            {
                var stundenplan = k.Substring(0, k.LastIndexOf("_"));
                if (!File.Exists(Environment.CurrentDirectory + Path.Combine("/Stundenpläne", stundenplan + "_abKW" + kw0str + ".pdf")))
                {
                    var url1 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw0str + ".pdf";
                    DownloadStundenplan(url1, stundenplan + "_abKW" + kw0str + ".pdf");
                }

                if (!File.Exists(Environment.CurrentDirectory + Path.Combine("/Sundenpläne", stundenplan + "_abKW" + kw1str + ".pdf")))
                {
                    var url2 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw1str + ".pdf";
                    DownloadStundenplan(url2, stundenplan + "_abKW" + kw1str + ".pdf");
                }

                var url3 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw2str + ".pdf";
                var url4 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw3str + ".pdf";

                DownloadStundenplan(url3, stundenplan + "_abKW" + kw2str + ".pdf");
                DownloadStundenplan(url4, stundenplan + "_abKW" + kw3str + ".pdf");
            }
        }

        private static readonly HttpClient client = new HttpClient();

        private void DownloadStundenplan(string url, string fileName)
        {
            client.GetAsync(url).ContinueWith(t =>
            {
                if (!t.IsCompletedSuccessfully || t.Result.StatusCode != System.Net.HttpStatusCode.OK)
                    return;

                t.Result.Content.ReadAsByteArrayAsync().ContinueWith(c =>
                {
                    if (!c.IsCompletedSuccessfully)
                        return;

                    try
                    {
                        if (cbArchiv.Checked)
                            File.WriteAllBytes(Path.Combine(archivepath, fileName), c.Result);
                    }
                    catch (Exception)
                    {
                    }

                    if (File.Exists(Environment.CurrentDirectory + Path.Combine("/Stundenpläne", fileName)))
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + "/tmp");
                        File.WriteAllBytes(Environment.CurrentDirectory + Path.Combine("/tmp", fileName), c.Result);
                        var f1 = CalculateMD5(Environment.CurrentDirectory + Path.Combine("/tmp", fileName));
                        var f2 = CalculateMD5(Environment.CurrentDirectory + Path.Combine("/Stundenpläne", fileName));

                        if (f1 == f2)
                        {
                            Directory.Delete(Environment.CurrentDirectory + "/tmp", true);
                            return;
                        }
                        else
                        {
                            Directory.Delete(Environment.CurrentDirectory + "/tmp", true);
                            notifyIcon1.BalloonTipTitle = "Stundenplan Aktualisiert!";
                            notifyIcon1.BalloonTipText = "Stundenplan Aktualisiert: " + fileName;
                            notifyIcon1.ShowBalloonTip(20000);
                        }
                    }

                    Directory.CreateDirectory(Environment.CurrentDirectory + "/Stundenpläne");
                    try
                    {
                        File.WriteAllBytes(Environment.CurrentDirectory + Path.Combine("/Stundenpläne", fileName), c.Result);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    if (!lbSp.Items.Contains(fileName))
                        lbSp.Items.Add(fileName);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private static string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filename);

            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (lbSp.SelectedItem == null)
                return;
            var path = Environment.CurrentDirectory + "/Stundenpläne/" + lbSp.SelectedItem.ToString();
            var p = new Process
            {
                StartInfo = new ProcessStartInfo(@path)
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Settings.Autoupdate)
            {
                UpdateStundenpläne();
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            var kwNeu = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(7), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var kwNeuStr = "" + kwNeu;
            if (kwNeu < 10) { kwNeuStr = "0" + kwNeu; }

            foreach (var k in Settings.Kurse)
            {
                string stundenplan = k.Substring(0, k.LastIndexOf("_"));
                string neusterStundenplan = stundenplan + "_abKW" + kwNeuStr + ".pdf";
                if (File.Exists(Environment.CurrentDirectory + Path.Combine("/Stundenpläne", neusterStundenplan)) && !Settings.LetzteNotify.Contains(neusterStundenplan))
                {
                    notifyIcon1.BalloonTipTitle = "Neuer Stundenplan verfügbar!";
                    notifyIcon1.BalloonTipText = "Neuer Stundenplan: " + neusterStundenplan;
                    notifyIcon1.ShowBalloonTip(20000);
                    Settings.AddNotify(neusterStundenplan);
                }
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;

                notifyIcon1.BalloonTipTitle = "Läuft im Hintergrund";
                notifyIcon1.BalloonTipText = "IBB Stundenpläne läuft weiter im Hintergrund.";
                notifyIcon1.ShowBalloonTip(2000);
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void CbAutoupdate_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Autoupdate = cbAutoupdate.Checked;
        }

        private void CbAutostart_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Autostart = cbAutostart.Checked;
            if (cbAutostart.Checked)
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var temppath = Environment.CurrentDirectory.Replace(@"\core", "");
                var regpath = Path.Combine(temppath, "Launcher.exe");

                reg.SetValue("IBB Stundenpläne", regpath);
            }
            else
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.DeleteValue("IBB Stundenpläne", false);
            }
        }

        private void CbMinimiert_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Minimiert = cbMinimiert.Checked;
        }

        private void BtnAktualisieren_Click(object sender, EventArgs e)
        {
            UpdateStundenpläne();
        }

        private void CboxMinuten_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Aktualisierung = Convert.ToInt32(cboxMinuten.Text);
            var time = Convert.ToInt32(cboxMinuten.Text) * 60 * 1000;
            timer1.Interval = time;
            timer1.Stop();
            timer1.Start();
        }

        private void Logodiscord_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo("https://discord.gg/cm9k6NH")
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private void Logo3dp_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo("https://3d-panther.de/?page_id=1004")
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void TrayMenuContext()
        {
            notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Archiv", null, MenuArchive_Click);
            notifyIcon1.ContextMenuStrip.Items.Add("Exit", null, MenuExit_Click);
        }

        void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void MenuArchive_Click(object sender, EventArgs e)
        {
            if (archivepath != "")
                Process.Start("explorer.exe", archivepath);
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void cbArchiv_CheckedChanged(object sender, EventArgs e)
        {
            if (cbArchiv.Checked && !(Settings.Archivieren))
            {
                FolderBrowserDialog objDialog = new FolderBrowserDialog();
                objDialog.Description = "Stundenpläne Speichern unter...";
                objDialog.SelectedPath = @"C:\";
                DialogResult objResult = objDialog.ShowDialog(this);
                if (objResult == DialogResult.OK)
                {
                    archivepath = objDialog.SelectedPath;
                    Settings.ArchivPath = archivepath;
                    Settings.Archivieren = true;
                    Settings.Save();
                    MessageBox.Show("Neuer Pfad : " + objDialog.SelectedPath);
                }
                else
                {
                    Settings.ArchivPath = archivepath;
                    Settings.Archivieren = false;
                    Settings.Save();
                    cbArchiv.Checked = false;
                }
            }
            else if (!cbArchiv.Checked)
            {
                archivepath = "";
                Settings.ArchivPath = archivepath;
                Settings.Archivieren = false;
                Settings.Save();
            }
        }
    }
}
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

// Erstellt duch Phillip Weitkamp
// Dieses Werk ist lizenziert unter einer Creative Commons Namensnennung 
// - Nicht-kommerziell - Weitergabe unter gleichen Bedingungen 4.0 International Lizenz.

namespace USIT2020Stundenpläne
{
    public partial class frmMain : Form
    {
        public Settings Settings { get; set; }

        private string Version = "1.1.0";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("Stundenpläne"))
                Directory.CreateDirectory("Stundenpläne");
            foreach (var f in Directory.GetFiles("Stundenpläne"))
            {
                lbSp.Items.Add(Path.GetFileName(f));
            }
            Settings = Settings.Load();
            UpdateKurse();
            UpdateStundenpläne();
            timer1.Start();
            timer2.Start();
            CheckforUpdates();
            notifyIcon1.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void btnRemove_Click(object sender, EventArgs e)
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

            foreach (var k in Settings.Kurse)
            {
                var stundenplan = k.Substring(0, k.LastIndexOf("_"));
                if (!File.Exists(Path.Combine("Stundenpläne", stundenplan + "_abKW" + kw0 + ".pdf")))
                {
                    var url1 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw0 + ".pdf";
                    DownloadStundenplan(url1, stundenplan + "_abKW" + kw0 + ".pdf");
                }           
                if (!File.Exists(Path.Combine("Sundenpläne", stundenplan + "_abKW" + kw1 + ".pdf")))
                {
                    var url2 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw1 + ".pdf";
                    DownloadStundenplan(url2, stundenplan + "_abKW" + kw1 + ".pdf");
                }                

                var url3 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw2 + ".pdf";
                var url4 = "https://us.ibb.com/umschueler/daten/" + stundenplan + "_abKW" + kw3 + ".pdf";
                                
                DownloadStundenplan(url3, stundenplan + "_abKW" + kw2 + ".pdf");
                DownloadStundenplan(url4, stundenplan + "_abKW" + kw3 + ".pdf");
            }
        }

        static readonly HttpClient client = new HttpClient();

        private void DownloadStundenplan(string url, string fileName, bool check = false)
        {
            client.GetAsync(url).ContinueWith(t =>
            {
                if (!t.IsCompletedSuccessfully || t.Result.StatusCode != System.Net.HttpStatusCode.OK)
                    return;

                t.Result.Content.ReadAsByteArrayAsync().ContinueWith(c =>
                {
                    if (!c.IsCompletedSuccessfully)
                        return;

                    if (File.Exists(Path.Combine("Stundenpläne", fileName)))
                    {
                        Directory.CreateDirectory("tmp");
                        File.WriteAllBytes(Path.Combine("tmp", fileName), c.Result);
                        var f1 = CalculateMD5(Path.Combine("tmp", fileName));
                        var f2 = CalculateMD5(Path.Combine("Stundenpläne", fileName));

                        if (f1 == f2)
                        {
                            Directory.Delete("tmp", true);
                            return;
                        }
                        else
                        {
                            Directory.Delete("tmp", true);
                            notifyIcon1.BalloonTipTitle = "Stundenplan Aktualisiert!";
                            notifyIcon1.BalloonTipText = "Stundenplan Aktualisiert: " + fileName;
                            notifyIcon1.ShowBalloonTip(20000);
                        }
                    }

                    Directory.CreateDirectory("Stundenpläne");
                    try
                    {
                        File.WriteAllBytes(Path.Combine("Stundenpläne", fileName), c.Result);
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

        static string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filename);

            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (lbSp.SelectedItem == null)
                return;
            var path = Application.StartupPath + "/Stundenpläne/" + lbSp.SelectedItem.ToString();

            var p = new Process
            {
                StartInfo = new ProcessStartInfo(@path)
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStundenpläne();
            CheckforUpdates();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var kwNeu = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now.AddDays(7), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            foreach (var k in Settings.Kurse)
            {
                var stundenplan = k.Substring(0, k.LastIndexOf("_"));
                var neusterStundenplan = stundenplan + "_abKW" + kwNeu + ".pdf";
                if (File.Exists(Application.StartupPath + Path.Combine("Stundenpläne", neusterStundenplan)) && !Settings.LetzteNotify.Contains(neusterStundenplan))
                {
                    notifyIcon1.BalloonTipTitle = "Neuer Stundenplan verfügbar!";
                    notifyIcon1.BalloonTipText = "Neuer Stundenplan: " + neusterStundenplan;
                    notifyIcon1.ShowBalloonTip(20000);
                    Settings.AddNotify(neusterStundenplan);
                }
            }
        }

        private void CheckforUpdates()
        {
            client.GetAsync("https://3d-panther.de/version.txt").ContinueWith(t =>
            {
                if (!t.IsCompletedSuccessfully || t.Result.StatusCode != System.Net.HttpStatusCode.OK)
                    return;

                t.Result.Content.ReadAsStringAsync().ContinueWith(c =>
                {
                    if (!c.IsCompletedSuccessfully)
                        return;
                    var v = c.Result;
                    if (v != Version + "\n")
                    {
                        MessageBox.Show("Update verfügbar!");
                        btnUpdate.Visible = true;
                    }
                        
                }, TaskScheduler.FromCurrentSynchronizationContext());

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"https://3d-panther.de/?page_id=1004")
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }
}

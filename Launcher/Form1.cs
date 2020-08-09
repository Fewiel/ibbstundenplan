using HashLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public partial class Form1 : Form
    {
        public static readonly List<string> blf = new List<string> { "settings.json" };
        public static readonly List<string> bld = new List<string> { "Stundenpläne" };
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            InitializeComponent();
        }

        private void SetUpdateText(string txt, int progress)
        {
            Invoke(new Action(() =>
            {
                lblLauncher.Text = txt;
                pbLauncher.Value = progress;
            }));
        }

        private async Task Download()
        {
            var updater = new Updater();
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "core"));
            var localHashes = Hasher.GenerateHashes(Path.Combine(Environment.CurrentDirectory, "core"), blf, bld);
            SetUpdateText("Frage Updateserver...", 30);
            var onlineHashes = await updater.
                DownloadHashesAsync("http://update.p-weitkamp.de/ibbstundenplan/hashes/hashes.json", client).ConfigureAwait(false);
            SetUpdateText("Prüfe auf Updates...", 60);
            var updates = updater.CompareHashes(Path.Combine(Environment.CurrentDirectory, "core"), localHashes, onlineHashes);
            SetUpdateText("Lade Updates herrunter...", 80);
            await updater.DownloadAsync("http://update.p-weitkamp.de/ibbstundenplan/data/",
                Path.Combine(Environment.CurrentDirectory, "core"), client, updates).ConfigureAwait(false);
            SetUpdateText("Starte Tool...", 100);
            Process.Start(Path.Combine(Environment.CurrentDirectory, "core/IBB Stundenpläne.exe"));
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetUpdateText("Prüfe Lokale Datein...", 10);
            Task.Factory.StartNew(Download).ContinueWith(t =>
            {
                if (!t.IsCompletedSuccessfully)
                {
                    var path = Path.Combine(Environment.CurrentDirectory, "core/IBB Stundenpläne.exe");
                    if (File.Exists(path))
                    {
                        var mbResult = MessageBox.Show("Update fehlgeschlagen. Versuche es später erneut." + Environment.NewLine +
                            "Willst du das Programm trotzdem Starten? (Es könnte zu Problemen führen)", "Fehler aufgetreten",
                            MessageBoxButtons.YesNo);
                        if (mbResult == DialogResult.Yes)
                            Process.Start(path);
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Download fehlgeschlagen, bitte versuche es später erneut." + Environment.NewLine +
                            "Solltest du Hilfe benötigen Email an: info@p-weitkamp.de");
                        Application.Exit();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}

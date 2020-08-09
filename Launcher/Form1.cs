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

            //var updater = new Updater();
            //var hashes = updater.GenerateHashes(@"C:\Users\pweit\Desktop\USIT2020Stundenpläne\USIT2020Stundenpläne\bin\Release\netcoreapp3.1\publish", blf, bld);

            //foreach (var f in hashes)
            //{
            //    Debug.WriteLine(f.Key + " - " + f.Value);
            //}
            lblLauncher.Text = "Prüfe auf Lokale Daten...";
            pbLauncher.Value = 10;
            Task.Factory.StartNew(Download);
        }

        private async Task Download()
        {
            var updater = new Updater();
            var localHashes = Hasher.GenerateHashes(Path.Combine(Environment.CurrentDirectory, "core"), blf, bld);
            lblLauncher.Text = "Frage Updateserver...";
            pbLauncher.Value = 30;
            var onlineHashes = await updater.DownloadHashesAsync("", client).ConfigureAwait(false);
            lblLauncher.Text = "Prüfe auf Updates...";
            pbLauncher.Value = 60;
            var updates = updater.CompareHashes(localHashes, onlineHashes);
            lblLauncher.Text = "Lade Updates herrunter...";
            pbLauncher.Value = 80;
            await updater.DownloadAsync("", client, updates);
            lblLauncher.Text = "Starte...";
            pbLauncher.Value = 100;
        }
    }
}

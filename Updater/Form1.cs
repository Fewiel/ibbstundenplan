using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    public partial class FrmMain : Form
    {
        private static readonly HttpClient vclient = new HttpClient();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void startDownload()
        {
            Thread thread = new Thread(() =>
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                var path = Environment.CurrentDirectory + "/IBB Stundenpläne.exe";
                client.DownloadFileAsync(new Uri("http://update.p-weitkamp.de/ibbstundenplan/file/stundenplan.exe"), @path);
            });
            thread.Start();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                label1.Text = "Download " + e.BytesReceived + " von " + e.TotalBytesToReceive;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                label1.Text = "Vollständig";
                btnStart.Enabled = false;
                btnAbort.Text = "Schließen";
            });
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            startDownload();
        }

        private void BtnAbort_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

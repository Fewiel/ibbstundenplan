using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        public Form1()
        {
            InitializeComponent();

            //var updater = new Updater();
            //var hashes = updater.GenerateHashes(@"C:\Users\pweit\Desktop\USIT2020Stundenpläne\USIT2020Stundenpläne\bin\Release\netcoreapp3.1\publish", blf, bld);

            //foreach (var f in hashes)
            //{
            //    Debug.WriteLine(f.Key + " - " + f.Value);
            //}
        }
    }
}

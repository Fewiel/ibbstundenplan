using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    internal class Updater
    {
        //Runterladen hashes
        public async Task<Dictionary<string, string>> DownloadHashesAsync(string url)
        {

        }


        //generate local hases (Bekomm dir vorgegeben)
        //vergleich online <-> local
        //  - 0. alt löschen
        //  - 1. Welche fehlen
        //  - 2. Unterschiede

        //neue/aktualisierte runterladen und schreiben/überschreiben
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using HashLib;
using Newtonsoft.Json;

namespace HashCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generiere Hashes...");
            var hashes = Hasher.GenerateHashes("C:/services/updater/ibbstundenplan/data", new List<string>(), new List<string>());
            File.WriteAllText("C:/services/updater/ibbstundenplan/hashes/hashes.json", JsonConvert.SerializeObject(hashes));
        }
    }
}

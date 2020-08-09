using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    internal class Updater
    {
        //Runterladen hashes
        public async Task<Dictionary<string, string>> DownloadHashesAsync(string url, HttpClient client)
        {
            var response = await client.GetAsync(url).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

    
        //vergleich online <-> local
        //  - 0. alt löschen
        //  - 1. Welche fehlen
        //  - 2. Unterschiede
        public Dictionary<string, string> CompareHashes(Dictionary<string, string> localHashes, Dictionary<string, string> onlineHashes)
        {
            var updates = new Dictionary<string, string>();

            foreach (var kvp in localHashes)
            {
                if (!onlineHashes.ContainsKey(kvp.Key))
                {
                    File.Delete(Path.Combine(Environment.CurrentDirectory, kvp.Key));
                }
            }

            foreach (var kvp in onlineHashes)
            {
                if (!localHashes.TryGetValue(kvp.Key, out var hash) || hash != kvp.Value)
                {
                    updates.Add(kvp.Key, kvp.Value);
                }
            }

            return updates;
        }

        //neue/aktualisierte runterladen und schreiben/überschreiben
        public async Task DownloadAsync(string url, HttpClient client, Dictionary<string, string> files)
        {
            foreach (var f in files.Keys)
            {
                var response = await client.GetAsync(url + f).ConfigureAwait(false);
                var data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                await File.WriteAllBytesAsync(Path.Combine(Environment.CurrentDirectory, f), data).ConfigureAwait(false);
            }
        }
    }
}
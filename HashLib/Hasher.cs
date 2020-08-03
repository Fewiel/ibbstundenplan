using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace HashLib
{
    public static class Hasher
    {
        public static Dictionary<string, string> GenerateHashes(string directory, List<string> blacklistedFiles,
          List<string> blacklistedDirectories, Dictionary<string, string> files = null, string curr = "")
        {
            files ??= new Dictionary<string, string>();

            foreach (var f in Directory.GetFiles(directory))
            {
                if (blacklistedFiles.Contains(f))
                    continue;
                var fileKey = Path.Combine(curr, Path.GetFileName(f));
                files.Add(fileKey, CalculateMD5(f));
            }

            foreach (var d in Directory.GetDirectories(directory))
            {
                if (blacklistedDirectories.Contains(d))
                    continue;

                GenerateHashes(d, blacklistedFiles, blacklistedDirectories, files, Path.Combine(curr, Path.GetFileName(d)));
            }

            return files;
        }

        private static string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filename);

            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace USIT2020Stundenpläne
{
    public class Settings
    {
        [JsonProperty("Autostart")]
        private bool _autostart;

        [JsonProperty("Autoupdate")]
        private bool _autoupdate = true;
        
        [JsonProperty("Notification")]
        private bool _notification;

        [JsonProperty("Aktualisierung")]
        private int _aktualisierung = 30;

        [JsonProperty("Minimiert")]
        private bool _minimiert;

        [JsonProperty("LetzteNotify")]
        private readonly List<string> _letztenotify = new List<string>();

        [JsonProperty("Kurse")]
        private readonly List<string> _kurse = new List<string>();

        [JsonIgnore]
        public IReadOnlyList<string> Kurse => _kurse;

        [JsonIgnore]
        public IReadOnlyList<string> LetzteNotify => _letztenotify;

        [JsonIgnore]
        public int Aktualisierung
        {
            get => _aktualisierung;
            set
            {
                if (value != _aktualisierung)
                {
                    _aktualisierung = value;
                    Save();
                }
            }
        }

        [JsonIgnore]
        public bool Minimiert
        {
            get => _minimiert;
            set
            {
                if (value != _minimiert)
                {
                    _minimiert = value;
                    Save();
                }
            }
        }

        [JsonIgnore]
        public bool Autoupdate
        {
            get => _autoupdate;
            set
            {
                if (value != _autoupdate)
                {
                    _autoupdate = value;
                    Save();
                }
            }
        }

        [JsonIgnore]
        public bool Notification
        {
            get => _notification;
            set
            {
                if (value != _notification)
                {
                    _notification = value;
                    Save();
                }
            }
        }

        [JsonIgnore]
        public bool Autostart
        {
            get => _autostart;
            set
            {
                if (value != _autostart)
                {
                    _autostart = value;
                    Save();
                }
            }
        }

        [JsonIgnore]
        public string Path { get; set; }

        public static Settings Load(string path)
        {
            if (!File.Exists(path))
                return new Settings { Path = path };

            var content = File.ReadAllText(path);
            var settings = JsonConvert.DeserializeObject<Settings>(content);
            settings.Path = path;
            return settings;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(Path, json);
        }

        public void AddKurs(string kurs)
        {
            if (_kurse.Contains(kurs))
                return;

            _kurse.Add(kurs);
            Save();
        }

        public void RemoveKurs(string kurs)
        {
            _kurse.Remove(kurs);
            Save();
        }

        public void AddNotify(string notify)
        {
            if (_letztenotify.Contains(notify))
                return;

            _letztenotify.Add(notify);
            Save();
        }
    }
}
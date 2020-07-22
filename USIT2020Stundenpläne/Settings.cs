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
        private bool _autostart;
        private bool _autoupdate = true;
        private bool _notification;
        private int _aktualisierung;
        private bool _minimiert;
        private readonly List<string> _letztenotify = new List<string>();
        private readonly List<string> _kurse = new List<string>();

        public IReadOnlyList<string> Kurse => _kurse;
        public IReadOnlyList<string> LetzteNotify => _letztenotify;

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

        public static Settings Load(string path = "settings.json")
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
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Logger;


namespace ConfigManager
{
    /// <summary>
    /// Configuration Manager for NormaCSBinder
    /// </summary>
    public class ConfigManager
    {
        private static ConfigManager instance;

        /// <summary>
        /// CM Constructor
        /// </summary>
        /// <param name="filename">settings filename</param>
        private ConfigManager(string filename)
        {
            this.FileName = filename;
            try
            {
                string input = File.ReadAllText(filename);
                this.Configuration = JsonConvert.DeserializeObject<Config>(input);
            }
            catch (IOException e)
            {
                Logger.Logger.Error(e, this.GetType().Name);
            }
        }

        public static ConfigManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigManager(Default.ConfigFile);
                }
                return instance;
            }
        }

        public string FileName { get; set; } // config file
        public Config Configuration { get; set; } // Deserialized Config object

        /// <summary>
        /// Save Configuration to file
        /// </summary>
        public void SaveConfig()
        {
            try
            {
                string output = JsonConvert.SerializeObject(this.Configuration);
                File.WriteAllText(this.FileName, output);
            }
            catch (IOException e)
            {
                Logger.Logger.Error(e, this.GetType().Name);
            }
        }
    }
}

using SchoolFeeding.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFeeding.Model.Utilities
{
    public record Configure
    {
        public static User CurrentUser { get; set; }

        public static object GetParametr(string Parametr) =>
            ConfigurationManager.AppSettings[Parametr]??"undefined";
        public static void SetParametr(string Parametr, object value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[Parametr].Value = value.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Uniconta.ClientTools.DataModel;

namespace LykatecMobileApp.Models
{
    public class LocalSettings
    {
        [PrimaryKey]
        public int Id { get; set; } = 1;
        public string APIUsername { get; set; } = "LykApi";
        public string APIPassword { get; set; } = "lyk123";
        public string ContactPersonUsername { get; set; }
        public string ContactPersonPassword { get; set; }
        public string DebtorAccount { get; set; }
    }
}

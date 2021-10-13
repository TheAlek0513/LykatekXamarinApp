using LykatekXamarinApp.Models.Uniconta;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Uniconta.ClientTools.DataModel;

namespace LykatekXamarinApp.Models
{
    public class ContactPerson
    {
        public ContactPerson()
        {

        }

        public ContactPerson(ContactClientUser contactClient)
        {
            KeyStr = contactClient.KeyStr;
            Debtor = contactClient.DCAccount;
            Name = contactClient.Name;
            Email = contactClient.Email;
            Phone = contactClient.Mobile;
            Password = contactClient.Password;
        }

        [PrimaryKey]
        public string KeyStr { get; set; }
        public string Debtor { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}

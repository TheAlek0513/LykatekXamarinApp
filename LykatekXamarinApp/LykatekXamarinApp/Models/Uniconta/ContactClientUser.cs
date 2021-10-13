using System;
using System.Collections.Generic;
using System.Text;
using Uniconta.ClientTools.DataModel;

namespace LykatekXamarinApp.Models.Uniconta
{
	public class ContactClientUser : ContactClient
	{
		public string Password
		{
			get { return this.GetUserFieldString(0); }
			set { this.SetUserFieldString(0, value); NotifyPropertyChanged("Password"); }
		}

		public bool MobileActive
		{
			get { return this.GetUserFieldBoolean(1); }
			set { this.SetUserFieldBoolean(1, value); NotifyPropertyChanged("MobileActive"); }
		}
	}

}

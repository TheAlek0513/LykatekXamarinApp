using System;
using System.Collections.Generic;
using System.Text;
using Uniconta.DataModel;

namespace LykatekXamarinApp.Models.Uniconta
{
	public class ConfigType : TableDataWithKey
	{
		public override int UserTableId { get { return 34493; } }
		public override int CompanyId { get { return 51398; } }

		public string DisplayText => KeyName;
		public string EnglishName
		{
			get { return this.GetUserFieldString(0); }
			set { this.SetUserFieldString(0, value); NotifyPropertyChanged("EnglishName"); }
		}

		public string ConfigGroup
		{
			get { return this.GetUserFieldString(1); }
			set { this.SetUserFieldString(1, value); NotifyPropertyChanged("ConfigGroup"); }
		}
	}

}

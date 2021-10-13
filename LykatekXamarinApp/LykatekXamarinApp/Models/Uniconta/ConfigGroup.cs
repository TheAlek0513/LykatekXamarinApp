using System;
using System.Collections.Generic;
using System.Text;
using Uniconta.DataModel;

namespace LykatekXamarinApp.Models.Uniconta
{
	public class ConfigGroup : TableDataWithKey
	{
		public override int UserTableId { get { return 34490; } }
		public override int CompanyId { get { return 51398; } }
		public string DisplayText => KeyName;
    }
}

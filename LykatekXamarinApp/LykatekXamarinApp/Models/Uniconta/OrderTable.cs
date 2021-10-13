using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;

namespace LykatekXamarinApp.Models.Uniconta
{

	public class OrderTable : TableDataWithKey
	{
		public override int UserTableId { get { return 34671; } }
		public override int CompanyId { get { return 51398; } }

		[ForeignKeyAttribute(ForeignKeyTable = typeof(Debtor))]
		[Display(Name = "Debitor")]
		public string Debtor
		{
			get { return this.GetUserFieldString(0); }
			set { this.SetUserFieldString(0, value); NotifyPropertyChanged("Debtor"); }
		}

		[ForeignKeyAttribute(ForeignKeyTable = typeof(Contact))]
		[Display(Name = "Kontaktperson")]
		public string ContactPerson
		{
			get { return this.GetUserFieldString(1); }
			set { this.SetUserFieldString(1, value); NotifyPropertyChanged("ContactPerson"); }
		}

		[Display(Name = "Dato/Tid")]
		public DateTime CreatedDateTime
		{
			get { return this.GetUserFieldDateTime(2); }
			set { this.SetUserFieldDateTime(2, value); NotifyPropertyChanged("CreatedDateTime"); }
		}

		[Display(Name = "Leveringsdato")]
		public DateTime DeliveryDate
		{
			get { return this.GetUserFieldDateTime(3); }
			set { this.SetUserFieldDateTime(3, value); NotifyPropertyChanged("DeliveryDate"); }
		}

		[Display(Name = "Serie")]
		public string ConfigSeries
		{
			get { return this.GetUserFieldString(4); }
			set { this.SetUserFieldString(4, value); NotifyPropertyChanged("ConfigSeries"); }
		}

		[Display(Name = "M1 (Nominel)")]
		public string M1Nominel
		{
			get { return this.GetUserFieldString(5); }
			set { this.SetUserFieldString(5, value); NotifyPropertyChanged("M1Nominel"); }
		}

		[Display(Name = "M1 - lgd(t)")]
		public string M1LengthTotal
		{
			get { return this.GetUserFieldString(6); }
			set { this.SetUserFieldString(6, value); NotifyPropertyChanged("M1LengthTotal"); }
		}

		[Display(Name = "M1 - lgd")]
		public string M1Length
		{
			get { return this.GetUserFieldString(7); }
			set { this.SetUserFieldString(7, value); NotifyPropertyChanged("M1Length"); }
		}

		[Display(Name = "M1 - c/c")]
		public string M1CC
		{
			get { return this.GetUserFieldString(8); }
			set { this.SetUserFieldString(8, value); NotifyPropertyChanged("M1CC"); }
		}

		[Display(Name = "M1 - gr°")]
		public string M1Degrees
		{
			get { return this.GetUserFieldString(9); }
			set { this.SetUserFieldString(9, value); NotifyPropertyChanged("M1Degrees"); }
		}

		[Display(Name = "M2 (Nominel)")]
		public string M2Nominel
		{
			get { return this.GetUserFieldString(10); }
			set { this.SetUserFieldString(10, value); NotifyPropertyChanged("M2Nominel"); }
		}

		[Display(Name = "M2 - lgd(t)")]
		public string M2LengthTotal
		{
			get { return this.GetUserFieldString(11); }
			set { this.SetUserFieldString(11, value); NotifyPropertyChanged("M2LengthTotal"); }
		}

		[Display(Name = "M2 - lgd")]
		public string M2Length
		{
			get { return this.GetUserFieldString(12); }
			set { this.SetUserFieldString(12, value); NotifyPropertyChanged("M2Length"); }
		}

		[Display(Name = "M2 - gr°")]
		public string M2Degrees
		{
			get { return this.GetUserFieldString(13); }
			set { this.SetUserFieldString(13, value); NotifyPropertyChanged("M2Degrees"); }
		}

		[Display(Name = "T1 (Nominel)")]
		public string T1Nominel
		{
			get { return this.GetUserFieldString(14); }
			set { this.SetUserFieldString(14, value); NotifyPropertyChanged("T1Nominel"); }
		}

		[Display(Name = "T1 - lgd(t)")]
		public string T1LengthTotal
		{
			get { return this.GetUserFieldString(15); }
			set { this.SetUserFieldString(15, value); NotifyPropertyChanged("T1LengthTotal"); }
		}

		[Display(Name = "T1 - lgd")]
		public string T1Length
		{
			get { return this.GetUserFieldString(16); }
			set { this.SetUserFieldString(16, value); NotifyPropertyChanged("T1Length"); }
		}

		[Display(Name = "T1 - gr°")]
		public string T1Degrees
		{
			get { return this.GetUserFieldString(17); }
			set { this.SetUserFieldString(17, value); NotifyPropertyChanged("T1Degrees"); }
		}

		[Display(Name = "Kommentar")]
		public string Comment
		{
			get { return this.GetUserFieldString(18); }
			set { this.SetUserFieldString(18, value); NotifyPropertyChanged("Comment"); }
		}

	}

}

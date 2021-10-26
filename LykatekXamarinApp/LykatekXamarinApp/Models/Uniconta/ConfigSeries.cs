using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Uniconta.Common;
using Uniconta.DataModel;
using Xamarin.Forms;

namespace LykatekXamarinApp.Models.Uniconta
{
		public class ConfigSeries : TableDataWithKey
		{
			public override int UserTableId { get { return 34503; } }
			public override int CompanyId { get { return 51398; } }
			public string DisplayText => KeyName;
			public ImageSource ImageSource { get; set; } = null;

			[Display(Name = "Engelsk navn")]
			public string EnglishName
			{
				get { return this.GetUserFieldString(0); }
				set { this.SetUserFieldString(0, value); NotifyPropertyChanged("EnglishName"); }
			}

			[ForeignKeyAttribute(ForeignKeyTable = typeof(ConfigType))]
			[Display(Name = "Konfigurator type")]
			public string ConfigType
			{
				get { return this.GetUserFieldString(1); }
				set { this.SetUserFieldString(1, value); NotifyPropertyChanged("ConfigType"); }
			}

			[Display(Name = "A-Udtagning")]
			public double WithdrawalA
			{
				get { return this.GetUserFieldDouble(2); }
				set { this.SetUserFieldDouble(2, value); NotifyPropertyChanged("WithdrawalA"); }
			}

			[Display(Name = "A-Savning")]
			public double SawingA
			{
				get { return this.GetUserFieldDouble(3); }
				set { this.SetUserFieldDouble(3, value); NotifyPropertyChanged("SawingA"); }
			}

			[Display(Name = "A-Affasening V-fuge")]
			public double PhasingVFugeA
			{
				get { return this.GetUserFieldDouble(4); }
				set { this.SetUserFieldDouble(4, value); NotifyPropertyChanged("PhasingVFugeA"); }
			}

			[Display(Name = "A-Glycerin")]
			public double GlycerinA
			{
				get { return this.GetUserFieldDouble(5); }
				set { this.SetUserFieldDouble(5, value); NotifyPropertyChanged("GlycerinA"); }
			}

			[Display(Name = "A-Opkravning")]
			public double CollectionA
			{
				get { return this.GetUserFieldDouble(6); }
				set { this.SetUserFieldDouble(6, value); NotifyPropertyChanged("CollectionA"); }
			}

			[Display(Name = "A-Svejsning")]
			public double WeldingA
			{
				get { return this.GetUserFieldDouble(7); }
				set { this.SetUserFieldDouble(7, value); NotifyPropertyChanged("WeldingA"); }
			}

			[Display(Name = "A-Ilagt mastic")]
			public double LoadedMasticA
			{
				get { return this.GetUserFieldDouble(8); }
				set { this.SetUserFieldDouble(8, value); NotifyPropertyChanged("LoadedMasticA"); }
			}

			[Display(Name = "Indpakket")]
			public bool Wrapped
			{
				get { return this.GetUserFieldBoolean(9); }
				set { this.SetUserFieldBoolean(9, value); NotifyPropertyChanged("Wrapped"); }
			}

			[Display(Name = "B-Udtagning")]
			public double WithdrawalB
			{
				get { return this.GetUserFieldDouble(10); }
				set { this.SetUserFieldDouble(10, value); NotifyPropertyChanged("WithdrawalB"); }
			}

			[Display(Name = "B-Savning")]
			public double SawingB
			{
				get { return this.GetUserFieldDouble(11); }
				set { this.SetUserFieldDouble(11, value); NotifyPropertyChanged("SawingB"); }
			}

			[Display(Name = "B-Glycerin")]
			public double GlycerinB
			{
				get { return this.GetUserFieldDouble(12); }
				set { this.SetUserFieldDouble(12, value); NotifyPropertyChanged("GlycerinB"); }
			}

			[Display(Name = "B-Opkravning")]
			public double CollectionB
			{
				get { return this.GetUserFieldDouble(13); }
				set { this.SetUserFieldDouble(13, value); NotifyPropertyChanged("CollectionB"); }
			}

			[Display(Name = "B-Svejsning")]
			public double WeldingB
			{
				get { return this.GetUserFieldDouble(14); }
				set { this.SetUserFieldDouble(14, value); NotifyPropertyChanged("WeldingB"); }
			}

			[Display(Name = "B-Ilagt Mastic")]
			public double LoadedMasticB
			{
				get { return this.GetUserFieldDouble(15); }
				set { this.SetUserFieldDouble(15, value); NotifyPropertyChanged("LoadedMasticB"); }
			}

			[Display(Name = "C-Udtagning")]
			public double WithdrawalC
			{
				get { return this.GetUserFieldDouble(16); }
				set { this.SetUserFieldDouble(16, value); NotifyPropertyChanged("WithdrawalC"); }
			}

			[Display(Name = "C-Savning")]
			public double SawingC
			{
				get { return this.GetUserFieldDouble(17); }
				set { this.SetUserFieldDouble(17, value); NotifyPropertyChanged("SawingC"); }
			}

			[Display(Name = "C-Glycerin")]
			public double GlycerinC
			{
				get { return this.GetUserFieldDouble(18); }
				set { this.SetUserFieldDouble(18, value); NotifyPropertyChanged("GlycerinC"); }
			}

			[Display(Name = "C-Opkravning")]
			public double CollectionC
			{
				get { return this.GetUserFieldDouble(19); }
				set { this.SetUserFieldDouble(19, value); NotifyPropertyChanged("CollectionC"); }
			}

			[Display(Name = "C-Svejsning")]
			public double WeldingC
			{
				get { return this.GetUserFieldDouble(20); }
				set { this.SetUserFieldDouble(20, value); NotifyPropertyChanged("WeldingC"); }
			}

			[Display(Name = "C-Ilagt Mastic")]
			public double LoadedMasticC
			{
				get { return this.GetUserFieldDouble(21); }
				set { this.SetUserFieldDouble(21, value); NotifyPropertyChanged("LoadedMasticC"); }
			}

			[Display(Name = "D-Udtagning")]
			public double WithdrawalD
			{
				get { return this.GetUserFieldDouble(22); }
				set { this.SetUserFieldDouble(22, value); NotifyPropertyChanged("WithdrawalD"); }
			}

			[Display(Name = "D-Savning")]
			public double SawingD
			{
				get { return this.GetUserFieldDouble(23); }
				set { this.SetUserFieldDouble(23, value); NotifyPropertyChanged("SawingD"); }
			}

			[Display(Name = "D-Glycerin")]
			public double GlycerinD
			{
				get { return this.GetUserFieldDouble(24); }
				set { this.SetUserFieldDouble(24, value); NotifyPropertyChanged("GlycerinD"); }
			}

			[Display(Name = "D-Opkravning")]
			public double CollectionD
			{
				get { return this.GetUserFieldDouble(25); }
				set { this.SetUserFieldDouble(25, value); NotifyPropertyChanged("CollectionD"); }
			}

			[Display(Name = "D-Svejsning")]
			public double WeldingD
			{
				get { return this.GetUserFieldDouble(26); }
				set { this.SetUserFieldDouble(26, value); NotifyPropertyChanged("WeldingD"); }
			}

			[Display(Name = "D-Ilagt Mastic")]
			public double LoadedMasticD
			{
				get { return this.GetUserFieldDouble(27); }
				set { this.SetUserFieldDouble(27, value); NotifyPropertyChanged("LoadedMasticD"); }
			}

			[Display(Name = "E-Udtagning")]
			public double WithdrawalE
			{
				get { return this.GetUserFieldDouble(28); }
				set { this.SetUserFieldDouble(28, value); NotifyPropertyChanged("WithdrawalE"); }
			}

			[Display(Name = "E-Savning")]
			public double SawingE
			{
				get { return this.GetUserFieldDouble(29); }
				set { this.SetUserFieldDouble(29, value); NotifyPropertyChanged("SawingE"); }
			}

			[Display(Name = "E-Glycerin")]
			public double GlycerinE
			{
				get { return this.GetUserFieldDouble(30); }
				set { this.SetUserFieldDouble(30, value); NotifyPropertyChanged("GlycerinE"); }
			}

			[Display(Name = "E-Opkravning")]
			public double CollectionE
			{
				get { return this.GetUserFieldDouble(31); }
				set { this.SetUserFieldDouble(31, value); NotifyPropertyChanged("CollectionE"); }
			}

			[Display(Name = "E-Svejsning")]
			public double WeldingE
			{
				get { return this.GetUserFieldDouble(32); }
				set { this.SetUserFieldDouble(32, value); NotifyPropertyChanged("WeldingE"); }
			}

			[Display(Name = "E-Ilagt Mastic")]
			public double LoadedMasticE
			{
				get { return this.GetUserFieldDouble(33); }
				set { this.SetUserFieldDouble(33, value); NotifyPropertyChanged("LoadedMasticE"); }
			}

			[Display(Name = "Udtagning - Plade")]
			public double WithdrawalPlate
			{
				get { return this.GetUserFieldDouble(34); }
				set { this.SetUserFieldDouble(34, value); NotifyPropertyChanged("WithdrawalPlate"); }
			}

			[Display(Name = "Savning - Plade")]
			public double SawingPlate
			{
				get { return this.GetUserFieldDouble(35); }
				set { this.SetUserFieldDouble(35, value); NotifyPropertyChanged("SawingPlate"); }
			}

			[Display(Name = "Glycerin - Plade")]
			public double GlycerinPlate
			{
				get { return this.GetUserFieldDouble(36); }
				set { this.SetUserFieldDouble(36, value); NotifyPropertyChanged("GlycerinPlate"); }
			}

			[Display(Name = "Opkravning - Plade")]
			public double CollectionPlate
			{
				get { return this.GetUserFieldDouble(37); }
				set { this.SetUserFieldDouble(37, value); NotifyPropertyChanged("CollectionPlate"); }
			}

			[Display(Name = "Svejsning - Plade")]
			public double WeldingPlate
			{
				get { return this.GetUserFieldDouble(38); }
				set { this.SetUserFieldDouble(38, value); NotifyPropertyChanged("WeldingPlate"); }
			}

			[Display(Name = "Efterbehandling")]
			public bool PostProcessing
			{
				get { return this.GetUserFieldBoolean(39); }
				set { this.SetUserFieldBoolean(39, value); NotifyPropertyChanged("PostProcessing"); }
			}

			[Display(Name = "A-Savning-Udregning")]
			public double SawingAEquation
			{
				get { return this.GetUserFieldDouble(40); }
				set { this.SetUserFieldDouble(40, value); NotifyPropertyChanged("SawingAEquation"); }
			}

			[Display(Name = "A - Glycerin - Udregning")]
			public double GlycerinAEquation
			{
				get { return this.GetUserFieldDouble(41); }
				set { this.SetUserFieldDouble(41, value); NotifyPropertyChanged("GlycerinAEquation"); }
			}

			[Display(Name = "A - Opkravning - Udregning")]
			public double CollectionAEquation
			{
				get { return this.GetUserFieldDouble(42); }
				set { this.SetUserFieldDouble(42, value); NotifyPropertyChanged("CollectionAEquation"); }
			}

			[Display(Name = "Svejsning - A - Udregning")]
			public double WeldingAEquation
			{
				get { return this.GetUserFieldDouble(43); }
				set { this.SetUserFieldDouble(43, value); NotifyPropertyChanged("WeldingAEquation"); }
			}

			[Display(Name = "Savning - B - Udregning")]
			public double SawingBEquation
			{
				get { return this.GetUserFieldDouble(44); }
				set { this.SetUserFieldDouble(44, value); NotifyPropertyChanged("SawingBEquation"); }
			}

			[Display(Name = "Glycerin - B - Udregning")]
			public double GlycerinBEquation
			{
				get { return this.GetUserFieldDouble(45); }
				set { this.SetUserFieldDouble(45, value); NotifyPropertyChanged("GlycerinBEquation"); }
			}

			[Display(Name = "Opkravning - B - Udregning")]
			public double CollectionBEquation
			{
				get { return this.GetUserFieldDouble(46); }
				set { this.SetUserFieldDouble(46, value); NotifyPropertyChanged("CollectionBEquation"); }
			}

			[Display(Name = "Svejsning - B - Udregning")]
			public double WeldingBEquation
			{
				get { return this.GetUserFieldDouble(47); }
				set { this.SetUserFieldDouble(47, value); NotifyPropertyChanged("WeldingBEquation"); }
			}

			[Display(Name = "Savning - C - Udregning")]
			public double SawingCEquation
			{
				get { return this.GetUserFieldDouble(48); }
				set { this.SetUserFieldDouble(48, value); NotifyPropertyChanged("SawingCEquation"); }
			}

			[Display(Name = "Glycerin - C - Udregning")]
			public double GlycerinCEquation
			{
				get { return this.GetUserFieldDouble(49); }
				set { this.SetUserFieldDouble(49, value); NotifyPropertyChanged("GlycerinCEquation"); }
			}

			[Display(Name = "Opkravning - C - Udregning")]
			public double CollectionCEquation
			{
				get { return this.GetUserFieldDouble(50); }
				set { this.SetUserFieldDouble(50, value); NotifyPropertyChanged("CollectionCEquation"); }
			}

			[Display(Name = "Svejsning - C - Udregning")]
			public double WeldingCEquation
			{
				get { return this.GetUserFieldDouble(51); }
				set { this.SetUserFieldDouble(51, value); NotifyPropertyChanged("WeldingCEquation"); }
			}

			[Display(Name = "Savning - D - Udregning")]
			public double SawingDEquation
			{
				get { return this.GetUserFieldDouble(52); }
				set { this.SetUserFieldDouble(52, value); NotifyPropertyChanged("SawingDEquation"); }
			}

			[Display(Name = "Glycerin - D - Udregning")]
			public double GlycerinDEquation
			{
				get { return this.GetUserFieldDouble(53); }
				set { this.SetUserFieldDouble(53, value); NotifyPropertyChanged("GlycerinDEquation"); }
			}

			[Display(Name = "Opkravning - D - Udregning")]
			public double CollectionDEquation
			{
				get { return this.GetUserFieldDouble(54); }
				set { this.SetUserFieldDouble(54, value); NotifyPropertyChanged("CollectionDEquation"); }
			}

			[Display(Name = "Svejsning - D - Udregning")]
			public double WeldingDEquation
			{
				get { return this.GetUserFieldDouble(55); }
				set { this.SetUserFieldDouble(55, value); NotifyPropertyChanged("WeldingDEquation"); }
			}

			[Display(Name = "Savning - E - Udregning")]
			public double SawingEEquation
			{
				get { return this.GetUserFieldDouble(56); }
				set { this.SetUserFieldDouble(56, value); NotifyPropertyChanged("SawingEEquation"); }
			}

			[Display(Name = "Glycerin - E - Udregning")]
			public double GlycerinEEquation
			{
				get { return this.GetUserFieldDouble(57); }
				set { this.SetUserFieldDouble(57, value); NotifyPropertyChanged("GlycerinEEquation"); }
			}

			[Display(Name = "Opkravning - E - Udregning")]
			public double CollectionEEquation
			{
				get { return this.GetUserFieldDouble(58); }
				set { this.SetUserFieldDouble(58, value); NotifyPropertyChanged("CollectionEEquation"); }
			}

			[Display(Name = "Svejsning - E - Udregning")]
			public double WeldingEEquation
			{
				get { return this.GetUserFieldDouble(59); }
				set { this.SetUserFieldDouble(59, value); NotifyPropertyChanged("WeldingEEquation"); }
			}

			[Display(Name = "Lager udtagning")]
			public double StorageWithdrawal
			{
				get { return this.GetUserFieldDouble(60); }
				set { this.SetUserFieldDouble(60, value); NotifyPropertyChanged("StorageWithdrawal"); }
			}

			[ForeignKeyAttribute(ForeignKeyTable = typeof(InvItem))]
			[Display(Name = "Varenummer")]
			public string InvItem
			{
				get { return this.GetUserFieldString(61); }
				set { this.SetUserFieldString(61, value); NotifyPropertyChanged("InvItem"); }
			}

			[Display(Name = "Vis på App")]
			public bool AppItem
			{
				get { return this.GetUserFieldBoolean(62); }
				set { this.SetUserFieldBoolean(62, value); NotifyPropertyChanged("AppItem"); }
			}

			[Display(Name = "M1 dim antal")]
			public long M1Dim
			{
				get { return this.GetUserFieldInt64(63); }
				set { this.SetUserFieldInt64(63, value); NotifyPropertyChanged("M1Dim"); }
			}

			[Display(Name = "M1 længde antal (t)")]
			public long M1TotalLength
			{
				get { return this.GetUserFieldInt64(64); }
				set { this.SetUserFieldInt64(64, value); NotifyPropertyChanged("M1TotalLength"); }
			}

			[Display(Name = "M1 grader antal")]
			public long M1Degrees
			{
				get { return this.GetUserFieldInt64(65); }
				set { this.SetUserFieldInt64(65, value); NotifyPropertyChanged("M1Degrees"); }
			}

			[Display(Name = "M2 dim antal")]
			public long M2Dim
			{
				get { return this.GetUserFieldInt64(66); }
				set { this.SetUserFieldInt64(66, value); NotifyPropertyChanged("M2Dim"); }
			}

			[Display(Name = "M2 længde antal (t)")]
			public long M2TotalLength
			{
				get { return this.GetUserFieldInt64(67); }
				set { this.SetUserFieldInt64(67, value); NotifyPropertyChanged("M2TotalLength"); }
			}

			[Display(Name = "M2 grader antal")]
			public long M2Degrees
			{
				get { return this.GetUserFieldInt64(68); }
				set { this.SetUserFieldInt64(68, value); NotifyPropertyChanged("M2Degrees"); }
			}

			[Display(Name = "T1 dim antal")]
			public long T1Dim
			{
				get { return this.GetUserFieldInt64(69); }
				set { this.SetUserFieldInt64(69, value); NotifyPropertyChanged("T1Dim"); }
			}

			[Display(Name = "T1 længde antal (t)")]
			public long T1TotalLength
			{
				get { return this.GetUserFieldInt64(70); }
				set { this.SetUserFieldInt64(70, value); NotifyPropertyChanged("T1TotalLength"); }
			}

			[Display(Name = "T1 grader antal")]
			public long T1Degrees
			{
				get { return this.GetUserFieldInt64(71); }
				set { this.SetUserFieldInt64(71, value); NotifyPropertyChanged("T1Degrees"); }
			}

			[Display(Name = "T2 dim antal")]
			public long T2Dim
			{
				get { return this.GetUserFieldInt64(72); }
				set { this.SetUserFieldInt64(72, value); NotifyPropertyChanged("T2Dim"); }
			}

			[Display(Name = "T2 længde antal")]
			public long T2Length
			{
				get { return this.GetUserFieldInt64(73); }
				set { this.SetUserFieldInt64(73, value); NotifyPropertyChanged("T2Length"); }
			}

			[Display(Name = "T2 grader antal")]
			public long T2Degrees
			{
				get { return this.GetUserFieldInt64(74); }
				set { this.SetUserFieldInt64(74, value); NotifyPropertyChanged("T2Degrees"); }
			}

			[Display(Name = "M1 - c/c")]
			public long M1CC
			{
				get { return this.GetUserFieldInt64(75); }
				set { this.SetUserFieldInt64(75, value); NotifyPropertyChanged("M1CC"); }
			}

			[Display(Name = "M1 længde")]
			public long M1Length
			{
				get { return this.GetUserFieldInt64(76); }
				set { this.SetUserFieldInt64(76, value); NotifyPropertyChanged("M1Length"); }
			}

			[Display(Name = "M2 længde")]
			public long M2Length
			{
				get { return this.GetUserFieldInt64(77); }
				set { this.SetUserFieldInt64(77, value); NotifyPropertyChanged("M2Length"); }
			}

			[Display(Name = "T1 Længde")]
			public long T1Length
			{
				get { return this.GetUserFieldInt64(78); }
				set { this.SetUserFieldInt64(78, value); NotifyPropertyChanged("T1Length"); }
			}

			[Display(Name = "Serienummer")]
			public string SeriesNumber
			{
				get { return this.GetUserFieldString(79); }
				set { this.SetUserFieldString(79, value); NotifyPropertyChanged("SeriesNumber"); }
			}
		}

	}


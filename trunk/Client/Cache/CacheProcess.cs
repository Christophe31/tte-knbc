﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Client.BusinessService;
using System.Threading;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.ServiceModel;
using DDay.iCal;
using System.ServiceModel.Security;
using System.Xml;

namespace Client
{
	internal class CacheProcess
	{
		public BusinessService.BusinessLayerClient Server;
		public BusinessService.UserData CurrentUser;
		public delegate void CacheGetter(IdName idn);
		public delegate void FileWriter(object obj, string fileName);

		public bool ServerAvailable
		{
			get
			{
				if (Server != null)
					return Server.State == System.ServiceModel.CommunicationState.Opened;
				return false;
			}
		}
		public bool CacheAvailable { get { return (new string[] {"CampusPeriodClassTree","CurrentUser", "University", "Campus", "Period"}).All(kv => File.Exists(otherFileNames[kv])); } }
		System.Runtime.Serialization.NetDataContractSerializer formatter = new System.Runtime.Serialization.NetDataContractSerializer();
		#region singleton
		static protected CacheProcess self;
		protected CacheProcess()
		{
			if (!Directory.Exists(folder))
				System.IO.Directory.CreateDirectory(folder);
			initCacheVars();
			if (File.Exists(otherFileNames["CurrentUser"]))
			{
				CurrentUser = ReadFromFile(otherFileNames["CurrentUser"]) as UserData;
			}
		}
		public bool logToWebService(string login, string password)
		{
			try
			{
				Server = new BusinessLayerClient();
				Server.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
				Server.ClientCredentials.UserName.UserName = login;
				Server.ClientCredentials.UserName.Password = password;
				Server.Open();
				CurrentUser = Server.getUserData();
				CurrentUser.Password = password;
				return true;
			}
			catch (MessageSecurityException)
			{
				return false;
			}
			catch (ArgumentException)
			{
				return false;
			}
		}

		static public CacheProcess Current
		{
			get
			{
				if (self != null)
					return self;
				self = new CacheProcess();
				return self;
			}
		}
		#endregion

		#region File Naming Region

		string extention = ".cache";
		string folder=@"Cache\";
		public Dictionary<string, string> otherFileNames;
		private void initCacheVars()
		{
			otherFileNames = new Dictionary<string, string> 
				{
					{"Promotion"				,folder+"PromotionList"+extention},
					{"Subject"					,folder+"SubjectList"+extention},
					{"Modality"					,folder+"ModalityList"+extention},
					{"CampusPeriodClassTree"	,folder+"CampusPeriodClassTree"+extention},
					{"CurrentUser"				,folder+"CurrentUser"+extention}
				};
			foreach (var s in EventType.EventTypeNames.Select(kv => kv.Key))
			{
				otherFileNames.Add(Enum.GetName(s.GetType(), s), folder+ Enum.GetName(s.GetType(), s) + "List" + extention);
			}
		}

		public string fileNameFromIdName(IdName idn)
		{
			return folder+idn.Name + "_" + idn.Id.ToString() + extention;
		}

		#endregion

		#region foos

		public void WriteToFile(object obj, string fileName)
		{

			FileStream fs = new FileStream(fileName, FileMode.Create);
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
			NetDataContractSerializer ser =
				new NetDataContractSerializer();
			ser.WriteObject(writer, obj);
			writer.Close();
		}
		public object ReadFromFile(string fileName)
		{


			FileStream fs = new FileStream(fileName,
		FileMode.Open);
			XmlDictionaryReader reader =
				XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
			NetDataContractSerializer ser = new NetDataContractSerializer();

			object o =
				ser.ReadObject(reader, true);
			fs.Close();
			return o;
	
		}
		public void RefreshCache(IdName idn)
		{
			((CacheGetter)refreshCache).BeginInvoke(idn, null, null);
		}

		private void refreshCache(IdName idn)
		{
			WriteToFile(new Tuple<DateTime,EventData[]>(DateTime.Now,Server.getEvents(idn.Id, DateTime.MinValue, DateTime.MaxValue)), fileNameFromIdName(idn));
		}
		#endregion
	}
}

using System;
using System.IO;
using Newtonsoft.Json;

namespace OZONEOnigiri.Configuration
{
	public class OnigiriConfig
	{
		public static readonly string SavePath = "./config.json";
		public string Token { get; set; }
		public string Prefix { get; set; }

		public static OnigiriConfig LoadIfExists()
		{
			if (!File.Exists(OnigiriConfig.SavePath))
			{
				File.WriteAllText(OnigiriConfig.SavePath, JsonConvert.SerializeObject(new OnigiriConfig()
				{
					Token = "",
					Prefix = "o!"
				}));

				Console.WriteLine("Configuration file created at " + OnigiriConfig.SavePath);
				Environment.Exit(0);
			}

			return JsonConvert.DeserializeObject<OnigiriConfig>(File.ReadAllText(OnigiriConfig.SavePath));
		}

		public void Save()
		{
			File.WriteAllText(OnigiriConfig.SavePath, JsonConvert.SerializeObject(this));
		}
	}
}
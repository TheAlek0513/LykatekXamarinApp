using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace LykatekXamarinApp.Util
{
    public class Logger
    {
        public static void log(string title, string description)
        {
            string token = "https://discord.com/api/webhooks/895969715820826674/TzPzUMgsFUftW09qZkcs7MBvUwh1_TD3n19Rnm58alem-H_haGFbbTC4I6jXetWAsrci";

            WebRequest wr = (HttpWebRequest)WebRequest.Create(token);

            wr.ContentType = "application/json";

            wr.Method = "POST";

            var dt = DateTime.UtcNow.ToLocalTime();

            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = "Logger",
                    embeds = new[]
                    {
                        new
                        {
                            description = description,
                            title = title + " " + dt.ToString()
                        }
                    }
                }); ;
                sw.WriteLine(json);
            }
            var response = (HttpWebResponse)wr.GetResponse();
        }
    }
}

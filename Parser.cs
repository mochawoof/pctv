using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pctv
{
    public class Parser
    {
        public string raw;
        public List<Channel> channels = new List<Channel>();

        public Parser(string masterUrl)
        {
            //fetch url
            HttpClient http = new HttpClient();
            http.Timeout = TimeSpan.FromSeconds(10);
            var response = Task.Run(() =>
            {
                return http.GetStringAsync(masterUrl);
            }).GetAwaiter().GetResult();


            //validate that this is m3u8
            if (!response.StartsWith("#EXTM3U"))
            {
                throw new Exception("Source isn't M3U8!");
            }

            //parse response
            string[] lines = response.Split("\n");
            for (int i=0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith("http"))
                {
                    if (i > 0)
                    {
                        string previousLine = lines[i - 1];
                        if (previousLine.StartsWith("#EXTINF"))
                        {
                            string[] attr = previousLine.Split(",");
                            Channel newChannel = new Channel();
                            newChannel.url = line;
                            newChannel.id = channels.Count.ToString();
                            newChannel.title = attr[attr.Length - 1];

                            channels.Add(newChannel);
                        }
                    }
                }
            }
        }
    }

    public class Channel
    {
        public string title;
        public string id;
        public string url;
        public string group;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace GhettoDiscordWPF
{
    class DiscordWebClient
    {
        String AuthToken;
        const string GET_DMCHANNEL_URL = ("https://discordapp.com/api/v6/users/@me/channels");
        const string DM_CHANNEL_URL = ("https://discordapp.com/api/v6/channels/{0}/messages?before={1}&limit=100");
        const string CURRENTUSER_URL = ("https://discordapp.com/api/v6/users/@me");

        public DiscordWebClient(string authToken)
        {
            AuthToken = authToken;
        }

        WebClient CreateWebClient()
        {
            WebClient discord = new WebClient();
            discord.Headers["Authorization"] = AuthToken;
            //discord.Headers["Authorization"] = "MjQ1Mzk0MTE3NzU1MDExMDgy.CwLclQ.QFXIkwqxJphjRLOU19vBS2jkyWw";
            return discord;
        }

        public string GetUser()
        {
            using (WebClient discord = CreateWebClient())
            {
                return JsonConvert.DeserializeObject<User>(discord.DownloadString(CURRENTUSER_URL)).username; 
            }
        }

        public Messages[] GetDMMessages(string channel_Id, string lastMessage)
        {
            using (WebClient discord = CreateWebClient())
            {
                string dmMessages = discord.DownloadString(string.Format(DM_CHANNEL_URL, channel_Id, lastMessage));
                Messages[] dmMessagesFormatted = JsonConvert.DeserializeObject<Messages[]>(dmMessages);
                return dmMessagesFormatted;
            }
        }

        public GuildChannel[] GetDMChannels()
        {
            using (WebClient discord = CreateWebClient())
            {
                string dmChannels = discord.DownloadString(GET_DMCHANNEL_URL);
                GuildChannel[] dmChannelsFormatted = JsonConvert.DeserializeObject<GuildChannel[]>(dmChannels);
                return dmChannelsFormatted;
            }
        }

    }
}
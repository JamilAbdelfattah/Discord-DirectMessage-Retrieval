using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhettoDiscordWPF
{
    class GuildChannel
    {
        public string id;
        public bool is_private;
        public recipient[] recipients;
        public string last_message_id;
    }

    class recipient
    {
        public string username;
    }
}

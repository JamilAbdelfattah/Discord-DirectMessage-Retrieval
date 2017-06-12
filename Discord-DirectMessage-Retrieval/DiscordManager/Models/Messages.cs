using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhettoDiscordWPF
{
    class Messages
    {
        public string id;
        public string channel_id;
        public Author author;
        public string content;
        public string timestamp;
        public string edited_timestamp;
    }

    class Author
    {
        public string username;
    }
}

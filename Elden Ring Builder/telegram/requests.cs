using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elden_Ring_Builder.models;

namespace Elden_Ring_Builder.telegram
{
    class requests
    {
        public class Application
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string RequestText { get; set; }
            public DateTime Timestamp { get; set; }

            public Application(string name, string email, string requestText, DateTime timestamp)
            {
                Name = name;
                Email = email;
                RequestText = requestText;
                Timestamp = timestamp;
            }

            public Application()
            {
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    class runes
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? effect { get; set; }
        public string? location { get; set; }
        public string? type { get; set; }
        public string? image_path { get; set; }

        public runes(string name, string? description, string? effect, string? location, string? type, string? image_path)
        {
            this.name = name;
            this.description = description;
            this.effect = effect;
            this.location = location;
            this.type = type;
            this.image_path = image_path;
        }
        public runes() { }
    }
}

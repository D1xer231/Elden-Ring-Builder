using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    [Table("runes")] // точное имя из MySQL
    public class runes
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? effect { get; set; }
        public string? location { get; set; }
        public string? type { get; set; }
        public string? image_path { get; set; }
        public string? map_link { get; set; }

        public runes(string name, string? description, string? effect, string? location, string? type, string? image_path, string? map_link)
        {
            this.name = name;
            this.description = description;
            this.effect = effect;
            this.location = location;
            this.type = type;
            this.image_path = image_path;
            this.map_link = map_link;
        }
        public runes() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    [Table("weapons")] // точное имя из MySQL
    public class weapons
    {
        public int id { get; set; }
        public string name { get; set; }
        public int physic { get; set; }
        public int magic { get; set; }
        public int fire { get; set; }
        public int ligt { get; set; }
        public int holy { get; set; }
        public int crift { get; set; }
        public string? image_path { get; set; }

        public string? sca_strength { get; set; }
        public string? sca_dexterity { get; set; }
        public string? sca_intelligence { get; set; }
        public string? sca_faith { get; set; }
        public string? sca_arcane { get; set; }
        public int? req_strength { get; set; }
        public int? req_dexterity { get; set; }
        public int? req_intelligence { get; set; }
        public int? req_faith { get; set; }
        public int? req_arcane { get; set; }
        public string? map_link { get; set; }
        public string? location { get; set; }


        public weapons(string name, int physic, int magic, int fire, int ligt, int holy, int crift, string image_path, string? sca_strength, string? sca_dexterity, string? sca_intelligence, string? sca_faith, string? sca_arcane, int? req_strength, int? req_dexterity, int? req_intelligence, int? req_faith, int? req_arcane, string? map_link, string? location)
        {
            this.name = name;
            this.physic = physic;
            this.magic = magic;
            this.fire = fire;
            this.ligt = ligt;
            this.holy = holy;
            this.crift = crift;
            this.image_path = image_path;
            this.sca_strength = sca_strength;
            this.sca_dexterity = sca_dexterity;
            this.sca_intelligence = sca_intelligence;
            this.sca_faith = sca_faith;
            this.sca_arcane = sca_arcane;
            this.req_strength = req_strength;
            this.req_dexterity = req_dexterity;
            this.req_intelligence = req_intelligence;
            this.req_faith = req_faith;
            this.req_arcane = req_arcane;
            this.map_link = map_link;
            this.location = location;
        }
        public weapons() { }
    }
}

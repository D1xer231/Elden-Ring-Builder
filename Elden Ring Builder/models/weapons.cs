using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    [Table("weapons")] // точное имя из MySQL
    class weapons
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

        public weapons(string name, int physic, int magic, int fire, int ligt, int holy, int crift, string image_path)
        {
            this.name = name;
            this.physic = physic;
            this.magic = magic;
            this.fire = fire;
            this.ligt = ligt;
            this.holy = holy;
            this.crift = crift;
            this.image_path = image_path;
        }
        public weapons() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    [Table("gallery")] // точное имя из MySQL
    class gallery
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string? description { get; set; }

        public gallery(string path)
        {
            this.path = path;
        }
        public gallery()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    using System.Text.Json.Serialization;

    public class WeaponResponse
    {
        public bool success { get; set; }
        public int count { get; set; }
        public List<WeaponApiModel> data { get; set; }
    }

    public class WeaponApiModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public Attack attack { get; set; }
    }

    public class Attack
    {
        public string name { get; set; }
        public string id { get; set; }

        public int physical { get; set; }
        public int magic { get; set; }
        public int fire { get; set; }
        public int lightning { get; set; }
        public int holy { get; set; }
        public int critical { get; set; }
    }


}

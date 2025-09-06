using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.models
{
    internal class builds
    {
        public int Id { get; set; }           
        public string Name { get; set; }
        public int? LVL { get; set; }
        public int Vigor { get; set; }
        public int Mind { get; set; }
        public int Endurance { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Faith { get; set; }
        public int Arcane { get; set; }
        public string? img_path { get; set; }     

        public builds(string name, int lvl, int vigor, int mind, int endurance, int strength, int dexterity, int intelligence, int faith, int arcane, string img_path)
        {
            Name = name;
            LVL = lvl;
            Vigor = vigor;
            Mind = mind;
            Endurance = endurance;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Faith = faith;
            Arcane = arcane;
            this.img_path = img_path;
        }

        public builds() { }
    }
}

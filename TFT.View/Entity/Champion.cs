using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TFT.View.Entity
{
    public class Champion
    {
        public string Name { get; set; }
        public string ChampionId { get; set; }
        public string Cost { get; set; }
        public List<string> TraitsId { get; set; }
        public List<string> Traits { get; set; }
        public List<string> TraitsImg { get; set; }
        public string ChampionImg => ChampionId + ".png";


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TFT.View.Entity;
using TFT.View.Models.Champions;

namespace TFT.View.Controllers
{
    public class ChampionsController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ChampionsList()
        {
            XElement ChampionsXML = XElement.Load("C:/Users/molme/source/repos/TFT.View/TFT.View/Data/Champions.xml");

            var queryXML =
                from champ in ChampionsXML.Elements("row")
                orderby int.Parse(champ.Element("cost").Value) descending
                select champ;



            List<Champion> champions = new List<Champion>();

            foreach (var item in queryXML)
            {
                var traitsId = new List<string>();
                var traits = new List<string>();
                var traitsImg = new List<string>();


                for (var i = 0; i < item.Elements("traits").ToArray().Length; i++)
                {
                    var trait = item.Elements("traits").ElementAt(i).Value;
                    traitsId.Add(trait);

                    string[] display = trait.Split('_');
                    traits.Add(display[1]);
                    traitsImg.Add(display[1] + ".png");

                }
                champions.Add(new Champion() { Name = item.Element("name").Value, ChampionId = item.Element("championId").Value, Cost = item.Element("cost").Value, TraitsId = traitsId, Traits = traits, TraitsImg = traitsImg });
            }

            var model = new ChampionsViewModel
            {
                Champions = champions
            };

            return PartialView("_ChampionsList", model);

        }
    }
}
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
            XElement TraitsXML = XElement.Load("C:/Users/molme/source/repos/TFT.View/TFT.View/Data/Traits.xml");


            var queryTraits =
               from trait in TraitsXML.Elements("row")
               orderby trait.Element("name").Value ascending
               select trait;

            List<Trait> traitsList = new List<Trait>();

            foreach (var item in queryTraits)
            {
                traitsList.Add(new Trait() { Name = item.Element("name").Value, TraitId = item.Element("key").Value, Type = item.Element("type").Value });
            }

            var model = new ChampionsViewModel
            {
                Traits = traitsList
            };
            return View(model);
        }

        public ActionResult ChampionsList()
        {
            XElement ChampionsXML = XElement.Load("C:/Users/molme/source/repos/TFT.View/TFT.View/Data/Champions.xml");


            var queryChampion =
                from champ in ChampionsXML.Elements("row")
                orderby int.Parse(champ.Element("cost").Value) descending
                select champ;

            List<Champion> champions = new List<Champion>();

            foreach (var item in queryChampion)
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

            var formData = new FormData();
            formData.Biggest = true;

            var model = new ChampionsViewModel
            {
                Champions = champions,
                FormData = formData
            };

            return PartialView("_ChampionsList", model);

        }

        [HttpPost]
        public ActionResult SearchChampion(FormData form)
        {
            XElement ChampionsXML = XElement.Load("C:/Users/molme/source/repos/TFT.View/TFT.View/Data/Champions.xml");

            var queryXML =
                from champ in ChampionsXML.Elements("row")
                orderby int.Parse(champ.Element("cost").Value) descending
                select champ;

            List<Champion> champions = new List<Champion>();

            foreach (var item in queryXML)
            {
                var name = item.Element("name").Value.ToLower();
                if (form.Search == null || name.Contains(form.Search.ToLower()))
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

            }

            var model = new ChampionsViewModel
            {
                Champions = champions,
                FormData = form
            };

            return PartialView("_ChampionsList", model);

        }
    }
}
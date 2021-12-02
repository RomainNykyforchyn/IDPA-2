using idpa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace idpa.Controllers
{
    public class HomeController : Controller
    {   private String xmlPath;
        private HomeModel model;
        private XDocument doc;
        public HomeController()
        {
            model = new HomeModel();
            doc = model.getDoc();
        }

        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String name,  String password)
        {
            XDocument doc = XDocument.Load(xmlPath);
            XmlNode node;
            return RedirectToAction("/Register");
        }


        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [HttpPost]
        public ActionResult Register(String name, String password, String international, String amount, String volume, String provider)
        {
            //Declaration
            int intAmount;
            Boolean boolInter;
            int intVolume;

            //Validation
            try
            {
                intAmount = Int32.Parse(amount);
                intVolume = Int32.Parse(volume);
            }
            catch (Exception e)
            {
                return View();
            }
            switch (provider)
            {
                case "Swisscom":
                    break;
                case "Sunrise": 
                    break;
                case "Salt": 
                    break;
                case "Yallo": 
                    break;
                case "Wingo": 
                    break;
                default: return View();

            }
            
            if (international.Equals("Ja"))
            {
                boolInter = true;
            }
            else { if (international.Equals("Nein"))
                {
                    boolInter = false;
                }
                else
                {
                    return View();
                }
            }
            

            
            
            XElement users = doc.Element("users");
            XElement user = new XElement("user");
            user.Add(new XElement("name", name), new XElement("password",password), new XElement("international", boolInter), new XElement("amount", intAmount), new XElement("volume", intVolume), new XElement("provider", provider));
            users.Add(user);
            /*school.Add(new XElement("Student",
                       new XElement("FirstName", "David"),
                       new XElement("LastName", "Smith")));*/
            doc.Save(model.getPath());
            return RedirectToAction("/Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
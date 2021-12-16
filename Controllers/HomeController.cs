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
        private bool isLoggedIn; 
        public HomeController()
        {
            
            
            
            model = new HomeModel();
            doc = model.getDoc();
        }

        
        [HttpGet]
        public ActionResult Index()
        {
            Session["isLoggedIn"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(String name,  String password)
        {
            XDocument doc = model.getDoc();
            try { 
               IEnumerable<XElement> user = from el in doc.Elements("user") where (string)el.Element("name")==name && (string)el.Element("password") == password select el;
                Session["isLoggedIn"] = true;
             }catch(Exception e)
            {

             }
            


            /*
             XElement root = XElement.Load("PurchaseOrder.xml");
IEnumerable<XElement> address =
    from el in root.Elements("Address")
    where (string)el.Attribute("Type") == "Billing"
    select el;
foreach (XElement el in address)
    Console.WriteLine(el);
             */

            return RedirectToAction("/Tarif");
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
            user.Add(new XAttribute("id", GenerateNextId()));
            user.Add(new XElement("name", name), new XElement("password",password), new XElement("international", boolInter), new XElement("amount", intAmount), new XElement("volume", intVolume), new XElement("provider", provider));
            users.Add(user);
            /*school.Add(new XElement("Student",
                       new XElement("FirstName", "David"),
                       new XElement("LastName", "Smith")));*/
            doc.Save(model.getPath());
            return RedirectToAction("/Index");
        }

        public ActionResult Tarif()
        {
            object value = Session["ediblesession"];
            if (value != null)
            {
                isLoggedIn = (bool)value;
            }
            else
            {
                isLoggedIn = false; // or whatever you want to do if there is no value
            }
            if (isLoggedIn)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("/Index");
                }  
            
            
            

            
        }
        private int GenerateNextId()
        {
            return doc.Descendants("user")
                       .OrderByDescending(x => Convert.ToInt32(x.Attribute("id").Value))
                       .Select(x => Convert.ToInt32(x.Attribute("id").Value))
                       .FirstOrDefault() + 1;
        }
    }
}
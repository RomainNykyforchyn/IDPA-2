using idpa.Content;
using idpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace idpa.Controllers
{
    public class HomeController : Controller
    {
        private HomeModel model;
        private XDocument doc;
        private bool isLoggedIn;
        private User loggedUser;
        //private String name, password;
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
        public ActionResult Index(String name, String password)
        {
            XDocument doc = model.getDoc();
            try
            {   
                IEnumerable<XElement> user_ = from el in doc.Elements("user") where (string)el.Element("name") == name && (string)el.Element("password") == password select el;
                //for(int i = 0; i < user.Elements().Count())
                //{

                //}
                XElement user = (XElement)user_;
                loggedUser = new User(user.Attributes()., user.Element, user.ElementAt(1), user.ElementAt(2), user.ElementAt(3), user.ElementAt(4), user.ElementAt(5), user.ElementAt(6));
                    Session["isLoggedIn"] = true;
                if()
                Session["name"] = name;
                Session["password"] = password;


            }
            catch (Exception e)
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
            //this.name = name;
            //this.password = password;
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
            else
            {
                if (international.Equals("Nein"))
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
            user.Add(new XElement("name", name), new XElement("password", password), new XElement("international", boolInter), new XElement("amount", intAmount), new XElement("volume", intVolume), new XElement("provider", provider), new XElement("admin", "0"));
            users.Add(user);
            /*school.Add(new XElement("Student",
                       new XElement("FirstName", "David"),
                       new XElement("LastName", "Smith")));*/
            doc.Save(model.getPath());
            
            return RedirectToAction("/Index");
        }

        public ActionResult Tarif()
        {
            object value = Session["isLoggedIn"];
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
        [HttpPost]
        public ActionResult Tarif(String international, String amount, String volume, String provider)
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
            else
            {
                if (international.Equals("Nein"))
                {
                    boolInter = false;
                }
                else
                {
                    return View();
                }
            }
            XElement users = doc.Element("users");
            foreach(XElement user in users.Elements())
            {
                String name = (String)user.Element("name");
                String _name = (String)Session["name"];
                String password = (String)user.Element("password");
                String _password = (String)Session["password"];
                if (name.Equals(_name) && password.Equals(_password))
                {
                    user.Element("international").SetValue(boolInter);
                    user.Element("amount").SetValue(intAmount);
                    user.Element("volume").SetValue(intVolume);
                    user.Element("provider").SetValue(provider);
                    doc.Save(model.getPath());
                    return RedirectToAction("/Tarif");
                }
            }
            //XElement user = new XElement("user");
            //user.Add(new XAttribute("id", GenerateNextId()));
            //user.Add(new XElement("name", name), new XElement("password", password), new XElement("international", boolInter), new XElement("amount", intAmount), new XElement("volume", intVolume), new XElement("provider", provider));
            //users.Add(user);
            /*school.Add(new XElement("Student",
                       new XElement("FirstName", "David"),
                       new XElement("LastName", "Smith")));*/
            
            return RedirectToAction("/Tarif");
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
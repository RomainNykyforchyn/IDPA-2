using idpa.Content;
using idpa.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace idpa.Controllers
{
    public class HomeController : Controller
    {
        private HomeModel model;
        private bool isLoggedIn;
        private User loggedUser;
        public HomeController()
        {
            model = new HomeModel();
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String name, String password)
        {
            XDocument doc = model.UsersDoc;
            try
            {
                XElement user = null;
                XElement users = doc.Element("users");
                for (int i = 0; i < users.Elements().Count(); i++)
                {
                    XElement user_ = users.Elements().ElementAt(i);
                    if(user_.Element("name").Value.Equals(name) && user_.Element("password").Value.Equals(password))
                    {
                        user = user_;
                    }
                }
                if (user == null)
                {
                    return RedirectToAction("/Index");
                }
                //IEnumerable<XElement> user_ = from el in doc.Elements("user") where (string)el.Element("name").Value == name && (string)el.Element("password").Value == password select el;
                //XElement user = user_.First();
                //foreach (XElement el in user_)
                //    Console.WriteLine(el.Name);
                
                int userId = Int32.Parse(user.Attribute("id").Value);
                String userName = user.Element("name").Value;
                String userPassword = user.Element("password").Value;
                bool userInternational = Boolean.Parse(user.Element("international").Value);
                int userAmount = Int32.Parse(user.Element("amount").Value);
                int userVolume = Int32.Parse(user.Element("volume").Value);
                String userProvider = user.Element("provider").Value;
                int userAdmin = Int32.Parse(user.Element("admin").Value);
                loggedUser = new User(userId, userAmount,userAdmin,userVolume, userName, userPassword,userProvider, userInternational );
                Session["loggedUser"] = loggedUser;
                Session["isLoggedIn"] = true;
                if (loggedUser.IsAdmin())
                {
                    return RedirectToAction("/Admin");
                }



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
            XDocument doc = model.UsersDoc;
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
            model.saveUsersDoc(doc);
            
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
            XDocument doc = model.UsersDoc;
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
                User loggedUser = (User)Session["loggedUser"];
                String name = (String)user.Element("name");
                String _name = loggedUser.Name;
                String password = (String)user.Element("password");
                String _password = loggedUser.Password;
                if (name.Equals(_name) && password.Equals(_password))
                {
                    user.Element("international").SetValue(boolInter);
                    user.Element("amount").SetValue(intAmount);
                    user.Element("volume").SetValue(intVolume);
                    user.Element("provider").SetValue(provider);
                    model.saveUsersDoc(doc);
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
        public ActionResult Admin()
        {
            object value = Session["isLoggedIn"];
            User user = (User)Session["loggedUser"];
            if (value != null)
            {
                isLoggedIn = (bool)value;
            }
            else
            {
                isLoggedIn = false;
            }

            if (isLoggedIn && user.IsAdmin())
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
            XDocument doc = model.UsersDoc;
            return doc.Descendants("user")
                       .OrderByDescending(x => Convert.ToInt32(x.Attribute("id").Value))
                       .Select(x => Convert.ToInt32(x.Attribute("id").Value))
                       .FirstOrDefault() + 1;
        }
    }
}
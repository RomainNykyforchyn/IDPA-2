﻿using idpa.Content;
using idpa.Models;
using System;
using System.Collections;
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
                foreach(XElement user_ in users.Elements())
                {
                    if(user_.Element("name").Value.Equals(name) && user_.Element("password").Value.Equals(password))
                    {
                        user = user_;
                    }
                }
                //for (int i = 0; i < users.Elements().Count(); i++)
                //{
                //    XElement user_ = users.Elements().ElementAt(i);
                    
                //}
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
            user.Add(new XAttribute("id", GenerateNextId(model.UsersDoc,"user")));
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
                    return RedirectToAction("/Result");
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
            if (!checkAdmin())
            {
                return RedirectToAction("/Index");
            }
            return View();
        }
        public ActionResult AddOffer()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("/Index");
            }
            return View();
        }
        public ActionResult AddOffer(String provider, String name, String location, String internet, float speed, String telephone, float price, float price2, int minimumTime, String addition)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("/Index");
            }
            XDocument doc = model.OffersDoc;
            Offer offer = new Offer(GenerateNextId(doc, "offer"), minimumTime, speed, price, price2, provider, name, location, internet, telephone, addition);
            
            XElement newOffer = new XElement("offer", new XElement("provider", provider), new XElement("name", name), new XElement("location", location), new XElement("internet", internet), new XElement("speed", speed), new XElement("telephone", telephone), new XElement("price", price), new XElement("price2", price2), new XElement("minimumTime", minimumTime), new XElement("addition", addition));
            doc.Root.Add(newOffer);
            model.saveOffersDoc(doc);
            return View();
        }
        public ActionResult DeleteOffer()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("/Index");
            }
            ArrayList offers = new ArrayList();
            XDocument doc = model.OffersDoc;
            XElement root = doc.Root;
            foreach(XElement offer in root.Elements())
            {
                offers.Add(new Offer(Int32.Parse(offer.Attribute("id").Value),Int32.Parse(offer.Element("minimumTime").Value), float.Parse(offer.Element("speed").Value), float.Parse(offer.Element("price").Value), float.Parse(offer.Element("price2").Value), offer.Element("provider").Value, offer.Element("name").Value, offer.Element("location").Value, offer.Element("internet").Value, offer.Element("telephone").Value, offer.Element("addition").Value));
            }
            String[] offerNames = new String[offers.Count] ;
            for(int i=0;i< offers.Count; i++)
            {
                Offer offer = (Offer)offers[i];
                offerNames[i] = offer.Name;
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult Result()
        {
            User loggedUser = (User)Session["loggedUser"];
            ArrayList offers = new ArrayList();
            XDocument doc = model.OffersDoc;
            XElement root = doc.Root;
            foreach (XElement offer in root.Elements())
            {
                offers.Add(new Offer(Int32.Parse(offer.Attribute("id").Value), Int32.Parse(offer.Element("minimumTime").Value), float.Parse(offer.Element("speed").Value), float.Parse(offer.Element("price").Value), float.Parse(offer.Element("price2").Value), offer.Element("provider").Value, offer.Element("name").Value, offer.Element("location").Value, offer.Element("internet").Value, offer.Element("telephone").Value, offer.Element("addition").Value));
            }

            ArrayList temp = new ArrayList();
            Boolean unlimt = false;
            String NullOffer = null;
            foreach (Offer of in offers)
            {
                // IDs.Add(of.Id);
                try
                {



                    if (loggedUser.Provider != of.Provider)
                    {
                        temp = offers;
                        offers.Remove(of);
                        if (loggedUser.International == of.isInternational())
                        {
                            temp = offers;
                            offers.Remove(of);
                            if (loggedUser.Amount >= 30)
                            {
                                temp = offers;
                                if (!of.Telephone.Contains("Unlimitiert") || !of.Telephone.Contains("unlmitiert"))
                                {
                                    offers.Remove(of);
                                    int volume_ = -1;
                                    Int32.TryParse(of.Internet, out volume_);
                                    if (volume_ != -1)
                                    {
                                        if (loggedUser.Volume > volume_)
                                        {
                                            temp = offers;
                                            offers.Remove(of);
                                        }
                                    }


                                }

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // 
                    throw;
                }



            }
            if (offers.Count == 0)
            {
                NullOffer = "Kein passendes Angebot gefunden!";
                //giz ned koleg
            }
            else
            { 
                Offer offer1 = (Offer)offers[0];
                Offer offer2 = (Offer)offers[0];
                Offer offer3 = (Offer)offers[0];
                ArrayList cheapOffers = new ArrayList();
                cheapOffers.Add(offer1);
                cheapOffers.Add(offer2);
                cheapOffers.Add(offer3);
                Offer nodeOffer = (Offer)offers[0];
                for(int i = 0; i < 3; i++)
                {
                    foreach(Offer of in offers)
                {
                    if (of.Price < nodeOffer.Price)
                    {
                        nodeOffer = of;
                    }
                }
                    cheapOffers[i] = nodeOffer;
                    offers.Remove(nodeOffer);
                }
                offer1 = (Offer)cheapOffers[0];
                offer2 = (Offer)cheapOffers[1];
                offer3 = (Offer)cheapOffers[2];
                ViewBag.provider = offer1.Provider;
                ViewBag.name = offer1.Name;
                ViewBag.location = offer1.Location;
                ViewBag.internet = offer1.Internet;
                ViewBag.speed = offer1.Speed;
                ViewBag.telephone = offer1.Telephone;
                ViewBag.price = offer1.Price;
                ViewBag.price2 = offer1.Price2;
                ViewBag.minimumTime = offer1.MinimumTime;
                ViewBag.addition = offer1.Addition;

                ViewBag.provider1 = offer2.Provider;
                ViewBag.name1 = offer2.Name;
                ViewBag.location1 = offer2.Location;
                ViewBag.internet1 = offer2.Internet;
                ViewBag.speed1 = offer2.Speed;
                ViewBag.telephone1 = offer2.Telephone;
                ViewBag.price1 = offer2.Price;
                ViewBag.price21 = offer2.Price2;
                ViewBag.minimumTime1 = offer2.MinimumTime;
                ViewBag.addition1 = offer2.Addition;

                ViewBag.provider2 = offer3.Provider;
                ViewBag.name2 = offer3.Name;
                ViewBag.location2 = offer3.Location;
                ViewBag.internet2 = offer3.Internet;
                ViewBag.speed2 = offer3.Speed;
                ViewBag.telephone2 = offer3.Telephone;
                ViewBag.price2 = offer3.Price;
                ViewBag.price22 = offer3.Price2;
                ViewBag.minimumTime2 = offer3.MinimumTime;
                ViewBag.addition2 = offer3.Addition;
            }


            return View();
        }
        public void btnAddOffer()
        {
            Redirect("~/Views/Home/AddOffer.cshtml");
        }
        private bool checkAdmin()
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
                return true;
            }
            else
            {
                return false;
            }
        }
        private int GenerateNextId(XDocument doc, String element)
        {
            return doc.Descendants(element)
                       .OrderByDescending(x => Convert.ToInt32(x.Attribute("id").Value))
                       .Select(x => Convert.ToInt32(x.Attribute("id").Value))
                       .FirstOrDefault() + 1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace idpa.Models
{
    public class HomeModel
    {
        private String xmlUsersPath, xmlOffersPath;
        private XDocument usersDoc, offersDoc;
        public HomeModel()
        {
            xmlUsersPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/users.xml");
            usersDoc = XDocument.Load(xmlUsersPath);
            xmlOffersPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/offers.xml");
            offersDoc = XDocument.Load(xmlOffersPath);
        }
        public void saveUsersDoc(XDocument doc)
        {
            doc.Save(xmlUsersPath);
        }
        public void saveOffersDoc(XDocument doc)
        {
            doc.Save(xmlOffersPath);
        }

        public string XmlUsersPath { get => xmlUsersPath; set => xmlUsersPath = value; }
        public string XmlOffersPath { get => xmlOffersPath; set => xmlOffersPath = value; }
        public XDocument UsersDoc { get => usersDoc; set => usersDoc = value; }
        public XDocument OffersDoc { get => offersDoc; set => offersDoc = value; }

    }
}
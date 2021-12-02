using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace idpa.Models
{
    public class HomeModel
    {
        private String xmlPath;
        private XDocument doc;
        public HomeModel()
        {
            xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/users.xml");
            doc = XDocument.Load(xmlPath);
        }
        public XDocument getDoc()
        {
            return this.doc;
        }
        public String getPath()
        {
            return xmlPath;
        }
    }
}
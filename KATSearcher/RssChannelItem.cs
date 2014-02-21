using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KATSearcher
{
    [XmlTypeAttribute("rssChannelItem", AnonymousType = true)]
    public class RssChannelItem
    {
        [XmlElementAttribute("title", Form = XmlSchemaForm.Unqualified)]
        public string Title { get; set; }

        [XmlElementAttribute("description", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        [XmlElementAttribute("category", Form = XmlSchemaForm.Unqualified)]
        public string Category { get; set; }

        [XmlElementAttribute("author", Form = XmlSchemaForm.Unqualified)]
        public string Author { get; set; }

        [XmlElementAttribute("link", Form = XmlSchemaForm.Unqualified)]
        public string Link { get; set; }

        [XmlElementAttribute("guid", Form = XmlSchemaForm.Unqualified)]
        public string Guid { get; set; }

        [XmlElementAttribute("pubDate", Form = XmlSchemaForm.Unqualified)]
        public string PubDate { get; set; }

        [XmlElementAttribute("contentLength", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string ContentLength { get; set; }

        [XmlElementAttribute("infoHash", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string InfoHash { get; set; }

        [XmlElementAttribute("magnetURI", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string MagnetURI { get; set; }

        [XmlElementAttribute("seeds", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string Seeds { get; set; }

        [XmlElementAttribute("peers", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string Peers { get; set; }

        [XmlElementAttribute("verified", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string Verified { get; set; }

        [XmlElementAttribute("fileName", Namespace = "http://xmlns.ezrss.it/0.1/")]
        public string FileName { get; set; }

        [XmlElementAttribute("enclosure", Form = XmlSchemaForm.Unqualified)]
        public RssChannelItemEnclosure[] Enclosure { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
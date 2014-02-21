using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KATSearcher
{
    [XmlTypeAttribute("rssChannel", AnonymousType = true)]
    public class RssChannel
    {
        [XmlElementAttribute("title", Form = XmlSchemaForm.Unqualified)]
        public string Title { get; set; }

        [XmlElementAttribute("link", Form = XmlSchemaForm.Unqualified)]
        public string Link { get; set; }

        [XmlElementAttribute("description", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        [XmlElementAttribute("item", Form = XmlSchemaForm.Unqualified)]
        public RssChannelItem[] Items { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
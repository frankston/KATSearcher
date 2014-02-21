using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KATSearcher
{
    [XmlTypeAttribute("rssChannelItemEnclosure", AnonymousType = true)]
    public class RssChannelItemEnclosure
    {
        [XmlAttributeAttribute("url")]
        public string Url { get; set; }

        [XmlAttributeAttribute("length")]
        public string Length { get; set; }

        [XmlAttributeAttribute("type")]
        public string Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
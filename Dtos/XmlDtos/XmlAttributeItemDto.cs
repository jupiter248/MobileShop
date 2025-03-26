using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainApi.Dtos.XmlDtos
{
    public class XmlAttributeItemDto
    {
        [XmlElement("Name")]
        public required string Name { get; set; }

        [XmlElement("Value")]
        public required string Value { get; set; }
    }
}
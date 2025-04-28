using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainApi.Application.Dtos.XmlDtos
{
    public class XmlAttributeDto
    {
        [XmlElement("Attribute")]
        public List<XmlAttributeItemDto> Items { get; set; } = new();

        public XmlAttributeDto() { }

        public XmlAttributeDto(Dictionary<string, string> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Items.Add(new XmlAttributeItemDto { Name = kvp.Key, Value = kvp.Value });
            }
        }
    }
}
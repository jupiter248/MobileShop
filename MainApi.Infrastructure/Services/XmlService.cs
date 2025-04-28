using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MainApi.Application.Dtos.XmlDtos;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Products.ProductAttributes;

namespace MainApi.Infrastructure.Services
{
    public class XmlService : IXmlService
    {

        public string GenerateAttributeXml(List<PredefinedProductAttributeValue> values)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            foreach (var item in values)
            {
                attributes.Add(item.ProductAttribute.Name, item.Name);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(XmlAttributeDto));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, new XmlAttributeDto(attributes));
                return writer.ToString();
            }
        }
        public List<PredefinedProductAttributeValue> ConvertToAttributeValueFromXml(string attributesXml)
        {
            List<PredefinedProductAttributeValue> predefinedProductAttributeValues = new List<PredefinedProductAttributeValue>();
            return predefinedProductAttributeValues;
        }
    }
}
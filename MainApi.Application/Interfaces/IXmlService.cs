using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.ProductAttributes;
using MainApi.Domain.Models.Products.ProductAttributes;

namespace MainApi.Application.Interfaces
{
    public interface IXmlService
    {
        string GenerateAttributeXml(List<PredefinedProductAttributeValue> values);
        List<PredefinedProductAttributeValue> ConvertToAttributeValueFromXml(string attributesXml);
    }
}
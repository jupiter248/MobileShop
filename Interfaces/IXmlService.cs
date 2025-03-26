using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.ProductAttributes;
using MainApi.Models.Products.ProductAttributes;

namespace MainApi.Interfaces
{
    public interface IXmlService
    {
        string GenerateAttributeXml(List<PredefinedProductAttributeValue> values);
    }
}
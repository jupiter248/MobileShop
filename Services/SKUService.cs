using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Interfaces;

namespace MainApi.Services
{
    public class SKUService : ISKUService
    {
        public string GenerateSKU(string productName, List<string> attributes)
        {
            string shortName = string.Join("", productName.Split(' ').Select(w => w[0])); // "Galaxy S24" → "GS"
            string attrCode = string.Join("", attributes.Select(a => a.Substring(0, 3).ToUpper())); // "128GB - Black" → "128B"

            return $"{shortName}-{attrCode}";
        }
    }
}
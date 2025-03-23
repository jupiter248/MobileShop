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
            if (attributes == null || attributes.Count < 3)
                throw new ArgumentException("Attributes list must contain at least 3 values (RAM, Storage, Color)");

            // Shorten product name to first 6 characters (customizable)
            string productCode = productName.Length > 6 ? productName.Substring(0, 6).ToUpper() : productName.ToUpper();

            string ramCode = "", storageCode = "", colorCode = "";

            // Detect and assign attributes dynamically
            foreach (var attr in attributes)
            {
                string attrTrimmed = attr.Trim().ToUpper();

                if (attrTrimmed.Contains("GB"))
                {
                    // If RAM is not set, assume this is RAM; otherwise, it's Storage
                    if (string.IsNullOrEmpty(ramCode))
                        ramCode = attrTrimmed.Replace("GB", "G"); // "16 GB" → "16G"
                    else
                        storageCode = attrTrimmed.Replace("GB", "G"); // "128 GB" → "128G"
                }
                else
                {
                    // Assume anything else is a color
                    colorCode = attrTrimmed.Substring(0, 3); // "Blue" → "BLU"
                }
            }

            // Validate that all components exist
            if (string.IsNullOrEmpty(ramCode) || string.IsNullOrEmpty(storageCode) || string.IsNullOrEmpty(colorCode))
                throw new ArgumentException("Failed to detect RAM, Storage, or Color correctly.");

            return $"{productCode}-{ramCode}-{storageCode}-{colorCode}";
        }
    }
}
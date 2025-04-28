using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Interfaces
{
    public interface ISKUService
    {
        string GenerateSKU(string productName, List<string> attributes);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Payment
{
    public class ZarinpalResponseDto
    {
        public Data? data { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }

        public class Data
        {
            public int Code { get; set; }
            public string? Message { get; set; }
            public string? Authority { get; set; }
            public string? Transaction_id { get; set; }

        }
    }
}
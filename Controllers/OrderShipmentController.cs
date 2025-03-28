using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/shipment")]
    public class OrderShipmentController : ControllerBase
    {
        private readonly IOrderShipment _orderShipment;
        public OrderShipmentController(IOrderShipment orderShipment)
        {
            _orderShipment = orderShipment;
        }
    }
}
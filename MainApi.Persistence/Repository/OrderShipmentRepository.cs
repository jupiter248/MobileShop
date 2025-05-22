using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Persistence.Repository
{

    public class OrderShipmentRepository : IOrderShipmentRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderShipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //Shipment 

        public async Task<List<OrderShipment>> GetAllOrderShipmentAsync(int orderId)
        {
            return await _context.OrderShipments.Where(s => s.OrderId == orderId).Include(s => s.ShipmentItems).Include(s => s.ShippingStatus).ToListAsync();
        }
        public async Task<OrderShipment> AddOrderShipmentAsync(OrderShipment orderShipment)
        {
            await _context.AddAsync(orderShipment);
            await _context.SaveChangesAsync();
            return orderShipment;
        }

        public async Task<OrderShipment?> GetShipmentByIdAsync(int shipmentId)
        {
            OrderShipment? shipment = await _context.OrderShipments.Include(s => s.ShipmentItems).FirstOrDefaultAsync(s => s.Id == shipmentId);
            return shipment;
        }

        public async Task DeleteShipmentAsync(OrderShipment orderShipment)
        {
            orderShipment.ShipmentItems.Select(s => _context.Remove(s));
            _context.Remove(orderShipment);

            await _context.SaveChangesAsync();
        }


        //Shipment Item

        public async Task<List<ShipmentItem>> AddItemToShipmentAsync(List<ShipmentItem> shipmentItems)
        {
            foreach (ShipmentItem item in shipmentItems)
            {
                await _context.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return shipmentItems;
        }
        public async Task DeleteFromShipmentAsync(ShipmentItem shipmentItem)
        {

            _context.Remove(shipmentItem);
            await _context.SaveChangesAsync();
        }



        //Shipment Status

        public async Task<ShippingStatus> AddShippingStatusAsync(ShippingStatus shippingStatus)
        {
            await _context.AddAsync(shippingStatus);
            await _context.SaveChangesAsync();
            return shippingStatus;
        }
        public async Task<List<ShippingStatus>> GetAllShippingStatusAsync()
        {
            return await _context.ShippingStatuses.ToListAsync();
        }
        public async Task<ShippingStatus?> GetShippingStatusByIdAsync(int shippingStatusId)
        {
            ShippingStatus? shippingStatus = await _context.ShippingStatuses.FirstOrDefaultAsync(s => s.Id == shippingStatusId);
            if (shippingStatus == null)
            {
                return null;
            }
            return shippingStatus;
        }
        public async Task DeleteShippingStatusAsync(ShippingStatus shippingStatus)
        {
            _context.Remove(shippingStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<ShipmentItem?> GetShipmentItemByIdAsync(int shipmentItemId)
        {
            ShipmentItem? shipmentItem = await _context.ShipmentItems.FirstOrDefaultAsync(i => i.Id == shipmentItemId);
            if (shipmentItem == null)
            {
                return null;
            }
            return shipmentItem;
        }
    }
}
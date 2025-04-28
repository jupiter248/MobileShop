using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;

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
            OrderShipment? shipment = await _context.OrderShipments.FindAsync(shipmentId);
            return shipment;
        }

        public async Task<bool> DeleteShipmentAsync(int shipmentId)
        {
            OrderShipment? shipment = await _context.OrderShipments.Include(s => s.ShipmentItems).FirstOrDefaultAsync(s => s.Id == shipmentId);
            if (shipment == null)
            {
                return false;
            }
            shipment.ShipmentItems.Select(s => _context.Remove(s));
            _context.Remove(shipment);

            await _context.SaveChangesAsync();

            return true;
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
        public async Task<bool> RemoveFromShipmentAsync(int itemId)
        {
            ShipmentItem? shipmentItem = await _context.ShipmentItems.FindAsync(itemId);
            if (shipmentItem == null)
            {
                return false;
            }
            _context.Remove(shipmentItem);
            await _context.SaveChangesAsync();

            return true;
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
        public async Task<ShippingStatus?> GetShippingStatusByNameAsync(string name)
        {
            ShippingStatus? shippingStatus = await _context.ShippingStatuses.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
            if (shippingStatus == null)
            {
                return null;
            }
            return shippingStatus;
        }
        public async Task<bool> RemoveShippingStatusAsync(int id)
        {
            ShippingStatus? shippingStatus = await _context.ShippingStatuses.FindAsync(id);
            if (shippingStatus == null)
            {
                return false;
            }
            _context.Remove(shippingStatus);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;

namespace ModelHouse.ServiceManagement.Domain.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> ListAsync();
        Task<IEnumerable<Order>> ListByUserId(long id);
        Task<OrderResponse> CreateAsync(Order order);
        Task<OrderResponse> DeleteAsync(long id);
        Task<OrderResponse> UpdateAsync(long id, Order order);
        Task<OrderResponse> GetOrderById(long id);
    }
}
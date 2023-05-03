using ModelHouse.Security.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.ServiceManagement.Services
{
    public class OrderService : IOrderService
    {
        
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> CreateAsync(Order order)
        {
            var user = await _userRepository.GetUserById(order.UserId);
            if (user == null)
                return new OrderResponse("User is not exist");
            try
            {
                await _orderRepository.AddAsync(order);
                await _unitOfWork.CompleteAsync();
                return new OrderResponse(order);
            }
            catch (Exception e)
            {
                return new OrderResponse($"Failed to register a Order: {e.Message}");
            }
        }

        public async Task<OrderResponse> DeleteAsync(long id)
        {
            var order_exist = await _orderRepository.FindByIdAsync(id);
            if (order_exist == null)
                return new OrderResponse("the Order is not existing");
            try
            {
                _orderRepository.DeleteAsync(order_exist);
                await _unitOfWork.CompleteAsync();
                return new OrderResponse(order_exist);
            }
            catch (Exception e)
            {
                return new OrderResponse($"An error occurred while deleting the Order: {e.Message}");
            }
        }

        public async Task<OrderResponse> GetOrderById(long id)
        {
            try
            {
                var account = await _orderRepository.FindByIdAsync(id);
                return new OrderResponse(account);
            }
            catch (Exception e)
            {
                return new OrderResponse($"Failed to find a current user Order: {e.Message}");
            }
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _orderRepository.ListAsync();
        }

        public async Task<IEnumerable<Order>> ListByUserId(long id)
        {
            return await _orderRepository.ListByUserId(id);
        }

        public Task<OrderResponse> UpdateAsync(long id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
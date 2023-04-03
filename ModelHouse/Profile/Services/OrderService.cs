using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Profile.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUserRepository userRepository, IOrderRepository orderRepository, IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _orderRepository.ListAsync();
    }

    public async Task<IEnumerable<Order>> ListByUserId(long id)
    {
        return await _orderRepository.ListByUserId(id);
    }

    public async Task<OrderResponse> CreateAsync(Order order)
    {
        var user = await _userRepository.FindByIdAsync(order.UserId);
        if (user == null)
            return new OrderResponse("User is not exist");
        var post_exist = await _postRepository.FindByIdAsync(order.PostId);
        if (post_exist == null)
            return new OrderResponse("The post Id is not exist");
        var send_user_exist = await _userRepository.FindByIdAsync(order.SendUserId);
        if (send_user_exist == null)
            return new OrderResponse("The user send is not exist");
        if (order.UserId == order.SendUserId)
            return new OrderResponse("A user cannot make an order to his own post");
        try
        {
            await _orderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return new OrderResponse(order);
        }
        catch (Exception e)
        {
            return new OrderResponse($"Failed to register a Project: {e.Message}");
        }
    }

    public async Task<OrderResponse> DeleteAsync(long id)
    {
        var order_exist = await _orderRepository.FindByIdAsync(id);
        if (order_exist == null)
            return new OrderResponse("the Project is not existing");
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

    public Task<OrderResponse> UpdateAsync(long id, Order order)
    {
        throw new NotImplementedException();
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
}
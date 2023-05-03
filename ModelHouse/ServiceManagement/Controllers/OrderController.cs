using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Resource.OrderResource;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.ServiceManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<GetOrderResource>> GetAllAsync()
        {
            var orders = await _orderService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<GetOrderResource>>(orders);
            return resources;
        }
        [HttpGet("" +
                 "user/{id}")]
        public async Task<IEnumerable<GetOrderResource>> GetAllByUserId(long id)
        {
            var orders = await _orderService.ListByUserId(id);
            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<GetOrderResource>>(orders);
            return resources;
        }
        [HttpGet("{id}")]
        public async Task<GetOrderResource> GetAccountById(long id)
        {
            var orderId = await _orderService.GetOrderById(id);
            var resources = _mapper.Map<Order, GetOrderResource>(orderId.Resource);
            return resources;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _orderService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var orderResource = _mapper.Map<Order, GetOrderResource>(result.Resource);

            return Ok(orderResource);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var order = _mapper.Map<CreateOrderResource, Order>(resource);

            var result = await _orderService.CreateAsync(order);

            if (!result.Success)
                return BadRequest(result.Message);
            var orderResource = _mapper.Map<Order, GetOrderResource>(result.Resource);
            return Ok(orderResource);
        }
    }
}
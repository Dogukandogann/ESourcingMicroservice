using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class GetOrdersByUsernameHandler : IRequestHandler<GetOrdersBySellerUsernameQuery,IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrdersByUsernameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersBySellerUsernameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrderBySellerUserName(request.UserName);
            var response = _mapper.Map<IEnumerable<OrderResponse>>(orderList);
            return response;
        }
    }
}

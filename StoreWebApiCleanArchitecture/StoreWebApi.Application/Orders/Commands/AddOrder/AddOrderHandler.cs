using MediatR;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Orders.Commands.AddOrder;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Orders.Commands
{
    public class AddOrderHandler: IRequestHandler<AddOrderCommand, Unit>
    {

        private readonly IStoreRepository _storeRepository;

        public AddOrderHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetByIdSpecification<Order>(request.Order.CartId);
            var order = await _storeRepository.FirstOrDefault(spec);
            if (order != null)
            {
                throw new EntityAlreadyExistsException("Order already exists");
            }

            var newOrder = new Order(0, request.Order.CartId, new Cart(), request.Order.Name, request.Order.Adress, request.Order.Phone, request.Order.PaymentMethod);
            await _storeRepository.AddAsync(newOrder);

            return Unit.Value;
        }
    }
}

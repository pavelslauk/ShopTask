using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Core.Utils;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel.Repositories;


namespace ShopTask.DomainModel
{
    public class OrderDomainService : IOrderDomainService
    {

        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;
        private IRepository<Order> _ordersRepository;

        public OrderDomainService(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository,
            IRepository<Product> productsRepository, IRepository<Order> ordersRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
        }

        public async Task<bool> SubmitOrder(Order order)
        {
            var productIds = order.OrderItems.Select(i => i.ProductId);
            var products = await _productsRepository.FindAsync(where: product => productIds.Contains(product.Id));
            foreach (var item in order.OrderItems)
            {
                item.Price = products.Single(p => p.Id == item.ProductId).Price;
            }
            _ordersRepository.Add(order);
            await _unitOfWork.CommitAsync();
            Logger.Default.Debug(BuildOrderMessage(order, products));

            return true;
        }

        private string BuildOrderMessage(Order order, IEnumerable<Product> products)
        {
            var message = new StringBuilder(order.Name + " " + order.Surname + " заказал:");
            message.AppendLine();
            foreach(var item in order.OrderItems)
            {
                message.AppendLine(item.Count + " " + products.Single(p => p.Id == item.ProductId).Title);
            }

            return message.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ShopTask.Models;
using ShopTask.DataAccess.Entities;

namespace ShopTask.Utils
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Product, ProductModel>();
                config.CreateMap<ProductModel, Product>();
                config.CreateMap<Category, CategoryModel>();
                config.CreateMap<Product, ProductOrderModel>().ForMember("Category", opt => opt.MapFrom(p => p.Category.Name));
                config.CreateMap<OrderModel, Order>();
                config.CreateMap<OrderItemModel, OrderItem>();
            });
        }
    }
}
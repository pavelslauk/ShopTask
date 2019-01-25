using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ShopTask.Application.Models;
using ShopTask.DomainModel.Entities;

namespace ShopTask.Application.Utils
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
                config.CreateMap<OrderDetailsModel, Order>();
                config.CreateMap<CartItemModel, OrderItem>();
            });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebApi.Models
{
    public class AddOrderRequest
    {
        public int CustomerId { get; set; }
        public List<AddOrderProductDto> Products { get; set; }
    }
}

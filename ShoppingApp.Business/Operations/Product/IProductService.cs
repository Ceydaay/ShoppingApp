using ShoppingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Product.Dtos
{
    public interface IProductService
    {
        Task<ServiceMessage> AddProduct(AddProductDto product);
       
        Task<ProductDto> GetProduct(int id);

        Task<List<ProductDto>> GetProducts();

    }
}

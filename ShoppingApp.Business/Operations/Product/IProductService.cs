using ShoppingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Product.Dtos
{
    // Ürün işlemlerini yöneten servis arayüzü
    public interface IProductService
    {
        // Yeni ürün ekler ve sonuç mesajı döner
        Task<ServiceMessage> AddProduct(AddProductDto product);

        // Verilen ID'ye göre ürünü getirir

        Task<ProductDto> GetProducts(int id);


        // Tüm ürünleri getirir
        Task<List<ProductDto>> GetAllProducts();

        // Stok miktarını günceller
        void UpdateStock(int id, int quantity);

        // Ürün fiyatını değiştirir ve sonuç mesajı döner
        Task<ServiceMessage> AdjustProductPrice(int id, int changeTo);

        // Ürünü siler ve sonuç mesajı döner
        Task<ServiceMessage> DeleteProduct(int id);


        // Mevcut bir ürünü günceller ve sonuç mesajı döner
        Task<ServiceMessage> UpdateProduct(UpdateProductDto product);

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositroy.Interfacies;
namespace Talabat.APIS.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenaricInterface<Product> _productRepo;

        public ProductsController(IGenaricInterface<Product> ProductRepo )
        {
            _productRepo = ProductRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
         
            var Products= await _productRepo.GetAllAsync();
            return Ok(Products);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product= await _productRepo.GetAsync(id);
            if (product == null)
            {
                return NotFound(new {Messge="Not Found" ,StatusCode=404});
            }
            return Ok(product);
            
        }
    }
}

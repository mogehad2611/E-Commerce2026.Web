using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    // products controller need to use product service
    // but what if we need to use other services too?
    // instead of injecting each service separately 
    // we can inject a service manager that contains all services
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct(int id)
        {
            var Product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            var Types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);

        }

    }
}

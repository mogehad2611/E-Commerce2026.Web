using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDTO> GetProductByIdAsync(int Id);
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

    }
}

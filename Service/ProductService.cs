using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs.ProductModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            // first call the required repo
            var Repo = unitOfWork.GetRepository<ProductBrand, int>();

            // use the repo to get what you need
            var Brands = await Repo.GetAllAsync();

            // auto map to DTO
            // install automapper to services
            // inject object from automapper
            var BrandsDTO = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTO>>(Brands);

            // return
            return BrandsDTO;

            // need to create mapping profile
            // need to add services to DI container

        }

        
        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = unitOfWork.GetRepository<Product, int>();
            var spec = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(spec);
            var data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
            var ProdCount = Products.Count();
            var TotalCount = await Repo.CountAsync(new ProdCountSpec(queryParams));
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, ProdCount,TotalCount, data);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int Id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(Id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            if(Product is null)
            {
                throw new ProductNotFoundException(Id);
            }
            return mapper.Map<Product, ProductDTO>(Product);
        }

    }
}

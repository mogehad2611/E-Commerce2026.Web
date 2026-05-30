using DomainLayer.Models.ProductModule;
using Service.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    class ProductWithBrandAndTypeSpecifications
        : BaseSpecifications<Product, int>
    {
        // Get All Products With Types And Brands
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.SortName:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.SortPrice:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.SortNameDesc:
                    AddOrderByDesc(P => P.Name);
                    break;
                case ProductSortingOptions.SortPriceDesc:
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        // Get Product By Id
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
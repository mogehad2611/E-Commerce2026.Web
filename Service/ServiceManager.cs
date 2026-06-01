using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , IBasketRepository basketRepository , UserManager<AppUser> userManager) : IServiceManager
    {
        // implement using lazy loading
        private readonly Lazy<IProductService> _productService
            = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _basketService
            = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _authService
            = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager));

        public IProductService ProductService => _productService.Value;
        public IAuthenticationService AuthenticationService => _authService.Value;
        public IBasketService BasketService => _basketService.Value;
    }
}

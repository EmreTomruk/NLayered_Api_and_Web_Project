using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        //private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork,
            IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
            //_mapper = mapper;
        }

        public async Task<List<ProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();
            var productsDto = _mapper.Map<List<ProductsWithCategoryDto>>(products);

            return productsDto;
        }
    }
}

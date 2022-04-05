using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    //[Authorize]
    //[ValidateFilterAttribute]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }

        [HttpGet("[action]")]//www.mysite.com/api/products/GetProductsWithCategory
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategoryAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var products = await _service.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]//www.mysite.com/api/products/5 Bu attribute yazilmazsa id bilgisini query string'e yazmamiz gerekecekti(/api/products?id=5)...
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost] 
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productDtoNew = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDtoNew));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Bu id'ye sahip ürün bulunamadı..."));

            await _service.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    //[Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        //private readonly IProductService _productService;
        //private readonly ICategoryService _categoryService;
        //private readonly IMapper _mapper;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService /*IProductService productService, ICategoryService categoryService, IMapper mapper*/)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        //    _productService = productService;
        //    _categoryService = categoryService;
        //    _mapper = mapper;
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            return View((await _productApiService.GetProductsWithCategoryAsync()));
        }

        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name"); 

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);
                //await _productService.AddAsync(_mapper.Map<Product>(productDto));

                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");

            return View(); //Burada ModelState gecerli degilse, ilgili sayfa tekrar yuklensin ve category bilgileri bir daha gelsin...
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                //await _productApiService.UpdateAsync(_mapper.Map<Product>(productDto));
                await _productApiService.UpdateAsync(productDto);

                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            //var product = await _productApiService.GetByIdAsync(id);
            await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

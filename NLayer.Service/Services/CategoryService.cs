using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        //private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork,
           ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
            //_mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var categoryWithProducts = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
            var categoryCategoryWithProductsDto = _mapper.Map<CategoryWithProductsDto>(categoryWithProducts);

            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryCategoryWithProductsDto); 
        }
    }
}

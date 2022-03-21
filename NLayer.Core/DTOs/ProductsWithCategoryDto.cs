using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductsWithCategoryDto : ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}

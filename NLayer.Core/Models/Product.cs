using NLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Product : BaseEntity
    {
        //public Product(string name, ProductFeature productFeature)
        //{
        //    Name = name ?? throw new ArgumentNullException(nameof(Name)); //Product Instance alinirken name=null olursa ArgumentNullException hatasi firlat...
        //    ProductFeature = productFeature ?? throw new ArgumentNullException(nameof(ProductFeature));
        //}
        //note: ?? operatoru bir değişkenin değerinin null olduğu durumda alternatif değer döndürebilmek icin kullanilir...

        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; } //Foreign Key

        public Category Category { get; set; } //Nav. Prop.
        public ProductFeature ProductFeature { get; set; }
    }
}

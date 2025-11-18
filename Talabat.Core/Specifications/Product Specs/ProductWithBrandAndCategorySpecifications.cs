using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        //This Constructor will be used to create object that will be used to get All Products
        public ProductWithBrandAndCategorySpecifications()
            : base()
        {
            AddIncludes();
        }

        //This Constructor will be used to create object that will be used to get a specific Product With Id
        public ProductWithBrandAndCategorySpecifications(int id)
            : base(P => P.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}

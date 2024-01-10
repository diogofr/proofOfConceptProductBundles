using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class ProductAssociation
    {
        public int ParentProductId { get; set; }
        public Product ParentProduct { get; set; }
        public int ChildProductId { get; set; }
        public Product ChildProduct { get; set; }

        public int RequiredQuantity { get; set; }
    }
}

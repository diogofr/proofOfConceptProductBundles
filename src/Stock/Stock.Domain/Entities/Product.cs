using Stock.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public ICollection<ProductAssociation> ChildrenProducts { get; set; }
    }
}

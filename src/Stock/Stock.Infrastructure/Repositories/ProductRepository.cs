using Microsoft.EntityFrameworkCore;
using Stock.Domain.Common;
using Stock.Domain.Entities;
using Stock.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
               .Include(p => p.ChildrenProducts)
               .ThenInclude(pa => pa.ChildProduct)
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
               .Include(p => p.ChildrenProducts)
               .ThenInclude(pa => pa.ChildProduct)
               .ToListAsync();
        }

        public async Task AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CalculateBundlesFromStock(int productId)
        {
            var product = await GetByIdAsync(productId);
            if (product == null)
                return 0;

            if (product.ChildrenProducts.Count > 0)
            {
                int minBundles = int.MaxValue;
                foreach (var association in product.ChildrenProducts)
                {
                    var childBundles = await CalculateBundlesFromStock(association.ChildProductId);
                    int bundlesFromChildProduct = childBundles / association.RequiredQuantity;
                    if (bundlesFromChildProduct < minBundles)
                        minBundles = bundlesFromChildProduct;
                }
                
                if (minBundles == int.MaxValue)
                {
                    minBundles = 0;
                }

                return minBundles;
            }

            return product.StockQuantity;
        }

        public async Task<int> CalculateBundlesFromStock1(int productId)
        {
            var product = await GetByIdAsync(productId);
            int minBundles = int.MaxValue;

            foreach (var association in product.ChildrenProducts)
            {
                var childProduct = await GetByIdAsync(association.ChildProductId);
                if (childProduct != null)
                {
                    int bundlesFromChildProduct = childProduct.StockQuantity / association.RequiredQuantity;

                    if (bundlesFromChildProduct < minBundles)
                    {
                        minBundles = bundlesFromChildProduct;
                    }
                }
            }

            if (minBundles == int.MaxValue)
            {
                minBundles = 0;
            }

            return minBundles;
        }
    }
}

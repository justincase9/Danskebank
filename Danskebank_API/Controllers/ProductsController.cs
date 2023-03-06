using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danskebank_API.Data;
using Danskebank_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Danskebank_API.Identity;
using Danskebank_API.Entities.Dto;
using System.Xml.Linq;

namespace Danskebank_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProductsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }


            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productInfo)
        {
            Product product = new Product();
            product.Name = productInfo.Name;
            product.Description = productInfo.Description;
            product.Price = productInfo.Price;
            var type = _context.ProductTypes.Where(x => x.Name == productInfo.Type).FirstOrDefault();
            if (type == null)
                product.TypeID = AddProductType(productInfo.Type);  
            var subtype = _context.ProductSubtypes.Where(x => x.Name == productInfo.Subtype).FirstOrDefault();
            if (type == null)
                product.SubtypeID = AddProductSubtype(productInfo.Subtype, product.TypeID);  

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        private int AddProductType(string productTypeName)
        {
            var productType = new ProductType();
            productType.Name = productTypeName;
            _context.ProductTypes.Add(productType);
            _context.SaveChanges();
            return productType.ProductTypeID;
        } 
        private int AddProductSubtype(string productSubtypeName,int productTypeId)
        {
            var productSubtype = new ProductSubtype();
            productSubtype.Name = productSubtypeName;
            productSubtype.TypeID = productTypeId;
            _context.ProductSubtypes.Add(productSubtype);
            _context.SaveChanges();
            return productSubtype.SubtypeID;
        }
    }
}

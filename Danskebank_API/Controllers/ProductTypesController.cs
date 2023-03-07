using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danskebank_API.Data;
using Danskebank_API.Entities;
using Danskebank_API.Identity;
using Microsoft.AspNetCore.Authorization;
using Danskebank_API.Entities.Dto;

namespace Danskebank_API.Controllers
{
    [Route("api/Types")]
    [ApiController]
    [Authorize]
    public class ProductTypesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProductTypesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);

            if (productType == null)
                return NotFound();

            return productType;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> PutProductType(int id, ProductType productType)
        {
            if (id != productType.ProductTypeID)
                return BadRequest();

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))               
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

       
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductType>> PostProductType(ProductTypeDto productType)
        {
            ProductType type = new ProductType();
            type.Name = productType.Name;

            _context.ProductTypes.Add(type);
            await _context.SaveChangesAsync();

            return type;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
                return NotFound();

            _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.ProductTypeID == id);
        }
    }
}

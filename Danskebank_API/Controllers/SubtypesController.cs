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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductSubtypesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProductSubtypesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductSubtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSubtype>>> GetProductSubtypes()
        {
            return await _context.ProductSubtypes.ToListAsync();
        }

        // GET: api/ProductSubtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubtype>> GetProductSubtype(int id)
        {
            var ProductSubtype = await _context.ProductSubtypes.FindAsync(id);

            if (ProductSubtype == null)
            {
                return NotFound();
            }

            return ProductSubtype;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> PutProductSubtype(int id, ProductSubtype ProductSubtype)
        {
            if (id != ProductSubtype.SubtypeID)
            {
                return BadRequest();
            }

            _context.Entry(ProductSubtype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSubtypeExists(id))
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

        // POST: api/ProductSubtypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductSubtype>> PostProductSubtype(ProductSubtypeDto ProductSubtype)
        {
            ProductSubtype subtype = new ProductSubtype();
            subtype.Name = ProductSubtype.Name;
            subtype.TypeID = ProductSubtype.TypeID;

            _context.ProductSubtypes.Add(subtype);
            await _context.SaveChangesAsync();

            return subtype;
        }

        // DELETE: api/ProductSubtypes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteProductSubtype(int id)
        {
            var ProductSubtype = await _context.ProductSubtypes.FindAsync(id);
            if (ProductSubtype == null)
            {
                return NotFound();
            }

            _context.ProductSubtypes.Remove(ProductSubtype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductSubtypeExists(int id)
        {
            return _context.ProductSubtypes.Any(e => e.SubtypeID == id);
        }
    }
}

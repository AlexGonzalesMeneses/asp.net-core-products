using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIAPROCO_WEB_API.Context;
using SIAPROCO_WEB_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIAPROCO_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly AppDbContext context;

        public ProductController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return context.Product.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = context.Product.FirstOrDefault(p => p.IdProduct == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            try
            {
                context.Product.Add(product);
                context.SaveChanges();
                return Created("the product is created", product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (product.IdProduct == id)
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(product);
            }
            else
            {
                return BadRequest("Id not compatible");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = context.Product.FirstOrDefault(p => p.IdProduct == id);
            if (product != null)
            {
                context.Product.Remove(product);
                context.SaveChanges();
                return Ok("The product was delete succesfully");
            }
            else
            {
                return BadRequest("Error");
            }
        }
    }
}

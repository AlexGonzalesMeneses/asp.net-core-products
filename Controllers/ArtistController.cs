using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Store_API.Entities;
using Music_Store_API.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        public readonly AppDbContext context;
        public ArtistController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Artist> Get()
        {
            return context.Artist.ToList();
        }

        [HttpGet("{id}")]
        public Artist Get(int id)
        {
            var product = context.Artist.FirstOrDefault(a => a.ArtistId == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Artist artist)
        {
            try
            {
                context.Artist.Add(artist);
                context.SaveChanges();
                return Created("the artist is created", artist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Artist artist)
        {
            if (artist.ArtistId == id)
            {
                context.Entry(artist).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(artist);
            }
            else
            {
                return BadRequest("Id not compatible");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var artist = context.Artist.FirstOrDefault(a => a.ArtistId == id);
            if (artist != null)
            {
                context.Artist.Remove(artist);
                context.SaveChanges();
                return Ok("The artist was delete succesfully");
            }
            else
            {
                return BadRequest("Error");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class SpeciesController : ApiController
    {
        private ZooAppContext db = new ZooAppContext();

        // GET: api/Species
        public IQueryable<Species> GetSpecies()
        {
            return db.Species;
        }

        // GET: api/Species/5
        [ResponseType(typeof(Species))]
        public async Task<IHttpActionResult> GetSpecies(int id)
        {
            Species species = await db.Species.FindAsync(id);
            if (species == null)
            {
                return NotFound();
            }

            return Ok(species);
        }

        // PUT: api/Species/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSpecies(int id, Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != species.Id)
            {
                return BadRequest();
            }

            db.Entry(species).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Species
        [ResponseType(typeof(Species))]
        public async Task<IHttpActionResult> PostSpecies(Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Species.Add(species);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = species.Id }, species);
        }

        // DELETE: api/Species/5
        [ResponseType(typeof(Species))]
        public async Task<IHttpActionResult> DeleteSpecies(int id)
        {
            Species species = await db.Species.FindAsync(id);
            if (species == null)
            {
                return NotFound();
            }

            db.Species.Remove(species);
            await db.SaveChangesAsync();

            return Ok(species);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpeciesExists(int id)
        {
            return db.Species.Count(e => e.Id == id) > 0;
        }
    }
}
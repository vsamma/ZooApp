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
    public class AnimalsController : ApiController
    {
        private ZooAppContext db = new ZooAppContext();

        // GET: api/Animals
        public IQueryable<AnimalDTO> GetAnimals()
        {
            var animals = from b in db.Animals
                          select new AnimalDTO()
                          {
                              Id = b.Id,
                              Name = b.Name,
                              YearOfBirth = b.YearOfBirth,
                              Age = b.Age,
                              CreationDate = b.CreationDate,//.HasValue ? b.CreationDate.Value : ,
                              Species = b.Species
                          };
            //return db.Animals
            //// new code:
            //.Include(b => b.Species);
            return animals;
        }

        // GET: api/Animals/5
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> GetAnimal(int id)
        {
            //Animal animal = await db.Animals.FindAsync(id);
            var animal = await db.Animals.Include(b => b.Species).Select(b =>
            new AnimalDTO()
            {
                Id = b.Id,
                Name = b.Name,
                YearOfBirth = b.YearOfBirth,
                Age = b.Age,
                CreationDate = b.CreationDate,
                Species = b.Species
            }).SingleOrDefaultAsync(b => b.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // PUT: api/Animals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnimal(int id, Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.Id)
            {
                return BadRequest();
            }

            animal.Age = (DateTime.UtcNow.Year - animal.YearOfBirth);

            db.Entry(animal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> PostAnimal(Animal animal)
        {
            bool isAnimalDuplicate = db.Animals.Any(x => x.Name == animal.Name && x.SpeciesId == animal.SpeciesId);

            if (isAnimalDuplicate)
            {
                ModelState.AddModelError("Error", "Samast liigist samanimeline loom on juba lisatud! Proovi uuesti");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            animal.CreationDate = DateTime.UtcNow;
            animal.Age = (DateTime.UtcNow.Year - animal.YearOfBirth);

            db.Animals.Add(animal);
            await db.SaveChangesAsync();

            // New code:
            // Load species name
            db.Entry(animal).Reference(x => x.Species).Load();

            var dto = new AnimalDTO()
            {
                Id = animal.Id,
                Name = animal.Name,
                YearOfBirth = animal.YearOfBirth,
                Age = animal.Age,
                CreationDate = animal.CreationDate,
                Species = animal.Species
            };


            return CreatedAtRoute("DefaultApi", new { id = animal.Id }, dto);
        }

        // DELETE: api/Animals/5
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> DeleteAnimal(int id)
        {
            Animal animal = await db.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            db.Animals.Remove(animal);
            await db.SaveChangesAsync();

            return Ok(animal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalExists(int id)
        {
            return db.Animals.Count(e => e.Id == id) > 0;
        }
    }
}
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Trupanion.Pets.Data.Entities;
using Trupanion.Pets.Data.Model;

namespace Trupanion.Pets.Service.Controllers
{
    public class PetsController : ApiController
    {
        private TrupanionEntities db = new TrupanionEntities();

        [HttpGet]
        [Route("api/Pets/{id}")]
        [ResponseType(typeof(Pet))]
        public async Task<IHttpActionResult> GetPet(int id)
        {
            Pet pet = await db.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpGet]
        [Route("api/Pets/ByOwner/{id}")]
        [ResponseType(typeof(List<Pet>))]
        public async Task<IHttpActionResult> GetPetsByOwner(int id)
        {
            var pets = await db.Pets.Where(p => p.PetOwnerId == id).ToListAsync();  
            return Ok(pets);
        }

        [HttpPut]
        [Route("api/Pets/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdatePet(int id, Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pet.Id)
            {
                return BadRequest();
            }

            if (!PetExists(id))
            {
                return NotFound();
            }

            db.Entry(pet).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/Pets")]
        [ResponseType(typeof(Pet))]
        public async Task<IHttpActionResult> CreatePet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { controller = "Pets", id = pet.Id }, pet);
        }

        [HttpDelete]
        [Route("api/Pets/{id}")]
        [ResponseType(typeof(Pet))]
        public async Task<IHttpActionResult> DeletePet(int id)
        {
            Pet pet = await db.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            db.Pets.Remove(pet);
            await db.SaveChangesAsync();

            return Ok(pet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool PetExists(int id)
        {
            return db.Pets.Count(e => e.Id == id) > 0;
        }
    }
}
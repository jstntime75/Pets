using System;
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
    public class PetOwnersController : ApiController
    {
        private TrupanionEntities db = new TrupanionEntities();

        [HttpGet]
        [Route("api/PetOwners")]
        public IQueryable<PetOwner> GetPetOwners()
        {
            return db.PetOwners.Where(o => o.CancelDate == null || o.CancelDate > DbFunctions.TruncateTime(DateTime.Now));
        }

        [HttpGet]
        [Route("api/PetOwners/{id}")]
        [ResponseType(typeof(PetOwner))]
        public async Task<IHttpActionResult> GetPetOwner(int id)
        {
            PetOwner owner = await db.PetOwners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }

        [HttpPut]
        [Route("api/PetOwners/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdatePetOwner(int id, PetOwner petOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petOwner.Id)
            {
                return BadRequest();
            }

            if (!PetOwnerExists(id))
            {
                return NotFound();
            }

            db.Entry(petOwner).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/PetOwners")]
        [ResponseType(typeof(PetOwner))]
        public async Task<IHttpActionResult> CreatePetOwner(PetOwner petOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PetOwners.Add(petOwner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { controller = "PetOwners", id = petOwner.Id }, petOwner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool PetOwnerExists(int id)
        {
            return db.PetOwners.Count(e => e.Id == id) > 0;
        }
    }
}
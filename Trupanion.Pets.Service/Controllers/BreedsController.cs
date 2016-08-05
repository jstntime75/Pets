using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Trupanion.Pets.Data.Entities;
using Trupanion.Pets.Data.Model;

namespace Trupanion.Pets.Service.Controllers
{
    public class BreedsController : ApiController
    {
        private TrupanionEntities db = new TrupanionEntities();

        [HttpGet]
        [Route("api/Breeds")]
        public IQueryable<Breed> GetBreeds()
        {
            return db.Breeds;
        }

        [HttpGet]
        [Route("api/Breeds/{id}")]
        [ResponseType(typeof(Breed))]
        public async Task<IHttpActionResult> GetBreed(short id)
        {
            Breed breed = await db.Breeds.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }

            return Ok(breed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
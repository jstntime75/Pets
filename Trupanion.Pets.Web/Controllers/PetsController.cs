using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Trupanion.Pets.Data.Entities;
using Trupanion.Pets.Web.Models;

namespace Trupanion.Pets.Web.Controllers
{
    public class PetsController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Add(int id)
        {
            var ownerTask = PetServiceApi.GetAsync<PetOwner>(string.Format("api/PetOwners/{0}", id));
            var breedsTask = PetServiceApi.GetAsync<List<Breed>>("api/Breeds");

            var vm = new AddPetViewModel();
            vm.OwnerId = id;

            var owner = await ownerTask;
            vm.OwnerName = owner.Name;
            vm.PolicyNumber = owner.PolicyNumber;

            var breeds = await breedsTask;
            foreach (var breed in breeds.OrderBy(b => b.Name))
            {
                vm.Breeds.Add(new SelectListItem { Value = breed.Id.ToString(), Text = breed.Name });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddPetViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var pet = new Pet();
                pet.BreedId = vm.SelectedBreedId;
                pet.DateOfBirth = vm.DateOfBirth;
                pet.Name = vm.Name;
                pet.PetOwnerId = vm.OwnerId;

                var result = await PetServiceApi.CreateAsync<Pet>("api/Pets", pet);

                return RedirectToAction("View", "Policies", new { id = vm.OwnerId });
            }

            var breeds = await PetServiceApi.GetAsync<List<Breed>>("api/Breeds");
            foreach (var breed in breeds.OrderBy(b => b.Name))
            {
                vm.Breeds.Add(new SelectListItem { Value = breed.Id.ToString(), Text = breed.Name });
            }

            return View();
        }

        public async Task<ActionResult> View(int id)
        {
            var pet = await PetServiceApi.GetAsync<Pet>(string.Format("api/Pets/{0}", id));
            var breed = await PetServiceApi.GetAsync<Breed>(string.Format("api/Breeds/{0}", pet.BreedId));
            var owner = await PetServiceApi.GetAsync<PetOwner>(string.Format("api/PetOwners/{0}", pet.PetOwnerId));

            var vm = new PetViewModel
            {
                Breed = breed.Name,
                BreedId = pet.BreedId,
                DateOfBirth = pet.DateOfBirth,
                Id = pet.Id,
                Name = pet.Name,
                PetOwnerId = pet.PetOwnerId,
                Owner = owner.Name
            };

            return View(vm);
        }

        public async Task<ActionResult> Transfer(int id)
        {
            var ownersTask = PetServiceApi.GetAsync<List<PetOwner>>("api/PetOwners");
            var pet = await PetServiceApi.GetAsync<Pet>(string.Format("api/Pets/{0}", id));
            var breed = await PetServiceApi.GetAsync<PetOwner>(string.Format("api/Breeds/{0}", pet.BreedId));
            var owner = await PetServiceApi.GetAsync<PetOwner>(string.Format("api/PetOwners/{0}", pet.PetOwnerId));

            var vm = new TransferPetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                OwnerId = pet.PetOwnerId,
                OwnerName = owner.Name,
                BreedId = pet.BreedId,
                Breed = breed.Name,
                DateOfBirth = pet.DateOfBirth 
            };

            var allOwners = await ownersTask;
            foreach (var newOwner in allOwners.Where(o => o.Id != pet.PetOwnerId).OrderBy(o => o.Name))
            {
                vm.Owners.Add(new SelectListItem { Value = newOwner.Id.ToString(), Text = newOwner.Name });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Transfer(TransferPetViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var pet = new Pet
                {
                    BreedId = vm.BreedId,
                    DateOfBirth = vm.DateOfBirth,
                    Id = vm.Id,
                    Name = vm.Name,
                    PetOwnerId = vm.SelectedOwnerId
                };

                await PetServiceApi.UpdateAsync<Pet>(string.Format("api/Pets/{0}", vm.Id), pet);
                return RedirectToAction("View", "Policies", new { id = vm.OwnerId });
            }

            var allOwners = await PetServiceApi.GetAsync<List<PetOwner>>("api/PetOwners");
            foreach (var newOwner in allOwners.Where(o => o.Id != vm.OwnerId).OrderBy(o => o.Name))
            {
                vm.Owners.Add(new SelectListItem { Value = newOwner.Id.ToString(), Text = newOwner.Name });
            }

            return View(vm);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var pet = await PetServiceApi.GetAsync<Pet>(string.Format("api/Pets/{0}", id));
            var breed = await PetServiceApi.GetAsync<Breed>(string.Format("api/Breeds/{0}", pet.BreedId));
            var owner = await PetServiceApi.GetAsync<PetOwner>(string.Format("api/PetOwners/{0}", pet.PetOwnerId));

            var vm = new PetViewModel
            {
                Breed = breed.Name,
                BreedId = pet.BreedId,
                DateOfBirth = pet.DateOfBirth,
                Id = pet.Id,
                Name = pet.Name,
                PetOwnerId = pet.PetOwnerId,
                Owner = owner.Name
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PetViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var deleted = await PetServiceApi.DeleteAsync<Pet>(string.Format("api/Pets/{0}", vm.Id));
                return RedirectToAction("View", "Policies", new { id = deleted.PetOwnerId });
            }

            return View(vm);
        }
    }
}
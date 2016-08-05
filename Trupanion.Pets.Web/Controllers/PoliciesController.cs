using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Trupanion.Pets.Data.Entities;
using Trupanion.Pets.Web.Models;

namespace Trupanion.Pets.Web.Controllers
{
    public class PoliciesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var owners = await PetServiceApi.GetAsync<List<PetOwner>>("api/PetOwners");

            var vms = new List<PetOwnerViewModel>();
            foreach (PetOwner owner in owners.OrderBy(o => o.Name))
            {
                vms.Add(new PetOwnerViewModel
                {
                    Id = owner.Id,
                    Name = owner.Name,
                    CancelDate = owner.CancelDate,
                    PolicyDate = owner.PolicyDate,
                    PolicyNumber = owner.PolicyNumber
                });
            }

            return View(vms);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(PetOwnerViewModel vm)
        {
            // TODO: make country dynamic

            if (ModelState.IsValid)
            {
                var owner = new PetOwner
                {
                    CountryId = 1,
                    Name = vm.Name
                };

                var created = await PetServiceApi.CreateAsync<PetOwner>("api/PetOwners", owner);

                int idLength = created.Id.ToString().Length;
                const int policyLength = 13;

                var buffer = new StringBuilder(13);
                buffer.Append("USA");

                int zeros = policyLength - 3 - idLength;
                for (int i = 0; i < zeros; i++)
                {
                    buffer.Append("0");
                }

                buffer.Append(created.Id);

                created.PolicyNumber = buffer.ToString();
                created.PolicyDate = DateTime.Now;

                await PetServiceApi.UpdateAsync(string.Format("api/PetOwners/{0}", created.Id), created);

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public async Task<ActionResult> View(int id)
        {
            var ownerTask = PetServiceApi.GetAsync<PetOwner>(string.Format("api/PetOwners/{0}", id));
            var petsTask = PetServiceApi.GetAsync<List<Pet>>(string.Format("api/Pets/ByOwner/{0}", id));

            var owner = await ownerTask;
            var vm = new PetOwnerViewModel
            {
                Id = owner.Id,
                Name = owner.Name,
                CancelDate = owner.CancelDate,
                PolicyDate = owner.PolicyDate,
                PolicyNumber = owner.PolicyNumber
            };

            var pets = await petsTask;
            foreach (var pet in pets.OrderBy(p => p.Name))
            {
                var breed = await PetServiceApi.GetAsync<Breed>(string.Format("api/Breeds/{0}", pet.BreedId));
                vm.Pets.Add(new PetViewModel
                {
                    Id = pet.Id,
                    BreedId = pet.BreedId,
                    Breed = breed.Name,
                    DateOfBirth = pet.DateOfBirth,
                    Name = pet.Name,
                    PetOwnerId = owner.Id,
                    Owner = owner.Name
                });
            }

            return View(vm);
        }

        public async Task<ActionResult> Cancel(int id)
        {
            var owner = await PetServiceApi.GetAsync<PetOwner>(string.Format("api/PetOwners/{0}", id));

            var vm = new PetOwnerViewModel
            {
                Id = id,
                CancelDate = owner.CancelDate,
                Name = owner.Name,
                PolicyDate = owner.PolicyDate,
                PolicyNumber = owner.PolicyNumber
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cancel(PetOwnerViewModel vm)
        {
            var owner = new PetOwner();
            owner.Id = vm.Id;
            owner.CancelDate = DateTime.Now;
            owner.CountryId = 1;
            owner.Name = vm.Name;
            owner.PolicyDate = vm.PolicyDate;
            owner.PolicyNumber = vm.PolicyNumber;

            var updated = await PetServiceApi.UpdateAsync<PetOwner>(string.Format("api/PetOwners/{0}", vm.Id), owner);
            return RedirectToAction("Index", "Policies");
        }
    }
}
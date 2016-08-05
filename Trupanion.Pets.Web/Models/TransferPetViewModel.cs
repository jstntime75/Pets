using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Trupanion.Pets.Web.Models
{
    public class TransferPetViewModel
    {
        private readonly List<SelectListItem> _owners = new List<SelectListItem>();

        public int Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public short BreedId { get; set; }

        public string Breed { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name = "New Owner")]
        public int SelectedOwnerId { get; set; }

        public List<SelectListItem> Owners
        {
            get
            {
                return _owners;
            }
        }
    }
}
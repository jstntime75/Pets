using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Trupanion.Pets.Web.Models
{
    public class AddPetViewModel
    {
        private readonly List<SelectListItem> _breeds = new List<SelectListItem>();

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        [Required]
        [Display(Name = "Breed")]
        public short SelectedBreedId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(40)]
        public string Name { get; set; }

        public string PolicyNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime? DateOfBirth { get; set; }

        public List<SelectListItem> Breeds
        {
            get
            {
                return _breeds;
            }
        }
    }
}
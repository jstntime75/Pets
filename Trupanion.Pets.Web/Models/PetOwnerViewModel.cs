using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trupanion.Pets.Web.Models
{
    public class PetOwnerViewModel
    {
        private readonly List<PetViewModel> _pets = new List<PetViewModel>();

        public int Id { get; set; }

        [Display(Name = "Pet Owner Name")]
        public string Name { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? PolicyDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public List<PetViewModel> Pets
        {
            get
            {
                return _pets;
            }
        }
    }
}
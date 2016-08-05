using System;

namespace Trupanion.Pets.Web.Models
{
    public class PetViewModel
    {
        public int Id { get; set; }

        public int PetOwnerId { get; set; }

        public string Owner { get; set; }

        public int BreedId { get; set; }

        public string Breed { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trupanion.Pets.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pet
    {
        public int Id { get; set; }
        public int PetOwnerId { get; set; }
        public short BreedId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
    
        public virtual Breed Breed { get; set; }
        public virtual PetOwner PetOwner { get; set; }
    }
}

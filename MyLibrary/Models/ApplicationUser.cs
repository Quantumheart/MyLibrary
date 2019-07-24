using AspNetCore.Identity.Mongo;
using System;
using System.ComponentModel.DataAnnotations;


namespace MyLibrary.Models
{
    public class ApplicationUser : MongoIdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public byte Level { get; set; }

    }
}

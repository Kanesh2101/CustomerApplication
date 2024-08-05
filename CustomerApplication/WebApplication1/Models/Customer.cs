using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CustomerApplication.Models
{
    public class Customer
    {
        [Key]
        public required string Email { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }


        public required string ContactNo { get; set; }

        public string? Address { get; set; }

    }
}

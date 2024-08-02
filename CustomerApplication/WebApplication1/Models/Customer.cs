using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CustomerApplication.Models
{
    public class Customer
    {
        [Key]
        public required string email { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }


        public string? ContactNo { get; set; }

        public string? Address { get; set; }

    }
}

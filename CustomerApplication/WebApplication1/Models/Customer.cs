using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CustomerApplication.Models
{
    public class Customer
    {
        [Key]
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]

        public required string Email { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [RegularExpression(@"\+?6?(?:01[0-46-9]\d{7,8}|0\d{8})")]
        [DataType(DataType.PhoneNumber)]
        public required string ContactNo { get; set; }

        [Required]
        [StringLength(200)]
        public required string Address { get; set; }

    }
}

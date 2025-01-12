using System.ComponentModel.DataAnnotations;

namespace CarRental.FrontEnd.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please specify a first name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "First name must contain only letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please specify a last name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Last name must contain only letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number can't be empty.")]
        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid phone number format. It must have 10-15 digits and may start with '+'.")]
        public string Phone { get; set; }
        public AddressModel Address { get; set; }
        public UserModel User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CarRental.FrontEnd.Models
{
    public class AddressModel
    {
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Country can't be empty.")]
        [StringLength(50, ErrorMessage = "Country name can't exceed 50 characters.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Country mast contain only letters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City can't be empty.")]
        [StringLength(50, ErrorMessage = "City name can't exceed 50 characters.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "City mast contain only letters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street can't be empty.")]
        [StringLength(100, ErrorMessage = "Street name can't exceed 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9\\s.,/-]+$", ErrorMessage = "Street must contain only letters, digits, spaces, commas, and periods.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Postal code cannot be empty.")]
        [RegularExpression(@"^(\d{2}-\d{3}|\d{3}-\d{2}|\d{5})$", ErrorMessage = "Postal code format is not correct.")]
        public string PostalCode { get; set; }
    }
}

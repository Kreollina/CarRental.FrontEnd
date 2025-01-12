using System.ComponentModel.DataAnnotations;

namespace CarRental.FrontEnd.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Email cannot be empty.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z].*[A-Z])(?=.*\d)(?=.*[\W]).*$",
        ErrorMessage = "Password must contain at least 2 uppercase letters, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat password cannot be empty.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string RepeatPassword { get; set; }
    }
}

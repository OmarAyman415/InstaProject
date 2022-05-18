using System.ComponentModel.DataAnnotations;

namespace Instagram.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string UserPassword { get; set; }
        [Required]
        [StringLength(20)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        public string ProfileImage { get; set; }
    }
}
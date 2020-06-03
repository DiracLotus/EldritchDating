using System.ComponentModel.DataAnnotations;

namespace EldritchDating.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "You must specify a password between 8 and 256 characters")]
        public string Password { get; set; }
    }
}
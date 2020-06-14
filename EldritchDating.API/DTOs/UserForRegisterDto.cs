using System;
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
        [Required]
        public string Devotion { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Location { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public int Age { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
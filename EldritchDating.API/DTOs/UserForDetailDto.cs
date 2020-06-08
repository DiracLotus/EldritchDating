using System;
using System.Collections.Generic;
using EldritchDating.API.Models;

namespace EldritchDating.API.DTOs
{
    public class UserForDetailDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Devotion { get; set; }
        public string KnownAs { get; set; }
        public int AccountAge { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string Location { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailDto> Photos { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace EldritchDating.API.DTOs
{
    public class UserForListDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Devotion { get; set; }
        public string KnownAs { get; set; }
        public int AccountAge { get; set; }
        public DateTime LastActive { get; set; }
        public string Location { get; set; }
        public string PhotoUrl { get; set; }
    }
}
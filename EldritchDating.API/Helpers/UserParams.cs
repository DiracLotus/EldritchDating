using System;

namespace EldritchDating.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = Math.Min(value, MaxPageSize); }
        }
        public int UserId { get; set; }
        public string Devotion { get; set; }
        public string OrderBy { get; set; }
    }
}
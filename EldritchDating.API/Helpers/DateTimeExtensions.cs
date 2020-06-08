using System;

namespace EldritchDating.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime original) 
        {
            var age = DateTime.Today.Year - original.Year;
            if (original.AddYears(age) > DateTime.Today) 
                age--;

            return age;
        }    
    }
}
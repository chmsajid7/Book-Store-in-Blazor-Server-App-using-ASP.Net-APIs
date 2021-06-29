using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Helpers
{
    public static class StringExtensions
    {
        public static string FormatNumberStringWithDigits(this string text, int digits)
        {
            var result = string.Empty;
           if (text.Length > digits)
            {
                result = text.Take(digits - 1).ToString();
            }
            return result;
        }
    }
}

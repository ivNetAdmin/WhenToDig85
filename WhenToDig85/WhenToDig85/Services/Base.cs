
using System;
using System.Text.RegularExpressions;

namespace WhenToDig85.Services
{
    public class Base
    {
        public string MakeSlug(string[] items)
        {
            var key = String.Join(string.Empty, items).ToLowerInvariant();
            return Regex.Replace(key, "[^0-9a-z]", string.Empty);
        }
    }
}
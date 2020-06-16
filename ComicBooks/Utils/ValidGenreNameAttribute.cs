using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Utils
{
    public class ValidGenreNameAttribute : ValidationAttribute
    {
        //
        private readonly string allowedLetters;
        public ValidGenreNameAttribute(string allowedLetters)
        {
            this.allowedLetters = allowedLetters;
        }
        //
        public override bool IsValid(object value)
        {
            string letters = value.ToString();
            Boolean isValid = false;
            foreach (char ch in letters)
            {
                foreach (char allowedCh in allowedLetters)
                {
                    if(ch == allowedCh)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
    }
}

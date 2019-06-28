using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class NonNegativeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) <= 0)
            {
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CustomDataAnnotations
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            return true;
        }

    }
}

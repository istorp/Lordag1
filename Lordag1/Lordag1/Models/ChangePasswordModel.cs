using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lordag1.Models
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage ="The new and compaird passwords does not match!")]
        public string ConfirmPassword { get; set; }

    }
}

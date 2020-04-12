using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lordag1.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="The title shound not be missing")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The title shound not be missing")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The title shound not be missing")]
        public string Color { get; set; }

    }
}

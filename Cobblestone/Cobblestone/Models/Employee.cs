using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cobblestone.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name Required"), DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required"), DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birth Date Required"), DisplayName("Birth Date")]
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
    }
}
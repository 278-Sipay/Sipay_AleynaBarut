using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SipayApi.Models
{
    public class Student
    {
        [DisplayName("Student Id")]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public long Id { get; set; }

        [DisplayName("Student Name")]
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Name { get; set; }

        [DisplayName("Student Lastname")]
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Lastname { get; set; }

        [DisplayName("Student Age")]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Age { get; set; }

        [DisplayName("Student Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

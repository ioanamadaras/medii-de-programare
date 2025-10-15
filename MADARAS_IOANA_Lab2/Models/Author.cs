using System.ComponentModel.DataAnnotations;

namespace MADARAS_IOANA_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "AuthorName")]
        public string FullName => $"{FirstName} {LastName}";
    }
}

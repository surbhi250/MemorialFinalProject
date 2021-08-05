using System.ComponentModel.DataAnnotations;

namespace Memorial.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name = "Author Name ")]
        public string AuthorName { get; set; }

        [Display(Name = "Email ")]
        public string Email { get; set; }


        [Display(Name = "Contact ")]
        public string Contact { get; set; }


    }
}

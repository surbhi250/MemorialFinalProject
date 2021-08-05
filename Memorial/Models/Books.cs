using System.ComponentModel.DataAnnotations;

namespace Memorial.Models
{
    public class Books
    {
        public int Id { get; set; }

        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "NumberOfChapters")]
        public string NumberOfChapters { get; set; }
        
        [Display(Name = "AuthorId")]
        public int AuthorId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memorial.Models
{
    public class BuyBooks
    {
        public int Id { get; set; }

        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "NumberOfChapters")]
        public string NumberOfChapters { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}

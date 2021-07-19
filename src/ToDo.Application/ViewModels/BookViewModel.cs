using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.ViewModels
{
    public class BookViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Synopsis { get; set; }
        public string Url { get; set; }
        public bool Available { get; set; } = true;
    }
}

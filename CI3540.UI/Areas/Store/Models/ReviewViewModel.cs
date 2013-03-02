using System;

namespace CI3540.UI.Areas.Store.Models
{
    public class ReviewViewModel
    {
        public virtual int Id { get; set; }
        public string AuthorName { get; set; }
        public virtual string Comment { get; set; }
        public virtual float Rating { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
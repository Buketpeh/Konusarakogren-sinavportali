using System;
using System.Collections.Generic;

namespace sinavkonusarakogrenn.Data.Entites
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual List<Question> Question { get; set; }
    }
}

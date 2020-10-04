using sinavkonusarakogrenn.Data.Entites;
using System.Collections.Generic;

namespace sinavkonusarakogrenn.Models.Home
{
    public class ExeamViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Question> Questions { get; set; }
    }
}
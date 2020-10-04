using System.Collections.Generic;

namespace sinavkonusarakogrenn.Models.Home
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<QuestionModel> Questions { get; set; }
    }
}
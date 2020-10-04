namespace sinavkonusarakogrenn.Data.Entites
{
    public class Question
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public string D { get; set; }

        public string Answer { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
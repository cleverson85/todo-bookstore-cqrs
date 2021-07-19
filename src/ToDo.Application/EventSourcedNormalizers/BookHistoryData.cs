namespace ToDo.Application.EventSourcedNormalizers
{
    public class BookHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Synopsis { get; set; }
        public byte[] Image { get; set; }
        public bool Available { get; set; }
        public string Timestamp { get; set; }
        public string Who { get; set; }
    }
}

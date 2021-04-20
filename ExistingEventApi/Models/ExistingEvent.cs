namespace ExistingEventApi.Models
{
    public class ExistingEvent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ExistingEvent(string name)
        {
            Name = name;
            Id = _count;
            _count++;
        }

        private static long _count = 1;
    }
}

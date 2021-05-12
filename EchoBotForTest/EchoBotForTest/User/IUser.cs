using EchoBotForTest.Command.Commands;

namespace EchoBotForTest.User
{
    public interface IUser
    {
        public CreateEventCommandState createEventState { get; set; }
        public PlaceBetCommandState placeBetState { get; set; }
        public long Id { get; set; }
    }
}

using ttocskcajBot.Entities;
using ttocskcajBot.Models;

namespace ttocskcajBot.Generators
{
    public class RoomGenerator : IGenerator
    {
        public IEntity New()
        {
            Room room = new Room();

            return room;
        }
    }
}

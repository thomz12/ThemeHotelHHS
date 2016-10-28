using System.Collections.Generic;

namespace Hotel.RoomsFactory
{
    public interface IRoomFactoryComponent
    {
        Room BuildRoom(Dictionary<string, string> data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Persons
{
    // Node used for finding the path.
    class RoomNode
    {
        public Room Room;
        public int Weight;
        public RoomNode PrevRoom;

        public RoomNode(Room room, int weight, RoomNode prevRoom)
        {
            Room = room;
            Weight = weight;
            PrevRoom = prevRoom;
        }
    }

    class PathFinder
    {
        /// <summary>
        /// Finds the path from the currentRoom to the targetRoom
        /// </summary>
        /// <param name="currentRoom">The current room</param>
        /// <param name="targetRoom">The room to path find to.</param>
        /// <returns>Null if not found, list of rooms you need to go through.</returns>
        public List<Room> FindPath(Room currentRoom, Room targetRoom)
        {
            List<RoomNode> queue = new List<RoomNode>();
            List<RoomNode> visited = new List<RoomNode>();
            RoomNode endNode = null;

            // Add the current room.
            queue.Add(new RoomNode(currentRoom, 0, null));

            while (queue.Count > 0)
            {
                RoomNode current = queue.OrderBy(x => x.Weight).First();

                if (current.Room == targetRoom)
                {
                    endNode = current;
                    break;
                }

                foreach (Room room in current.Room.Neighbors.Values)
                {
                    if (!visited.Select(x => x.Room).Contains(room))
                        queue.Add(new RoomNode(room, current.Weight + room.Weight, current));
                }

                visited.Add(current);
                queue.Remove(current);
            }

            // If no end node was found, return null.
            if (endNode == null)
                return null;

            // The path to follow
            List<RoomNode> path = new List<RoomNode>();
            // begin with the end node
            path.Add(endNode);
            // keep adding the previous node to the final path
            while (true)
            {
                // the previous node
                RoomNode node = path.Last().PrevRoom;

                if (node != null)
                    path.Add(node);
                else
                    break;
            }

            // reverse so that current room is start room, and end room is the last room.
            return path.Select(x => x.Room).Reverse().ToList();
        }
    }
}

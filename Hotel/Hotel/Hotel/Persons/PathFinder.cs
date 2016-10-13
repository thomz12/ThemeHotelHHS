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
        public float Weight;
        public RoomNode PrevRoom;

        public RoomNode(Room room, float weight, RoomNode prevRoom)
        {
            Room = room;
            Weight = weight;
            PrevRoom = prevRoom;
        }
    }

    public delegate bool FindPath(Room room);

    public class PathFinder
    {
        public bool UseElevator { get; set; }

        public PathFinder()
        {
            UseElevator = true;
        }

        /// <summary>
        /// Find the closest room with the given argument
        /// </summary>
        /// <param name="currentRoom">The room to start the pathfinding from.</param>
        /// <param name="rule">The lambda exprission for the room to find.</param>
        /// <returns>A list with rooms, that form a path.</returns>
        public List<Room> Find(Room currentRoom, FindPath rule)
        {
            List<RoomNode> queue = new List<RoomNode>();
            List<RoomNode> visited = new List<RoomNode>();
            RoomNode endNode = null;

            // Add the current room.
            queue.Add(new RoomNode(currentRoom, 0, null));

            // Go through all the items in the queue
            while (queue.Count > 0)
            {
                // Get the node with the lowest weight.
                RoomNode current = queue.OrderBy(x => x.Weight).First();

                // if the current room is the target room, were done.
                if (rule(current.Room) == true)
                {
                    endNode = current;
                    break;
                }

                // For every neighbor in the room.
                foreach (Room room in current.Room.Neighbors.Values)
                {
                    if (!UseElevator && room is ElevatorShaft && current.Room is ElevatorShaft)
                        continue;

                    // Returns this room from the queue or null when it is not yet in the queue.
                    RoomNode nodeToCheck = visited.Where(x => x.Room == room).FirstOrDefault();

                    // If the node is not yet visited.
                    if (nodeToCheck == null)
                    {
                        queue.Add(new RoomNode(room, current.Weight + room.Weight, current));
                    }
                    else
                    {
                        if (nodeToCheck.Weight > current.Weight + room.Weight)
                        {
                            nodeToCheck.PrevRoom = current;
                            nodeToCheck.Weight = current.Weight + room.Weight;
                        }
                    }
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
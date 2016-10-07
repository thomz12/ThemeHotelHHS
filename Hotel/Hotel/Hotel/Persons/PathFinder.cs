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

    public class PathFinder
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
            
            // Go through all the items in the queue
            while (queue.Count > 0)
            {
                // Get the node with the lowest weight.
                RoomNode current = queue.OrderBy(x => x.Weight).First();

                // if the current room is the target room, were done.
                if (current.Room == targetRoom)
                {
                    endNode = current;
                    break;
                }

                // For every neighbor in the room.
                foreach (Room room in current.Room.Neighbors.Values)
                {
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

        /// <summary>
        /// Finds the path to the closest room with a certain state.
        /// </summary>
        /// <param name="startRoom">The room to start the search from.</param>
        /// <param name="state">The state the room must have.</param>
        /// <returns>A path from the start room to the closest room with a certain state.</returns>
        public List<Room> FindPathToRoomWithState(Room startRoom, RoomState state)
        {
            // Initialize variables
            List<RoomNode> queue = new List<RoomNode>();
            List<RoomNode> visited = new List<RoomNode>();
            RoomNode endNode = null;

            // add the current room to the list to start off dijkstra's.
            queue.Add(new RoomNode(startRoom, 0, null));

            // Go through all the items in the queue O(n2)
            while(queue.Count > 0)
            {
                // Get the node with the lowest weight
                RoomNode current = queue.OrderBy(x => x.Weight).First();

                // Check if a room is dirty
                if(current.Room.State == state)
                {
                    endNode = current;
                    break;
                }

                // For every neighbor in the room, check if it is already in the list
                foreach (Room thisNeighbor in current.Room.Neighbors.Values)
                {
                    // Returns thisNeighbor from the queue or null when it is not yet in the queue.
                    RoomNode nodeToCheck = queue.Where(x => x.Room == thisNeighbor).FirstOrDefault();

                    // If the nodeToCheck is null, add a new node to the queue
                    if(nodeToCheck == null)
                    {
                        queue.Add(new RoomNode(thisNeighbor, current.Weight + thisNeighbor.Weight, current));
                    }
                    else
                    {
                        // This neighbor is already in the queue.
                        if(nodeToCheck.Weight > thisNeighbor.Weight + current.Weight)
                        {
                            nodeToCheck.PrevRoom = current;
                            nodeToCheck.Weight = current.Weight + thisNeighbor.Weight;
                        }
                    }
                }

                // We are done with this node, so add it to the visited list and remove it from the queue.
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

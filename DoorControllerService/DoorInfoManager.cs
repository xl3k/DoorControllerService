using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorControllerService
{
    public static class DoorInfoManager
    {
        static List<Door> doors;

        static DoorInfoManager()
        { 
            Initialize();
        }

        static void Initialize()
        {
            // simulate data initialization
            doors = new List<Door>
            {
                new Door{ Label = "Door 1", IsLocked = true, IsOpen= false, },
                new Door{ Label = "Door 2", IsLocked = true, IsOpen= false, },
                new Door{ Label = "Door 3", IsLocked = true, IsOpen= false, },
                new Door{ Label = "Door 4", IsLocked = false, IsOpen= true, },
                new Door{ Label = "Door 5", IsLocked = true, IsOpen= false, },
                new Door{ Label = "Door 6", IsLocked = true, IsOpen= false, },
                new Door{ Label = "Door 7", IsLocked = false, IsOpen= false,},
                new Door{ Label = "Door 8", IsLocked = false, IsOpen= true, }
            };
        }

        public static Door AddDoor(Door door)
        {
                      
            Door newDoor = new Door { IsLocked = door.IsLocked, IsOpen = door.IsOpen, Label = door.Label };
            doors.Add(newDoor);
            return newDoor;
        }

        public static bool DeleteDoor(string id)
        {
            
            var found = doors.FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                doors.Remove(found);
                return true;
            }
            return false;
        }

        public static List<Door> GetDoors()
        {
            
            return doors.ToList();
        }

        public static Door UpdateDoor(string id, Door door)
        {
            
            if (door != null)
            {
                // update door
                var found = doors.FirstOrDefault(x => x.Id == id);
                if (found != null)
                {
                    found.Label = door.Label;
                    found.IsLocked = door.IsLocked;
                    found.IsOpen = door.IsOpen;
                    return found;
                }
            }
            return null;
        }

    }
}
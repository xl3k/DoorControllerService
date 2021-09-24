using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoorControllerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = true)]

    public class DoorController : IDoorController
    {
        //List<IBroadcastReceiver> subscribedClientList = new List<IBroadcastReceiver>();
        private static Dictionary<string, IDoorControllerCallback> subscribedClientList = new Dictionary<string, IDoorControllerCallback>();
        public void AddDoor(Door door)
        {
            Door newDoor = DoorInfoManager.AddDoor(door);
            foreach (var client in subscribedClientList)
            {
                client.Value.OnDoorAdded(newDoor);
            }
        }

        public void DeleteDoor(string id)
        {
             bool success = DoorInfoManager.DeleteDoor(id);

            foreach(var client in subscribedClientList)
            {
                client.Value.OnDoorDeleted(id, success);
            }           
            
        }

        public void GetDoors()
        {
             List<Door> doors = DoorInfoManager.GetDoors();

            Callback.OnDoorListReceived(doors);
        }

        public void UpdateDoor(string id, Door door)
        {
            Door newDoor = DoorInfoManager.UpdateDoor(id,door);

            foreach (var client in subscribedClientList)
            {
                client.Value.OnDoorUpdated(newDoor);
            }
        }

        public bool Subscribe(string clientId)
        {
            IDoorControllerCallback callback = OperationContext.Current.GetCallbackChannel<IDoorControllerCallback>();

            lock (subscribedClientList)
            {
                if (!subscribedClientList.ContainsKey(clientId))
                {
                    subscribedClientList.Add(clientId, callback);
                    return true;
                }

                return false;
            }
        }

        public bool UnSubscribe(string clientId)
        {
            lock (subscribedClientList)
            {
                if (subscribedClientList.ContainsKey(clientId))
                {
                    subscribedClientList.Remove(clientId);
                    return true;
                }
                return false;
            }
        }

        IDoorControllerCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IDoorControllerCallback>();
            }
        }
    }
}

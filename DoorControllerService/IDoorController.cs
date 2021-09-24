using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoorControllerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Name = "DoorControllerService", CallbackContract = typeof(IDoorControllerCallback))]
    public interface IDoorController
    {
        [OperationContract]
        void GetDoors();

        [OperationContract]        
        void UpdateDoor(string id, Door door);
        [OperationContract]
        void AddDoor( Door door);
        [OperationContract]
        void DeleteDoor(string id);

        [OperationContract]
        bool Subscribe(string clientId);
        [OperationContract]
        bool UnSubscribe(string clientId);
       

    }

    public interface IDoorControllerCallback
    {   
        [OperationContract]
        void OnDoorListReceived(List<Door> doors);

        [OperationContract]
        void OnDoorUpdated(Door door);
        [OperationContract]
        void OnDoorDeleted(string id, bool success);
        [OperationContract]
        void OnDoorAdded(Door door);
    }

    //public interface IBroadcastReceiver
    //{
    //    [OperationContract]
    //    string GetClientId();
    //    [OperationContract]
    //    void OnDoorUpdated(Door door);
    //    [OperationContract]
    //    void OnDoorDeleted(string id,bool success);
    //    [OperationContract]
    //    void OnDoorAdded(Door door);
    //}


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Door
    {        
        string label = "";
        bool isOpen = false;
        bool isLocked = true;
        readonly string id = Guid.NewGuid().ToString();



        [DataMember]
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        [DataMember]
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        [DataMember]
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        [DataMember]
        public string Id 
        { 
            get { return id; }
            set { string temp = value; }
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ThirdAssignment
{
    [Serializable]
    class Room : IRoom
    {
        private int roomNumber;
        private int numberOfBeds;
        private bool ownBathRoom;

        private static int roomNumberReference;

        private static string filePath = @"U:\dotNet\ThirdAssignment\rooms.dat";
        private static BinaryFormatter binaryFormatter = new BinaryFormatter();
        private static FileStream fileStream; 

        public Room(int numberOfBeds, bool ownBathRoom) {
            this.roomNumber = roomNumberReference++;
            this.numberOfBeds = numberOfBeds;
            this.ownBathRoom = ownBathRoom;
        }

        public override string ToString()
        {
            return "" + roomNumber + "\tNumber of Beds: " + numberOfBeds + " Has own bathroom: " + ownBathRoom;
        }
        public static bool saveRooms(List<Room> rooms) {
            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                foreach (Room room in rooms)
                {
                    binaryFormatter.Serialize(fileStream, room);
                }
                binaryFormatter.Serialize(fileStream, roomNumberReference);
                fileStream.Close();
                return true;
            }
            catch (IOException e) {
                Console.WriteLine("Couldn't save the Rooms: " + e.Message);
                return false;
            }
        }
        public static List<Room> loadRooms() {
            List<Room> loadedRooms = new List<Room>();
            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                Object obj;
                while(fileStream.Position != fileStream.Length)
                {
                    obj = binaryFormatter.Deserialize(fileStream);
                    if (obj is Room)
                    {
                        loadedRooms.Add((Room)obj);
                    }
                    if (obj is int)
                    {
                        roomNumberReference = (int)obj;
                    }
                }
                fileStream.Close();
                return loadedRooms;
            }
            catch (IOException e)
            {
                Console.WriteLine("Couldn't load the Rooms: " + e.Message);
                return null;
            }
            catch (SerializationException)
            {
                return null;
            }
        }

        public int RoomNumber {
            get {
                return this.roomNumber;
            }
            set {
                this.roomNumber = value;
            }
        }
        public int NumberOfBeds{
            get{
                return this.numberOfBeds;
            }
            set {
                this.numberOfBeds = value;
            }
        }
        public bool OwnBathRoom {
            get {
                return this.ownBathRoom;
            }
            set {
                this.ownBathRoom = value;
            }
        }


    }
}

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
    class Booking
    {
        private int bookingNumber;
        private Room room;
        private Customer customer;
        private DateTime arrival;
        private DateTime departure;
        private int numberOfPeople;

        private static int bookingNumberReference;

        private static string filePath = @"U:\dotNet\ThirdAssignment\bookings.dat";
        private static BinaryFormatter binaryFormatter = new BinaryFormatter();
        private static FileStream fileStream;

        public Booking(Room r, Customer c, DateTime arrival, DateTime departure, int numberOfPeople) {
            this.bookingNumber = bookingNumberReference++;
            this.room = r;
            this.customer = c;
            this.arrival = arrival;
            this.departure = departure;
            this.numberOfPeople = numberOfPeople;
        }

        public override string ToString() {
            return "Booking Number: " + bookingNumber + "\nRoom (" + room.ToString() + ")\nis reserved for \n" + customer.ToString() + "\nduring this Time: " + String.Format("{0:d.M.yyyy}", arrival) + " - " + String.Format("{0:d.M.yyyy}", departure) + "\nFor " + numberOfPeople + " People\n";
        }
        internal static bool saveBookings(List<Booking> bookings)
        {
            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                foreach (Booking b in bookings)
                {
                    binaryFormatter.Serialize(fileStream, b);
                }
                binaryFormatter.Serialize(fileStream, bookingNumberReference);
                fileStream.Close();
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Couldn't save the Bookings: " + e.Message);
                return false;
            }
        }
        internal static List<Booking> loadBookings()
        {
            List<Booking> loadedBookings = new List<Booking>();
            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                Object obj;
                while (fileStream.Position != fileStream.Length)
                {
                    obj = binaryFormatter.Deserialize(fileStream);
                    if (obj is Booking)
                    {
                        loadedBookings.Add((Booking)obj);
                    }
                    if (obj is int) {
                        bookingNumberReference = (int)obj;
                    }
                }
                fileStream.Close();
                return loadedBookings;
            }
            catch (IOException e)
            {
                Console.WriteLine("Couldn't load the Bookings: " + e.Message);
                return null;
            }
            catch (SerializationException)
            {
                return null;
            }
        }
    }
}

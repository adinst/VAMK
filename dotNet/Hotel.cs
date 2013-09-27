using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ThirdAssignment
{
    class Hotel
    {
        private string hotelName;
        private List<Room> rooms; 
        private List<Customer> customers;
        private List<Booking> bookings;

        public Hotel(string hotelName)
        {
            this.hotelName = hotelName;
            this.rooms = new List<Room>();
            this.customers = new List<Customer>();
            this.bookings = new List<Booking>();
        }
        public bool loadData() {
            this.rooms = Room.loadRooms();
            this.customers = Customer.loadCustomer();
            this.bookings = Booking.loadBookings();
            return true;
        }
        public bool input() {
            bool loop = true;
            while(loop){
                Console.WriteLine("\nWelcome to : "+hotelName+"\n\n1\tAdd Customer\n2\tAdd Room\n3\tAdd Booking\n4\tShow Customers\n5\tShow Rooms\n6\tShow Bookings\n7\tExit\n");
                int choice = Convert.ToInt16(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Firstname: ");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Lastname: ");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Birthday: ");
                        DateTime birthDay = Convert.ToDateTime(Console.ReadLine());
                        this.customers.Add(new Customer(firstName,lastName,birthDay));
                        if (Customer.saveCustomer(this.customers))
                        {
                            Console.WriteLine("Customer accepted!");
                        }
                        else {
                            Console.WriteLine("Customer not accepted!");
                        }                        
                        break;
                    case 2:
                        Console.WriteLine("Number of Beds: ");
                        int numberOfBeds = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Own Bathroom (True or False): ");
                        bool ownBathRoom = Convert.ToBoolean(Console.ReadLine());
                        Console.WriteLine("Birthday: ");
                        this.rooms.Add(new Room(numberOfBeds,ownBathRoom));
                        if (Room.saveRooms(this.rooms))
                        {
                            Console.WriteLine("Room accepted!");
                        }
                        else {
                            Console.WriteLine("Room not accepted!");
                        }                        
                        break;
                    case 3:
                        Console.WriteLine("\nChoose a Room: ");
                        foreach (Room r in rooms) {
                            Console.WriteLine(r.ToString());
                        }
                        int roomNumber = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("\nChoose a Customer: ");
                        foreach (Customer c in customers) {
                            Console.WriteLine(c.ToString());
                        }
                        int customerNumber = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("\nArrival (Date): ");
                        DateTime arrival = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("\nDeparture (Date): ");
                        DateTime departure = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Number of People: ");
                        int numberOfPeople = Convert.ToInt16(Console.ReadLine());
                        Customer customer = null;
                        foreach (Customer c in customers) {
                            if (customerNumber == c.CustomerNumber) {
                                customer = c;
                            }
                        }
                        Room room = null;
                        foreach (Room r in rooms) {
                            if (roomNumber == r.RoomNumber) {
                                room = r;
                            }
                        }
                        if (room != null && customer != null) {
                            bookings.Add(new Booking(room, customer, arrival, departure, numberOfPeople));
                        }                        
                        if (Booking.saveBookings(bookings))
                        {
                            Console.WriteLine("Booking accepted!");
                        }
                        else {
                            Console.WriteLine("Booking not accepted!");
                        }                        
                        break;
                    case 4:
                        Console.WriteLine("\nAll Customers: ");
                        foreach (Customer c in customers) {
                            Console.WriteLine(c.ToString());
                        }
                        break;
                    case 5:
                        Console.WriteLine("\nAll Rooms: ");
                        foreach (Room r in rooms) {
                            Console.WriteLine(r.ToString());
                        }
                        break;
                    case 6:
                        Console.WriteLine("\nAll Bookings: ");
                        foreach (Booking b in bookings) {
                            Console.WriteLine(b.ToString());
                        }
                        break;
                    default:
                        loop = false;
                        break;
                }

            }            
            return false;
        }

    }
}

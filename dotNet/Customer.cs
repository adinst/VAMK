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
    class Customer: ICustomer
    {
        private int customerNumber;
        private string firstName;
        private string lastName;
        private DateTime birthDay;

        private static int customerNumberReference;

        private static string filePath = @"U:\dotNet\ThirdAssignment\customers.dat";
        private static BinaryFormatter binaryFormatter = new BinaryFormatter();
        private static FileStream fileStream;

        public Customer(string firstName, string lastName, DateTime birthDay)
        {
            this.customerNumber = customerNumberReference++;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDay = birthDay;
        }

        public override string ToString()
        {
            return "" + customerNumber + "\tFirst Name: " + firstName + " Last Name: " + lastName + " Date of Birth: " + String.Format("{0:d.M.yyyy}", birthDay);
        }

        public static bool saveCustomer(List<Customer> customers)
        {
            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                foreach (Customer customer in customers)
                {
                    binaryFormatter.Serialize(fileStream, customer);
                }
                binaryFormatter.Serialize(fileStream, customerNumberReference);
                fileStream.Close();
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Couldn't save the Customers: " + e.Message);
                return false;
            }
        }
        public static List<Customer> loadCustomer()
        {
            List<Customer> loadedCustomers = new List<Customer>();
            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                Object obj;
                while (fileStream.Position != fileStream.Length)
                {
                    obj = binaryFormatter.Deserialize(fileStream);
                    if (obj is Customer)
                    {
                        loadedCustomers.Add((Customer)obj);
                    }
                    if (obj is int) {
                        customerNumberReference = (int)obj;
                    }
                }
                fileStream.Close();
                return loadedCustomers;
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

        public string FirstName
        {
            get {return firstName;}
            set{this.firstName=value;}
        }
        public string LastName
        {
            get { return lastName; }
            set { this.lastName = value; }
        }
        public DateTime BirthDay
        {
            get {return birthDay;}
            set{this.birthDay=value;}
        }
        public int CustomerNumber 
        { 
            get { return this.customerNumber; } 
            set { this.customerNumber = value; } 
        }
    }
}

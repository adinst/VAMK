using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThirdAssignment
{
    interface ICustomer
    {
        int CustomerNumber
        {
            get;
            set;
        }
        string FirstName
        {
            get;
            set;
        }
        string LastName
        {
            get;
            set;
        }
        DateTime BirthDay
        {
            get;
            set;
        }


    }
}

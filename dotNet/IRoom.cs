using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThirdAssignment
{
    interface IRoom
    {
        int RoomNumber
        {
            get;
            set;
        }
        int NumberOfBeds { 
            get;
            set;
        }
        bool OwnBathRoom {
            get;
            set;
        }
    }
}

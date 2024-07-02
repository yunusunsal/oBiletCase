using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Enums
{
    //Obilet Business Api'den dönecek status değerini tutan Enum
    public enum ResponseStatus
    {
        Success,
        InvalidDepartureDate,
        InvalidRoute,
        InvalidLocation,
        Timeout

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartActS.DataModels
{
    public static class Common
    {
        public  enum eRequest
        {
            Pending =0,
            Approved =1,
            Confirmed = 2,
            Expired =3,
            Closed =4

        }
        public enum eResponse
        {
            Pending = 0,
            Confirmed = 1,
            Rejected = 2,
            Expired =3,
            Closed =4

        }
    }
}
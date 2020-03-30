using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
namespace ManagementPanel
{
    public static class StaticClass
    {
        public static SqlConnection constr;
        
        public static SqlConnection constemp;
        public static Thread tGetOnline;
        public static Int32 LocationX=0;
        public static Int32 LocationY=0;
        public static Int32 dealerUserId=0;
        public static string DealerUserName = "";
        public static Int32 DfClientId = 409;
        public static Int32 DealerDfClientId = 0;
        public static Int32 CountryCode = 1;
        public static string DealerCode = "MyClaud000";
        public static Int32 DealerTokenId = 0;


        //public static string PlayVerName = "";
        //public static Int32 PlayDealerId = 0;
        //public static Int32 PlayFormatId = 0;
    }
}

//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
   public  class Customers // מחלקה שמטפלת בתכונות לקוח 
    {
        private int    CustomerID;        // תעדות זהות של לקוח
        private string CustomerFName;     // שם פרטי של לקוח 
        private string CustomerLName;     // שם משפחה של לקוח 
        private string CustomerAddress;   // כתובת של לקוח 
        private string CustomerTelephone; // מספר טלפון של לקוח
        private string CustomerEmail;     // כתובת אימייל של לקוח

        // getters and setters למשתני המחלקה

        public int Customer_ID
        {
            get
            {
                return CustomerID;
            }
            set
            {
                if (value > 0)
                    CustomerID = value;
            }
        }

        public string Customer_FirstName
        {
            get
            {
                return CustomerFName;
            }
            set
            {
                CustomerFName = value;
            }
        }

        public string Customer_LastName
        {
            get
            {
                return CustomerLName;
            }
            set
            {
                CustomerLName = value;
            }
        }

        public string Customer_Address
        {
            get
            {
                return CustomerAddress;
            }
            set
            {
                CustomerAddress = value;
            }
        }

        public string Customer_Telephone
        {
            get
            {
                return CustomerTelephone;
            }
            set
            { 
                     CustomerTelephone = value;
            }
        }

        public string Customer_Email
        {
            get
            {
                return CustomerEmail;
            }
            set
            {
                CustomerEmail = value;
            }
        }


    }
}

//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public  class CustomersOrders // מחלקה שמטפלת בהזמנות של לקוחות 
    {
        private int      CustomersOrdersID;                     //מספר הזמנה של לקוח
        private int      CustomersOrdersClientID;               //מספר תעודת זהות של לקוח
        private string   CustomersOrdersClientName;             //שם פרטי של לקוח
        private bool     CustomersOrdersOrderSuppliedToCustomer;//האם הלקוח קיבל את ההזמנה שלו
        private DateTime CustomersOrdersDate;                   //תאריך הזמנה 
        private double   CustomerOrdersPrice;                   //מחיר ההזמנה 
        private string   CustomersOrdersWorkerApproved;         //שם העובד שביצע את ההזמנה
         
        // getters and setters למשתני המחלקה

        public int CustomersOrders_ID
        {
            get
            {
                return CustomersOrdersID;
            }
            set
            {
                if (value > 0)
                    CustomersOrdersID = value;
            }
        }

        public int CustomersOrders_ClientID
        {
            get
            {
                return CustomersOrdersClientID;
            }
            set
            {
                if (value > 0)
                    CustomersOrdersClientID = value;
            }
        }

        public string CustomersOrders_ClientName
        {
            get
            {
                return CustomersOrdersClientName;
            }
            set
            {
                CustomersOrdersClientName = value;
            }
        }

        public bool CustomersOrders_OrderSuppliedToCustomer
        {
            get
            {
                return CustomersOrdersOrderSuppliedToCustomer;
            }
            set
            {
                CustomersOrdersOrderSuppliedToCustomer = value;
            }
        }

        public DateTime CustomersOrders_Date
        {
            get
            {
                return CustomersOrdersDate;
            }
            set
            {
                 CustomersOrdersDate = value;
            }
        }

        public double CustomerOrders_Price
        {
            get
            {
                return CustomerOrdersPrice;
            }
            set
            {   if(value > 0)
                    CustomerOrdersPrice = value;
            }
        }

        public string CustomersOrders_WorkerApproved
        {
            get
            {
                return CustomersOrdersWorkerApproved;
            }
            set
            {
                CustomersOrdersWorkerApproved = value;
            }
        }
    }
}

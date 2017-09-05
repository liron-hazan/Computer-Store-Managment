//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store
{
    public class CustomersOrdersDetail  // מחלקה שמטפלת בפירוט הזמנות של לקוחות 

    {
        private int    CustomerOrderID;                 //מספר הזמנה של לקוח
        private int    CustomerOrderClientID;           //מספר תעודת זהות של לקוח
        private string CustomerOrderClientName;         //שם פרטי של לקוח
        private int    CustomerOrderProductID;          //מזהה מוצר שלקוח הזמין
        private string CustomerOrderProductDescription; //תיאור מוצר שלקוח הזמין
        private int    CustomerOrderQuantity;           //כמות שהלקוח הזמין
        private bool   CustomerOrderProductReceived;    //האם הלקוח קיבל את המוצר

        // getters and setters למשתני המחלקה

        public int CustomerOrder_ID
        {
            get
            {
                return CustomerOrderID;
            }
            set
            {
                if (value > 0)
                    CustomerOrderID = value;
            }
        }

        public int CustomerOrder_ClientID
        {
            get
            {
                return CustomerOrderClientID;
            }
            set
            {
                if (value > 0)
                    CustomerOrderClientID = value;
            }
        }

        public string CustomerOrder_ClientName
        {
            get
            {
                return CustomerOrderClientName;
            }
            set
            {
                CustomerOrderClientName = value;
            }
        }

        public int CustomerOrder_ProductID
        {
            get
            {
                return CustomerOrderProductID;
            }
            set
            {
                if (value > 0)
                    CustomerOrderProductID = value;
            }
        }


        public string CustomerOrder_ProductDescription
        {
            get
            {
                return CustomerOrderProductDescription;
            }
            set
            {
                CustomerOrderProductDescription = value;
            }
        }

        public int CustomerOrder_Quantity
        {
            get
            {
                return CustomerOrderQuantity;
            }
            set
            {
                if (value>0)
                CustomerOrderQuantity = value;
            }
        }

        public bool CustomerOrder_ProductReceived
        {
            get
            {
                return CustomerOrderProductReceived;
            }
            set
            {
                CustomerOrderProductReceived = value;
            }
        }
    }
}

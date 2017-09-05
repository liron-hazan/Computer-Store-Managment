//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Archive                                        //מחלקה שמטפלת בארכיון 
    {
        private DateTime ArchiveCustomersOrderDate;             //תאריך הזמנה 
        private int      ArchiveCustomerOrderID;                //מספר הזמנה של לקוח
        private int      ArchiveCustomerOrderClientID;          //מספר תעודת זהות של לקוח  
        private string   ArchiveCustomerOrderClientName;        //שם פרטי של לקוח
        private int      ArchiveCustomerOrderProductID;         //מזהה מוצר שלקוח הזמין 
        private string   ArchiveCustomerOrderProductDescription;//תיאור מוצר שלקוח הזמין 
        private int      ArchiveCustomerOrderQuantity;          //כמות שהלקוח הזמין
        private int      ArchiveCustomerOrderPrice;             //מחיר שהלקוח שילם באותה תקופה

        // getters and setters למשתני המחלקה


        public DateTime ArchiveCustomersOrder_Date
        {
            get
            {
                return ArchiveCustomersOrderDate;
            }
            set
            {
                ArchiveCustomersOrderDate = value;
            }
        }
        public int ArchiveCustomerOrder_ID
        {
            get
            {
                return ArchiveCustomerOrderID;
            }
            set
            {
                if (value > 0)
                    ArchiveCustomerOrderID = value;
            }
        }

        public int ArchiveCustomerOrder_ClientID
        {
            get
            {
                return ArchiveCustomerOrderClientID;
            }
            set
            {
                if (value > 0)
                    ArchiveCustomerOrderClientID = value;
            }
        }

        public string ArchiveCustomerOrder_ClientName
        {
            get
            {
                return ArchiveCustomerOrderClientName;
            }
            set
            {
                ArchiveCustomerOrderClientName = value;
            }
        }

        public int ArchiveCustomerOrder_ProductID
        {
            get
            {
                return ArchiveCustomerOrderProductID;
            }
            set
            {
                if (value > 0)
                    ArchiveCustomerOrderProductID = value;
            }
        }


        public string ArchiveCustomerOrder_ProductDescription
        {
            get
            {
                return ArchiveCustomerOrderProductDescription;
            }
            set
            {
                ArchiveCustomerOrderProductDescription = value;
            }
        }

        public int ArchiveCustomerOrder_Quantity
        {
            get
            {
                return ArchiveCustomerOrderQuantity;
            }
            set
            {
                if (value > 0)
                    ArchiveCustomerOrderQuantity = value;
            }
        }

        public int ArchiveCustomerOrder_Price
        {
            get
            {
                return ArchiveCustomerOrderPrice;
            }
            set
            {
                if (value > 0)
                    ArchiveCustomerOrderPrice = value;
            }
        }


    }
}







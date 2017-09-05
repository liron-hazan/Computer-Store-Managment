//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
   public  class Suppliers // מחלקה שמטפלת בספק 
    {

        private int    SupplierID;        // מזהה ספק 
        private string SupplierName;      // שם של הספק 
        private string SupplierTelephone; // מספר טלפון של הספק
      
        // getters and setters למשתני המחלקה

        public int Supplier_ID
        {
            get
            {
                return SupplierID;
            }
            set
            {
                if (value > 0)
                    SupplierID = value;
            }
        }

        public string Supplier_Name
        {
            get
            {
                return SupplierName;
            }
            set
            {
                SupplierName = value;
            }
        }

        public string Supplier_Telephone
        {
            get
            {
                return SupplierTelephone;
            }
            set
            {
                    SupplierTelephone = value;
            }
        }

    }

}

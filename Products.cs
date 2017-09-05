//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
      public  class Products // מחלקה שמטפלת במוצר
    {
        private int    ProductID;                //מספר סידורי של המוצר 
        private string ProductName;              // שם המוצר 
        private int    ProductSupplierIdentifier;// מזהה ספק של המוצר 
        private string ProductDescription;       // תיאור המוצר 
        private string ProductManufacturer;      // היצרן של המוצר
        private double ProductPrice;             // מחיר המוצר 
        private int    ProductStock;             // כמות במלאי של המוצר 
        private string ProductType;              // סוג המוצר 

        // getters and setters למשתני המחלקה

        public int Proudct_ID
        {
            get
            {
                return ProductID;
            }
            set
            {
                if (value > 0)
                    ProductID = value;
            }
        }

        public string Product_Name
        {
            get
            {
                return ProductName;
            }
            set
            {
                    ProductName = value;
            }
        }

        public int Product_SupplierIdentifier
        {
            get
            {
                return ProductSupplierIdentifier;
            }
            set
            {
                if (value > 0)
                    ProductSupplierIdentifier = value;
            }
        }

        public string Product_Description
        {
            get
            {
                return ProductDescription;
            }
            set
            {
                ProductDescription = value;
            }
        }

        public string Product_Manufacturer
        {
            get
            {
                return ProductManufacturer;
            }
            set
            {
                ProductManufacturer = value;
            }
        }

        public double Product_Price
        {
            get
            {
                return ProductPrice;
            }
            set
            {
                if (value > 0)
                    ProductPrice = value;
            }
        }

        public int Product_Stock
        {
            get
            {
                return ProductStock;
            }
            set
            {
                if (value >= 0)
                    ProductStock = value;
            }
        }

        public string Product_Type
        {
            get
            {
                return ProductType;
            }
            set
            {
                ProductType = value;
            }
        }

    }
}

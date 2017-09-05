//שמות תכנתים: לירון חזן  וכפיר ארגנטל

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public  class DBSQL : DbAccess//מחלקת השאילתות שלנו 
    {

        public DBSQL(string connectionString) // בנאי של DBSQL
            : base(connectionString)
        { }

        public Workers[] GetWorkerData()//פונקציה לאחסון נתוני עובד
        {
            DataSet ds = new DataSet();
            ArrayList Workers1 = new ArrayList();
            string cmdStr = "SELECT * FROM   [Workers]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                Workers worker = new Workers();
                worker.Worker_ID = int.Parse(tType[0].ToString());
                worker.Worker_Name = tType[1].ToString();
                worker.Worker_UserName = tType[2].ToString();
                worker.Worker_Password = tType[3].ToString();
                worker.Manager_IsAManager = bool.Parse(tType[4].ToString());

                Workers1.Add(worker);
            }
            return (Workers[])Workers1.ToArray(typeof(Workers));
        }

        public Customers[] GetCustomerData()//פונקציה לאחסון נתוני לקוחות
        {
            DataSet ds = new DataSet();
            ArrayList Customer1 = new ArrayList();
            string cmdStr = "SELECT * FROM   [Customers]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                Customers customer = new Customers();
                customer.Customer_ID = int.Parse(tType[0].ToString());
                customer.Customer_FirstName = tType[1].ToString();
                customer.Customer_LastName = tType[2].ToString();
                customer.Customer_Address = tType[3].ToString();
                customer.Customer_Telephone = (tType[4].ToString());
                customer.Customer_Email = tType[5].ToString();

                Customer1.Add(customer);
            }
            return (Customers[])Customer1.ToArray(typeof(Customers));
        }


       

        public Products[] GetProductsData()//פונקציה לאחסון נתוני מוצרים
        {
            DataSet ds = new DataSet();
            ArrayList PC = new ArrayList();
            string cmdStr = "SELECT * FROM   [Products]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                Products p = new Products();
                p.Proudct_ID = int.Parse(tType[0].ToString());
                p.Product_SupplierIdentifier = int.Parse(tType[1].ToString());
                p.Product_Type = (tType[2].ToString());
                p.Product_Manufacturer = (tType[3].ToString());
                p.Product_Name = tType[4].ToString();
                p.Product_Description = (tType[5].ToString());
                p.Product_Price = int.Parse(tType[6].ToString());
                p.Product_Stock = int.Parse((tType[7].ToString()));
                PC.Add(p);

            }
            return (Products[])PC.ToArray(typeof(Products));
        }


        public Suppliers[] GetSupplierData()//פונקציה לאחסון נתוני ספקים
        {
            DataSet ds = new DataSet();
            ArrayList Suppliers = new ArrayList();
            string cmdStr = "SELECT * FROM   [Suppliers]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                Suppliers supplier = new Suppliers();
                supplier.Supplier_ID = int.Parse(tType[0].ToString());
                supplier.Supplier_Name = tType[1].ToString();
                supplier.Supplier_Telephone = tType[2].ToString();


                Suppliers.Add(supplier);
            }
            return (Suppliers[])Suppliers.ToArray(typeof(Suppliers));
        }


        public CustomersOrdersDetail[] GetOrderDetailData() //פונקציה לאחסון  פירוט הזמנה של לקוח 
        {
            DataSet ds = new DataSet();
            ArrayList OrderDetail = new ArrayList();
            string cmdStr = "SELECT * FROM   [CustomersOrdersDetail]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                CustomersOrdersDetail CusOrder = new CustomersOrdersDetail();
                CusOrder.CustomerOrder_ID = int.Parse(tType[0].ToString());
                CusOrder.CustomerOrder_ClientID = int.Parse(tType[1].ToString());
                CusOrder.CustomerOrder_ClientName = tType[2].ToString();
                CusOrder.CustomerOrder_ProductID = int.Parse(tType[3].ToString());
                CusOrder.CustomerOrder_ProductDescription = tType[4].ToString();
                CusOrder.CustomerOrder_Quantity = int.Parse(tType[5].ToString());
                CusOrder.CustomerOrder_ProductReceived = bool.Parse(tType[6].ToString());


                OrderDetail.Add(CusOrder);
            }
            return (CustomersOrdersDetail[])OrderDetail.ToArray(typeof(CustomersOrdersDetail));
        }

        public CustomersOrders[] GetOrderData() //פונקציה לאחסון  רשימת הזמנות של כל הלקוחות
        {
            DataSet ds = new DataSet();
            ArrayList Order = new ArrayList();
            string cmdStr = "SELECT * FROM   [CustomersOrders]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                CustomersOrders CusOrder = new CustomersOrders();
                CusOrder.CustomersOrders_ID = int.Parse(tType[0].ToString());
                CusOrder.CustomersOrders_ClientID = int.Parse(tType[1].ToString());
                CusOrder.CustomersOrders_ClientName = tType[2].ToString();
                CusOrder.CustomersOrders_OrderSuppliedToCustomer = bool.Parse(tType[3].ToString());
                CusOrder.CustomersOrders_Date = DateTime.Parse(tType[4].ToString());
                CusOrder.CustomerOrders_Price = double.Parse(tType[5].ToString());
                CusOrder.CustomersOrders_WorkerApproved= tType[6].ToString();


                Order.Add(CusOrder);
            }
            return (CustomersOrders[])Order.ToArray(typeof(CustomersOrders));
        }

        public Archive[] GetArchiveOrderData() //פונקציה לאחסון  הזמנות מהארכיון  
        {
            DataSet ds = new DataSet();
            ArrayList Archive = new ArrayList();
            string cmdStr = "SELECT * FROM   [Archive]";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultiplyQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            foreach (DataRow tType in dt.Rows)
            {
                Archive ArchiveCusOrder = new Archive();
                ArchiveCusOrder.ArchiveCustomersOrder_Date = DateTime.Parse(tType[0].ToString());
                ArchiveCusOrder.ArchiveCustomerOrder_ID = int.Parse(tType[1].ToString());
                ArchiveCusOrder.ArchiveCustomerOrder_ClientID = int.Parse(tType[2].ToString());
                ArchiveCusOrder.ArchiveCustomerOrder_ClientName = tType[3].ToString();
                ArchiveCusOrder.ArchiveCustomerOrder_ProductID = int.Parse(tType[4].ToString());
                ArchiveCusOrder.ArchiveCustomerOrder_ProductDescription = tType[5].ToString();
                ArchiveCusOrder.ArchiveCustomerOrder_Quantity = int.Parse(tType[6].ToString());
                ArchiveCusOrder.ArchiveCustomerOrder_Price = int.Parse(tType[7].ToString());

                Archive.Add(ArchiveCusOrder);
            }
            return (Archive[])Archive.ToArray(typeof(Archive));
        }

        public void AddCustomer(Customers customer)//פונקציה להוספת לקוח למאגר הלקוחות
        {
            string cmdStr = "INSERT INTO Customers (ID,First_Name,Last_Name,Address,Telephone,Email) VALUES (@ID,@First_Name,@Last_Name,@Address,@Telephone,@Email)";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@ID", customer.Customer_ID);
                command.Parameters.AddWithValue("@First_Name", customer.Customer_FirstName);
                command.Parameters.AddWithValue("@Last_Name", customer.Customer_LastName);
                command.Parameters.AddWithValue("@Address", customer.Customer_Address);
                command.Parameters.AddWithValue("@Telephone", customer.Customer_Telephone);
                command.Parameters.AddWithValue("@Email", customer.Customer_Email);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateCustomerFirstName(Customers customer)//פונקציה לעדכון שם פרטי של  לקוח
        {
            string cmdStr = "UPDATE Customers SET First_Name=@First_Name WHERE ID=" + customer.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@First_Name", customer.Customer_FirstName);
                base.ExecuteSimpleQuery(command);
            }
        }
        public void UpdateCustomerLastName(Customers customer)//פונקציה לעדכון שם משפחה של  לקוח
        {
            string cmdStr = "UPDATE Customers SET Last_Name=@Last_Name WHERE ID=" + customer.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Last_Name", customer.Customer_LastName);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateCustomerAddress(Customers customer)//פונקציה לעדכון כתובת של  לקוח
        {
            string cmdStr = "UPDATE Customers SET Address=@Address WHERE ID=" + customer.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Address", customer.Customer_Address);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateCustomerTelephone(Customers customer)//פונקציה לעדכון טלפון של  לקוח
        {
            string cmdStr = "UPDATE Customers SET Telephone=@Telephone WHERE ID=" + customer.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Telephone", customer.Customer_Telephone);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateCustomerEmail(Customers customer)//פונקציה לעדכון כתובת מייל של  לקוח
        {
            string cmdStr = "UPDATE Customers SET Email=@Email WHERE ID=" + customer.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Email", customer.Customer_Email);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateProductName(Products product)//פונקציה לעדכון שם של  מוצר
        {
            string cmdStr = "UPDATE Products SET Product_Name=@Product_Name WHERE Product_ID=" + product.Proudct_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Product_Name", product.Product_Name);
                base.ExecuteSimpleQuery(command);
            }
        }


        public void UpdateProductDescription(Products product)//פונקציה לעדכון תיאור של  מוצר
        {
            string cmdStr = "UPDATE Products SET Description=@Description WHERE Product_ID=" + product.Proudct_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Description", product.Product_Description);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateProductPrice(Products product)//פונקציה לעדכון מחיר של  מוצר
        {
            string cmdStr = "UPDATE Products SET Price=@Price WHERE Product_ID=" + product.Proudct_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Price", product.Product_Price);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateProductStock(Products product)//פונקציה לעדכון מלאי של  מוצר
        {
            string cmdStr = "UPDATE Products SET In_Stock=@In_Stock WHERE Product_ID=" + product.Proudct_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@In_Stock", product.Product_Stock);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateSupplierName(Suppliers supplier)//פונקציה לעדכון שם של  ספק
        {
            string cmdStr = "UPDATE Suppliers SET Supplier_Name=@Supplier_Name WHERE ID=" + supplier.Supplier_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Supplier_Name", supplier.Supplier_Name);
                base.ExecuteSimpleQuery(command);
            }
        }


        public void UpdateSupplierPhone(Suppliers supplier)//פונקציה לעדכון טלפון של  ספק
        {
            string cmdStr = "UPDATE Suppliers SET Telephone=@Telephone WHERE ID=" + supplier.Supplier_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Telephone", supplier.Supplier_Telephone);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void AddCustomerOrderDetail(CustomersOrdersDetail CusOrder)//  פונקציה להוספת הזמנה למאגר ההזמנות המפורט
        {
            string cmdStr = "INSERT INTO CustomersOrdersDetail (OrderID,CustomerID,CustomerName,ProductID,ProductDescription,Quantity) VALUES (@OrderID,@CustomerID,@CustomerName,@ProductID,@ProductDescription,@Quantity)";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@OrderID", CusOrder.CustomerOrder_ID);
                command.Parameters.AddWithValue("@CustomerID", CusOrder.CustomerOrder_ClientID);
                command.Parameters.AddWithValue("@CustomerName", CusOrder.CustomerOrder_ClientName);
                command.Parameters.AddWithValue("@ProductID", CusOrder.CustomerOrder_ProductID);
                command.Parameters.AddWithValue("@ProductDescription", CusOrder.CustomerOrder_ProductDescription);
                command.Parameters.AddWithValue("@Quantity", CusOrder.CustomerOrder_Quantity);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void AddCustomerOrder(CustomersOrders CusOrder)//פונקציה להוספת הזמנה למאגר ההזמנות
        {
            string cmdStr = "INSERT INTO CustomersOrders (OrderID,CustomerID,CustomerName,Order_Supplied_To_Customer,OrderDate,OrderPrice,WorkerApproved) VALUES (@OrderID,@CustomerID,@CustomerName,@Order_Supplied_To_Customer,@OrderDate,@OrderPrice,@WorkerApproved)";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@OrderID", CusOrder.CustomersOrders_ID);
                command.Parameters.AddWithValue("@CustomerID", CusOrder.CustomersOrders_ClientID);
                command.Parameters.AddWithValue("@CustomerName", CusOrder.CustomersOrders_ClientName);
                command.Parameters.AddWithValue("@Order_Supplied_To_Customer", CusOrder.CustomersOrders_OrderSuppliedToCustomer);
                command.Parameters.AddWithValue("@OrderDate", CusOrder.CustomersOrders_Date);
                command.Parameters.AddWithValue("@OrderPrice", CusOrder.CustomerOrders_Price);
                command.Parameters.AddWithValue("@WorkerApproved", CusOrder.CustomersOrders_WorkerApproved);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void AddProduct(Products product)//פונקציה להוספת מוצר למאגר המוצרים
        {
            string cmdStr = "INSERT INTO Products (Product_ID,Product_Name,Product_Type,Manufacturer,Supplier_Identifier,Description,Price,In_Stock) VALUES (@Product_ID,@Product_Name,@Product_Type,@Manufacturer,@Supplier_Identifier,@Description,@Price,@In_Stock)";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Product_ID", product.Proudct_ID);
                command.Parameters.AddWithValue("@Product_Name", product.Product_Name);
                command.Parameters.AddWithValue("@Product_Type", product.Product_Type);
                command.Parameters.AddWithValue("@Manufacturer", product.Product_Manufacturer);
                command.Parameters.AddWithValue("@Supplier_Identifier", product.Product_SupplierIdentifier);
                command.Parameters.AddWithValue("@Description", product.Product_Description);
                command.Parameters.AddWithValue("@Price", product.Product_Price);
                command.Parameters.AddWithValue("@In_Stock", product.Product_Stock);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void RemoveProduct(Products product)//פונקציה למחיקת פרטי מוצר
        {
            string cmdStr = "DELETE  FROM Products WHERE Product_ID = @Product_ID";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Product_ID", product.Proudct_ID);
                command.Parameters.AddWithValue("@Product_Name", product.Product_Name);
                command.Parameters.AddWithValue("@Product_Type", product.Product_Type);
                command.Parameters.AddWithValue("@Product_Manufacturer", product.Product_Manufacturer);
                command.Parameters.AddWithValue("@Supplier_Identifier", product.Product_SupplierIdentifier);
                command.Parameters.AddWithValue("@Description", product.Product_Description);
                command.Parameters.AddWithValue("@Price", product.Product_Price);
                command.Parameters.AddWithValue("@In_Stock", product.Product_Stock);

                base.ExecuteSimpleQuery(command);
            }
        }

        public void AddSupplier(Suppliers supplier)//פונקציה להוספת ספק למאגר הספקים
        {
            string cmdStr = "INSERT INTO Suppliers (ID,Supplier_Name,Telephone) VALUES (@ID,@Supplier_Name,@Telephone)";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@ID", supplier.Supplier_ID);
                command.Parameters.AddWithValue("@Supplier_Name", supplier.Supplier_Name);
                command.Parameters.AddWithValue("@Telephone", supplier.Supplier_Telephone);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void RemoveSupplier(Suppliers supplier)//פונקציה למחיקת פרטי ספק
        {
            string cmdStr = "DELETE  FROM Suppliers WHERE ID = @ID";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@ID", supplier.Supplier_ID);
                command.Parameters.AddWithValue("@Supplier_Name", supplier.Supplier_Name);
                command.Parameters.AddWithValue("@Telephone", supplier.Supplier_Telephone);
                base.ExecuteSimpleQuery(command);
            }
        }

    

  
        public string GetCustomerNameByID(Customers cus)//פונקציה שמחזירה את שם הלקוח לפי הת.ז שלו 
        {
            OleDbConnection connection = new OleDbConnection();
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            DBSQL dataB = new DBSQL(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            Customers[] customer = dataB.GetCustomerData();
            int CustomerLength = customer.Length;//שומר את אורך רשימת הלקוחות
            if (CustomerLength > 0)//אם אורך רשימת הלקוחות גדול מ0
            {
                for (int i = 0; i < customer.Length; i++)
                {

                    if (customer[i].Customer_ID.Equals(cus.Customer_ID)) // בדיקה שהת.ז שבטבלת לקוחות תואמת לת.ז שקיבלנו
                    {
                        return customer[i].Customer_FirstName + " " + customer[i].Customer_LastName;
                    }

                }
            }
            return "Customer Not Found"; // אם אין לקוח כזה 
        }

        public int GetSupplierIDByProductID(Products prod)//פונקציה שמחזירה את המספר הסידורי של הספק לפי ההזמנות שהוא מספק 
        {
            OleDbConnection connection = new OleDbConnection();
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            DBSQL dataB = new DBSQL(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            Suppliers[] supplier = dataB.GetSupplierData();

            int SupplierLength = supplier.Length;//שומר את אורך רשימת הספקים
            if (SupplierLength > 0)//אם אורך רשימת הספקים גדול מ0
            {
                for (int i = 0; i < supplier.Length; i++)
                {
                    if (prod.Product_SupplierIdentifier.Equals(supplier[i].Supplier_ID))
                        return supplier[i].Supplier_ID;
                }
            }
            return 0; // אם אין ספק כזה 
        }

        public string GetSupplierNameByProductID(Products prod)//פונקציה שמחזירה את השם של הספק לפי ההזמנות שהוא מספק 
        {
            OleDbConnection connection = new OleDbConnection();
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            DBSQL dataB = new DBSQL(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            Suppliers[] supplier = dataB.GetSupplierData();

            int SupplierLength = supplier.Length;//שומר את אורך רשימת הספקים
            if (SupplierLength > 0)//אם אורך רשימת הספקים גדול מ0
            {
                for (int i = 0; i < supplier.Length; i++)
                {
                    if (prod.Product_SupplierIdentifier.Equals(supplier[i].Supplier_ID))
                        return supplier[i].Supplier_Name;


                }
            }
            return "Supplier Not Found"; // אם אין ספק כזה 
        }


        public string GetProductDescriptionByProductID(Products prod)//פונקציה שמחזירה את תיאור המוצר  לפי ה מס' מזהה שלו 
        {
            OleDbConnection connection = new OleDbConnection();
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            DBSQL dataB = new DBSQL(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            Products[] product = dataB.GetProductsData();
            int ProductLength = product.Length;//שומר את אורך רשימת המוצרים
            if (ProductLength > 0)//אם אורך רשימת המוצרים גדול מ0
            {
                for (int i = 0; i < product.Length; i++)
                {

                    if (product[i].Proudct_ID.Equals(prod.Proudct_ID)) // בדיקה שהמס' מזהה  שבטבלת מוצרים תואמת למס' מזהה שקיבלנו
                    {
                        return product[i].Product_Description;
                    }

                }
            }
            return "Product Not Found"; // אם אין מוצר כזה 
        }


        public void SubProductStock(Products product, CustomersOrdersDetail c)//פונקציה להורדת מלאי של מוצר ספציפי  לאחר ביצוע הזמנה 
        {
            string cmdStr = "UPDATE Products SET In_Stock=" + product.Product_Stock + " WHERE Product_ID=" + c.CustomerOrder_ProductID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Stock", product.Product_Stock);
                base.ExecuteSimpleQuery(command);
            }
        }


        public void UpdateCustomerNameInCustomerOrders(Customers cus, CustomersOrders cusorder)//פונקציה לעדכון שם של  לקוח בטבלת סיכום הזמנות במידה והלקוח שינה את שמו
        {
            string cmdStr = "UPDATE CustomersOrders SET CustomerName=" + cusorder.CustomersOrders_ClientName + " WHERE CustomerID=" + cus.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@First_Name", cusorder.CustomersOrders_ClientName);
                base.ExecuteSimpleQuery(command);
            }
        }


        public void UpdateCustomerNameInCustomersOrdersDetail(Customers cus, CustomersOrdersDetail cusorder)//פונקציה לעדכון שם של  לקוח בטבלת  הזמנות  מפורטות במידה והלקוח שינה את שמו
        {
            string cmdStr = "UPDATE CustomersOrdersDetail SET CustomerName =" + cusorder.CustomerOrder_ClientName + " WHERE CustomerID=" + cus.Customer_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@First_Name", cusorder.CustomerOrder_ClientName);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void RemoveCustomersOrder(CustomersOrders CusOrder)//פונקציה למחיקת פרטי הזמנה בטבלת סיכום הזמנות
        {
            string cmdStr = "DELETE  FROM CustomersOrders WHERE OrderID = @OrderID";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@OrderID", CusOrder.CustomersOrders_ID);
                command.Parameters.AddWithValue("@CustomerID", CusOrder.CustomersOrders_ClientID);
                command.Parameters.AddWithValue("@CustomerName", CusOrder.CustomersOrders_ClientName);

                base.ExecuteSimpleQuery(command);
            }
        }

     

     
        public void RemoveCustomersOrderDetail(CustomersOrdersDetail CusOrderDet)//פונקציה למחיקת פרטי הזמנה בטבלת  הזמנות מפורטות
        {
            string cmdStr = "DELETE  FROM CustomersOrdersDetail WHERE OrderID = @OrderID";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@OrderID", CusOrderDet.CustomerOrder_ID);
                command.Parameters.AddWithValue("@CustomerID", CusOrderDet.CustomerOrder_ClientID);
                command.Parameters.AddWithValue("@CustomerName", CusOrderDet.CustomerOrder_ClientName);
                command.Parameters.AddWithValue("@ProductID", CusOrderDet.CustomerOrder_ProductID);
                command.Parameters.AddWithValue("@ProductDescription", CusOrderDet.CustomerOrder_ProductDescription);
                command.Parameters.AddWithValue("@Quantity", CusOrderDet.CustomerOrder_Quantity);

                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateDGVCustomer(DataGridView dgvUpdateCustomer) // עדכון לקוחות  מיידי DGV
        {
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Customers", connection);
            DataSet set = new DataSet();
            ada.Fill(set, "Customers");
            DataTable tab = new DataTable();
            tab = set.Tables["Customers"];
            dgvUpdateCustomer.DataSource = tab;
        }

        public void AddWorker(Workers worker)//פונקציה להוספת עובד למאגר העובדים
        {
            string cmdStr = "INSERT INTO Workers (ID,WorkerName,UserName,[Password]) VALUES (@ID,@WorkerName,@UserName,@Password)";

            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@ID", worker.Worker_ID);
                command.Parameters.AddWithValue("@WorkerName", worker.Worker_Name);
                command.Parameters.AddWithValue("@UserName", worker.Worker_UserName);
                command.Parameters.AddWithValue("@Password", worker.Worker_Password);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void RemoveWorker(Workers worker)// פונקציה למחיקת עובד    
        {
            string cmdStr = "DELETE  FROM Workers WHERE ID = @ID";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@ID", worker.Worker_ID);
                command.Parameters.AddWithValue("@WorkerName", worker.Worker_Name);
                command.Parameters.AddWithValue("@UserName", worker.Worker_UserName);
                command.Parameters.AddWithValue("@Password", worker.Worker_Password);
                base.ExecuteSimpleQuery(command);

            }
        }

        public void UpdateWorkerName(Workers worker)//פונקציה לעדכון שם פרטי של עובד
        {
            string cmdStr = "UPDATE Workers SET WorkerName=@WorkerName WHERE ID=" + worker.Worker_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@WorkerName", worker.Worker_Name);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateWorkerPassword(Workers worker)//פונקציה לעדכון סיסמא של עובד
        {
            string cmdStr = "UPDATE Workers SET [Password]=@Password WHERE ID=" + worker.Worker_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Password", worker.Worker_Password);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateWorkerToManger(Workers worker)//פונקציה שהופכת את העובד למנהל הסניף
        {
            string cmdStr = "UPDATE Workers SET Manager=@Manager WHERE ID=" + worker.Worker_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Manager", worker.Manager_IsAManager);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateOrderReceivedInCustomersOrdersDetail(CustomersOrdersDetail cusorderdet)//פונקציה שמעדכנת שהלקוח קיבל את הזמנתו בטבלת הזמנות מפורטות 
        {
            string cmdStr = "UPDATE CustomersOrdersDetail SET ProductReceived=@ProductReceived WHERE OrderID=" + cusorderdet.CustomerOrder_ID + "AND ProductID=" + cusorderdet.CustomerOrder_ProductID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@ProductReceived", cusorderdet.CustomerOrder_ProductReceived);
                base.ExecuteSimpleQuery(command);
            }
        }

        public void UpdateOrderReceivedInCustomersOrders(CustomersOrders cusorder)//פונקציה שמעדכנת שהלקוח קיבל את הזמנתו בטבלת הזמנות כלליות 
        {
            string cmdStr = "UPDATE CustomersOrders SET Order_Supplied_To_Customer=@Order_Supplied_To_Customer WHERE OrderID=" + cusorder.CustomersOrders_ID;
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Order_Supplied_To_Customer", cusorder.CustomersOrders_OrderSuppliedToCustomer);
                base.ExecuteSimpleQuery(command);
            }
        }

        public bool isNumber(string num)//פונקציה לבדיקת קלט מורכב מספרות
        {
            int i;
            int count = 0;
            for (i = 0; i < num.Length; i++)
                if (num[i] >= '0' && num[i] <= '9')
                    count++;

            if (count == num.Length)
                return true;
            return false;
        }


        public bool isLetter(string name)//פונקציה לבדיקת קלט מורכב מאותיות
        {
            int flag = 0;
            for (int j = 0; j < name.Length; j++)
                if (!(Char.IsLetter(name[j])) && name[j] != ' ')
                    flag = 1;
            if (flag == 0)
                return true;
            return false;
        }

        public bool IsValidEmail(string email) // פונקציה לבדיקת תקינות של אימייל 
        {
            try
           {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;

            }
            catch
            {
                return false;
            }

        }

        public bool CheckLeadingZero(string number)// פונקציה שבודקת האם המשתמש הזין 0 בתחילת מספר 
        {
            if (number[0] == '0') // בדיקה האם המשתמש הזין 0 בתחילת המספר
                return true;
            return false;
        }

        public DateTime GetDateWithoutMilliseconds(DateTime date) // פונקציה לקבלת התאריך הנוכחי 
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

       
        }
    }
//שמות תכנתים: לירון חזן  וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;
using System.IO;

namespace Store
{
    public partial class frmPcStore : Form
    {

        private OleDbConnection connection = new OleDbConnection(); // משתנה להתחברות לבסיס נתונים 
        private static string PathDB = Application.StartupPath + @"\PcStore.ACCDB"; // משתנה שמכיל את המיקום של קובץ הבסיס נתונים שלנו
        private DBSQL dataB = new DBSQL(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;"); // משתנה שמקושר לכל הפעולות שניתן לעשות בבסיס הנתונים 
        private System.Media.SoundPlayer player = new SoundPlayer(); // משתנה שאחראי לשמע 
        DataTable tabSearchProduct; // משתנה שאחראי על שורת חיפוש מוצר לפי מזהה המוצר 
        DataTable tabSearchSupplier; // משתנה שאחראי על שורת חיפוש ספק  לפי מזהה ספק 
        DataTable tabSearchCustomer; // משתנה שאחראי על שורת חיפוש לקוח לפי ת"ז 
        DataTable tabSearchCustomerOrder; // משתנה שאחראי על שורת חיפוש הזמנות של לקוח לפי ת"ז של הלקוח 
        DataTable tabSearchCustomerOrder1; // משתנה שאחראי על שורת חיפוש הזמנות של לקוח לפי ת"ז של הלקוח 
        DataTable tabSearchWorker; // משתנה שאחראי על שורת חיפוש עובד  לפי ת"ז של העובד 
        public frmPcStore()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }



        public void btnMenu_Click(Object sender, EventArgs e) // תפריט  
        {
            Point point = new Point(10, 75);
            btnPopUpMenu.Visible = true;
            btnPopUpMenu.Show(point);
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e) // מחיקת מוצר  
        {
            if (MessageBox.Show("  Are You Sure You Want To Remove The Product ?  ", "Remove Product", MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            {

                Products p = new Products();
                int deleteflag = 0; // משתנה עזר למחיקת מוצר 
                Products[] products = dataB.GetProductsData();
                Products[] products1 = dataB.GetProductsData(); // משתנה עזר לאחר מחיקת מוצר 
                string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB"; // משתנה שמכיל את המיקום של קובץ הבסיס נתונים שלנו
                OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;"); // משתנה להתחברות לבסיס נתונים 

                if (dgvProducts.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvProducts.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dgvProducts.Rows[selectedrowindex];

                    if (Convert.ToString(selectedRow.Cells["Product_ID"].Value) == "") // בדיקה שהשורה ב DGV לא ריקה
                    {
                        MessageBox.Show("Please Choose A Product", "Error");
                        return;
                    }

                    string value = Convert.ToString(selectedRow.Cells["Product_ID"].Value);
                    p.Proudct_ID = int.Parse(value);


                    // עדכון קומבו בוקס מתאים בלשונית הזמנות  לאחר מחיקת מוצר

                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i].Proudct_ID.Equals(int.Parse(value))) // מציאת מזהה המוצר המתאים 
                        {
                            if (deleteflag == 0 && products[i].Product_Type.Equals("RAM")) // אם המוצר שמחקנו הוא מסוג ראם
                            {
                                dataB.RemoveProduct(p);
                                MessageBox.Show("Product Removed", "Success");
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command1 = new OleDbCommand();
                                connection1.Open();
                                command1.Connection = connection1;
                                string query1 = "select * from Products";
                                command1.CommandText = query1;
                                cboxCustomerProductRAM.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("RAM"))
                                        cboxCustomerProductRAM.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                                connection1.Close();

                                cboxCustomerProductRAM.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                                deleteflag = 1;
                            }

                            if (products[i].Product_Type.Equals("CPU") && deleteflag == 0) // אם המוצר שמחקנו הוא מסוג מעבד
                            {
                                dataB.RemoveProduct(p);
                                MessageBox.Show("Product Removed", "Success");
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command1 = new OleDbCommand();
                                connection1.Open();
                                command1.Connection = connection1;
                                string query1 = "select * from Products ";
                                command1.CommandText = query1;
                                cboxCustomerProductCPU.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("CPU"))
                                        cboxCustomerProductCPU.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                                connection1.Close();

                                cboxCustomerProductCPU.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                                deleteflag = 1;
                            }

                            if (deleteflag == 0 && products[i].Product_Type.Equals("GPU")) // אם המוצר שמחקנו הוא מסוג כרטיס מסך
                            {
                                dataB.RemoveProduct(p);
                                MessageBox.Show("Product Removed", "Success");
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command1 = new OleDbCommand();
                                connection1.Open();
                                command1.Connection = connection1;
                                string query1 = "select * from Products ";
                                command1.CommandText = query1;
                                cboxCustomerProductGPU.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("GPU"))
                                        cboxCustomerProductGPU.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם


                                connection1.Close();

                                cboxCustomerProductGPU.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                                deleteflag = 1;
                            }

                            if (deleteflag == 0 && products[i].Product_Type.Equals("MB")) // אם המוצר שמחקנו הוא מסוג לוח אם
                            {

                                dataB.RemoveProduct(p);
                                MessageBox.Show("Product Removed", "Success");
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command1 = new OleDbCommand();
                                connection1.Open();
                                command1.Connection = connection1;
                                string query1 = "select * from Products ";
                                command1.CommandText = query1;
                                cboxCustomerProducMotherBoard.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("MB"))
                                        cboxCustomerProducMotherBoard.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם


                                connection1.Close();

                                cboxCustomerProducMotherBoard.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                            }
                        }
                    }    


                }

                // רענון טבלת מוצרים בלשונית מוצר לאחר מחיקת מוצר
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Products", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "Products");
                DataTable tab = new DataTable();
                tab = set.Tables["Products"];
                dgvProducts.DataSource = tab;
                dgvProducts.Sort(dgvProducts.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // רענון טבלת מוצרים בלשונית הזמנות לאחר מחיקת מוצר
                string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection2);
                DataSet set1 = new DataSet();
                ada1.Fill(set1, "Products");
                DataTable tabSearchProduct = new DataTable();
                tabSearchProduct = set1.Tables["Products"];
                dgvProductOrder.DataSource = tab;
                dgvProductOrder.Sort(dgvProductOrder.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
                dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // עדכון קומבו בוקס לאחר מחיקת מוצר

                OleDbCommand command = new OleDbCommand();
                connection.Open();
                command.Connection = connection;
                string query = "select * from Products";
                command.CommandText = query;
                cboxProductList.Items.Clear();
                OleDbDataReader reader1 = command.ExecuteReader();
                while (reader1.Read())
                {
                    cboxProductList.Items.Add(reader1["PRODUCT_ID"].ToString()); // הוספת נתונים לקומבו בוקס

                }

                connection.Close();

                cboxProductList.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 

            }





        }

        private void btnRemoveSupplier_Click(object sender, EventArgs e) // מחיקת ספק
        {
            if (MessageBox.Show(" Are You Sure You Want To Remove This Supplier ?  ", "Remove Supplier", MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            {
                Suppliers s = new Suppliers();
                Products[] products = dataB.GetProductsData();
                Products[] products1 = dataB.GetProductsData();
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");


                if (dgvSuppliers.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvSuppliers.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dgvSuppliers.Rows[selectedrowindex];

                    if (Convert.ToString(selectedRow.Cells["ID"].Value) == "") // בדיקה שהשורה ב DGV לא ריקה
                    {
                        MessageBox.Show("Please Choose A Supplier", "Error");
                        return;
                    }

                    string value = Convert.ToString(selectedRow.Cells["ID"].Value);
                    s.Supplier_ID = int.Parse(value);
                    dataB.RemoveSupplier(s);
                    MessageBox.Show("Supplier Removed", "Success");

                    // מחיקת מוצר שנגמר במלאי במקרה שהספק שמחקנו מספק את המוצר 

                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i].Product_SupplierIdentifier.Equals(int.Parse(value)) && products[i].Product_Stock == 0)
                        {
                            dataB.RemoveProduct(products[i]);

                            if (products[i].Product_Type.Equals("RAM")) // אם המוצר שמחקנו הוא מסוג ראם
                            {
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command2 = new OleDbCommand();
                                connection.Open();
                                command2.Connection = connection;
                                string query2 = "select * from Products ";
                                command2.CommandText = query2;
                                cboxCustomerProductRAM.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("RAM"))
                                        cboxCustomerProductRAM.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                                connection.Close();

                                cboxCustomerProductRAM.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                            }

                            if (products[i].Product_Type.Equals("CPU")) // אם המוצר שמחקנו הוא מסוג מעבד
                            {
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command2 = new OleDbCommand();
                                connection.Open();
                                command2.Connection = connection;
                                string query2 = "select * from Products ";
                                command2.CommandText = query2;
                                cboxCustomerProductCPU.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("CPU"))
                                        cboxCustomerProductCPU.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                                connection.Close();

                                cboxCustomerProductCPU.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                            }

                            if (products[i].Product_Type.Equals("GPU")) // אם המוצר שמחקנו הוא מסוג כרטיס מסך
                            {
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command2 = new OleDbCommand();
                                connection.Open();
                                command2.Connection = connection;
                                string query2 = "select * from Products ";
                                command2.CommandText = query2;
                                cboxCustomerProductGPU.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("CPU"))
                                        cboxCustomerProductGPU.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                                connection.Close();

                                cboxCustomerProductGPU.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                            }

                            if (products[i].Product_Type.Equals("MB")) // אם המוצר שמחקנו הוא מסוג לוח אם
                            {
                                products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר מחיקת מוצר 
                                OleDbCommand command2 = new OleDbCommand();
                                connection.Open();
                                command2.Connection = connection;
                                string query2 = "select * from Products ";
                                command2.CommandText = query2;
                                cboxCustomerProducMotherBoard.Items.Clear();

                                for (int j = 0; j < products1.Length; j++)
                                    if (products1[j].Product_Type.Equals("MB"))
                                        cboxCustomerProducMotherBoard.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                                connection.Close();

                                cboxCustomerProducMotherBoard.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                            }
                        }
                    }

                }

                // רענון טבלה לאחר מחיקת ספק
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Suppliers", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "Supplier");
                DataTable tab = new DataTable();
                tab = set.Tables["Supplier"];
                dgvSuppliers.DataSource = tab;
                dgvSuppliers.Sort(dgvSuppliers.Columns["ID"], ListSortDirection.Ascending);
                dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                // עדכון קומבו בוקס לאחר מחיקת ספק

                OleDbCommand command = new OleDbCommand();
                connection.Open();
                command.Connection = connection;
                string query = "select * from Suppliers";
                command.CommandText = query;
                cboxSupplierList.Items.Clear();
                OleDbDataReader reader1 = command.ExecuteReader();
                while (reader1.Read())
                {
                    cboxSupplierList.Items.Add(reader1["ID"].ToString()); // הוספת נתונים לקומבו בוקס

                }

                connection.Close();

                cboxSupplierList.Sorted = true; // מיון קומבו בוקס לאחר מחיקת ספק 

                // עדכון קומבו בוקס  של ספק בלשונית מוצרים לאחר מחיקת ספק

                OleDbCommand command3 = new OleDbCommand();
                connection.Open();
                command3.Connection = connection;
                string query3 = "select * from Suppliers";
                command3.CommandText = query3;
                cboxAddProductSupplierIdentity.Items.Clear();
                OleDbDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    cboxAddProductSupplierIdentity.Items.Add(reader3["ID"].ToString()); // הוספת נתונים לקומבו בוקס

                }

                connection.Close();

                cboxAddProductSupplierIdentity.Sorted = true; // מיון קומבו בוקס לאחר מחיקת ספק 


                //  רענון טבלת מוצרים  לאחר מחיקת ספק בלשונית מוצרים 
                string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection);
                DataSet set1 = new DataSet();
                ada1.Fill(set1, "Products");
                DataTable tabSearchProduct = new DataTable();
                tabSearchProduct = set1.Tables["Products"];
                dgvProducts.DataSource = tabSearchProduct;
                dgvProducts.Sort(dgvProducts.Columns["Product_ID"], ListSortDirection.Ascending);
                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                //  רענון טבלת מוצרים  לאחר מחיקת ספק בלשונית הזמנות 
                string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;");
                OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM Products", connection);
                DataSet set2 = new DataSet();
                ada2.Fill(set2, "Products");
                DataTable tabSearchSupplier = new DataTable();
                tabSearchSupplier = set2.Tables["Products"];
                dgvProductOrder.DataSource = tabSearchSupplier;
                dgvProductOrder.Sort(dgvProductOrder.Columns["Product_ID"], ListSortDirection.Ascending);
                dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // עדכון קומבו בוקס  שבלשונית הזמנות לאחר מחיקת ספק

                OleDbCommand command1 = new OleDbCommand();
                connection.Open();
                command.Connection = connection;
                string query1 = "select * from Products";
                command.CommandText = query1;
                cboxProductList.Items.Clear();
                OleDbDataReader reader2 = command.ExecuteReader();
                while (reader2.Read())
                {
                    cboxProductList.Items.Add(reader2["Product_ID"].ToString()); // הוספת נתונים לקומבו בוקס

                }

                connection.Close();

                cboxSupplierList.Sorted = true; // מיון קומבו בוקס לאחר מחיקת ספק 

            }

        }

        private void btnFindCustomer_Click(object sender, EventArgs e) // חיפוש לקוח 
        {
            Customers[] customer = dataB.GetCustomerData();
            int flag = 0;
            int CustomerLength = customer.Length;//שומר את אורך רשימת הלקוחות
            if (CustomerLength > 0)//אם אורך רשימת הלקוחות גדול מ0
            {
                for (int i = 0; i < customer.Length; i++)
                {

                    if (txtboxSearchCustomer.Text != "" && dataB.isNumber(txtboxSearchCustomer.Text)) // בדיקת תקינות
                    {
                        if (int.Parse(txtboxSearchCustomer.Text).Equals(customer[i].Customer_ID)) // בדיקה שהלקוח שמנסה לחפש קיים במאגר הלקוחות
                        {
                            flag = 1; // יש לקוח כזה 
                            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                            connection.Open();
                            OleDbCommand cmd = connection.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM Customers WHERE ID=" + customer[i].Customer_ID;
                            cmd.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dt);
                            dgvCustomers.DataSource = dt;
                            connection.Close();
                            return;
                        }


                    }

                }
            }

            if (txtboxSearchCustomer.Text == "" || !dataB.isNumber(txtboxSearchCustomer.Text)) // בדיקת תקינות קלט
            {
                MessageBox.Show("Please Enter Correct Input", "Error");
                return;
            }

            if (flag == 0) // בדיקה אם קיים לקוח 
            {
                MessageBox.Show("No Such Customer", "Error");
                txtboxSearchCustomer.Text = "";
                return;
            }


        }

        private void frmPcStore_Load(object sender, EventArgs e) // טעינת הטופס הראשי
        {
            tabPcStore.TabPages.Remove(tabManager); // הסתרת לשונית מנהל לעובד חסר הרשאה
            frmLogIn.checkmanager = "kfir";
            frmLogIn.checkuser = "kfir";




            // טעינת רשימת לקוחות ל dgv
            string PathDB4 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection4 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB4 + ";Persist Security Info=False;");
            OleDbDataAdapter ada4 = new OleDbDataAdapter("SELECT * FROM Customers", connection4);
            DataSet set4 = new DataSet();
            ada4.Fill(set4, "Customers");
            tabSearchCustomer = new DataTable();
            tabSearchCustomer = set4.Tables["Customers"];
            dgvCustomers.DataSource = tabSearchCustomer;
            dgvCustomers.Sort(dgvCustomers.Columns["Last_Name"], ListSortDirection.Ascending);
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // טעינת רשימת לקוחות ל dgv של הזמנות לקוח

            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT ID,First_Name,Last_Name FROM Customers", connection);
            DataSet set = new DataSet();
            ada.Fill(set, "Customers");
            tabSearchCustomerOrder1 = set.Tables["Customers"];
            dgvCustomerOrder.DataSource = tabSearchCustomerOrder1;
            dgvCustomerOrder.Sort(dgvCustomerOrder.Columns["Last_Name"], ListSortDirection.Ascending);
            dgvCustomerOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // טעינת רשימת הזמנות של לקוח ל dgv 

            string PathDB6 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection6 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB6 + ";Persist Security Info=False;");
            OleDbDataAdapter ada6 = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail", connection6);
            DataSet set6 = new DataSet();
            ada6.Fill(set6, "CustomersOrdersDetail");
            tabSearchCustomerOrder = new DataTable();
            tabSearchCustomerOrder = set6.Tables["CustomersOrdersDetail"];
            dgvCustomerOrderDetail.DataSource = tabSearchCustomerOrder;
            dgvCustomerOrderDetail.Sort(dgvCustomerOrderDetail.Columns["OrderID"], ListSortDirection.Ascending);
            dgvCustomerOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // טעינת רשימת מוצרים ל dgv של הזמנות לקוח

            string PathDB3 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection3 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB3 + ";Persist Security Info=False;");
            OleDbDataAdapter ada3 = new OleDbDataAdapter("SELECT * FROM Products", connection3);
            DataSet set3 = new DataSet();
            ada3.Fill(set3, "Products");
            DataTable tab3 = new DataTable();
            tab3 = set3.Tables["Products"];
            dgvProductOrder.DataSource = tab3;
            dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvProductOrder.Sort(dgvProductOrder.Columns["Product_ID"], ListSortDirection.Ascending);

            // טעינת רשימת הזמנות מוצרים מהספק  ל dgv
            string PathDB8 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection8 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB8 + ";Persist Security Info=False;");
            OleDbDataAdapter ada8 = new OleDbDataAdapter("SELECT * FROM SuppliersOrders", connection8);
            //  OleDbDataAdapter ada8 = new OleDbDataAdapter("SELECT ID,Supplier_Name,Supply_ProductID,Customer_ID,Supply_Quantity FROM  SuppliersOrders", connection8);
            DataSet set8 = new DataSet();
            DataTable tab8 = new DataTable();
            ada8.Fill(set8, "SuppliersOrders");
            tab8 = set8.Tables["SuppliersOrders"];
            dgvSupplierOrderDetail.DataSource = tab8;
            dgvSupplierOrderDetail.Sort(dgvSupplierOrderDetail.Columns["Supplier_ID"], ListSortDirection.Ascending);
            dgvSupplierOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSupplierOrderDetail.Columns["Supply_ProductID"].HeaderText = "PID";
            dgvSupplierOrderDetail.Columns["Supply_Quantity"].HeaderText = "Quantity";


            // טעינת רשימת מוצרים ל dgv

            string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
            OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection1);
            DataSet set1 = new DataSet();
            ada1.Fill(set1, "Products");
            tabSearchProduct = new DataTable();
            tabSearchProduct = set1.Tables["Products"];
            dgvProducts.DataSource = tabSearchProduct;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvProducts.Sort(dgvProducts.Columns["Product_ID"], ListSortDirection.Ascending);

            // טעינת רשימת ספקים ל dgv

            string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;");
            OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM Suppliers", connection2);
            DataSet set2 = new DataSet();
            ada2.Fill(set2, "Suppliers");
            tabSearchSupplier = new DataTable();
            tabSearchSupplier = set2.Tables["Suppliers"];
            dgvSuppliers.DataSource = tabSearchSupplier;
            dgvSuppliers.Sort(dgvSuppliers.Columns["ID"], ListSortDirection.Ascending);
            dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // טעינת תעודות זהות לקומבו בוקס של לקוחות 
            OleDbCommand command = new OleDbCommand();
            connection.Open();
            command.Connection = connection;
            string query = "select * from Customers";
            command.CommandText = query;

            OleDbDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                cboxCustomerOrder.Items.Add(reader1["ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection.Close();

            // טעינת תעודות זהות לקומבו בוקס של ספקים 
            OleDbCommand command13 = new OleDbCommand();
            connection.Open();
            command13.Connection = connection;
            string query13 = "select * from Suppliers";
            command13.CommandText = query13;

            OleDbDataReader reader13 = command13.ExecuteReader();
            while (reader13.Read())
            {
                cboxSupplierList.Items.Add(reader13["ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }
            connection.Close();

            // טעינת מזהה מוצר לקומבו בוקס של מוצרים 
            OleDbCommand command14 = new OleDbCommand();
            connection.Open();
            command14.Connection = connection;
            string query14 = "select * from Products";
            command14.CommandText = query14;

            OleDbDataReader reader14 = command14.ExecuteReader();
            while (reader14.Read())
            {
                cboxProductList.Items.Add(reader14["PRODUCT_ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }
            connection.Close();
            cboxProductList.Sorted = true;


            // טעינת ערכים בהתאמה לקומבו בוקס
            OleDbCommand command1 = new OleDbCommand();
            connection1.Open();
            command.Connection = connection1;
            string query1 = "select * from Products";
            command.CommandText = query1;
            int length = dataB.GetProductsData().Length;
            Products[] products = new Products[length];
            products = dataB.GetProductsData();

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Product_Type.Equals("RAM"))
                    cboxCustomerProductRAM.Items.Add(products[i].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                if (products[i].Product_Type.Equals("GPU"))
                    cboxCustomerProductGPU.Items.Add(products[i].Proudct_ID.ToString()); // הוספת נתונים לקומבו בוקס של כרטיס מסך

                if (products[i].Product_Type.Equals("CPU"))
                    cboxCustomerProductCPU.Items.Add(products[i].Proudct_ID.ToString()); // הוספת נתונים לקומבו בוקס של מעבד
                if (products[i].Product_Type.Equals("MB"))
                    cboxCustomerProducMotherBoard.Items.Add(products[i].Proudct_ID.ToString()); // הוספת נתונים לקומבו בוקס של לוח אם
            }

            connection1.Close();

            // מיון קומבו בוקסים של הזמנות בסדר עולה
            cboxCustomerOrder.Sorted = true;
            cboxCustomerProductRAM.Sorted = true;
            cboxCustomerProductCPU.Sorted = true;
            cboxCustomerProductGPU.Sorted = true;
            cboxCustomerProducMotherBoard.Sorted = true;

            string PathDB5 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection5 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB5 + ";Persist Security Info=False;");
            OleDbDataAdapter ada5 = new OleDbDataAdapter("SELECT ID,First_Name,Last_Name FROM Customers", connection5);
            DataSet set5 = new DataSet();
            ada.Fill(set5, "Customers");
            DataTable tab5 = new DataTable();
            tab5 = set.Tables["Customers"];

            OleDbCommand command5 = new OleDbCommand();
            connection5.Open();
            command.Connection = connection5;
            string query5 = "select * from Customers";
            command.CommandText = query5;

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cboxCustomerList.Items.Add(reader["ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection.Close();

            // טעינת סוג מוצר  לקומבו בוקס
            OleDbCommand command15 = new OleDbCommand();
            OleDbConnection connection15 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            connection.Open();
            command15.Connection = connection;
            string query15 = "select * from Products";
            command15.CommandText = query15;

            OleDbDataReader reader15 = command15.ExecuteReader();
            while (reader15.Read())
            {
                if (!cboxAddProductType.Items.Contains(reader15["Product_Type"].ToString())) // בדיקה שאין ערכים כפולים בקומבו בוקס 
                    cboxAddProductType.Items.Add(reader15["Product_Type"].ToString()); // הוספת נתונים לקומבו בוקס


            }

            connection.Close();


            // טעינת סוג יצרן  לקומבו בוקס
            OleDbCommand command16 = new OleDbCommand();
            OleDbConnection connection16 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            connection1.Open();
            command16.Connection = connection1;
            string query16 = "select * from Products";
            command16.CommandText = query16;
            OleDbDataReader reader2 = command16.ExecuteReader();
            while (reader2.Read())
            {
                if (!cboxAddProductManufacturer.Items.Contains(reader2["Manufacturer"].ToString())) // בדיקה שאין ערכים כפולים בקומבו בוקס 
                    cboxAddProductManufacturer.Items.Add(reader2["Manufacturer"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection1.Close();

            // טעינת ת.ז של ספק   לקומבו בוקס
            OleDbCommand command17 = new OleDbCommand();
            OleDbConnection connection17 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            connection2.Open();
            command17.Connection = connection2;
            string query17 = "select * from suppliers";
            command17.CommandText = query17;
            OleDbDataReader reader3 = command17.ExecuteReader();
            while (reader3.Read())
            {
                if (!cboxAddProductSupplierIdentity.Items.Contains(reader3["ID"].ToString())) // בדיקה שאין ערכים כפולים בקומבו בוקס 
                    cboxAddProductSupplierIdentity.Items.Add(reader3["ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection2.Close();

            // מיון קומבו בוקסים של מוצרים בסדר עולה
            cboxProductList.Sorted = true;
            cboxAddProductManufacturer.Sorted = true;
            cboxAddProductType.Sorted = true;
            cboxAddProductSupplierIdentity.Sorted = true;

            // בדיקה האם העובד שהתחבר למערכת הינו מנהל הסניף 

            Workers[] worker = dataB.GetWorkerData();

            for (int i = 0; i < worker.Length; i++)
                if (worker[i].Worker_UserName.Equals(frmLogIn.checkmanager) && worker[i].Manager_IsAManager == true) // בדיקה האם העובד שהתחבר הוא מנהל  הסניף
                {

                    tabPcStore.TabPages.Add(tabManager); // הצגת לשונית מנהל לעובד בעל הרשאות מנהל

                    // טעינת רשימת עובדים  ל dgv
                    string PathDB9 = Application.StartupPath + @"\PcStore.ACCDB";
                    OleDbConnection connection9 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB9 + ";Persist Security Info=False;");
                    OleDbDataAdapter ada9 = new OleDbDataAdapter("SELECT * FROM Workers", connection9);
                    DataSet set9 = new DataSet();
                    ada9.Fill(set9, "Workers");
                    tabSearchWorker = new DataTable();
                    tabSearchWorker = set9.Tables["Workers"];
                    dgvWorkers.DataSource = tabSearchWorker;
                    dgvWorkers.Sort(dgvWorkers.Columns["ID"], ListSortDirection.Ascending);
                    dgvWorkers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    OleDbCommand command6 = new OleDbCommand();
                    connection6.Open();
                    command.Connection = connection6;
                    string query6 = "select * from Workers";
                    command.CommandText = query6;

                    OleDbDataReader reader4 = command.ExecuteReader();
                    while (reader4.Read())
                    {
                        cboxWorkerList.Items.Add(reader4["ID"].ToString()); // הוספת נתונים לקומבו בוקס

                    }


                }
            string[] filename = new string[1]; // משתנה עזר למציאת המסלול של הקובץ 
            string filenameExt; // משתנה למציאת סיומת של הקובץ

            // מציאת העובד שהתחבר למערכת והצגת תמונתו המתאימה 
            for (int i = 0; i < worker.Length; i++)
                if (worker[i].Worker_UserName.Equals(frmLogIn.checkuser))
                {
                    foreach (TabPage tabPge in tabPcStore.TabPages)
                    {
                        PictureBox pic = new PictureBox();

                        pic.Name = "picboxUser1";
                        filename = Directory.GetFiles(Application.StartupPath + @"\pictures\users\", worker[i].Worker_ID + "*");
                        filenameExt = Path.GetExtension(filename[0]);
                        pic.ImageLocation = Application.StartupPath + @"\pictures\users\" + worker[i].Worker_ID + Path.GetExtension(Application.StartupPath + @"\pictures\users\" + worker[i].Worker_ID + filenameExt);
                        pic.Location = new Point(0, 0);
                        pic.Size = new System.Drawing.Size(50, 50);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Click += new EventHandler(this.picboxUser_Click);
                        System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                        gp.AddEllipse(pic.DisplayRectangle);
                        pic.Region = new Region(gp);
                        tabPge.Controls.Add(pic);

                    }
                }
        }

        private void btnFindSuppliers_Click(object sender, EventArgs e) // חיפוש ספק
        {
            Suppliers[] supplier = dataB.GetSupplierData();
            int flag = 0;
            int supplierLength = supplier.Length;//שומר את אורך רשימת הספקים
            if (supplierLength > 0)//אם אורך רשימת הספקים גדול מ0
            {
                for (int i = 0; i < supplier.Length; i++)
                {

                    if (txtboxSearchSuppliers.Text != "" && dataB.isNumber(txtboxSearchSuppliers.Text)) // בדיקת תקינות 
                    {
                        if (int.Parse(txtboxSearchSuppliers.Text).Equals(supplier[i].Supplier_ID)) // בדיקה שהספק שרוצים לחפש קיים במאגר הספקים
                        {
                            flag = 1; // יש ספק 
                            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                            connection.Open();
                            OleDbCommand cmd = connection.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM Suppliers WHERE ID=" + supplier[i].Supplier_ID;
                            cmd.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dt);
                            dgvSuppliers.DataSource = dt;
                            connection.Close();
                            return;
                        }


                    }

                }
            }

            if (txtboxSearchSuppliers.Text == "" || !dataB.isNumber(txtboxSearchSuppliers.Text)) // בדיקת תקינות קלט
            {
                MessageBox.Show("Please Enter Correct Input", "Error");
                return;
            }

            if (flag == 0)
            {
                MessageBox.Show("No Such Supplier", "Error");
                txtboxSearchSuppliers.Text = "";
                return;
            }



        }

        private void btnApplyOrder_Click(object sender, EventArgs e) // ביצוע הזמנה 
        {
            if (cboxCustomerOrder.SelectedIndex == -1) // בדיקה אם לא בחרנו בקומבובוקס של לקוח 
            {
                MessageBox.Show("Please choose a Customer", "Error");
                txtboxCustomerProductRAM.Text = "";
                txtboxCustomerProductGPU.Text = "";
                txtboxCustomerProductCPU.Text = "";
                txtboxCustomerProducMotherBoard.Text = "";
                return;
            }

            if (cboxCustomerProductRAM.SelectedIndex == -1 && cboxCustomerProductCPU.SelectedIndex == -1 && cboxCustomerProductGPU.SelectedIndex == -1 && cboxCustomerProducMotherBoard.SelectedIndex == -1) //  בדיקה שבחרנו לפחות  קומבו בוקס אחד  של המוצרים 
            {
                MessageBox.Show("Please choose at least One Product", "Error");
                txtboxCustomerProductRAM.Text = "";
                txtboxCustomerProductGPU.Text = "";
                txtboxCustomerProductCPU.Text = "";
                txtboxCustomerProducMotherBoard.Text = "";
                return;
            }

            Random rnd = new Random(); // משתנה להגרלת מספר 
            CustomersOrders CusOrder = new CustomersOrders();
            Customers customer = new Customers();
            int length = dataB.GetProductsData().Length;
            Products[] products = new Products[length];
            products = dataB.GetProductsData();
            Suppliers supplier = new Suppliers();
            Suppliers[] suppliers = dataB.GetSupplierData();
            CustomersOrdersDetail CusOrderDetail = new CustomersOrdersDetail();
            SuppliersOrders supplierorder = new SuppliersOrders();
            customer.Customer_ID = int.Parse(cboxCustomerOrder.SelectedItem.ToString()); // קבלת ת.ז של הלקוח שרוצה לבצע הזמנה
            CusOrderDetail.CustomerOrder_ID = rnd.Next(10000, 10000000); // הגרלת מס' הזמנה 

            if ((cboxCustomerProductRAM.SelectedIndex != -1 && txtboxCustomerProductRAM.Text == "")
                || (cboxCustomerProductGPU.SelectedIndex != -1 && txtboxCustomerProductGPU.Text == "")
                || (cboxCustomerProductCPU.SelectedIndex != -1 && txtboxCustomerProductCPU.Text == "")
                || (cboxCustomerProducMotherBoard.SelectedIndex != -1 && txtboxCustomerProducMotherBoard.Text == "")) // בדיקת תקינות שבחרנו  מוצר שאנחנו רוצים להזמין אבל שכחנו לבחור את הכמות  
            {
                MessageBox.Show("Please enter valid stock number", "Error");
                txtboxCustomerProductRAM.Text = "";
                txtboxCustomerProductGPU.Text = "";
                txtboxCustomerProductCPU.Text = "";
                txtboxCustomerProducMotherBoard.Text = "";
                return;

            }

            if ((cboxCustomerProductRAM.SelectedIndex == -1 && txtboxCustomerProductRAM.Text != "")
               || (cboxCustomerProductGPU.SelectedIndex == -1 && txtboxCustomerProductGPU.Text != "")
               || (cboxCustomerProductCPU.SelectedIndex == -1 && txtboxCustomerProductCPU.Text != "")
               || (cboxCustomerProducMotherBoard.SelectedIndex == -1 && txtboxCustomerProducMotherBoard.Text != "")) // בדיקת תקינות שבחרנו כמות של המוצר שאנחנו רוצים להזמין אבל שכחנו לבחור את המוצר  
            {
                MessageBox.Show("Please Enter Product And Quantity ", "Error");
                txtboxCustomerProductRAM.Text = "";
                txtboxCustomerProductGPU.Text = "";
                txtboxCustomerProductCPU.Text = "";
                txtboxCustomerProducMotherBoard.Text = "";
                return;

            }


            CusOrderDetail.CustomerOrder_ClientID = customer.Customer_ID;
            CusOrderDetail.CustomerOrder_ClientName = dataB.GetCustomerNameByID(customer);


            for (int i = 0; i < products.Length; i++) // לולאה שרצה על מס' המוצרים שיש בטבלת המוצרים 

            {

                if (cboxCustomerProductRAM.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProductRAM.Text))) // אם לקוח החליט להזמין זכרון ראם 
                {
                    CusOrderDetail.CustomerOrder_ProductID = products[i].Proudct_ID;
                    CusOrderDetail.CustomerOrder_ProductDescription = dataB.GetProductDescriptionByProductID(products[i]);
                    CusOrderDetail.CustomerOrder_Quantity = int.Parse(txtboxCustomerProductRAM.Text);
                    products[i].Proudct_ID = int.Parse(cboxCustomerProductRAM.SelectedItem.ToString());
                    if (products[i].Product_Stock - int.Parse(txtboxCustomerProductRAM.Text) < 0) // בדיקה שהכמות המוזמנת  גדולה מהכמות שבמלאי 
                    {
                        supplierorder.SupplierOrders_ID = dataB.GetSupplierIDByProductID(products[i]);
                        supplierorder.SupplierOrders_Name = dataB.GetSupplierNameByProductID(products[i]);
                        supplierorder.SupplierOrders_OrderID = CusOrderDetail.CustomerOrder_ID;
                        supplierorder.SupplierOrders_ProductID = products[i].Proudct_ID;
                        supplierorder.SupplierOrders_CustomerID = customer.Customer_ID;
                        supplierorder.SupplierOrders_Quantity = Math.Abs(products[i].Product_Stock - int.Parse(txtboxCustomerProductRAM.Text));

                        products[i].Product_Stock = 0;

                        dataB.AddSupplierOrder(supplierorder);
                        MessageBox.Show("Product not in stock, Ordered from supplier.", "Warning"); // הודעה שהמוצר לא במלאי , נשלחה הזמנה לספק 

                    }
                    else // במידה והכמות המוזמנת קטנה מהכמות שבמלאי 
                    {
                        products[i].Product_Stock = products[i].Product_Stock - int.Parse(txtboxCustomerProductRAM.Text);
                    }

                    dataB.AddCustomerOrderDetail(CusOrderDetail);

                }

                if (cboxCustomerProductGPU.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProductGPU.Text))) // אם לקוח החליט להזמין כרטיס מסך   
                {
                    CusOrderDetail.CustomerOrder_ProductID = products[i].Proudct_ID;
                    CusOrderDetail.CustomerOrder_ProductDescription = dataB.GetProductDescriptionByProductID(products[i]);
                    CusOrderDetail.CustomerOrder_Quantity = int.Parse(txtboxCustomerProductGPU.Text);
                    products[i].Proudct_ID = int.Parse(cboxCustomerProductGPU.SelectedItem.ToString());

                    if (products[i].Product_Stock - int.Parse(txtboxCustomerProductGPU.Text) < 0) // בדיקה שהכמות המוזמנת  גדולה מהכמות שבמלאי 
                    {
                        supplierorder.SupplierOrders_ID = dataB.GetSupplierIDByProductID(products[i]);
                        supplierorder.SupplierOrders_Name = dataB.GetSupplierNameByProductID(products[i]);
                        supplierorder.SupplierOrders_ProductID = products[i].Proudct_ID;
                        supplierorder.SupplierOrders_OrderID = CusOrderDetail.CustomerOrder_ID;
                        supplierorder.SupplierOrders_CustomerID = customer.Customer_ID;
                        supplierorder.SupplierOrders_Quantity = Math.Abs(products[i].Product_Stock - int.Parse(txtboxCustomerProductGPU.Text));


                        products[i].Product_Stock = 0;



                        dataB.AddSupplierOrder(supplierorder);
                        MessageBox.Show("Product not in stock, Ordered from supplier.", "Warning"); // הודעה שהמוצר לא במלאי , נשלחה הזמנה לספק 

                    }
                    else // במידה והכמות המוזמנת גדולה מהכמות שבמלאי 
                    {
                        products[i].Product_Stock = products[i].Product_Stock - int.Parse(txtboxCustomerProductGPU.Text);

                    }

                    dataB.AddCustomerOrderDetail(CusOrderDetail);

                }

                if (cboxCustomerProductCPU.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProductCPU.Text))) // אם לקוח החליט להזמין מעבד 
                {
                    CusOrderDetail.CustomerOrder_ProductID = products[i].Proudct_ID;
                    CusOrderDetail.CustomerOrder_ProductDescription = dataB.GetProductDescriptionByProductID(products[i]);
                    CusOrderDetail.CustomerOrder_Quantity = int.Parse(txtboxCustomerProductCPU.Text);
                    products[i].Proudct_ID = int.Parse(cboxCustomerProductCPU.SelectedItem.ToString());

                    if (products[i].Product_Stock - int.Parse(txtboxCustomerProductCPU.Text) < 0) // בדיקה שהכמות המוזמנת  גדולה מהכמות שבמלאי 
                    {
                        supplierorder.SupplierOrders_ID = dataB.GetSupplierIDByProductID(products[i]);
                        supplierorder.SupplierOrders_Name = dataB.GetSupplierNameByProductID(products[i]);
                        supplierorder.SupplierOrders_ProductID = products[i].Proudct_ID;
                        supplierorder.SupplierOrders_CustomerID = customer.Customer_ID;
                        supplierorder.SupplierOrders_OrderID = CusOrderDetail.CustomerOrder_ID;
                        supplierorder.SupplierOrders_Quantity = Math.Abs(products[i].Product_Stock - int.Parse(txtboxCustomerProductCPU.Text));


                        products[i].Product_Stock = 0;



                        dataB.AddSupplierOrder(supplierorder);
                        MessageBox.Show("Product not in stock, Ordered from supplier.", "Warning"); // הודעה שהמוצר לא במלאי , נשלחה הזמנה לספק 


                    }

                    else // במידה והכמות המוזמנת גדולה מהכמות שבמלאי 
                    {
                        products[i].Product_Stock = products[i].Product_Stock - int.Parse(txtboxCustomerProductCPU.Text);

                    }

                    dataB.AddCustomerOrderDetail(CusOrderDetail);

                }

                if (cboxCustomerProducMotherBoard.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProducMotherBoard.Text))) // אם לקוח החליט להזמין לוח אם 
                {
                    CusOrderDetail.CustomerOrder_ProductID = products[i].Proudct_ID;
                    CusOrderDetail.CustomerOrder_ProductDescription = dataB.GetProductDescriptionByProductID(products[i]);
                    CusOrderDetail.CustomerOrder_Quantity = int.Parse(txtboxCustomerProducMotherBoard.Text);
                    products[i].Proudct_ID = int.Parse(cboxCustomerProducMotherBoard.SelectedItem.ToString());

                    if (products[i].Product_Stock - int.Parse(txtboxCustomerProducMotherBoard.Text) < 0) // בדיקה שהכמות המוזמנת  גדולה מהכמות שבמלאי 
                    {
                        supplierorder.SupplierOrders_ID = dataB.GetSupplierIDByProductID(products[i]);
                        supplierorder.SupplierOrders_Name = dataB.GetSupplierNameByProductID(products[i]);
                        supplierorder.SupplierOrders_ProductID = products[i].Proudct_ID;
                        supplierorder.SupplierOrders_OrderID = CusOrderDetail.CustomerOrder_ID;
                        supplierorder.SupplierOrders_Quantity = Math.Abs(products[i].Product_Stock - int.Parse(txtboxCustomerProducMotherBoard.Text));


                        products[i].Product_Stock = 0;


                        dataB.AddSupplierOrder(supplierorder);
                        MessageBox.Show("Product not in stock, Ordered from supplier.", "Warning"); // הודעה שהמוצר לא במלאי , נשלחה הזמנה לספק 


                    }

                    else // במידה והכמות המוזמנת גדולה מהכמות שבמלאי 
                    {
                        products[i].Product_Stock = products[i].Product_Stock - int.Parse(txtboxCustomerProducMotherBoard.Text);

                    }

                    dataB.AddCustomerOrderDetail(CusOrderDetail);

                }

                // חיסור מלאי של מוצר שלקוח הזמין  

                if (cboxCustomerProductRAM.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProductRAM.Text)))
                    dataB.SubProductStock(products[i], CusOrderDetail);

                if (cboxCustomerProductGPU.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProductGPU.Text)))
                    dataB.SubProductStock(products[i], CusOrderDetail);

                if (cboxCustomerProductCPU.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProductCPU.Text)))
                    dataB.SubProductStock(products[i], CusOrderDetail);

                if (cboxCustomerProducMotherBoard.SelectedIndex > -1 && products[i].Proudct_ID.Equals(int.Parse(cboxCustomerProducMotherBoard.Text)))
                    dataB.SubProductStock(products[i], CusOrderDetail);

            }

            // הוספת לקוח לטבלת CustomersOrders

            CusOrder.CustomersOrders_ID = CusOrderDetail.CustomerOrder_ID; // קבלת מס' הזמנה  של הלקוח ע"מ להוסיף אותו לטבלת סיכום הזמנות 
            CusOrder.CustomersOrders_ClientID = customer.Customer_ID;  // קבלת ת.ז של הלקוח ע"מ להוסיף אותו לטבלת סיכום הזמנות 
            CusOrder.CustomersOrders_ClientName = CusOrderDetail.CustomerOrder_ClientName;  // קבלת השם של הלקוח ע"מ להוסיף אותו לטבלת סיכום הזמנות 
            CusOrder.CustomersOrders_OrderSuppliedToCustomer = "NO"; // הלקוח לא קיבל את ההזמנה שלו 
            dataB.AddCustomerOrder(CusOrder);
            MessageBox.Show("Order Succesfully Added", "Success");
            txtboxCustomerProducMotherBoard.Text = "";
            txtboxCustomerProductCPU.Text = "";
            txtboxCustomerProductGPU.Text = "";
            txtboxCustomerProductRAM.Text = "";
            cboxCustomerOrder.Text = "";
            cboxCustomerProducMotherBoard.Text = "";
            cboxCustomerProductGPU.Text = "";
            cboxCustomerProductRAM.Text = "";
            cboxCustomerProductCPU.Text = "";

            // רענון טבלת הזמנות בלשונית הזמנות  לאחר הזמנת מוצר
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail", connection);
            DataSet set = new DataSet();
            ada.Fill(set, "CustomersOrdersDetail");
            DataTable tab = new DataTable();
            tab = set.Tables["CustomersOrdersDetail"];
            dgvCustomerOrderDetail.DataSource = tab;
            dgvCustomerOrderDetail.Sort(dgvCustomerOrderDetail.Columns["OrderID"], ListSortDirection.Ascending);
            dgvCustomerOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון טבלת מוצרים  בלשונית הזמנות לאחר הזמנת מוצר
            string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
            OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection1);
            DataSet set1 = new DataSet();
            ada1.Fill(set1, "Products");
            DataTable tab1 = new DataTable();
            tab1 = set1.Tables["Products"];
            dgvProductOrder.DataSource = tab1;
            dgvProductOrder.Sort(dgvProductOrder.Columns["Product_ID"], ListSortDirection.Ascending);
            dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון טבלת מוצרים  בלשונית מוצרים לאחר הזמנת מוצר
            string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;");
            OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM Products", connection2);
            DataSet set2 = new DataSet();
            ada2.Fill(set2, "Products");
            DataTable tab2 = new DataTable();
            tab2 = set2.Tables["Products"];
            dgvProducts.DataSource = tab2;
            dgvProducts.Sort(dgvProducts.Columns["Product_ID"], ListSortDirection.Ascending);
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון טבלת הזמנות מספק  בלשונית הזמנות  לאחר הזמנת מוצר
            string PathDB3 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection3 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB3 + ";Persist Security Info=False;");
            OleDbDataAdapter ada3 = new OleDbDataAdapter("SELECT * FROM SuppliersOrders", connection3);
            DataSet set3 = new DataSet();
            ada3.Fill(set3, "SuppliersOrders");
            DataTable tab3 = new DataTable();
            tab3 = set3.Tables["SuppliersOrders"];
            dgvSupplierOrderDetail.DataSource = tab3;
            dgvSupplierOrderDetail.Sort(dgvSupplierOrderDetail.Columns["Supplier_ID"], ListSortDirection.Ascending);
            dgvSupplierOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // איפוס השדות על מנת ביצוע הזמנה מחדש
            cboxCustomerProductGPU.SelectedIndex = -1;
            cboxCustomerProductCPU.SelectedIndex = -1;
            cboxCustomerProductRAM.SelectedIndex = -1;
            cboxCustomerProducMotherBoard.SelectedIndex = -1;


        }



        private void txtboxSearchCustomer_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxSearchCustomer.Text) == false)
            {
                Error.SetError(txtboxSearchCustomer, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSearchCustomer.Text = "";
                Error.Clear();
            }


            DataView dv = new DataView(tabSearchCustomer);
            dv.RowFilter = string.Format("CONVERT({0},System.String) LIKE '%{1}%'", "ID", txtboxSearchCustomer.Text);
            dgvCustomers.DataSource = dv;

        }

        private void btnRemoveOrder_Click(object sender, EventArgs e) // מחיקת הזמנה 
        {
            if (MessageBox.Show(" Are You Sure You Want To Remove The Order ?  ", "Remove Order", MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            {

                CustomersOrdersDetail cusorderdet = new CustomersOrdersDetail(); // משתנה שמכיל הזמנה של מוצר מסויים
                CustomersOrdersDetail[] cusorderdet1 = dataB.GetOrderDetailData(); // משתנה שמכיל את כל ההזמנות של הלקוחות 
                CustomersOrders cusorder = new CustomersOrders(); // משתנה שמכיל הזמנה בטבלת סיכום הזמנות 
                SuppliersOrders[] supporders = dataB.GetSupplierOrderData(); // משתנה שמכיל את כל ההזמנות מהספק 
                SuppliersOrders supporder = new SuppliersOrders(); // משתנה שמכיל הזמנה מהספק 
                Products[] products = dataB.GetProductsData(); // משתנה שמכיל את כל המוצרים שבחנות 
                int supplierStockPerCustomer = 0; // משתנה זמני שמכיל את כמות ההזמנה של הספק
                if (dgvCustomerOrderDetail.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvCustomerOrderDetail.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dgvCustomerOrderDetail.Rows[selectedrowindex];

                    if (Convert.ToString(selectedRow.Cells["CustomerID"].Value) == "") // בדיקה שהשורה ב DGV לא ריקה
                    {
                        MessageBox.Show("Please Choose A Order", "Error");
                        return;
                    }

                    string CustomerID = Convert.ToString(selectedRow.Cells["CustomerID"].Value); // תעודת זהות של הלקוח שברצונינו למחוק את ההזמנה שלו 
                    cusorderdet.CustomerOrder_ClientID = int.Parse(CustomerID);

                    string OrderID = Convert.ToString(selectedRow.Cells["OrderID"].Value); // מס' הזמנה של הלקוח שברצונינו למחוק את ההזמנה שלו


                    for (int i = 0; i < cusorderdet1.Length; i++) // רץ על טבלת הזמנות של לקוח 
                        for (int j = 0; j < products.Length; j++) // רץ על טבלת מוצרים 

                            if (cusorderdet1[i].CustomerOrder_ProductID.Equals(products[j].Proudct_ID) && cusorderdet1[i].CustomerOrder_ClientID.Equals(int.Parse(CustomerID)) && cusorderdet1[i].CustomerOrder_ID.Equals(int.Parse(OrderID))) // מצאנו את המוצר שברצונינו להחזיר למלאי 
                            {

                                for (int k = 0; k < supporders.Length; k++)
                                    if (supporders[k].SupplierOrders_OrderID.Equals(cusorderdet1[i].CustomerOrder_ID) && supporders[k].SupplierOrders_ProductID.Equals(cusorderdet1[i].CustomerOrder_ProductID)) //  אם הלקוח  הזמין מוצר שלא אזל מהמלאי אז
                                        supplierStockPerCustomer = supporders[k].SupplierOrders_Quantity;

                                products[j].Product_Stock = products[j].Product_Stock + cusorderdet1[i].CustomerOrder_Quantity - supplierStockPerCustomer;
                                dataB.UpdateProductStock(products[j]); // ביצוע עדכון לכמות המלאי של המוצר לאחר מחיקת הזמנה 

                                cusorderdet.CustomerOrder_ID = int.Parse(OrderID); // שמירת מס' ההזמנה של הלקוח המתאים על מנת שנוכל למחוק את ההזמנה המתאימה שלו 
                                cusorder.CustomersOrders_ID = cusorderdet1[i].CustomerOrder_ID; // שמירת מס' הזמנה של לקוח על מנת שנוכל למחוק אותו מטבלת סיכום הזמנות 
                                supporder.SupplierOrders_OrderID = int.Parse(OrderID); // שמירת מס' הזמנה של הלקוח המתאים על מנת שנוכל למחוק אותו מטבלת הזמנות מהספק
                            }


                    dataB.RemoveCustomersOrderDetail(cusorderdet);
                    dataB.RemoveCustomersOrder(cusorder);
                    dataB.RemoveSuppliersOrders(supporder);
                    MessageBox.Show("Order Removed", "Success");

                }

                // רענון טבלת הזמנות בלשונית הזמנות לאחר מחיקת הזמנה
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "CustomersOrdersDetail");
                DataTable tab = new DataTable();
                tab = set.Tables["CustomersOrdersDetail"];
                dgvCustomerOrderDetail.DataSource = tab;
                dgvCustomerOrderDetail.Sort(dgvCustomerOrderDetail.Columns["OrderID"], ListSortDirection.Ascending);
                dgvCustomerOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                // רענון טבלת  מוצרים בלשונית הזמנות לאחר מחיקת הזמנה
                string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection1);
                DataSet set1 = new DataSet();
                ada1.Fill(set1, "Products");
                DataTable tab1 = new DataTable();
                tab1 = set1.Tables["Products"];
                dgvProductOrder.DataSource = tab1;
                dgvProductOrder.Sort(dgvProductOrder.Columns["Product_ID"], ListSortDirection.Ascending);
                dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // רענון טבלת  מוצרים בלשונית מוצרים  לאחר מחיקת הזמנה
                string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;");
                OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM Products", connection2);
                DataSet set2 = new DataSet();
                ada2.Fill(set2, "Products");
                DataTable tab2 = new DataTable();
                tab2 = set2.Tables["Products"];
                dgvProducts.DataSource = tab2;
                dgvProducts.Sort(dgvProducts.Columns["Product_ID"], ListSortDirection.Ascending);
                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // רענון טבלת  הזמנות מוצרים מהספק  בלשונית הזמנות  לאחר מחיקת הזמנה
                string PathDB3 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection3 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB3 + ";Persist Security Info=False;");
                OleDbDataAdapter ada3 = new OleDbDataAdapter("SELECT * FROM SuppliersOrders", connection3);
                DataSet set3 = new DataSet();
                ada3.Fill(set3, "SuppliersOrders");
                DataTable tab3 = new DataTable();
                tab3 = set3.Tables["SuppliersOrders"];
                dgvSupplierOrderDetail.DataSource = tab3;
                dgvSupplierOrderDetail.Sort(dgvSupplierOrderDetail.Columns["Supplier_ID"], ListSortDirection.Ascending);
                dgvSupplierOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void txtboxCustomerProductRAM_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxCustomerProductRAM.Text) == false)
            {
                Error.SetError(txtboxCustomerProductRAM, "Error");
                MessageBox.Show("Error Input Please Try Again");
                txtboxCustomerProductRAM.Text = "";
                Error.Clear();
            }
        }

        private void txtboxCustomerProductGPU_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxCustomerProductGPU.Text) == false)
            {
                Error.SetError(txtboxCustomerProductGPU, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerProductGPU.Text = "";
                Error.Clear();
            }
        }

        private void txtboxCustomerProductCPU_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxCustomerProductCPU.Text) == false)
            {
                Error.SetError(txtboxCustomerProductCPU, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerProductCPU.Text = "";
                Error.Clear();
            }
        }

        private void txtboxCustomerProducMotherBoard_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxCustomerProducMotherBoard.Text) == false)
            {
                Error.SetError(txtboxCustomerProducMotherBoard, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerProducMotherBoard.Text = "";
                Error.Clear();
            }
        }

        private void txtboxSearchProducts_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxSearchProducts.Text) == false)
            {
                Error.SetError(txtboxSearchProducts, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSearchProducts.Text = "";
                Error.Clear();
            }

            DataView dv = new DataView(tabSearchProduct);
            dv.RowFilter = string.Format("CONVERT({0},System.String) LIKE '%{1}%'", "Product_ID", txtboxSearchProducts.Text);
            dgvProducts.DataSource = dv;
        }

        private void txtboxSearchSuppliers_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxSearchSuppliers.Text) == false)
            {
                Error.SetError(txtboxSearchSuppliers, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSearchSuppliers.Text = "";
                Error.Clear();
            }

            DataView dv = new DataView(tabSearchSupplier);
            dv.RowFilter = string.Format("CONVERT({0},System.String) LIKE '%{1}%'", "ID", txtboxSearchSuppliers.Text);
            dgvSuppliers.DataSource = dv;
        }

        private void btnUpdateSupplier_Click(object sender, EventArgs e) // עדכון ספק 
        {
            Suppliers s = new Suppliers();
            SuppliersOrders so = new SuppliersOrders();

            if (dgvSuppliers.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvSuppliers.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvSuppliers.Rows[selectedrowindex];
                int columnIndex = dgvSuppliers.CurrentCell.ColumnIndex; // מקבל את מס' העמודה של התא הנבחר 
                string columnName = dgvSuppliers.Columns[columnIndex].Name; // מקבל את שם העמודה של התא הנבחר
                string value = Convert.ToString(selectedRow.Cells[columnName].Value); // מקבל את הערך של התא הנבחר



                if (columnName.Equals("Supplier_Name")) // אם העמודה הנבחרת היא שם ספק 
                {
                    s.Supplier_Name = value;
                    s.Supplier_ID = int.Parse(Convert.ToString(selectedRow.Cells["ID"].Value));
                    dataB.UpdateSupplierName(s);
                    so.SupplierOrders_Name = s.Supplier_Name;
                    dataB.UpdateSupplierOrderNameBySupplierID(so, s);
                    MessageBox.Show("Supplier Updated", "Success");
                }

                if (columnName.Equals("Telephone")) // אם העמודה הנבחרת היא מס' טלפון 
                {
                    s.Supplier_Telephone = value;
                    s.Supplier_ID = int.Parse(Convert.ToString(selectedRow.Cells["ID"].Value));
                    dataB.UpdateSupplierPhone(s);
                    MessageBox.Show("Supplier Updated", "Success");
                }

            }

            // רענון טבלה לאחר עדכון ספק 
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Suppliers", connection);
            DataSet set = new DataSet();
            ada.Fill(set, "Suppliers");
            DataTable tab = new DataTable();
            tab = set.Tables["Suppliers"];
            dgvSuppliers.DataSource = tab;
        }



        private void txtboxCustomerLastName_TextChanged(object sender, EventArgs e)
        {
            if (dataB.isLetter(txtboxCustomerLastName.Text) == false)
            {
                Error.SetError(txtboxCustomerLastName, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerLastName.Text = "";
                Error.Clear();
            }
        }



        private void txtboxCustomerTelephone_TextChanged(object sender, EventArgs e)
        {
            if (dataB.isNumber(txtboxCustomerTelephone.Text) == false)
            {
                Error.SetError(txtboxCustomerTelephone, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerTelephone.Text = "";
                Error.Clear();
            }
        }

        private void txtboxCustomerID_TextChanged(object sender, EventArgs e)
        {
            Customers c = new Customers();
            Customers[] customer = dataB.GetCustomerData();
            int flag = 0;

            if (dataB.isNumber(txtboxCustomerID.Text) == false)
            {
                Error.SetError(txtboxCustomerID, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerID.Text = "";
                Error.Clear();
            }

            for (int i = 0; i < customer.Length; i++)
            {
                if (!txtboxCustomerID.Text.Equals(customer[i].Customer_ID.ToString())) // בדיקה שת.ז של הלקוח לא קיים במאגר 
                {
                    txtboxCustomerFirstName.Text = "";
                    txtboxCustomerLastName.Text = "";
                    txtboxCustomerAddress.Text = "";
                    txtboxCustomerTelephone.Text = "";
                    txtboxCustomerEmail.Text = "";
                    cboxCustomerList.Text = "";


                }

                if (txtboxCustomerID.Text.Equals(customer[i].Customer_ID.ToString())) // בדיקה שת.ז של הלקוח קיים במאגר 
                {
                    txtboxCustomerID.Text = customer[i].Customer_ID.ToString();
                    txtboxCustomerFirstName.Text = customer[i].Customer_FirstName.ToString();
                    txtboxCustomerLastName.Text = customer[i].Customer_LastName.ToString();
                    txtboxCustomerTelephone.Text = customer[i].Customer_Telephone.ToString();
                    txtboxCustomerAddress.Text = customer[i].Customer_Address.ToString();
                    txtboxCustomerEmail.Text = customer[i].Customer_Email.ToString();
                    flag = 1;
                }

                if (flag == 1)
                    return;
            }


        }



        private void txtboxCustomerFirstName_TextChanged(object sender, EventArgs e)
        {
            if (dataB.isLetter(txtboxCustomerFirstName.Text) == false)
            {
                Error.SetError(txtboxCustomerFirstName, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerFirstName.Text = "";
                Error.Clear();
            }
        }


        private void btnAddCustomerDialog_Click(object sender, EventArgs e) // הוספה או עדכון של לקוח 
        {
            Customers c = new Customers();
            Customers[] customer = dataB.GetCustomerData();
            int flag = 0;
            int length = txtboxCustomerID.Text.ToString().Length; // בדיקת כמות ספרות של ת.ז. לצורך בדיקת תקינות

            CustomersOrders cusorder = new CustomersOrders();
            CustomersOrdersDetail cusorderdet = new CustomersOrdersDetail();
            int CustomerLength = customer.Length;//שומר את אורך רשימת הלקוחות
            if (CustomerLength > 0)//אם אורך רשימת הלקוחות גדול מ0
            {

                for (int i = 0; i < customer.Length; i++)
                {
                    // עדכון לקוח

                    if (txtboxCustomerID.Text.Equals(customer[i].Customer_ID.ToString())) // בדיקה האם הלקוח קיים לפי ה ת.ז 
                    {

                        if (dataB.IsValidEmail(txtboxCustomerEmail.Text) == false && dataB.isLetter(txtboxCustomerEmail.Text) == false) // בדיקה שהזנו כתובת מייל חוקית
                        {
                            MessageBox.Show("Please Enter A Valid Email", "Error");
                            txtboxCustomerID.Text = "";
                            txtboxCustomerFirstName.Text = "";
                            txtboxCustomerLastName.Text = "";
                            txtboxCustomerAddress.Text = "";
                            txtboxCustomerTelephone.Text = "";
                            txtboxCustomerEmail.Text = "";
                            cboxCustomerList.Text = "";
                            return;
                        }


                        if (txtboxCustomerTelephone.Text.ToString().Length < 10) //    בדיקה שהזנו מס' פלאפון תקין  במידה ובחרנו בקומבו בוקס
                        {
                            MessageBox.Show("Please Enter 10 Digit Phone Number", "Error");
                            txtboxCustomerID.Text = "";
                            txtboxCustomerFirstName.Text = "";
                            txtboxCustomerLastName.Text = "";
                            txtboxCustomerAddress.Text = "";
                            txtboxCustomerTelephone.Text = "";
                            txtboxCustomerEmail.Text = "";
                            cboxCustomerList.Text = "";
                            return;
                        }

                        if (txtboxCustomerID.Text == "" || txtboxCustomerFirstName.Text == "" || txtboxCustomerLastName.Text == "" || txtboxCustomerAddress.Text == "" || txtboxCustomerTelephone.Text == "" || txtboxCustomerEmail.Text == "") // בדיקת תקינות עדכון  לקוח    
                        {
                            MessageBox.Show("Please Fill All The Fields", "Error");
                            txtboxCustomerID.Text = "";
                            txtboxCustomerFirstName.Text = "";
                            txtboxCustomerLastName.Text = "";
                            txtboxCustomerAddress.Text = "";
                            txtboxCustomerTelephone.Text = "";
                            txtboxCustomerEmail.Text = "";
                            cboxCustomerList.Text = "";
                            return;
                        }

                        c.Customer_ID = customer[i].Customer_ID;
                        c.Customer_FirstName = txtboxCustomerFirstName.Text;
                        dataB.UpdateCustomerFirstName(c);
                        c.Customer_LastName = txtboxCustomerLastName.Text;
                        dataB.UpdateCustomerLastName(c);
                        c.Customer_Address = txtboxCustomerAddress.Text;
                        dataB.UpdateCustomerAddress(c);
                        c.Customer_Telephone = txtboxCustomerTelephone.Text;
                        dataB.UpdateCustomerTelephone(c);
                        c.Customer_Email = txtboxCustomerEmail.Text;
                        dataB.UpdateCustomerEmail(c);
                        cusorder.CustomersOrders_ClientName = txtboxCustomerFirstName.Text;
                        cusorderdet.CustomerOrder_ClientName = txtboxCustomerFirstName.Text;
                        dataB.UpdateCustomerNameInCustomersOrdersDetail(c, cusorderdet); // עדכון שם של לקוח בטבלת פירוט  הזמנות של לקוחות 
                        dataB.UpdateCustomerNameInCustomerOrders(c, cusorder); // עדכון שם של לקוח בטבלת הזמנות של לקוחות 
                        MessageBox.Show("Customer Updated Successfully", "Success");
                        txtboxCustomerID.Text = "";
                        txtboxCustomerFirstName.Text = "";
                        txtboxCustomerLastName.Text = "";
                        txtboxCustomerAddress.Text = "";
                        txtboxCustomerTelephone.Text = "";
                        txtboxCustomerEmail.Text = "";
                        cboxCustomerList.Text = "";

                        // רענון טבלה לאחר הוספת/עדכון לקוח
                        string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                        OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Customers", connection);
                        DataSet set = new DataSet();
                        ada.Fill(set, "Customers");
                        DataTable tab = new DataTable();
                        tab = set.Tables["Customers"];
                        dgvCustomers.DataSource = tab;

                        //   רענון טבלה לאחר הוספת/עדכון לקוח בטבלת לקוחות שבלשונית הזמנות
                        string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
                        OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Customers", connection1);
                        DataSet set1 = new DataSet();
                        ada1.Fill(set1, "Customers");
                        DataTable tab1 = new DataTable();
                        tab1 = set1.Tables["Customers"];
                        dgvCustomerOrder.DataSource = tab1;

                        //רענון טבלה לאחר הוספת/עדכון לקוח בטבלת הזמנות של לקוח  שבלשונית הזמנות
                        string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;");
                        OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail", connection2);
                        DataSet set2 = new DataSet();
                        ada2.Fill(set2, "CustomersOrdersDetail");
                        DataTable tab2 = new DataTable();
                        tab2 = set2.Tables["CustomersOrdersDetail"];
                        dgvCustomerOrderDetail.DataSource = tab2;

                        return;
                    }


                }

            }

            // הוספת לקוח חדש למאגר הלקוחות 

            if (length != 9) // בדיקה שערך ת.ז מכיל 9 ספרות במידה ולא בחרנו בקומבוקס 
            {
                MessageBox.Show("Please Enter 9 Digit ID", "Error");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";
                return;
            }

            if (dataB.IsValidEmail(txtboxCustomerEmail.Text) == false && dataB.isLetter(txtboxCustomerEmail.Text) == false) // בדיקה שהזנו כתובת מייל חוקית
            {
                MessageBox.Show("Please Enter A Valid Email", "Error");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";
                return;
            }

            if (txtboxCustomerID.Text == "" || txtboxCustomerFirstName.Text == "" || txtboxCustomerLastName.Text == "" || txtboxCustomerAddress.Text == "" || txtboxCustomerTelephone.Text == "" || txtboxCustomerEmail.Text == "") // בדיקת תקינות הוספת לקוח חדש   
            {
                MessageBox.Show("Please Fill All The Fields", "Error");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";
                return;
            }

            if (txtboxCustomerTelephone.Text.ToString().Length != 10) //    בדיקה שהזנו מס' פלאפון תקין  במידה ולא בחרנו בקומבו בוקס
            {
                MessageBox.Show("Please Enter 10 Digit Phone Number", "Error");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";
                return;
            }
            else


            if (length != 9) // בדיקה שערך ת.ז מכיל 9 ספרות במידה ובחרנו בקומבו בוקס  
            {
                MessageBox.Show("Please Enter 9 Digit ID", "Error");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";

                return;
            }



            for (int i = 0; i < customer.Length; i++)
            {
                if (int.Parse(txtboxCustomerID.Text).Equals(customer[i].Customer_ID)) // בדיקה שהלקוח לא קיים במאגר הלקוחות 
                    flag = 1;
            }

            if (flag == 0) // בדיקת שהלקוח לא קיים במאגר הלקוחות 
            {
                if (txtboxCustomerID.Text == "" || txtboxCustomerFirstName.Text == "" || txtboxCustomerLastName.Text == "" || txtboxCustomerAddress.Text == "" || txtboxCustomerTelephone.Text == "" || txtboxCustomerEmail.Text == "") // בדיקת תקינות הוספת לקוח חדש אם בחרנו בקומבו בוקס
                {
                    MessageBox.Show("Please Fill All The Fields", "Error");
                    txtboxCustomerID.Text = "";
                    txtboxCustomerFirstName.Text = "";
                    txtboxCustomerLastName.Text = "";
                    txtboxCustomerAddress.Text = "";
                    txtboxCustomerTelephone.Text = "";
                    txtboxCustomerEmail.Text = "";
                    cboxCustomerList.Text = "";
                    return;
                }

                c.Customer_ID = int.Parse(txtboxCustomerID.Text);
                c.Customer_FirstName = txtboxCustomerFirstName.Text;
                c.Customer_LastName = txtboxCustomerLastName.Text;
                c.Customer_Address = txtboxCustomerAddress.Text;
                c.Customer_Telephone = txtboxCustomerTelephone.Text;
                c.Customer_Email = txtboxCustomerEmail.Text;
                dataB.AddCustomer(c);
                MessageBox.Show("Customer Added Successfuly!", "Success");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";

                // רענון טבלה לאחר הוספת/עדכון לקוח
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Customers", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "Customers");
                DataTable tab = new DataTable();
                tab = set.Tables["Customers"];
                dgvCustomers.DataSource = tab;


                //   רענון טבלה לאחר הוספת/עדכון לקוח בטבלת לקוחות שבלשונית הזמנות
                string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Customers", connection1);
                DataSet set1 = new DataSet();
                ada1.Fill(set1, "Customers");
                DataTable tab1 = new DataTable();
                tab1 = set1.Tables["Customers"];
                dgvCustomerOrder.DataSource = tab1;

                //רענון טבלה לאחר הוספת/עדכון לקוח בטבלת הזמנות של לקוח  שבלשונית הזמנות
                string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;");
                OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail", connection2);
                DataSet set2 = new DataSet();
                ada2.Fill(set2, "CustomersOrdersDetail");
                DataTable tab2 = new DataTable();
                tab2 = set2.Tables["CustomersOrdersDetail"];
                dgvCustomerOrderDetail.DataSource = tab2;

                // רענון קומבו בוקס לאחר הוספת לקוח

                OleDbCommand command1 = new OleDbCommand();
                connection2.Open();
                command1.Connection = connection2;
                string query1 = "select * from Customers";
                command1.CommandText = query1;
                cboxCustomerList.Items.Clear();
                OleDbDataReader reader2 = command1.ExecuteReader();
                while (reader2.Read())
                {
                    cboxCustomerList.Items.Add(reader2["ID"].ToString()); // הוספת נתונים לקומבו בוקס

                }

                connection2.Close();

                cboxCustomerList.Sorted = true; // מיון קומבו בוקס לאחר הוספת לקוח 

                return;

            }

            else // הלקוח קיים כבר ולכן נציג הודעת שגיאה

            {
                MessageBox.Show("The Customer Already Exists", "Error");
                txtboxCustomerID.Text = "";
                txtboxCustomerFirstName.Text = "";
                txtboxCustomerLastName.Text = "";
                txtboxCustomerAddress.Text = "";
                txtboxCustomerTelephone.Text = "";
                txtboxCustomerEmail.Text = "";
                cboxCustomerList.Text = "";
                return;
            }
        }

        private void cboxCustomerList_SelectedIndexChanged(object sender, EventArgs e) // טעינת פרטי לקוח לטקסט בוקס המתאים לצורך עדכון 
        {

            Customers[] customer = dataB.GetCustomerData();
            int CustomerLength = customer.Length;//שומר את אורך רשימת הלקוחות

            if (CustomerLength > 0)//אם אורך רשימת הלקוחות גדול מ0
            {
                for (int i = 0; i < customer.Length; i++)
                {


                    if (cboxCustomerList.SelectedItem.ToString().Equals(customer[i].Customer_ID.ToString())) // בדיקה האם הלקוח קיים לפי ה ת.ז שבקומבו בוקס
                    {
                        txtboxCustomerID.Text = customer[i].Customer_ID.ToString();
                        txtboxCustomerFirstName.Text = customer[i].Customer_FirstName.ToString();
                        txtboxCustomerLastName.Text = customer[i].Customer_LastName.ToString();
                        txtboxCustomerTelephone.Text = customer[i].Customer_Telephone.ToString();
                        txtboxCustomerAddress.Text = customer[i].Customer_Address.ToString();
                        txtboxCustomerEmail.Text = customer[i].Customer_Email.ToString();

                    }

                }

            }
        }


        private void cboxSupplierList_SelectedIndexChanged(object sender, EventArgs e) // טעינת פרטי לקוח לטקסט בוקס המתאים לצורך עדכון
        {
            Suppliers[] supplier = dataB.GetSupplierData();
            int SupplierLength = supplier.Length;//שומר את אורך רשימת הספקים

            if (SupplierLength > 0)//אם אורך רשימת הספקים גדול מ0
            {
                for (int i = 0; i < supplier.Length; i++)
                {


                    if (cboxSupplierList.SelectedItem.ToString().Equals(supplier[i].Supplier_ID.ToString())) // בדיקה האם הספק קיים לפי ה ת.ז שבקומבו בוקס
                    {
                        txtboxSupplierID.Text = supplier[i].Supplier_ID.ToString();
                        txtboxSupplierName.Text = supplier[i].Supplier_Name.ToString();
                        txtboxSupplierPhone.Text = supplier[i].Supplier_Telephone.ToString();

                    }

                }

            }
        }

        private void txtboxSupplierID_TextChanged(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();
            Suppliers[] suppliers = dataB.GetSupplierData();
            int flag = 0;

            if (dataB.isNumber(txtboxSupplierID.Text) == false)
            {
                Error.SetError(txtboxSupplierID, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSupplierID.Text = "";
                Error.Clear();
            }

            for (int i = 0; i < suppliers.Length; i++)
            {

                if (!txtboxSupplierID.Text.Equals(suppliers[i].Supplier_ID.ToString())) // בדיקה שהמס' הסידורי  של הספק לא קיים במאגר 
                {
                    txtboxSupplierName.Text = "";
                    txtboxSupplierPhone.Text = "";



                }

                if (txtboxSupplierID.Text.Equals(suppliers[i].Supplier_ID.ToString())) // בדיקה שהמס' הסידורי  של הספק  קיים במאגר 
                {
                    txtboxCustomerID.Text = suppliers[i].Supplier_ID.ToString();
                    txtboxSupplierName.Text = suppliers[i].Supplier_Name.ToString();
                    txtboxSupplierPhone.Text = suppliers[i].Supplier_Telephone.ToString();
                    flag = 1;
                }

                if (flag == 1)
                    return;
            }


        }


        private void txtboxSupplierName_TextChanged(object sender, EventArgs e)
        {
            if (dataB.isLetter(txtboxSupplierName.Text) == false)
            {
                Error.SetError(txtboxSupplierName, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSupplierName.Text = "";
                Error.Clear();
            }
        }

        private void txtboxSupplierPhone_TextChanged(object sender, EventArgs e)
        {
            if (dataB.isNumber(txtboxSupplierPhone.Text) == false)
            {
                Error.SetError(txtboxSupplierPhone, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSupplierPhone.Text = "";
                Error.Clear();
            }
        }

        private void btnAddSupplierDialog_Click(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();
            Suppliers[] Supplier = dataB.GetSupplierData();
            int flag = 0;
            SuppliersOrders sorder = new SuppliersOrders();
            int SupplierLength = Supplier.Length;//שומר את אורך רשימת הספקים
            string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB"; // משתנה שמכיל את המיקום של קובץ הבסיס נתונים שלנו
            OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;"); // משתנה להתחברות לבסיס נתונים 


            // עדכון ספק 

            if (SupplierLength > 0)//אם אורך רשימת הספקים גדול מ0
            {

                if (txtboxSupplierID.Text == "" || txtboxSupplierName.Text == "" || txtboxSupplierPhone.Text == "") // בדיקת תקינות 
                {

                    MessageBox.Show("Please Fill All The Fields", "Error");
                    txtboxSupplierID.Text = "";
                    txtboxSupplierName.Text = "";
                    txtboxSupplierPhone.Text = "";
                    cboxSupplierList.Text = "";
                    return;
                }

                for (int i = 0; i < Supplier.Length; i++)
                {

                    if (txtboxSupplierID.Text.Equals(Supplier[i].Supplier_ID.ToString())) // בדיקה האם הספק קיים לפי ה ת.ז 
                    {

                        if (txtboxSupplierPhone.Text.ToString().Length < 10) //    בדיקה שהזנו מס' פלאפון תקין  במידה ובחרנו בקומבו בוקס
                        {
                            MessageBox.Show("Please Enter 10 Digit Phone Number", "Error");
                            txtboxSupplierID.Text = "";
                            txtboxSupplierID.Text = "";
                            txtboxSupplierName.Text = "";
                            txtboxSupplierPhone.Text = "";
                            return;
                        }

                        s.Supplier_ID = Supplier[i].Supplier_ID;
                        s.Supplier_Name = txtboxSupplierName.Text;
                        dataB.UpdateSupplierName(s);
                        s.Supplier_Telephone = txtboxSupplierPhone.Text;
                        dataB.UpdateSupplierPhone(s);
                        sorder.SupplierOrders_Name = txtboxSupplierName.Text;
                        dataB.UpdateSupplierOrderNameBySupplierID(sorder, s);
                        MessageBox.Show("Supplier Updated Successfully", "Success");
                        txtboxSupplierID.Text = "";
                        txtboxSupplierName.Text = "";
                        txtboxSupplierPhone.Text = "";
                        cboxSupplierList.Text = "";

                        //  רענון טבלה לאחר הוספת/עדכון ספק בלשונית ספקים
                        string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                        OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Suppliers", connection);
                        DataSet set = new DataSet();
                        ada.Fill(set, "Suppliers");
                        DataTable tab = new DataTable();
                        tab = set.Tables["Suppliers"];
                        dgvSuppliers.DataSource = tab;

                        //  רענון טבלה לאחר הוספת/עדכון ספק בטבלת הזמנות של לקוח בלשונית הזמנות 
                        string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
                        OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM SuppliersOrders", connection1);
                        DataSet set1 = new DataSet();
                        ada1.Fill(set1, "SuppliersOrders");
                        DataTable tab1 = new DataTable();
                        tab1 = set1.Tables["SuppliersOrders"];
                        dgvSupplierOrderDetail.DataSource = tab1;

                        return;
                    }


                }

            }

            // הוספת ספק חדש למאגר הספקים 



            if (txtboxSupplierID.Text == "" || txtboxSupplierName.Text == "" || txtboxSupplierPhone.Text == "") // בדיקת תקינות הוספת ספק חדש  אם לא בחרנו בקומבו בוקס 
            {
                MessageBox.Show("Please Fill All The Fields", "Error");
                txtboxSupplierID.Text = "";
                txtboxSupplierName.Text = "";
                txtboxSupplierPhone.Text = "";
                cboxSupplierList.Text = "";
                return;
            }

            if (txtboxSupplierPhone.Text.ToString().Length != 10) //    בדיקה שהזנו מס' פלאפון תקין  במידה ולא בחרנו בקומבו בוקס
            {
                MessageBox.Show("Please Enter 10 Digit Phone Number", "Error");
                txtboxSupplierID.Text = "";
                txtboxSupplierName.Text = "";
                txtboxSupplierPhone.Text = "";
                cboxSupplierList.Text = "";
                return;
            }

            for (int i = 0; i < Supplier.Length; i++)
            {
                if (int.Parse(txtboxSupplierID.Text).Equals(Supplier[i].Supplier_ID)) // בדיקה שהספק לא קיים במאגר הספקים 
                    flag = 1;
            }

            if (flag == 0) // בדיקת שהספק לא קיים במאגר הספקים 
            {
                if (txtboxSupplierID.Text == "" || txtboxSupplierName.Text == "" || txtboxSupplierPhone.Text == "") // בדיקת תקינות הוספת ספק חדש אם בחרנו בקומבו בוקס
                {
                    MessageBox.Show("Please Fill All The Fields", "Error");
                    txtboxSupplierID.Text = "";
                    txtboxSupplierName.Text = "";
                    txtboxSupplierPhone.Text = "";
                    cboxSupplierList.Text = "";
                    return;
                }

                s.Supplier_ID = int.Parse(txtboxSupplierID.Text);
                s.Supplier_Name = txtboxSupplierName.Text;
                s.Supplier_Telephone = txtboxSupplierPhone.Text;
                dataB.AddSupplier(s);
                MessageBox.Show("Supplier Added Successfuly!", "Success");
                txtboxSupplierID.Text = "";
                txtboxSupplierName.Text = "";
                txtboxSupplierPhone.Text = "";
                cboxSupplierList.Text = "";

                // רענון טבלה לאחר הוספת/עדכון ספק
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Suppliers", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "Suppliers");
                DataTable tab = new DataTable();
                tab = set.Tables["Suppliers"];
                dgvSuppliers.DataSource = tab;


            }

            else // הספק קיים כבר ולכן נציג הודעת שגיאה

            {
                MessageBox.Show("The Supplier Already Exists", "Error");
                txtboxSupplierID.Text = "";
                txtboxSupplierID.Text = "";
                txtboxSupplierID.Text = "";
                txtboxSupplierID.Text = "";
                txtboxSupplierName.Text = "";
                txtboxSupplierPhone.Text = "";
                cboxSupplierList.Text = "";
                return;
            }

            // רענון קומבו בוקס לאחר הוספת/עדכון ספק בלשונית ספקים

            OleDbCommand command1 = new OleDbCommand();
            connection2.Open();
            command1.Connection = connection2;
            string query1 = "select * from Suppliers";
            command1.CommandText = query1;
            cboxSupplierList.Items.Clear();
            OleDbDataReader reader2 = command1.ExecuteReader();
            while (reader2.Read())
            {
                cboxSupplierList.Items.Add(reader2["ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection2.Close();

            cboxSupplierList.Sorted = true; // מיון קומבו בוקס לאחר הוספת/עדכון ספק 

        }


        private void btnAddProductDialog_Click(object sender, EventArgs e) // הוספת/עדכון מוצר
        {
            Products p = new Products();
            Products[] product = dataB.GetProductsData();
            Products[] products = dataB.GetProductsData();
            Products[] products1 = dataB.GetProductsData();
            string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB"; // משתנה שמכיל את המיקום של קובץ הבסיס נתונים שלנו
            OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;"); // משתנה להתחברות לבסיס נתונים 

            int flag = 0;
            int ProductLength = product.Length;//שומר את אורך רשימת המוצרים

            CustomersOrdersDetail cusorderdet = new CustomersOrdersDetail();


            if (ProductLength > 0)//אם אורך רשימת המוצרים גדול מ 0
            {
                for (int i = 0; i < product.Length; i++)
                {

                    if (txtboxAddProductSerialNum.Text.Equals(product[i].Proudct_ID.ToString())) // בדיקה האם המוצר  קיים לפי המזהה מוצר שלו 
                    {


                        if (txtboxAddProductSerialNum.Text == "" || txtboxAddProductName.Text == "" || cboxAddProductType.Text == "" || cboxAddProductManufacturer.Text == "" || cboxAddProductSupplierIdentity.Text == "" || txtboxAddProductDescription.Text == "" || txtboxAddProductPrice.Text == "" || txtboxAddProductInStock.Text == "") // בדיקת תקינות קלט
                        {

                            MessageBox.Show("Please Enter Correct Input", "Error");
                            cboxProductList.Text = "";
                            txtboxAddProductSerialNum.Text = "";
                            txtboxAddProductName.Text = "";
                            cboxAddProductType.Text = "";
                            cboxAddProductManufacturer.Text = "";
                            cboxAddProductSupplierIdentity.Text = "";
                            txtboxAddProductDescription.Text = "";
                            txtboxAddProductPrice.Text = "";
                            txtboxAddProductInStock.Text = "";
                            return;
                        }

                        p.Proudct_ID = product[i].Proudct_ID;
                        p.Product_Price = int.Parse(txtboxAddProductPrice.Text);
                        dataB.UpdateProductPrice(p);
                        p.Product_Stock = int.Parse(txtboxAddProductInStock.Text);
                        dataB.UpdateProductStock(p);

                        MessageBox.Show("Product Updated Successfully", "Success");
                        cboxProductList.Text = "";
                        txtboxAddProductSerialNum.Text = "";
                        txtboxAddProductName.Text = "";
                        cboxAddProductType.Text = "";
                        cboxAddProductManufacturer.Text = "";
                        cboxAddProductSupplierIdentity.Text = "";
                        txtboxAddProductDescription.Text = "";
                        txtboxAddProductPrice.Text = "";
                        txtboxAddProductInStock.Text = "";

                        // רענון טבלה לאחר הוספת/עדכון מוצר
                        string PathDB3 = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection3 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB3 + ";Persist Security Info=False;");
                        OleDbDataAdapter ada3 = new OleDbDataAdapter("SELECT * FROM Products", connection3);
                        DataSet set3 = new DataSet();
                        ada3.Fill(set3, "Products");
                        DataTable tab3 = new DataTable();
                        tab3 = set3.Tables["Products"];
                        dgvProducts.DataSource = tab3;
                        dgvProducts.Sort(dgvProducts.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
                        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        // רענון טבלת מוצרים בלשונית הזמנות לאחר הוספת/עדכון מוצר
                        string PathDB4 = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection4 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB4 + ";Persist Security Info=False;");
                        OleDbDataAdapter ada4 = new OleDbDataAdapter("SELECT * FROM Products", connection4);
                        DataSet set4 = new DataSet();
                        ada4.Fill(set4, "Products");
                        DataTable tab4 = new DataTable();
                        tab4 = set4.Tables["Products"];
                        dgvProductOrder.DataSource = tab4;
                        dgvProductOrder.Sort(dgvProductOrder.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
                        dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        return;
                    }

                }
            }


            //  else


            // הוספת מוצר 

            if (ProductLength > 0)//אם אורך רשימת המוצרים גדול מ0
            {

                if (txtboxAddProductSerialNum.Text == "" || txtboxAddProductName.Text == "" || cboxAddProductType.SelectedIndex == -1 || cboxAddProductManufacturer.SelectedIndex == -1 || cboxAddProductSupplierIdentity.SelectedIndex == -1 || txtboxAddProductDescription.Text == "" || txtboxAddProductPrice.Text == "" || txtboxAddProductInStock.Text == "") // בדיקת תקינות קלט
                {
                    MessageBox.Show("Please Enter Correct Input", "Error");
                    cboxProductList.Text = "";
                    txtboxAddProductSerialNum.Text = "";
                    txtboxAddProductName.Text = "";
                    cboxAddProductType.Text = "";
                    cboxAddProductManufacturer.Text = "";
                    cboxAddProductSupplierIdentity.Text = "";
                    txtboxAddProductDescription.Text = "";
                    txtboxAddProductPrice.Text = "";
                    txtboxAddProductInStock.Text = "";
                    return;
                }

                for (int i = 0; i < product.Length; i++)
                {


                    if (txtboxAddProductSerialNum.Text.Equals(product[i].Proudct_ID.ToString())) // בדיקה האם הלקוח קיים לפי ה ת.ז 
                    {
                        MessageBox.Show("Error : Duplicate Product Serial", "Error");
                        flag = 1;

                    }


                }

                if (flag == 0) // נוסיף מוצר חדש כי המספר הסידורי שלו לא נמצא במאגר הקיים של המוצרים
                {
                    p.Proudct_ID = int.Parse(txtboxAddProductSerialNum.Text);
                    p.Product_Name = txtboxAddProductName.Text;
                    p.Product_Type = cboxAddProductType.Text;
                    p.Product_Manufacturer = cboxAddProductManufacturer.Text;
                    p.Product_SupplierIdentifier = int.Parse(cboxAddProductSupplierIdentity.Text);
                    p.Product_Description = txtboxAddProductDescription.Text;
                    p.Product_Price = double.Parse(txtboxAddProductPrice.Text);
                    p.Product_Stock = int.Parse(txtboxAddProductInStock.Text);
                    dataB.AddProduct(p);
                    MessageBox.Show("Product Added Successfuly!", "Success");
                    cboxProductList.Text = "";
                    txtboxAddProductSerialNum.Text = "";
                    txtboxAddProductName.Text = "";
                    cboxAddProductType.Text = "";
                    cboxAddProductManufacturer.Text = "";
                    cboxAddProductSupplierIdentity.Text = "";
                    txtboxAddProductDescription.Text = "";
                    txtboxAddProductPrice.Text = "";
                    txtboxAddProductInStock.Text = "";

                }

            }

            else // הטבלה ריקה ולכן נוסיף מוצר למאגר המוצרים

            {

                p.Proudct_ID = int.Parse(txtboxAddProductSerialNum.Text);
                p.Product_Name = txtboxAddProductName.Text;
                p.Product_Type = cboxAddProductType.Text;
                p.Product_Manufacturer = cboxAddProductManufacturer.Text;
                p.Product_SupplierIdentifier = int.Parse(cboxAddProductSupplierIdentity.Text);
                p.Product_Description = txtboxAddProductDescription.Text;
                p.Product_Price = double.Parse(txtboxAddProductPrice.Text);
                p.Product_Stock = int.Parse(txtboxAddProductInStock.Text);
                dataB.AddProduct(p);
                MessageBox.Show("Product Added Successfuly!", "Success");
                cboxProductList.Text = "";
                txtboxAddProductSerialNum.Text = "";
                txtboxAddProductName.Text = "";
                cboxAddProductType.Text = "";
                cboxAddProductManufacturer.Text = "";
                cboxAddProductSupplierIdentity.Text = "";
                txtboxAddProductDescription.Text = "";
                txtboxAddProductPrice.Text = "";
                txtboxAddProductInStock.Text = "";

            }

            // רענון טבלה לאחר הוספת/עדכון מוצר
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Products", connection);
            DataSet set = new DataSet();
            ada.Fill(set, "Products");
            DataTable tab = new DataTable();
            tab = set.Tables["Products"];
            dgvProducts.DataSource = tab;
            dgvProducts.Sort(dgvProducts.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון טבלת מוצרים בלשונית הזמנות לאחר הוספת/עדכון מוצר
            string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
            OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection1);
            DataSet set1 = new DataSet();
            ada1.Fill(set1, "Products");
            DataTable tab1 = new DataTable();
            tab1 = set1.Tables["Products"];
            dgvProductOrder.DataSource = tab1;
            dgvProductOrder.Sort(dgvProductOrder.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
            dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון קומבו בוקס לאחר הוספת/עדכון מוצר בלשונית מוצרים

            OleDbCommand command1 = new OleDbCommand();
            connection1.Open();
            command1.Connection = connection1;
            string query1 = "select * from Products";
            command1.CommandText = query1;
            cboxProductList.Items.Clear();
            OleDbDataReader reader2 = command1.ExecuteReader();
            while (reader2.Read())
            {
                cboxProductList.Items.Add(reader2["PRODUCT_ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection1.Close();

            cboxProductList.Sorted = true; // מיון קומבו בוקס לאחר הוספת/עדכון מוצר 



            // עדכון קומבו בוקס מתאים בלשונית הזמנות  לאחר הוספת מוצר

            products = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר הוספת מוצר 

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Product_Type.Equals(p.Product_Type)) // מציאת סוג המוצר המתאים 
                {
                    if (products[i].Product_Type.Equals("RAM")) // אם המוצר שמחקנו הוא מסוג ראם
                    {
                        products1 = dataB.GetProductsData();
                        OleDbCommand command2 = new OleDbCommand();
                        connection2.Open();
                        command2.Connection = connection2;
                        string query2 = "select * from Products";
                        command2.CommandText = query2;
                        cboxCustomerProductRAM.Items.Clear();

                        for (int j = 0; j < products1.Length; j++)
                            if (products1[j].Product_Type.Equals("RAM"))
                                cboxCustomerProductRAM.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                        connection2.Close();

                        cboxCustomerProductRAM.Sorted = true; // מיון קומבו בוקס לאחר הוספת מוצר 
                    }

                    if (products[i].Product_Type.Equals("CPU")) // אם המוצר שמחקנו הוא מסוג מעבד
                    {
                        products1 = dataB.GetProductsData();
                        OleDbCommand command2 = new OleDbCommand();
                        connection2.Open();
                        command2.Connection = connection2;
                        string query2 = "select * from Products ";
                        command2.CommandText = query2;
                        cboxCustomerProductCPU.Items.Clear();

                        for (int j = 0; j < products.Length; j++)
                            if (products1[j].Product_Type.Equals("CPU"))
                                cboxCustomerProductCPU.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם

                        connection2.Close();

                        cboxCustomerProductCPU.Sorted = true; // מיון קומבו בוקס לאחר הוספת מוצר 
                    }

                    if (products[i].Product_Type.Equals("GPU")) // אם המוצר שמחקנו הוא מסוג כרטיס מסך
                    {

                        products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר הוספת מוצר 
                        OleDbCommand command2 = new OleDbCommand();
                        connection2.Open();
                        command2.Connection = connection2;
                        string query2 = "select * from Products ";
                        command2.CommandText = query2;
                        cboxCustomerProductGPU.Items.Clear();

                        for (int j = 0; j < products1.Length; j++)
                            if (products1[j].Product_Type.Equals("GPU"))
                                cboxCustomerProductGPU.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם


                        connection2.Close();

                        cboxCustomerProductGPU.Sorted = true; // מיון קומבו בוקס לאחר הוספת מוצר 
                    }

                    if (products[i].Product_Type.Equals("MB")) // אם המוצר שמחקנו הוא מסוג לוח אם
                    {

                        products1 = dataB.GetProductsData(); // עדכון רשימת מוצרים לאחר הוספת מוצר 
                        OleDbCommand command2 = new OleDbCommand();
                        connection2.Open();
                        command2.Connection = connection2;
                        string query2 = "select * from Products ";
                        command2.CommandText = query2;
                        cboxCustomerProducMotherBoard.Items.Clear();

                        for (int j = 0; j < products1.Length; j++)
                            if (products1[j].Product_Type.Equals("MB"))
                                cboxCustomerProducMotherBoard.Items.Add(products1[j].Proudct_ID.ToString()); //   הוספת נתונים לקומבו בוקס של ראם


                        connection2.Close();

                        cboxCustomerProducMotherBoard.Sorted = true; // מיון קומבו בוקס לאחר מחיקת מוצר 
                    }
                }
            }
        }

        private void cboxProductList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Products[] product = dataB.GetProductsData();
            int ProductLength = product.Length;//שומר את אורך רשימת המוצרים

            if (ProductLength > 0)//אם אורך רשימת המוצרים גדול מ0
            {
                for (int i = 0; i < product.Length; i++)
                {


                    if (cboxProductList.SelectedItem.ToString().Equals(product[i].Proudct_ID.ToString())) // בדיקה האם הספק קיים לפי ה ת.ז שבקומבו בוקס
                    {
                        txtboxAddProductSerialNum.Text = product[i].Proudct_ID.ToString();
                        cboxAddProductSupplierIdentity.Text = product[i].Product_SupplierIdentifier.ToString();
                        cboxAddProductType.Text = product[i].Product_Type.ToString();
                        cboxAddProductManufacturer.Text = product[i].Product_Manufacturer.ToString();
                        txtboxAddProductName.Text = product[i].Product_Name.ToString();
                        txtboxAddProductDescription.Text = product[i].Product_Description.ToString();
                        txtboxAddProductPrice.Text = product[i].Product_Price.ToString();
                        txtboxAddProductInStock.Text = product[i].Product_Stock.ToString();

                    }

                }

            }
        }

        private void txtboxSearchOrder_TextChanged(object sender, EventArgs e)
        {

            CustomersOrdersDetail[] cusorderdet = dataB.GetOrderDetailData();

            DataView dv1 = new DataView(tabSearchCustomerOrder1);
            dv1.RowFilter = string.Format("CONVERT({0},System.String) LIKE '%{1}%'", "ID", txtboxSearchOrder.Text);
            dgvCustomerOrder.DataSource = dv1;

            if (txtboxSearchOrder.Text != "")
                for (int i = 0; i < cusorderdet.Length; i++)
                {
                    if (dataB.isNumber(txtboxSearchOrder.Text) == false)
                    {
                        Error.SetError(txtboxSearchOrder, "Error");
                        MessageBox.Show("Error Input Please Try Again", "Error");
                        txtboxSearchOrder.Text = "";
                        Error.Clear();
                        return;
                    }

                    if (cusorderdet[i].CustomerOrder_ClientID.Equals(int.Parse(txtboxSearchOrder.Text)))
                    {
                        string PathDB6 = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection6 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB6 + ";Persist Security Info=False;");
                        OleDbDataAdapter ada6 = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail WHERE CustomerID =" + txtboxSearchOrder.Text, connection6);
                        DataSet set6 = new DataSet();
                        ada6.Fill(set6, "CustomersOrdersDetail");
                        tabSearchCustomerOrder = new DataTable();
                        tabSearchCustomerOrder = set6.Tables["CustomersOrdersDetail"];
                        dgvCustomerOrderDetail.DataSource = tabSearchCustomerOrder;
                        dgvCustomerOrderDetail.Sort(dgvCustomerOrderDetail.Columns["OrderID"], ListSortDirection.Ascending);
                        dgvCustomerOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    }

                }
        }
        private void txtboxAddProductSerialNum_TextChanged(object sender, EventArgs e)
        {
            Products p = new Products();
            Products[] products = dataB.GetProductsData();
            int flag = 0;

            if (dataB.isNumber(txtboxAddProductSerialNum.Text) == false)
            {
                Error.SetError(txtboxCustomerID, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxCustomerID.Text = "";
                Error.Clear();
            }

            cboxAddProductSupplierIdentity.Enabled = true;
            cboxAddProductType.Enabled = true;
            cboxAddProductManufacturer.Enabled = true;
            txtboxAddProductName.ReadOnly = false;
            txtboxAddProductDescription.ReadOnly = false;

            for (int i = 0; i < products.Length; i++)
            {
                if (!txtboxAddProductSerialNum.Text.Equals(products[i].Proudct_ID.ToString())) // בדיקה שהמס' מזהה  של המוצר  לא קיים במאגר 
                {
                    cboxProductList.Text = "";
                    txtboxAddProductName.Text = "";
                    cboxAddProductType.Text = "";
                    cboxAddProductManufacturer.Text = "";
                    cboxAddProductSupplierIdentity.Text = "";
                    txtboxAddProductDescription.Text = "";
                    txtboxAddProductPrice.Text = "";
                    txtboxAddProductInStock.Text = "";

                }

                if (txtboxAddProductSerialNum.Text.Equals(products[i].Proudct_ID.ToString())) // בדיקה שהמס' מזהה  של המוצר קיים במאגר 
                {
                    txtboxAddProductSerialNum.Text = products[i].Proudct_ID.ToString();
                    txtboxAddProductName.Text = products[i].Product_Name.ToString();
                    cboxAddProductType.Text = products[i].Product_Type.ToString();
                    cboxAddProductManufacturer.Text = products[i].Product_Manufacturer.ToString();
                    cboxAddProductSupplierIdentity.Text = products[i].Product_SupplierIdentifier.ToString();
                    txtboxAddProductDescription.Text = products[i].Product_Description.ToString();
                    txtboxAddProductPrice.Text = products[i].Product_Price.ToString();
                    txtboxAddProductInStock.Text = products[i].Product_Stock.ToString();
                    flag = 1;
                }



                if (flag == 1)
                {
                    cboxAddProductSupplierIdentity.Enabled = false;
                    cboxAddProductType.Enabled = false;
                    cboxAddProductManufacturer.Enabled = false;
                    txtboxAddProductName.ReadOnly = true;
                    txtboxAddProductDescription.ReadOnly = true;
                    return;
                }



            }
        }
        private void txtboxAddProductPrice_TextChanged(object sender, EventArgs e)

        {
            if (dataB.isNumber(txtboxAddProductPrice.Text) == false)
            {
                Error.SetError(txtboxAddProductPrice, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxAddProductPrice.Text = "";
                Error.Clear();
            }
        }

        private void txtboxAddProductInStock_TextChanged(object sender, EventArgs e)
        {
            if (dataB.isNumber(txtboxAddProductInStock.Text) == false)
            {
                Error.SetError(txtboxAddProductInStock, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxAddProductInStock.Text = "";
                Error.Clear();
            }
        }

        private void btnResetData_Click(object sender, EventArgs e) // לצורך בדיקות , למחוק שנסיים
        {
            Products[] products = dataB.GetProductsData();
            dataB.RemoveData();
            dataB.UpdateProductStockTo10(products, 10);
            if (MessageBox.Show(" Test Order From Supplier ?  ", "Exit", MessageBoxButtons.YesNo) ==
                     DialogResult.Yes)
                dataB.UpdateProductStockTo10(products, 0);

            // רענון טבלת הזמנות מספק  בלשונית הזמנות  לאחר הזמנת מוצר
            string PathDB3 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection3 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB3 + ";Persist Security Info=False;");
            OleDbDataAdapter ada3 = new OleDbDataAdapter("SELECT * FROM SuppliersOrders", connection3);
            DataSet set3 = new DataSet();
            ada3.Fill(set3, "SuppliersOrders");
            DataTable tab3 = new DataTable();
            tab3 = set3.Tables["SuppliersOrders"];
            dgvSupplierOrderDetail.DataSource = tab3;
            dgvSupplierOrderDetail.Sort(dgvSupplierOrderDetail.Columns["Supplier_ID"], ListSortDirection.Ascending);
            dgvSupplierOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון טבלת מוצרים בלשונית הזמנות לאחר הוספת/עדכון מוצר
            string PathDB1 = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB1 + ";Persist Security Info=False;");
            OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM Products", connection1);
            DataSet set1 = new DataSet();
            ada1.Fill(set1, "Products");
            DataTable tab1 = new DataTable();
            tab1 = set1.Tables["Products"];
            dgvProductOrder.DataSource = tab1;
            dgvProductOrder.Sort(dgvProductOrder.Columns["PRODUCT_ID"], ListSortDirection.Ascending);
            dgvProductOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // רענון טבלת הזמנות בלשונית הזמנות  לאחר הזמנת מוצר
            string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM CustomersOrdersDetail", connection);
            DataSet set = new DataSet();
            ada.Fill(set, "CustomersOrdersDetail");
            DataTable tab = new DataTable();
            tab = set.Tables["CustomersOrdersDetail"];
            dgvCustomerOrderDetail.DataSource = tab;
            dgvCustomerOrderDetail.Sort(dgvCustomerOrderDetail.Columns["OrderID"], ListSortDirection.Ascending);
            dgvCustomerOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }


        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)  // כפתור התנתקות מהמערכת 
        {
            if (MessageBox.Show("  Are You Sure You Want To Log Out From The System ?  ", "Log Out", MessageBoxButtons.YesNo) ==
           DialogResult.Yes)
            {
                frmLogIn log = new frmLogIn();
                log.Show();
                this.Visible = false;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) // כפתור יציאה מהמערכת 
        {
            if (MessageBox.Show("  Are You Sure You Want To Exit ?  ", "Exit", MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void picboxUser_Click(object sender, EventArgs e) // תפריט גישה לעובד 
        {
            Point point = new Point(10, 75);
            btnPopUpMenu.Visible = true;
            btnPopUpMenu.Show(point);
        }

        private void txtboxSearchWorker_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isNumber(txtboxSearchWorker.Text) == false)
            {
                Error.SetError(txtboxSearchWorker, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxSearchWorker.Text = "";
                Error.Clear();
            }


            DataView dv = new DataView(tabSearchWorker);
            dv.RowFilter = string.Format("CONVERT({0},System.String) LIKE '%{1}%'", "ID", txtboxSearchWorker.Text);
            dgvWorkers.DataSource = dv;
        }

        private void txtboxWorkerID_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            Workers w = new Workers();
            Workers[] worker = dataB.GetWorkerData();
            int flag = 0;

            if (dataB.isNumber(txtboxWorkerID.Text) == false)
            {
                Error.SetError(txtboxWorkerID, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxWorkerID.Text = "";
                Error.Clear();
            }

            for (int i = 0; i < worker.Length; i++)
            {
                if (!txtboxWorkerID.Text.Equals(worker[i].Worker_ID.ToString())) // בדיקה שת.ז של העובד לא קיים במאגר 
                {
                    txtboxWorkerName.Text = "";
                    txtboxWorkerUserName.Text = "";
                    txtboxWorkerPassword.Text = "";
                    cboxWorkerList.Text = "";

                }

                if (txtboxWorkerID.Text.Equals(worker[i].Worker_ID.ToString())) // בדיקה שת.ז של העובד קיים במאגר 
                {
                    txtboxWorkerID.Text = worker[i].Worker_ID.ToString();
                    txtboxWorkerName.Text = worker[i].Worker_Name.ToString();
                    txtboxWorkerUserName.Text = worker[i].Worker_UserName.ToString();
                    txtboxWorkerPassword.Text = worker[i].Worker_Password.ToString();
                    flag = 1;
                }

                if (flag == 1)
                    return;
            }
        }

        private void txtboxWorkerName_TextChanged(object sender, EventArgs e) // בדיקת קלט לפי סוג הטקסט שמוזן 
        {
            if (dataB.isLetter(txtboxWorkerName.Text) == false)
            {
                Error.SetError(txtboxWorkerName, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxWorkerName.Text = "";
                Error.Clear();
            }
        }



        private void btnAddWorkerDialog_Click(object sender, EventArgs e) // הוספת/עדכון עובד 
        {
            Workers w = new Workers();
            Workers[] worker = dataB.GetWorkerData();
            int flag = 0;
            SuppliersOrders sorder = new SuppliersOrders();
            int WorkerLength = worker.Length;//שומר את אורך רשימת העובדים
            string PathDB2 = Application.StartupPath + @"\PcStore.ACCDB"; // משתנה שמכיל את המיקום של קובץ הבסיס נתונים שלנו
            OleDbConnection connection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB2 + ";Persist Security Info=False;"); // משתנה להתחברות לבסיס נתונים 


            // עדכון עובד 

            if (WorkerLength > 0)//אם אורך רשימת העובדים גדול מ0
            {

                if (txtboxWorkerID.Text == "" || txtboxWorkerName.Text == "" || txtboxWorkerUserName.Text == "" || txtboxWorkerPassword.Text == "") // בדיקת תקינות 
                {

                    MessageBox.Show("Please Fill All The Fields", "Error");
                    txtboxWorkerID.Text = "";
                    txtboxWorkerName.Text = "";
                    txtboxWorkerUserName.Text = "";
                    txtboxWorkerPassword.Text = "";
                    return;
                }

                for (int i = 0; i < worker.Length; i++)
                {

                    if (txtboxWorkerID.Text.Equals(worker[i].Worker_ID.ToString())) // בדיקה האם העובד קיים לפי ה ת.ז 
                    {

                        for (int j = 0; j < worker.Length; j++)
                        {
                            if (txtboxWorkerUserName.Text.Equals(worker[j].Worker_UserName)) // בדיקה שהשם משתמש של העובד שמעדכנים לא קיים כבר  
                            {
                                MessageBox.Show("Please Choose Another User Name", "User Name Exists");
                                txtboxWorkerID.Text = "";
                                txtboxWorkerName.Text = "";
                                txtboxWorkerUserName.Text = "";
                                txtboxWorkerPassword.Text = "";
                                cboxWorkerList.Text = "";
                                return;
                            }
                        }

                        w.Worker_ID = worker[i].Worker_ID;
                        w.Worker_Name = txtboxWorkerName.Text;
                        dataB.UpdateWorkerName(w);
                        w.Worker_Password = txtboxWorkerPassword.Text;
                        dataB.UpdateWorkerPassword(w);
                        sorder.SupplierOrders_Name = txtboxSupplierName.Text;
                        MessageBox.Show("Worker Updated Successfully", "Success");
                        txtboxWorkerID.Text = "";
                        txtboxWorkerName.Text = "";
                        txtboxWorkerUserName.Text = "";
                        txtboxWorkerPassword.Text = "";
                        cboxWorkerList.Text = "";

                        //  רענון טבלה לאחר הוספת/עדכון עובד בלשונית עובדים
                        string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                        OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Workers", connection);
                        DataSet set = new DataSet();
                        ada.Fill(set, "Workers");
                        DataTable tab = new DataTable();
                        tab = set.Tables["Workers"];
                        dgvWorkers.DataSource = tab;


                        return;
                    }


                }

            }

            // הוספת עובד חדש למאגר העובדים 



            if (txtboxWorkerID.Text == "" || txtboxWorkerName.Text == "" || txtboxWorkerUserName.Text == "" || txtboxWorkerPassword.Text == "") // בדיקת תקינות 
            {

                MessageBox.Show("Please Fill All The Fields", "Error");
                txtboxWorkerID.Text = "";
                txtboxWorkerName.Text = "";
                txtboxWorkerUserName.Text = "";
                txtboxWorkerPassword.Text = "";
                cboxWorkerList.Text = "";
                return;
            }


            for (int i = 0; i < worker.Length; i++)
            {
                if (int.Parse(txtboxWorkerID.Text).Equals(worker[i].Worker_ID)) // בדיקה שהעובד לא קיים במאגר העובדים 
                    flag = 1;
            }

            if (flag == 0) // בדיקת שהעובד לא קיים במאגר העובדים 
            {
                if (txtboxWorkerID.Text == "" || txtboxWorkerName.Text == "" || txtboxWorkerUserName.Text == "" || txtboxWorkerPassword.Text == "") // בדיקת תקינות הוספת עובד חדש 
                {
                    MessageBox.Show("Please Fill All The Fields", "Error");
                    txtboxWorkerID.Text = "";
                    txtboxWorkerName.Text = "";
                    txtboxWorkerUserName.Text = "";
                    txtboxWorkerPassword.Text = "";
                    cboxWorkerList.Text = "";
                    return;
                }

                for (int i = 0; i < worker.Length; i++)
                {
                    if (txtboxWorkerUserName.Text.Equals(worker[i].Worker_UserName)) // בדיקה שהשם משתמש של העובד שמוסיפים לא קיים  
                    {
                        MessageBox.Show("Please Choose Another User Name", "User Name Exists");
                        txtboxWorkerID.Text = "";
                        txtboxWorkerName.Text = "";
                        txtboxWorkerUserName.Text = "";
                        txtboxWorkerPassword.Text = "";
                        cboxWorkerList.Text = "";
                        return;
                    }
                }

                w.Worker_ID = int.Parse(txtboxWorkerID.Text);
                w.Worker_Name = txtboxWorkerName.Text;
                w.Worker_UserName = txtboxWorkerUserName.Text;
                w.Worker_Password = txtboxWorkerPassword.Text;
                dataB.AddWorker(w);
                MessageBox.Show("Worker Added Successfuly!", "Success");
                txtboxWorkerID.Text = "";
                txtboxWorkerName.Text = "";
                txtboxWorkerUserName.Text = "";
                txtboxWorkerPassword.Text = "";
                cboxWorkerList.Text = "";

                // רענון טבלה לאחר הוספת/עדכון עובד
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Workers", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "Workers");
                DataTable tab = new DataTable();
                tab = set.Tables["Workers"];
                dgvWorkers.DataSource = tab;

            }

            else // העובד קיים כבר ולכן נציג הודעת שגיאה

            {
                MessageBox.Show("The Worker Already Exists", "Error");
                txtboxWorkerID.Text = "";
                txtboxWorkerName.Text = "";
                txtboxWorkerUserName.Text = "";
                txtboxWorkerPassword.Text = "";
                cboxWorkerList.Text = "";
                return;
            }

            OleDbCommand command1 = new OleDbCommand();
            connection2.Open();
            command1.Connection = connection2;
            string query1 = "select * from Workers";
            command1.CommandText = query1;
            cboxWorkerList.Items.Clear();
            OleDbDataReader reader2 = command1.ExecuteReader();
            while (reader2.Read())
            {
                cboxWorkerList.Items.Add(reader2["ID"].ToString()); // הוספת נתונים לקומבו בוקס

            }

            connection2.Close();

            cboxWorkerList.Sorted = true; // מיון קומבו בוקס לאחר הוספת/עדכון עובד 

        }

        private void btnRemoveWorker_Click(object sender, EventArgs e) // מחיקת עובד 
        {
            if (MessageBox.Show(" Are You Sure You Want To Remove This Worker ?  ", "Remove Worker", MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            {
                Workers w = new Workers();
                string PathDB = Application.StartupPath + @"\PcStore.ACCDB";
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");


                if (dgvWorkers.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvWorkers.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dgvWorkers.Rows[selectedrowindex];

                    if (Convert.ToString(selectedRow.Cells["ID"].Value) == "") // בדיקה שהשורה ב DGV לא ריקה
                    {
                        MessageBox.Show("Please Choose A Worker", "Error");
                        return;
                    }

                    string value = Convert.ToString(selectedRow.Cells["ID"].Value);
                    w.Worker_ID = int.Parse(value);
                    dataB.RemoveWorker(w);
                    MessageBox.Show("Worker Removed", "Success");
                }



                // רענון טבלה לאחר מחיקת עובד
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM Workers", connection);
                DataSet set = new DataSet();
                ada.Fill(set, "Workers");
                DataTable tab = new DataTable();
                tab = set.Tables["Workers"];
                dgvWorkers.DataSource = tab;
                dgvWorkers.Sort(dgvWorkers.Columns["ID"], ListSortDirection.Ascending);
                dgvWorkers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                // עדכון קומבו בוקס לאחר מחיקת עובד

                OleDbCommand command = new OleDbCommand();
                connection.Open();
                command.Connection = connection;
                string query = "select * from Workers";
                command.CommandText = query;
                cboxWorkerList.Items.Clear();
                OleDbDataReader reader1 = command.ExecuteReader();
                while (reader1.Read())
                {
                    cboxWorkerList.Items.Add(reader1["ID"].ToString()); // הוספת נתונים לקומבו בוקס

                }

                connection.Close();

                cboxWorkerList.Sorted = true; // מיון קומבו בוקס לאחר מחיקת ספק 

            }
        }

        private void cboxWorkerList_SelectedIndexChanged(object sender, EventArgs e) // טעינת פרטי העובד לטקסט בוקס המתאים לצורך עדכון 
        {
            Workers[] worker = dataB.GetWorkerData();
            int WorkerLength = worker.Length;//שומר את אורך רשימת העובדים

            if (WorkerLength > 0)//אם אורך רשימת העובדים גדול מ0
            {
                for (int i = 0; i < worker.Length; i++)
                {


                    if (cboxWorkerList.SelectedItem.ToString().Equals(worker[i].Worker_ID.ToString())) // בדיקה האם העובד קיים לפי ה ת.ז שבקומבו בוקס
                    {
                        txtboxWorkerID.Text = worker[i].Worker_ID.ToString();
                        txtboxWorkerName.Text = worker[i].Worker_Name.ToString();
                        txtboxWorkerUserName.Text = worker[i].Worker_UserName.ToString();
                        txtboxWorkerPassword.Text = worker[i].Worker_Password.ToString();
                    }

                }

            }
        }



     
        

        private void dgvWorkers_CurrentCellDirtyStateChanged(object sender, EventArgs e) // מתן/אי מתן הרשאות מנהל
        {
            int columnIndex = dgvWorkers.CurrentCell.ColumnIndex;
            DataGridViewRow selectedRow = dgvWorkers.Rows[dgvWorkers.SelectedCells[0].RowIndex];
            Workers worker = new Workers();
            Workers[] worker1 = dataB.GetWorkerData();
            Boolean checkManagerStatus = bool.Parse(dgvWorkers.SelectedCells[0].Value.ToString()); // משתנה שבודק האם העובד הוא  מנהל או עובד 

            if (dgvWorkers.Columns[columnIndex].Name == "Manager" && checkManagerStatus == false) // הפיכת עובד למנהל
            {
                worker.Worker_ID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                worker.Manager_IsAManager = true;
                dataB.UpdateWorkerToManger(worker);
                MessageBox.Show("Successfully Changed" + worker.Worker_Name + "  To Manager");
            }

            else
            {

                int count = 0;

                for (int i = 0; i < worker1.Length; i++)
                    if (worker1[i].Manager_IsAManager == true)
                        count++;

                if (count == 1) // בדיקה שאם יש רק מנהל אחד, אין אפשרות להפוך אותו לעובד כי אחרת לעולם לא יהיו לנו מנהלים בחנות 
                {
                    MessageBox.Show("Unable To Switch From Manager To Worker ");
                    DataGridViewCheckBoxCell cell = this.dgvWorkers.CurrentCell as DataGridViewCheckBoxCell;
                    cell.Value = true;
                    this.dgvWorkers.RefreshEdit();
                    return;
                }

                worker.Worker_ID = Convert.ToInt32(selectedRow.Cells["ID"].Value); // הפיכת מנהל לעובד
                worker.Manager_IsAManager = false;
                dataB.UpdateWorkerToManger(worker);
                MessageBox.Show("Successfully Changed" + worker.Worker_Name + "  To Worker");
            }
        }

        
    }

}

    


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
using System.Text.RegularExpressions;

namespace Store
{
    public partial class frmLogIn : Form // טופס מסך התחברות למערכת
    {
        private OleDbConnection connection = new OleDbConnection(); // משתנה להתחברות לבסיס נתונים 
        private static string PathDB = Application.StartupPath + @"\PcStore.ACCDB"; // משתנה שמכיל את המיקום של קובץ הבסיס נתונים שלנו
        private DBSQL dataB = new DBSQL(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathDB + ";Persist Security Info=False;");
        private System.Media.SoundPlayer player = new SoundPlayer();
        public static string checkmanager; // משתנה שעוזר לנו לדעת האם העובד הוא מנהל הסניף
        public static string checkuser; // משתנה שעוזר לנו לדעת איזה עובד  התחבר למערכת

        public frmLogIn() // הגדרות ברירת מחדל ושינוי גודל החלון למסך מלא 
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void btnLogIn_Click(object sender, EventArgs e) // בדיקה האם פרטי ההתחברות  של העובד נכונים
        {
            Workers[] worker = dataB.GetWorkerData();
            checkmanager = txtboxLogIn.Text;
            checkuser= txtboxLogIn.Text;
            lblPasswordWarningMessage.Location = new Point(758, 717);
            int flag = 0; // משתנה עזר לבדיקה האם העובד נמצא במאגר העובדים
            int WorkerLength = worker.Length;//שומר את אורך רשימת העובדים

            if ( txtboxPassword.Text == "") // בדיקה שהזנו שם משתמש וגם סיסמא 
            {
                MessageBox.Show("Please Enter A Password ", "Error");
                return;
            }

            if (WorkerLength > 0)//אם אורך רשימת העובדים גדול מ0
            {
                for (int i = 0; i < worker.Length; i++)
                {

                    if (txtboxLogIn.Text != "" && txtboxPassword.Text != "")
                    {
                        if (txtboxLogIn.Text.Equals(worker[i].Worker_UserName) && txtboxPassword.Text.Equals(worker[i].Worker_Password)) // בדיקה שהעובד שמנסה להתחבר קיים במאגר העובדים
                            flag = 1;
                    }

                }

            }
            if (flag == 1) // העובד קיים במאגר העובדים ולכן הוא יקבל גישה למערכת  
            {

                player.SoundLocation = Application.StartupPath + @"\sound\access_granted.wav";
                player.Load();
                player.PlaySync();
                txtboxLogIn.Text = "";
                txtboxPassword.Text = "";
                this.Visible = false;
                picboxBack.Visible = false;
                txtboxPassword.Visible = false;
                btnLogIn.Visible = false;
                frmPcStore pc = new frmPcStore();
                pc.Show();
            }

            else // העובד לא קיים במאגר העובדים ולכן הוא  לא יקבל גישה למערכת  
            {
                player.SoundLocation = Application.StartupPath + @"\sound\access_denied.wav";
                player.Load();
                player.PlaySync();
                txtboxPassword.Text = "";
                lblPasswordWarningMessage.Text = "                 Password Not Found , Please Try Again";
                lblPasswordWarningMessage.BackColor = Color.Transparent;
                lblPasswordWarningMessage.ForeColor = Color.Red;
                lblPasswordWarningMessage.Font = new Font(lblPasswordWarningMessage.Font, FontStyle.Bold);
                lblPasswordWarningMessage.Font = new Font("", 14);
                return;

            }

               
        }

        private void txtboxPassword_TextChanged(object sender, EventArgs e) // בדיקת תקינות שדה שדה סיסמא 
        {
            txtboxPassword.PasswordChar = '*';

            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                MessageBox.Show("The Caps Lock key is ON.","Warning");
            }

            lblPasswordWarningMessage.Text = "";

        }
        private void frmLogIn_Load(object sender, EventArgs e) // טעינת טופס התחברות למערכת
        {
            picboxLogInPicture.BackColor= System.Drawing.Color.Transparent;
            picboxNext.BackColor= System.Drawing.Color.Transparent;
            picboxBack.BackColor = System.Drawing.Color.Transparent;
            picboxBack.Visible = false;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(picboxLogInPicture.DisplayRectangle);
            picboxLogInPicture.Region = new Region(gp);
            
        }

        private void txtboxLogIn_TextChanged(object sender, EventArgs e) // בדיקת תקינות שדה שם משתמש של עובד 
        {
            if (!Regex.IsMatch(txtboxLogIn.Text, @"^[a-zA-Z0-9_\s]*$"))
            {
                Error.SetError(txtboxLogIn, "Error");
                MessageBox.Show("Error Input Please Try Again", "Error");
                txtboxLogIn.Text = "";
                Error.Clear();
            }


            lblUserNameWarningMessage.Text = "";
        }

  
        private void picboxBack_Click(object sender, EventArgs e) // כפתור חזרה לבחירת משתמש 
        {
            picboxBack.Visible = false;
            txtboxPassword.Visible = false;
            btnLogIn.Visible = false;
            txtboxLogIn.Visible = true;
            picboxNext.Visible = true;
            txtboxLogIn.Text = "Enter User Name";
            picboxLogInPicture.BackgroundImage = Image.FromFile(Application.StartupPath + @"\pictures\userlogin.png");
            txtboxPassword.Text= "";
            lblPasswordWarningMessage.Text = "";

        }

        private void picboxNext_Click(object sender, EventArgs e) // כפתור מעבר להזנת סיסמת עובד על מנת להכנס למערכת
        {
            Workers w = new Workers();
            Workers[] worker = dataB.GetWorkerData();
            int flag = 0; // משתנה עזר לבדיקה האם העובד קיים במאגר העובדים 
            string[] filename = new string[1]; // משתנה עזר למציאת המסלול של הקובץ 
            string filenameExt; // משתנה למציאת סיומת של הקובץ
            for (int i = 0; i < worker.Length; i++)
            {

                if (txtboxLogIn.Text.Equals(worker[i].Worker_UserName.ToString())) // בדיקה שהעובד קיים במאגר 
                {
                    filename = Directory.GetFiles(Application.StartupPath + @"\pictures\users\", worker[i].Worker_ID + "*");


                    if (filename == null || filename.Length == 0) // אם לעובד אין תמונה אז נשים לו תמונת ברירת מחדל
                    {
                        filename = Directory.GetFiles(Application.StartupPath + @"\pictures\users\", "defaultworker.png");
                        filenameExt = Path.GetExtension(filename[0]);
                        picboxLogInPicture.BackgroundImage = Image.FromFile(Application.StartupPath + @"\pictures\users\defaultworker" + filenameExt);
                        flag = 1;

                    }
                    else
                    {
                        filenameExt = Path.GetExtension(filename[0]);
                        picboxLogInPicture.BackgroundImage = Image.FromFile(Application.StartupPath + @"\pictures\users\" + worker[i].Worker_ID + filenameExt);
                        flag = 1;
                    }

                }

            }

            if (flag == 0)  // העובד לא קיים במאגר העובדים 
            {
                txtboxLogIn.Text = "";
                lblUserNameWarningMessage.Text = "                  User Not Found , Please Try Again";
                lblUserNameWarningMessage.BackColor = Color.Transparent;
                lblUserNameWarningMessage.ForeColor = Color.Red;
                lblUserNameWarningMessage.Font = new Font(lblUserNameWarningMessage.Font, FontStyle.Bold);
                lblUserNameWarningMessage.Font = new Font("", 14);
                return;
            }

            picboxBack.Visible = true;
            txtboxPassword.Visible = true;
            btnLogIn.Visible = true;
            txtboxLogIn.Visible = false;
            picboxNext.Visible = false;
            txtboxPassword.Location = new Point(768, 740);
            picboxBack.Location = new Point(670, 710);
            btnLogIn.Location = new Point(883, 793);

        }

        private void txtboxLogIn_KeyDown(object sender, KeyEventArgs e) // התחברות למערכת באמצעות הקשת על מקש אנטר
        {
            if (e.KeyCode == Keys.Enter)
            {
                picboxNext_Click((object)sender, (EventArgs)e);
            }
        }

        private void txtboxPassword_KeyDown(object sender, KeyEventArgs e) // התחברות למערכת באמצעות הקשת על מקש אנטר
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogIn_Click((object)sender, (EventArgs)e);
            }
        }

        private void txtboxLogIn_Click(object sender, EventArgs e) // איפוס שדה שם משתמש ברגע שרוצים להזין שם משתמש 
        {
            txtboxLogIn.Text = "";

        }

        private void txtboxPassword_Click(object sender, EventArgs e) // איפוס שדה סיסמא ברגע שרוצים להזין סיסמא 
        {
            txtboxPassword.Text = "";
        }
    }
}

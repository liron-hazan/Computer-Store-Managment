//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Workers // מחלקה שמטפלת בעובד
    {
        private int     WorkerID;          //תעודת זהות של עובד 
        private string  WorkerName;        //שם העובד 
        private string  WorkerUserName;    //שם משתמש של העובד
        private string  WorkerPassword;    //סיסמא של העובד
        private Boolean WorkerIsAManager;  //משתנה שבודק האם העובד הוא מנהל

        // getters and setters למשתני המחלקה

        public int Worker_ID
        {
            get
            {
                return WorkerID;
            }
            set
            {
                if (value > 0)
                    WorkerID = value;
            }
        }

        public string Worker_Name
        {
            get
            {
                return WorkerName;
            }
            set
            {
                WorkerName = value;
            }
        }

        public string Worker_UserName
        {
            get
            {
                return WorkerUserName;
            }
            set
            {
                WorkerUserName = value;
            }
        }

        public string Worker_Password
        {
            get
            {
                return WorkerPassword;
            }
            set
            {
                WorkerPassword = value;
            }
        }

        public Boolean Manager_IsAManager
        {
            get
            {
                return WorkerIsAManager;
            }
            set
            {

                WorkerIsAManager = value;
            }
        }


    }
}

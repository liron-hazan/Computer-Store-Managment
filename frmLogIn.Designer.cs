namespace Store
{
    partial class frmLogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogIn));
            this.txtboxPassword = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.picboxLogInPicture = new System.Windows.Forms.PictureBox();
            this.lblPasswordWarningMessage = new System.Windows.Forms.Label();
            this.picboxNext = new System.Windows.Forms.PictureBox();
            this.picboxBack = new System.Windows.Forms.PictureBox();
            this.txtboxLogIn = new System.Windows.Forms.TextBox();
            this.lblUserNameWarningMessage = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblWelcomeUser = new System.Windows.Forms.Label();
            this.Error = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picboxLogInPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Error)).BeginInit();
            this.SuspendLayout();
            // 
            // txtboxPassword
            // 
            this.txtboxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtboxPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.txtboxPassword.Location = new System.Drawing.Point(768, 793);
            this.txtboxPassword.Name = "txtboxPassword";
            this.txtboxPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtboxPassword.Size = new System.Drawing.Size(472, 29);
            this.txtboxPassword.TabIndex = 7;
            this.txtboxPassword.Text = "Enter Password";
            this.txtboxPassword.Visible = false;
            this.txtboxPassword.Click += new System.EventHandler(this.txtboxPassword_Click);
            this.txtboxPassword.TextChanged += new System.EventHandler(this.txtboxPassword_TextChanged);
            this.txtboxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtboxPassword_KeyDown);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogIn.BackgroundImage = global::Store.Properties.Resources.login;
            this.btnLogIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogIn.Font = new System.Drawing.Font("Arial", 12F);
            this.btnLogIn.ForeColor = System.Drawing.Color.Brown;
            this.btnLogIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLogIn.Location = new System.Drawing.Point(889, 845);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(230, 70);
            this.btnLogIn.TabIndex = 8;
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Visible = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // picboxLogInPicture
            // 
            this.picboxLogInPicture.BackgroundImage = global::Store.Properties.Resources.userlogin;
            this.picboxLogInPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picboxLogInPicture.Location = new System.Drawing.Point(768, 284);
            this.picboxLogInPicture.Name = "picboxLogInPicture";
            this.picboxLogInPicture.Size = new System.Drawing.Size(472, 450);
            this.picboxLogInPicture.TabIndex = 11;
            this.picboxLogInPicture.TabStop = false;
            // 
            // lblPasswordWarningMessage
            // 
            this.lblPasswordWarningMessage.AutoSize = true;
            this.lblPasswordWarningMessage.Location = new System.Drawing.Point(772, 774);
            this.lblPasswordWarningMessage.Name = "lblPasswordWarningMessage";
            this.lblPasswordWarningMessage.Size = new System.Drawing.Size(0, 13);
            this.lblPasswordWarningMessage.TabIndex = 15;
            // 
            // picboxNext
            // 
            this.picboxNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picboxNext.BackgroundImage")));
            this.picboxNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picboxNext.Location = new System.Drawing.Point(1241, 710);
            this.picboxNext.Name = "picboxNext";
            this.picboxNext.Size = new System.Drawing.Size(95, 83);
            this.picboxNext.TabIndex = 16;
            this.picboxNext.TabStop = false;
            this.picboxNext.Click += new System.EventHandler(this.picboxNext_Click);
            // 
            // picboxBack
            // 
            this.picboxBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picboxBack.BackgroundImage")));
            this.picboxBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picboxBack.Location = new System.Drawing.Point(670, 710);
            this.picboxBack.Name = "picboxBack";
            this.picboxBack.Size = new System.Drawing.Size(95, 83);
            this.picboxBack.TabIndex = 17;
            this.picboxBack.TabStop = false;
            this.picboxBack.Click += new System.EventHandler(this.picboxBack_Click);
            // 
            // txtboxLogIn
            // 
            this.txtboxLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtboxLogIn.ForeColor = System.Drawing.Color.DarkGray;
            this.txtboxLogIn.Location = new System.Drawing.Point(768, 740);
            this.txtboxLogIn.Name = "txtboxLogIn";
            this.txtboxLogIn.Size = new System.Drawing.Size(472, 29);
            this.txtboxLogIn.TabIndex = 6;
            this.txtboxLogIn.Text = "Enter User Name";
            this.txtboxLogIn.Click += new System.EventHandler(this.txtboxLogIn_Click);
            this.txtboxLogIn.TextChanged += new System.EventHandler(this.txtboxLogIn_TextChanged);
            this.txtboxLogIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtboxLogIn_KeyDown);
            // 
            // lblUserNameWarningMessage
            // 
            this.lblUserNameWarningMessage.AutoSize = true;
            this.lblUserNameWarningMessage.Location = new System.Drawing.Point(758, 717);
            this.lblUserNameWarningMessage.Name = "lblUserNameWarningMessage";
            this.lblUserNameWarningMessage.Size = new System.Drawing.Size(0, 13);
            this.lblUserNameWarningMessage.TabIndex = 14;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(1010, 774);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblWelcome.Size = new System.Drawing.Size(0, 13);
            this.lblWelcome.TabIndex = 18;
            // 
            // lblWelcomeUser
            // 
            this.lblWelcomeUser.AutoSize = true;
            this.lblWelcomeUser.Location = new System.Drawing.Point(1086, 864);
            this.lblWelcomeUser.Name = "lblWelcomeUser";
            this.lblWelcomeUser.Size = new System.Drawing.Size(0, 13);
            this.lblWelcomeUser.TabIndex = 19;
            // 
            // Error
            // 
            this.Error.ContainerControl = this;
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Store.Properties.Resources.background1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1876, 953);
            this.Controls.Add(this.lblWelcomeUser);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.picboxBack);
            this.Controls.Add(this.picboxNext);
            this.Controls.Add(this.lblPasswordWarningMessage);
            this.Controls.Add(this.lblUserNameWarningMessage);
            this.Controls.Add(this.picboxLogInPicture);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.txtboxPassword);
            this.Controls.Add(this.txtboxLogIn);
            this.Name = "frmLogIn";
            this.Text = "LogIn";
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picboxLogInPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtboxPassword;
        private System.Windows.Forms.PictureBox picboxLogInPicture;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Label lblPasswordWarningMessage;
        private System.Windows.Forms.PictureBox picboxNext;
        private System.Windows.Forms.PictureBox picboxBack;
        private System.Windows.Forms.Label lblUserNameWarningMessage;
        public System.Windows.Forms.TextBox txtboxLogIn;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblWelcomeUser;
        private System.Windows.Forms.ErrorProvider Error;
    }
}
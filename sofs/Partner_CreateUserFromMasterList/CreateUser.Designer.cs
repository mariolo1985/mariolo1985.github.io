namespace Partner_CreateUserFromMasterList
{
    partial class CreateUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_SiteUrl = new System.Windows.Forms.TextBox();
            this.txb_MasterListName = new System.Windows.Forms.TextBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.prog_Create = new System.Windows.Forms.ProgressBar();
            this.txb_output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Partner Root Site Url:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Partner Master List Name:";
            // 
            // txb_SiteUrl
            // 
            this.txb_SiteUrl.Location = new System.Drawing.Point(170, 13);
            this.txb_SiteUrl.Name = "txb_SiteUrl";
            this.txb_SiteUrl.Size = new System.Drawing.Size(450, 20);
            this.txb_SiteUrl.TabIndex = 2;
            this.txb_SiteUrl.Text = "https://partneroasis.";
            this.txb_SiteUrl.TextChanged += new System.EventHandler(this.txb_SiteUrl_TextChanged);
            // 
            // txb_MasterListName
            // 
            this.txb_MasterListName.Location = new System.Drawing.Point(170, 50);
            this.txb_MasterListName.Name = "txb_MasterListName";
            this.txb_MasterListName.Size = new System.Drawing.Size(450, 20);
            this.txb_MasterListName.TabIndex = 3;
            this.txb_MasterListName.Text = "Partner Master";
            this.txb_MasterListName.TextChanged += new System.EventHandler(this.txb_MasterListName_TextChanged);
            // 
            // btn_run
            // 
            this.btn_run.Enabled = false;
            this.btn_run.Location = new System.Drawing.Point(545, 82);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 23);
            this.btn_run.TabIndex = 4;
            this.btn_run.Text = "Run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // prog_Create
            // 
            this.prog_Create.Location = new System.Drawing.Point(15, 438);
            this.prog_Create.Name = "prog_Create";
            this.prog_Create.Size = new System.Drawing.Size(654, 23);
            this.prog_Create.TabIndex = 5;
            // 
            // txb_output
            // 
            this.txb_output.BackColor = System.Drawing.Color.Black;
            this.txb_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_output.ForeColor = System.Drawing.Color.Yellow;
            this.txb_output.Location = new System.Drawing.Point(15, 121);
            this.txb_output.MaxLength = 0;
            this.txb_output.Multiline = true;
            this.txb_output.Name = "txb_output";
            this.txb_output.ReadOnly = true;
            this.txb_output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txb_output.Size = new System.Drawing.Size(654, 300);
            this.txb_output.TabIndex = 6;
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 473);
            this.Controls.Add(this.txb_output);
            this.Controls.Add(this.prog_Create);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.txb_MasterListName);
            this.Controls.Add(this.txb_SiteUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CreateUser";
            this.Text = "Partner - Create User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_SiteUrl;
        private System.Windows.Forms.TextBox txb_MasterListName;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.ProgressBar prog_Create;
        private System.Windows.Forms.TextBox txb_output;
    }
}


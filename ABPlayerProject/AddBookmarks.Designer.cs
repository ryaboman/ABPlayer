namespace ABPlayer {
    partial class AddBookmarks {
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
            this.textBoxDescriptionBookmark = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textBoxNameBookmark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.indicatorTime = new System.Windows.Forms.Label();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.BtnPlay = new RMControls.Player.BtnPlay();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.BtnReverse = new RMControls.Player.BtnReverse();
            this.elementHost3 = new System.Windows.Forms.Integration.ElementHost();
            this.BtnForward = new RMControls.Player.BtnForward();
            this.textBxNameAudiofile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxDescriptionBookmark
            // 
            this.textBoxDescriptionBookmark.Location = new System.Drawing.Point(29, 204);
            this.textBoxDescriptionBookmark.Multiline = true;
            this.textBoxDescriptionBookmark.Name = "textBoxDescriptionBookmark";
            this.textBoxDescriptionBookmark.Size = new System.Drawing.Size(196, 117);
            this.textBoxDescriptionBookmark.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(29, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(150, 342);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxNameBookmark
            // 
            this.textBoxNameBookmark.Location = new System.Drawing.Point(29, 157);
            this.textBoxNameBookmark.Name = "textBoxNameBookmark";
            this.textBoxNameBookmark.Size = new System.Drawing.Size(196, 20);
            this.textBoxNameBookmark.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Наименование";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Описание";
            // 
            // indicatorTime
            // 
            this.indicatorTime.AutoSize = true;
            this.indicatorTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.indicatorTime.Location = new System.Drawing.Point(88, 46);
            this.indicatorTime.Name = "indicatorTime";
            this.indicatorTime.Size = new System.Drawing.Size(77, 24);
            this.indicatorTime.TabIndex = 6;
            this.indicatorTime.Text = "3:02:07";
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(102, 81);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(49, 42);
            this.elementHost1.TabIndex = 9;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.BtnPlay;
            // 
            // elementHost2
            // 
            this.elementHost2.Location = new System.Drawing.Point(29, 81);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(49, 42);
            this.elementHost2.TabIndex = 10;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.BtnReverse;
            // 
            // elementHost3
            // 
            this.elementHost3.Location = new System.Drawing.Point(175, 81);
            this.elementHost3.Name = "elementHost3";
            this.elementHost3.Size = new System.Drawing.Size(49, 42);
            this.elementHost3.TabIndex = 11;
            this.elementHost3.Text = "elementHost3";
            this.elementHost3.Child = this.BtnForward;
            // 
            // textBxNameAudiofile
            // 
            this.textBxNameAudiofile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBxNameAudiofile.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBxNameAudiofile.Location = new System.Drawing.Point(0, 0);
            this.textBxNameAudiofile.Multiline = true;
            this.textBxNameAudiofile.Name = "textBxNameAudiofile";
            this.textBxNameAudiofile.ReadOnly = true;
            this.textBxNameAudiofile.Size = new System.Drawing.Size(255, 42);
            this.textBxNameAudiofile.TabIndex = 13;
            // 
            // AddBookmarks
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(255, 381);
            this.Controls.Add(this.textBxNameAudiofile);
            this.Controls.Add(this.elementHost3);
            this.Controls.Add(this.elementHost2);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.indicatorTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNameBookmark);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textBoxDescriptionBookmark);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(271, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(271, 420);
            this.Name = "AddBookmarks";
            this.Text = "Закладка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDescriptionBookmark;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textBoxNameBookmark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label indicatorTime;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private System.Windows.Forms.Integration.ElementHost elementHost3;
        private RMControls.Player.BtnForward BtnForward;
        private RMControls.Player.BtnReverse BtnReverse;
        private RMControls.Player.BtnPlay BtnPlay;
        private System.Windows.Forms.TextBox textBxNameAudiofile;
    }
}
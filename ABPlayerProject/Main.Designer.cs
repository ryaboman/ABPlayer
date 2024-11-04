namespace ABPlayer {
    partial class Main {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.cmbBxBookmark = new System.Windows.Forms.ComboBox();
            this.textBoxForBookmark = new System.Windows.Forms.TextBox();
            this.addBookmarks = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.elementHost5 = new System.Windows.Forms.Integration.ElementHost();
            this.BtnForward = new RMControls.Player.BtnForward();
            this.elementHost4 = new System.Windows.Forms.Integration.ElementHost();
            this.BtnReverse = new RMControls.Player.BtnReverse();
            this.elementHost3 = new System.Windows.Forms.Integration.ElementHost();
            this.BtnStop = new RMControls.Player.BtnStop();
            this.BtnPlay_userControl = new System.Windows.Forms.Integration.ElementHost();
            this.BtnPlay = new RMControls.Player.BtnPlay();
            this.cmbBxSelectorSpeedPlayback = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameAudiobook = new System.Windows.Forms.Label();
            this.timeValueCurrentAudiofile = new System.Windows.Forms.Label();
            this.timeValueCurrentAudiobook = new System.Windows.Forms.Label();
            this.btnLibrary = new System.Windows.Forms.Button();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.sliderAudiobook = new RMControls.Player.Slider();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.sliderAudiofile = new RMControls.Player.Slider();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 1000;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // cmbBxBookmark
            // 
            this.cmbBxBookmark.FormattingEnabled = true;
            this.cmbBxBookmark.Location = new System.Drawing.Point(55, 184);
            this.cmbBxBookmark.Name = "cmbBxBookmark";
            this.cmbBxBookmark.Size = new System.Drawing.Size(168, 21);
            this.cmbBxBookmark.TabIndex = 6;
            this.cmbBxBookmark.SelectedIndexChanged += new System.EventHandler(this.cmbBxBookmark_SelectedIndexChanged);
            // 
            // textBoxForBookmark
            // 
            this.textBoxForBookmark.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxForBookmark.Location = new System.Drawing.Point(20, 211);
            this.textBoxForBookmark.Multiline = true;
            this.textBoxForBookmark.Name = "textBoxForBookmark";
            this.textBoxForBookmark.ReadOnly = true;
            this.textBoxForBookmark.Size = new System.Drawing.Size(203, 96);
            this.textBoxForBookmark.TabIndex = 7;
            // 
            // addBookmarks
            // 
            this.addBookmarks.Location = new System.Drawing.Point(20, 182);
            this.addBookmarks.Name = "addBookmarks";
            this.addBookmarks.Size = new System.Drawing.Size(29, 23);
            this.addBookmarks.TabIndex = 8;
            this.addBookmarks.Text = "+";
            this.addBookmarks.UseVisualStyleBackColor = true;
            this.addBookmarks.Click += new System.EventHandler(this.addBookmarks_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.elementHost5);
            this.groupBox1.Controls.Add(this.elementHost4);
            this.groupBox1.Controls.Add(this.elementHost3);
            this.groupBox1.Controls.Add(this.BtnPlay_userControl);
            this.groupBox1.Controls.Add(this.cmbBxSelectorSpeedPlayback);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nameAudiobook);
            this.groupBox1.Controls.Add(this.timeValueCurrentAudiofile);
            this.groupBox1.Controls.Add(this.timeValueCurrentAudiobook);
            this.groupBox1.Controls.Add(this.btnLibrary);
            this.groupBox1.Controls.Add(this.elementHost2);
            this.groupBox1.Controls.Add(this.addBookmarks);
            this.groupBox1.Controls.Add(this.elementHost1);
            this.groupBox1.Controls.Add(this.textBoxForBookmark);
            this.groupBox1.Controls.Add(this.cmbBxBookmark);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 374);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // elementHost5
            // 
            this.elementHost5.Location = new System.Drawing.Point(177, 124);
            this.elementHost5.Name = "elementHost5";
            this.elementHost5.Size = new System.Drawing.Size(44, 41);
            this.elementHost5.TabIndex = 24;
            this.elementHost5.Text = "elementHost5";
            this.elementHost5.Child = this.BtnForward;
            // 
            // elementHost4
            // 
            this.elementHost4.Location = new System.Drawing.Point(18, 124);
            this.elementHost4.Name = "elementHost4";
            this.elementHost4.Size = new System.Drawing.Size(44, 41);
            this.elementHost4.TabIndex = 23;
            this.elementHost4.Text = "elementHost4";
            this.elementHost4.Child = this.BtnReverse;
            // 
            // elementHost3
            // 
            this.elementHost3.Location = new System.Drawing.Point(124, 124);
            this.elementHost3.Name = "elementHost3";
            this.elementHost3.Size = new System.Drawing.Size(44, 41);
            this.elementHost3.TabIndex = 22;
            this.elementHost3.Text = "elementHost3";
            this.elementHost3.Child = this.BtnStop;
            // 
            // BtnPlay_userControl
            // 
            this.BtnPlay_userControl.Location = new System.Drawing.Point(71, 124);
            this.BtnPlay_userControl.Name = "BtnPlay_userControl";
            this.BtnPlay_userControl.Size = new System.Drawing.Size(44, 41);
            this.BtnPlay_userControl.TabIndex = 21;
            this.BtnPlay_userControl.Text = "elementHost5";
            this.BtnPlay_userControl.Child = this.BtnPlay;
            // 
            // cmbBxSelectorSpeedPlayback
            // 
            this.cmbBxSelectorSpeedPlayback.FormattingEnabled = true;
            this.cmbBxSelectorSpeedPlayback.Items.AddRange(new object[] {
            "0.5x",
            "0.75x",
            "1.0x",
            "1.25x",
            "1.5x",
            "1.75x",
            "2.0x"});
            this.cmbBxSelectorSpeedPlayback.Location = new System.Drawing.Point(21, 335);
            this.cmbBxSelectorSpeedPlayback.Name = "cmbBxSelectorSpeedPlayback";
            this.cmbBxSelectorSpeedPlayback.Size = new System.Drawing.Size(64, 21);
            this.cmbBxSelectorSpeedPlayback.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Скорость";
            // 
            // nameAudiobook
            // 
            this.nameAudiobook.AutoSize = true;
            this.nameAudiobook.Location = new System.Drawing.Point(17, 13);
            this.nameAudiobook.MaximumSize = new System.Drawing.Size(212, 13);
            this.nameAudiobook.Name = "nameAudiobook";
            this.nameAudiobook.Size = new System.Drawing.Size(212, 13);
            this.nameAudiobook.TabIndex = 12;
            this.nameAudiobook.Text = "Джон Фаулз - Башня из черного дерева";
            this.nameAudiobook.MouseEnter += new System.EventHandler(this.nameAudiobook_MouseEnter);
            this.nameAudiobook.MouseLeave += new System.EventHandler(this.nameAudiobook_MouseLeave);
            // 
            // timeValueCurrentAudiofile
            // 
            this.timeValueCurrentAudiofile.AutoSize = true;
            this.timeValueCurrentAudiofile.Location = new System.Drawing.Point(77, 67);
            this.timeValueCurrentAudiofile.Name = "timeValueCurrentAudiofile";
            this.timeValueCurrentAudiofile.Size = new System.Drawing.Size(96, 13);
            this.timeValueCurrentAudiofile.TabIndex = 13;
            this.timeValueCurrentAudiofile.Text = "01:50:07/03:58:24";
            // 
            // timeValueCurrentAudiobook
            // 
            this.timeValueCurrentAudiobook.AutoSize = true;
            this.timeValueCurrentAudiobook.Location = new System.Drawing.Point(77, 31);
            this.timeValueCurrentAudiobook.Name = "timeValueCurrentAudiobook";
            this.timeValueCurrentAudiobook.Size = new System.Drawing.Size(96, 13);
            this.timeValueCurrentAudiobook.TabIndex = 12;
            this.timeValueCurrentAudiobook.Text = "00:50:07/01:01:00";
            // 
            // btnLibrary
            // 
            this.btnLibrary.Location = new System.Drawing.Point(149, 333);
            this.btnLibrary.Name = "btnLibrary";
            this.btnLibrary.Size = new System.Drawing.Size(75, 23);
            this.btnLibrary.TabIndex = 10;
            this.btnLibrary.Text = "Библиотека";
            this.btnLibrary.UseVisualStyleBackColor = true;
            this.btnLibrary.Click += new System.EventHandler(this.btnLibrary_Click);
            // 
            // elementHost2
            // 
            this.elementHost2.Location = new System.Drawing.Point(21, 42);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(203, 40);
            this.elementHost2.TabIndex = 4;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.sliderAudiobook;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(21, 81);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(203, 20);
            this.elementHost1.TabIndex = 2;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.sliderAudiofile;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 401);
            this.Controls.Add(this.groupBox1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(285, 440);
            this.MinimumSize = new System.Drawing.Size(285, 440);
            this.Name = "Main";
            this.Text = "ABPlayer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private System.Windows.Forms.ComboBox cmbBxBookmark;
        private System.Windows.Forms.TextBox textBoxForBookmark;
        private System.Windows.Forms.Button addBookmarks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLibrary;
        private System.Windows.Forms.Label nameAudiobook;
        private System.Windows.Forms.Label timeValueCurrentAudiofile;
        private System.Windows.Forms.Label timeValueCurrentAudiobook;
        private System.Windows.Forms.ComboBox cmbBxSelectorSpeedPlayback;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip;
        private RMControls.Player.Slider sliderAudiobook;
        private System.Windows.Forms.Integration.ElementHost BtnPlay_userControl;
        private RMControls.Player.BtnPlay BtnPlay;
        private System.Windows.Forms.Integration.ElementHost elementHost3;
        private RMControls.Player.BtnStop BtnStop;
        private System.Windows.Forms.Integration.ElementHost elementHost4;
        private RMControls.Player.BtnReverse BtnReverse;
        private System.Windows.Forms.Integration.ElementHost elementHost5;
        private RMControls.Player.BtnForward BtnForward;
        private RMControls.Player.Slider sliderAudiofile;
    }
}


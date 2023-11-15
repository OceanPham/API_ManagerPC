namespace WindowsFormsApp2
{
    partial class Form1
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnEndTask = new System.Windows.Forms.Button();
            this.btnShowMemmory = new System.Windows.Forms.Button();
            this.btnShowTaskManager = new System.Windows.Forms.Button();
            this.btnShowThisPC = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnShowInforPC = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCreateProccess = new System.Windows.Forms.Button();
            this.ptbLock = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLock)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(140, 26);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(83, 33);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnEndTask
            // 
            this.btnEndTask.Location = new System.Drawing.Point(140, 65);
            this.btnEndTask.Name = "btnEndTask";
            this.btnEndTask.Size = new System.Drawing.Size(83, 33);
            this.btnEndTask.TabIndex = 2;
            this.btnEndTask.Text = "End task";
            this.btnEndTask.UseVisualStyleBackColor = true;
            this.btnEndTask.Click += new System.EventHandler(this.btnEndTask_Click);
            // 
            // btnShowMemmory
            // 
            this.btnShowMemmory.Location = new System.Drawing.Point(20, 131);
            this.btnShowMemmory.Name = "btnShowMemmory";
            this.btnShowMemmory.Size = new System.Drawing.Size(111, 69);
            this.btnShowMemmory.TabIndex = 3;
            this.btnShowMemmory.Text = "Show memmory";
            this.btnShowMemmory.UseVisualStyleBackColor = true;
            this.btnShowMemmory.Click += new System.EventHandler(this.btnShowMemmory_Click);
            // 
            // btnShowTaskManager
            // 
            this.btnShowTaskManager.Location = new System.Drawing.Point(20, 26);
            this.btnShowTaskManager.Name = "btnShowTaskManager";
            this.btnShowTaskManager.Size = new System.Drawing.Size(111, 69);
            this.btnShowTaskManager.TabIndex = 4;
            this.btnShowTaskManager.Text = "Show Task Manager";
            this.btnShowTaskManager.UseVisualStyleBackColor = true;
            this.btnShowTaskManager.Click += new System.EventHandler(this.btnShowTaskManager_Click);
            // 
            // btnShowThisPC
            // 
            this.btnShowThisPC.Location = new System.Drawing.Point(20, 219);
            this.btnShowThisPC.Name = "btnShowThisPC";
            this.btnShowThisPC.Size = new System.Drawing.Size(111, 69);
            this.btnShowThisPC.TabIndex = 6;
            this.btnShowThisPC.Text = "Show Exploler";
            this.btnShowThisPC.UseVisualStyleBackColor = true;
            this.btnShowThisPC.Click += new System.EventHandler(this.btnShowThisPC_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(140, 219);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(83, 33);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(140, 256);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(83, 33);
            this.btnPaste.TabIndex = 8;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(341, 48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(694, 488);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // btnShowInforPC
            // 
            this.btnShowInforPC.Location = new System.Drawing.Point(20, 316);
            this.btnShowInforPC.Name = "btnShowInforPC";
            this.btnShowInforPC.Size = new System.Drawing.Size(111, 60);
            this.btnShowInforPC.TabIndex = 10;
            this.btnShowInforPC.Text = "Show Infor PC";
            this.btnShowInforPC.UseVisualStyleBackColor = true;
            this.btnShowInforPC.Click += new System.EventHandler(this.btnShowInforPC_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WindowsFormsApp2.Properties.Resources.shut;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(20, 495);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 50);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnCreateProccess
            // 
            this.btnCreateProccess.Location = new System.Drawing.Point(20, 407);
            this.btnCreateProccess.Name = "btnCreateProccess";
            this.btnCreateProccess.Size = new System.Drawing.Size(111, 60);
            this.btnCreateProccess.TabIndex = 12;
            this.btnCreateProccess.Text = "Create Proccess (Notepad)";
            this.btnCreateProccess.UseVisualStyleBackColor = true;
            this.btnCreateProccess.Click += new System.EventHandler(this.btnCreateProccess_Click);
            // 
            // ptbLock
            // 
            this.ptbLock.BackgroundImage = global::WindowsFormsApp2.Properties.Resources._lock;
            this.ptbLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbLock.Location = new System.Drawing.Point(85, 495);
            this.ptbLock.Name = "ptbLock";
            this.ptbLock.Size = new System.Drawing.Size(49, 50);
            this.ptbLock.TabIndex = 13;
            this.ptbLock.TabStop = false;
            this.ptbLock.Click += new System.EventHandler(this.ptbLock_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp2.Properties.Resources.back;
            this.ClientSize = new System.Drawing.Size(1071, 567);
            this.Controls.Add(this.ptbLock);
            this.Controls.Add(this.btnCreateProccess);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnShowInforPC);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnShowThisPC);
            this.Controls.Add(this.btnShowTaskManager);
            this.Controls.Add(this.btnShowMemmory);
            this.Controls.Add(this.btnEndTask);
            this.Controls.Add(this.btnRefresh);
            this.Name = "Form1";
            this.Text = "My Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnEndTask;
        private System.Windows.Forms.Button btnShowMemmory;
        private System.Windows.Forms.Button btnShowTaskManager;
        private System.Windows.Forms.Button btnShowThisPC;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnShowInforPC;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCreateProccess;
        private System.Windows.Forms.PictureBox ptbLock;
    }
}


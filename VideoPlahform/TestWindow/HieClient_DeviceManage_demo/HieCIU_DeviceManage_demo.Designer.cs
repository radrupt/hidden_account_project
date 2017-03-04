namespace TestWindow.HieClient_DeviceManage_demo
{
    partial class HieCIU_DeviceManage_demo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.devicemangegrid = new System.Windows.Forms.DataGridView();
            this.probedevice = new System.Windows.Forms.Button();
            this.configuredevice = new System.Windows.Forms.Button();
            this.configurealldevice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.devicemangegrid)).BeginInit();
            this.SuspendLayout();
            // 
            // devicemangegrid
            // 
            this.devicemangegrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.devicemangegrid.Location = new System.Drawing.Point(12, 48);
            this.devicemangegrid.Name = "devicemangegrid";
            this.devicemangegrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.devicemangegrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.devicemangegrid.RowTemplate.Height = 23;
            this.devicemangegrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.devicemangegrid.Size = new System.Drawing.Size(1210, 238);
            this.devicemangegrid.StandardTab = true;
            this.devicemangegrid.TabIndex = 0;
            // 
            // probedevice
            // 
            this.probedevice.Location = new System.Drawing.Point(12, 12);
            this.probedevice.Name = "probedevice";
            this.probedevice.Size = new System.Drawing.Size(75, 23);
            this.probedevice.TabIndex = 1;
            this.probedevice.Text = "探测设备";
            this.probedevice.UseVisualStyleBackColor = true;
            this.probedevice.Click += new System.EventHandler(this.probedevice_Click);
            // 
            // configuredevice
            // 
            this.configuredevice.Location = new System.Drawing.Point(124, 12);
            this.configuredevice.Name = "configuredevice";
            this.configuredevice.Size = new System.Drawing.Size(75, 23);
            this.configuredevice.TabIndex = 2;
            this.configuredevice.Text = "配置设备";
            this.configuredevice.UseVisualStyleBackColor = true;
            this.configuredevice.Click += new System.EventHandler(this.configuredevice_Click);
            // 
            // configurealldevice
            // 
            this.configurealldevice.Location = new System.Drawing.Point(242, 12);
            this.configurealldevice.Name = "configurealldevice";
            this.configurealldevice.Size = new System.Drawing.Size(91, 23);
            this.configurealldevice.TabIndex = 3;
            this.configurealldevice.Text = "配置所有设备";
            this.configurealldevice.UseVisualStyleBackColor = true;
            this.configurealldevice.Click += new System.EventHandler(this.configurealldevice_Click);
            // 
            // HieCIU_DeviceManage_demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 296);
            this.Controls.Add(this.configurealldevice);
            this.Controls.Add(this.configuredevice);
            this.Controls.Add(this.probedevice);
            this.Controls.Add(this.devicemangegrid);
            this.Name = "HieCIU_DeviceManage_demo";
            this.Text = "HieCIU_DeviceManage_demo";
            ((System.ComponentModel.ISupportInitialize)(this.devicemangegrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView devicemangegrid;
        private System.Windows.Forms.Button probedevice;
        private System.Windows.Forms.Button configuredevice;
        private System.Windows.Forms.Button configurealldevice;
    }
}
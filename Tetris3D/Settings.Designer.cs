namespace Tetris3D
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.lblGridSize = new System.Windows.Forms.Label();
            this.tbGridSize = new System.Windows.Forms.TrackBar();
            this.lblCellSize = new System.Windows.Forms.Label();
            this.tbCellSize = new System.Windows.Forms.TrackBar();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCellSize)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridSize.Location = new System.Drawing.Point(22, 19);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(117, 26);
            this.lblGridSize.TabIndex = 0;
            this.lblGridSize.Text = "Grid Size:";
            // 
            // tbGridSize
            // 
            this.tbGridSize.Location = new System.Drawing.Point(27, 57);
            this.tbGridSize.Maximum = 5;
            this.tbGridSize.Minimum = 2;
            this.tbGridSize.Name = "tbGridSize";
            this.tbGridSize.Size = new System.Drawing.Size(193, 45);
            this.tbGridSize.TabIndex = 1;
            this.tbGridSize.Value = 2;
            this.tbGridSize.ValueChanged += new System.EventHandler(this.tbGridSize_ValueChanged);
            // 
            // lblCellSize
            // 
            this.lblCellSize.AutoSize = true;
            this.lblCellSize.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCellSize.Location = new System.Drawing.Point(22, 122);
            this.lblCellSize.Name = "lblCellSize";
            this.lblCellSize.Size = new System.Drawing.Size(113, 26);
            this.lblCellSize.TabIndex = 0;
            this.lblCellSize.Text = "Cell Size:";
            // 
            // tbCellSize
            // 
            this.tbCellSize.Location = new System.Drawing.Point(27, 164);
            this.tbCellSize.Maximum = 40;
            this.tbCellSize.Minimum = 10;
            this.tbCellSize.Name = "tbCellSize";
            this.tbCellSize.Size = new System.Drawing.Size(193, 45);
            this.tbCellSize.TabIndex = 1;
            this.tbCellSize.Value = 30;
            this.tbCellSize.ValueChanged += new System.EventHandler(this.tbCellSize_ValueChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(92, 390);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbCellSize);
            this.Controls.Add(this.tbGridSize);
            this.Controls.Add(this.lblCellSize);
            this.Controls.Add(this.lblGridSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.tbGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCellSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGridSize;
        private System.Windows.Forms.TrackBar tbGridSize;
        private System.Windows.Forms.Label lblCellSize;
        private System.Windows.Forms.TrackBar tbCellSize;
        private System.Windows.Forms.Button btnClose;
    }
}
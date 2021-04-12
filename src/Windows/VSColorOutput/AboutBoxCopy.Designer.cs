namespace ColorizeOutput {
  sealed partial class AboutBoxCopy {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBoxCopy));
      this.closeButton = new System.Windows.Forms.Button();
      this.rtbCopy = new System.Windows.Forms.RichTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // closeButton
      // 
      this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.closeButton.Location = new System.Drawing.Point(462, 275);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(75, 26);
      this.closeButton.TabIndex = 25;
      this.closeButton.Text = "&Close";
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // rtbCopy
      // 
      this.rtbCopy.Dock = System.Windows.Forms.DockStyle.Top;
      this.rtbCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.rtbCopy.Location = new System.Drawing.Point(9, 9);
      this.rtbCopy.Name = "rtbCopy";
      this.rtbCopy.Size = new System.Drawing.Size(523, 252);
      this.rtbCopy.TabIndex = 26;
      this.rtbCopy.Text = "";
      this.rtbCopy.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbCopy_LinkClicked);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(6, 269);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(224, 15);
      this.label1.TabIndex = 27;
      this.label1.Text = "Short info has been copied to clipboard.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(6, 286);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(255, 15);
      this.label2.TabIndex = 28;
      this.label2.Text = "Use the above edit box to copy additional info.";
      // 
      // AboutBoxCopy
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Gainsboro;
      this.ClientSize = new System.Drawing.Size(541, 303);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.rtbCopy);
      this.Controls.Add(this.closeButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutBoxCopy";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "About ...";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.RichTextBox rtbCopy;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}

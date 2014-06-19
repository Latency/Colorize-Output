namespace ColorizeOutput {
  sealed partial class AboutBox {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.labelBuild = new System.Windows.Forms.Label();
      this.rtbBuild = new System.Windows.Forms.RichTextBox();
      this.closeButton = new System.Windows.Forms.Button();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelInstalledPlugins = new System.Windows.Forms.Label();
      this.copyButton = new System.Windows.Forms.Button();
      this.logoPictureBox = new System.Windows.Forms.PictureBox();
      this.labelPlugins = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.Silver;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.labelBuild);
      this.panel1.Controls.Add(this.rtbBuild);
      this.panel1.Location = new System.Drawing.Point(0, 165);
      this.panel1.Margin = new System.Windows.Forms.Padding(0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(541, 62);
      this.panel1.TabIndex = 13;
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.Gainsboro;
      this.panel2.Location = new System.Drawing.Point(4, 22);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(528, 1);
      this.panel2.TabIndex = 33;
      // 
      // labelBuild
      // 
      this.labelBuild.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelBuild.ForeColor = System.Drawing.Color.Black;
      this.labelBuild.Location = new System.Drawing.Point(1, 30);
      this.labelBuild.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelBuild.Name = "labelBuild";
      this.labelBuild.Size = new System.Drawing.Size(536, 20);
      this.labelBuild.TabIndex = 32;
      this.labelBuild.Text = "      Build $BUILD_VERSION on $BUILD_DATE";
      this.labelBuild.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // rtbBuild
      // 
      this.rtbBuild.BackColor = System.Drawing.Color.Silver;
      this.rtbBuild.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtbBuild.Cursor = System.Windows.Forms.Cursors.Default;
      this.rtbBuild.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbBuild.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rtbBuild.ForeColor = System.Drawing.SystemColors.WindowText;
      this.rtbBuild.Location = new System.Drawing.Point(0, 0);
      this.rtbBuild.Name = "rtbBuild";
      this.rtbBuild.ReadOnly = true;
      this.rtbBuild.ShowSelectionMargin = true;
      this.rtbBuild.Size = new System.Drawing.Size(539, 60);
      this.rtbBuild.TabIndex = 31;
      this.rtbBuild.TabStop = false;
      this.rtbBuild.Text = "$COMPANY $PRODUCT $VERSION";
      this.rtbBuild.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbBuild_LinkClicked);
      // 
      // closeButton
      // 
      this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.closeButton.Location = new System.Drawing.Point(464, 260);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(75, 26);
      this.closeButton.TabIndex = 25;
      this.closeButton.Text = "&Close";
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // labelVersion
      // 
      this.labelVersion.Font = new System.Drawing.Font("High Tower Text", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelVersion.ForeColor = System.Drawing.Color.Black;
      this.labelVersion.Location = new System.Drawing.Point(3, 255);
      this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(536, 20);
      this.labelVersion.TabIndex = 26;
      this.labelVersion.Text = "Running in Visual Studio ...";
      this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelInstalledPlugins
      // 
      this.labelInstalledPlugins.Font = new System.Drawing.Font("High Tower Text", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelInstalledPlugins.ForeColor = System.Drawing.Color.Black;
      this.labelInstalledPlugins.Location = new System.Drawing.Point(3, 234);
      this.labelInstalledPlugins.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelInstalledPlugins.Name = "labelInstalledPlugins";
      this.labelInstalledPlugins.Size = new System.Drawing.Size(140, 20);
      this.labelInstalledPlugins.TabIndex = 28;
      this.labelInstalledPlugins.Text = "Installed plugins:";
      this.labelInstalledPlugins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // copyButton
      // 
      this.copyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.copyButton.Location = new System.Drawing.Point(371, 260);
      this.copyButton.Name = "copyButton";
      this.copyButton.Size = new System.Drawing.Size(75, 26);
      this.copyButton.TabIndex = 30;
      this.copyButton.Text = "&Copy >>";
      this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
      // 
      // logoPictureBox
      // 
      this.logoPictureBox.BackColor = System.Drawing.Color.Black;
      this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.logoPictureBox.Image = global::ColorizeOutput.Properties.Resources.ColorizeOutputLogo;
      this.logoPictureBox.Location = new System.Drawing.Point(-7, 0);
      this.logoPictureBox.Margin = new System.Windows.Forms.Padding(0);
      this.logoPictureBox.Name = "logoPictureBox";
      this.logoPictureBox.Size = new System.Drawing.Size(555, 165);
      this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.logoPictureBox.TabIndex = 12;
      this.logoPictureBox.TabStop = false;
      // 
      // labelPlugins
      // 
      this.labelPlugins.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelPlugins.ForeColor = System.Drawing.Color.MidnightBlue;
      this.labelPlugins.Location = new System.Drawing.Point(122, 234);
      this.labelPlugins.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelPlugins.Name = "labelPlugins";
      this.labelPlugins.Size = new System.Drawing.Size(417, 20);
      this.labelPlugins.TabIndex = 31;
      this.labelPlugins.Text = "...";
      this.labelPlugins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AboutBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.DarkGray;
      this.ClientSize = new System.Drawing.Size(541, 287);
      this.Controls.Add(this.labelPlugins);
      this.Controls.Add(this.copyButton);
      this.Controls.Add(this.labelInstalledPlugins);
      this.Controls.Add(this.logoPictureBox);
      this.Controls.Add(this.closeButton);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.labelVersion);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutBox";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "About ...";
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox logoPictureBox;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelInstalledPlugins;
    private System.Windows.Forms.Button copyButton;
    private System.Windows.Forms.Label labelPlugins;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label labelBuild;
    private System.Windows.Forms.RichTextBox rtbBuild;
  }
}

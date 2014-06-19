using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AssemblyInfo;
using Microsoft.VisualStudio.Shell;

namespace ColorizeOutput {
  sealed partial class AboutBox : Form {
    private readonly Dictionary<string, string> _plugins;
    private readonly string _copyright;

    public AboutBox() {
      InitializeComponent();
      var asm = Assembly.GetExecutingAssembly();
      // Extensions from AssemblyInfo
      var asmVersion = asm.AssemblyVersion();
      var asmCompany = asm.Company();
      var asmTitle = asm.ProductTitle();
      var asmFileVersion = asm.AssemblyFileVersion();

      _copyright = @"Copyright © 1998-2014 " + asmCompany + @".  All rights reserved.";

      _plugins = new Dictionary<string, string> {
        {"NuGet support for " + asmTitle, 'v' + asmFileVersion + " by " + asmCompany}
      };

      Text = String.Format("About {0}", asm.Product());
      rtbBuild.Text = asmTitle + @" v" + asmFileVersion + @" at " + asmCompany;
      labelBuild.Text = labelBuild.Text.Replace("$BUILD_VERSION", 'v' + asmVersion);
      //  UTC in RFC 3339
      labelBuild.Text = labelBuild.Text.Replace("$BUILD_DATE", String.Format("{0:yyyy-MM-dd} at {0:H:mm:ss}", File.GetCreationTime(asm.Location)));
      foreach (var plugin in _plugins) {
        try {
          labelPlugins.Text = labelPlugins.Text.Remove(labelPlugins.Text.IndexOf("...", StringComparison.Ordinal)) + plugin.Key;
        } catch (ArgumentOutOfRangeException) {
          labelPlugins.Text += @", " + plugin.Key;
        }
      }
      // Obtain the IDE build version.
      var dte = (EnvDTE.DTE) Package.GetGlobalService(typeof(EnvDTE.DTE));
      labelVersion.Text = labelVersion.Text.Remove(labelVersion.Text.IndexOf(" ...", StringComparison.Ordinal)) + (dte != null ? ' ' + dte.Version + '.' : String.Empty);
    }

    private void closeButton_Click(object sender, EventArgs e) {
      Close();
    }

    private void copyButton_Click(object sender, EventArgs e) {
      using (var frm = new AboutBoxCopy(Text, rtbBuild.Text + Environment.NewLine + labelBuild.Text, labelVersion.Text, _copyright, _plugins)) {
        frm.ShowDialog();
      }
    }

    private void rtbBuild_LinkClicked(object sender, LinkClickedEventArgs e) {
      System.Diagnostics.Process.Start(e.LinkText);
    }
  }
}

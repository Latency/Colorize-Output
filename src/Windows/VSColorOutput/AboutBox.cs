// ****************************************************************************
// * Project:  Colorize-Output
// * File:     AboutBox.cs
// * Date:     07/26/2014
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AssemblyLoader;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Process = System.Diagnostics.Process;

namespace ColorizeOutput {
    internal sealed partial class AboutBox : Form {
    private readonly string _copyright;
    private readonly Dictionary<string, string> _plugins;


    public AboutBox() {
            ThreadHelper.ThrowIfNotOnUIThread();
            InitializeComponent();
      var asm = Assembly.GetExecutingAssembly();
      // Extensions from AssemblyInfo
      var asmTitle = asm.ProductTitle();
      var asmFileVersion = asm.AssemblyFileVersion();

      _copyright = @"GNU LESSER GENERAL PUBLIC LICENSE Version 3, 29 June 2007  -  All rights reserved.";

      _plugins = new Dictionary<string, string> {
        {
          "NuGet support for " + asmTitle, 'v' + asmFileVersion + " by " + asm.Company()
        }
      };

      Text = $@"About {asm.Product()}";
      rtbBuild.Text = asmTitle + @" v" + asmFileVersion + @" at " + asm.Copyright();
      labelBuild.Text = labelBuild.Text.Replace("$BUILD_VERSION", 'v' + asm.AssemblyVersion());
      //  UTC in RFC 3339
      labelBuild.Text = labelBuild.Text.Replace("$BUILD_DATE", string.Format("{0:yyyy-MM-dd} at {0:H:mm:ss}", File.GetCreationTime(asm.Location)));
      foreach (var plugin in _plugins) {
        try {
          labelPlugins.Text = labelPlugins.Text.Remove(labelPlugins.Text.IndexOf("...", StringComparison.Ordinal)) + plugin.Key;
        } catch (ArgumentOutOfRangeException) {
          labelPlugins.Text += @", " + plugin.Key;
        }
      }
      // Obtain the IDE build version.
      var dte = (DTE) Package.GetGlobalService(typeof (DTE));
      labelVersion.Text = $@"{labelVersion.Text.Remove(labelVersion.Text.IndexOf(" ...", StringComparison.Ordinal))}{(dte != null ? ' ' + dte.Version + '.' : string.Empty)}";
    }

    private void CloseButton_Click(object sender, EventArgs e) {
      Close();
    }

    private void CopyButton_Click(object sender, EventArgs e) {
      using (var frm = new AboutBoxCopy(Text, rtbBuild.Text + Environment.NewLine + labelBuild.Text, labelVersion.Text, _copyright, _plugins))
        frm.ShowDialog();
    }

    private void RtbBuild_LinkClicked(object sender, LinkClickedEventArgs e) {
      Process.Start(e.LinkText);
    }
  }
}
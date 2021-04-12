// ****************************************************************************
// * Project:  Colorize-Output
// * File:     AboutBoxCopy.cs
// * Date:     07/26/2014
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ColorizeOutput {
  sealed partial class AboutBoxCopy : Form {
    public AboutBoxCopy(string title, string text, string vsVersion, string copyright, IReadOnlyCollection<KeyValuePair<string, string>> plugins) {
      InitializeComponent();

      Text = title;
      rtbCopy.Text = text + Environment.NewLine;

      Clipboard.SetText(rtbCopy.Text);

      if (!String.IsNullOrEmpty(vsVersion)) {
        rtbCopy.Text += Environment.NewLine + @"Plugins: " + plugins.Count + Environment.NewLine;
        var x = 0;
        foreach (var plugin in plugins)
          rtbCopy.Text += @"#" + ++x + @". " + '"' + plugin.Key + @""" " + plugin.Value + Environment.NewLine;
        rtbCopy.Text += vsVersion + Environment.NewLine;
      }

      rtbCopy.Text += Environment.NewLine + copyright;
    }

    private void closeButton_Click(object sender, EventArgs e) {
      Close();
    }

    private void rtbCopy_LinkClicked(object sender, LinkClickedEventArgs e) {
      Process.Start(e.LinkText);
    }
  }
}
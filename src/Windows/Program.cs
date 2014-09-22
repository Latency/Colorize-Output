// ****************************************************************************
// * Project:  Colorize-Output
// * File:     Program.cs
// * Date:     07/26/2014
// ****************************************************************************

using System;

namespace ColorizeOutput {
  internal class Program {
    [STAThread]
    private static void Main() {
      using (var frm = new AboutBox())
        frm.ShowDialog();
    }
  }
}
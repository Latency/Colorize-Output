// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     Program.cs
// * Date:     06/18/2014
// ****************************************************************************

using System;

namespace ColorizeOutput {
  internal class Program {
    [STAThread]
    private static void Main(string[] args) {
      using (var frm = new AboutBox())
        frm.ShowDialog();
    }
  }
}
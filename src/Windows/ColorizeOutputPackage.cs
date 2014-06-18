// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     ColorizeOutputPackage.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

#endregion

namespace ColorizeOutput {
  [Guid("BFA8DB9A-F7D4-4405-A97C-2FC515611B22")]
  [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\12.0")]
  [PackageRegistration(UseManagedResourcesOnly = true)]
  [ProvideOptionPage(typeof (ColorizeOutputOptions), ColorizeOutputOptions.Category, ColorizeOutputOptions.SubCategory, 1000, 1001, true)]
  [ProvideProfile(typeof (ColorizeOutputOptions), ColorizeOutputOptions.Category, ColorizeOutputOptions.SubCategory, 1000, 1001, true)]
  [ProvideAutoLoad("adfc4e64-0397-11d1-9f4e-00a0c911004f")] //UICONTEXT_NoSolution; always loads 
  [InstalledProductRegistration("ColorizeOutput", "Colorized debug and build window output messages - http://bio-hazard.cx/colorizeoutput", "1.4.5")]
  public class ColorizeOutputPackage : Package {
    protected override void Initialize() {
      TextManagerEvents.RegisterForTextManagerEvents();
    }
  }
}
// ****************************************************************************
// * Project:  VSPackage2
// * File:     VSPackage2Package.cs
// * Date:     07/27/2014
// ****************************************************************************

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using AssemblyInfo;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace OpenSourceCommunity.VSPackage2 {
  /// <summary>
  ///   This is the class that implements the package exposed by this assembly.
  ///   The minimum requirement for a class to be considered a valid package for Visual Studio
  ///   is to implement the IVsPackage interface and register itself with the shell.
  ///   This package uses the helper classes defined inside the Managed Package Framework (MPF)
  ///   to do it: it derives from the Package class that provides the implementation of the
  ///   IVsPackage interface and uses the registration attributes defined in the framework to
  ///   register itself and its components with the shell.
  /// </summary>
  // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
  // a package.
  [PackageRegistration(UseManagedResourcesOnly = true)]
  // This attribute is used to register the information needed to show this package
  // in the Help/About dialog of Visual Studio.
// ReSharper disable once CSharpWarnings::CS0618
  [InstalledProductRegistration("productName", "productDetails", "productID")]
  // This attribute is needed to let the shell know that this package exposes some menus.
  [ProvideMenuResource("Menus.ctmenu", 1)]
  [Guid(GuidList.guidVSPackage2PkgString)]
// ReSharper disable once InconsistentNaming
  public sealed class VSPackage2Package : Package, IVsInstalledProduct {
    #region Properties

    private static Assembly _assembly;

    #endregion Properties

    /// <summary>
    ///   Default constructor of the package.
    ///   Inside this method you can place any initialization code that does not require
    ///   any Visual Studio service because at this point the package object is created but
    ///   not sited yet inside Visual Studio environment. The place to do all the other
    ///   initialization is the Initialize method.
    /// </summary>
    public VSPackage2Package() {
      Debug.WriteLine("Entering constructor for: {0}", this);
      _assembly = typeof (ColorizeOutput.IRegistryKey).Assembly;
    }

    #region Package Members

    /// <summary>
    ///   Initialization of the package; this method is called right after the package is sited, so this is the place
    ///   where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    protected override void Initialize() {
      Debug.WriteLine("Entering Initialize() of: {0}", this);

      base.Initialize();

      // Add our command handlers for menu (commands must exist in the .vsct file)
      var mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
      if (null == mcs)
        return;
      // Create the command for the menu item.
      mcs.AddCommand(new MenuCommand(MenuItemCallback, new CommandID(GuidList.guidVSPackage2CmdSet, (int) PkgCmdIDList.cmdidColorizeOutput)));
    }

    #endregion

    /////////////////////////////////////////////////////////////////////////////
    // Overridden Package Implementation

    public int IdBmpSplash(out uint pIdBmp) {
      pIdBmp = 500;
      return VSConstants.S_OK;
    }

    public int OfficialName(out string pbstrName) {
      pbstrName = "My Package";
      return VSConstants.S_OK;
    }

// ReSharper disable once InconsistentNaming
    public int ProductID(out string pbstrPID) {
      pbstrPID = "My Package ID";
      return VSConstants.S_OK;
    }

    public int ProductDetails(out string pbstrProductDetails) {
      pbstrProductDetails = _assembly.Description();
      return VSConstants.S_OK;
    }

    public int IdIcoLogoForAboutbox(out uint pIdIco) {
      pIdIco = 400;
      return VSConstants.S_OK;
    }

    /// <summary>
    ///   This function is the callback used to execute a command when the a menu item is clicked.
    ///   See the Initialize method to see how the menu item is associated to this function using
    ///   the OleMenuCommandService service and the MenuCommand class.
    /// </summary>
    private void MenuItemCallback(object sender, EventArgs e) {
      // Show a Message Box to prove we were here
      var uiShell = (IVsUIShell) GetService(typeof (SVsUIShell));
      var clsid = Guid.Empty;
      int result;
      ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(0, ref clsid, "Colorize Output", string.Format("Inside {0}.MenuItemCallback()", this), string.Empty, 0, OLEMSGBUTTON.OLEMSGBUTTON_OK,
        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST, OLEMSGICON.OLEMSGICON_INFO, 0, // false
        out result));
    }
  }
}
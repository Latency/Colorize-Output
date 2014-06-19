// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     FontAndColorStorage.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

#endregion

namespace ColorizeOutput {
  public static class FontAndColorStorage {
    private const int IsUpdating = 1;
    private const int NotUpdating = 0;


    private static readonly Dictionary<string, ColorableItemInfo[]> _colorMap = new Dictionary<string, ColorableItemInfo[]> {
      {OutputClassificationDefinitions.BuildHead, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.BuildText, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogInfo, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogWarn, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogError, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogCustom1, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogCustom2, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogCustom3, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.LogCustom4, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.FindResultsFilename, new[] {new ColorableItemInfo()}},
      {OutputClassificationDefinitions.FindResultsSearchTerm, new[] {new ColorableItemInfo()}}
    };

    private static int _updateState;
    public static IVsFontAndColorStorage Override { get; set; }

    private static string COLORREF_string(uint color) {
      var str = string.Format("#{0}", color.ToString("X8"));
      return "#FF" + str.Substring(7, 2) + str.Substring(5, 2) + str.Substring(3, 2);
    }

    public static IVsFontAndColorStorage GetFontAndColorStorageService() {
      return Override ?? Package.GetGlobalService(typeof (SVsFontAndColorStorage)) as IVsFontAndColorStorage;
    }

    public static void UpdateColors() {
      if (Interlocked.Exchange(ref _updateState, IsUpdating) == IsUpdating)
        return;

      const uint flags = (uint) (__FCSTORAGEFLAGS.FCSF_PROPAGATECHANGES | __FCSTORAGEFLAGS.FCSF_LOADDEFAULTS | __FCSTORAGEFLAGS.FCSF_NOAUTOCOLORS);

      var store = GetFontAndColorStorageService();
      if (store != null) {
        try {
          store.OpenCategory(DefGuidList.guidTextEditorFontCategory, flags);
          foreach (var color in _colorMap)
            store.GetItem(color.Key, color.Value);
          store.CloseCategory();
          store.OpenCategory(DefGuidList.guidOutputWindowFontCategory, flags);
          foreach (var color in _colorMap)
            store.SetItem(color.Key, color.Value);

          store.CloseCategory();

          // Hack to work-around the fact that Visual Studio doesn't use our colors
          // for the "Find Results" window.  Since it *does* use the default colors 
          // we provide, we write the colors to an easily accessible location in the
          // registry, and load those as the defaults.
          using (var root = VSRegistry.RegistryRoot(__VsLocalRegistryType.RegType_UserSettings, true)) {
            if (root != null) {
              using (var regkey = root.CreateSubKey(@"DialogPage\" + typeof (FontAndColorStorage).Namespace + ".ColorizeOutputOptions")) {
                // Open the category with different options; if we get a different color than
                // we originally got (above), we know the color is set to "Default" or "Auto".
                store.OpenCategory(DefGuidList.guidTextEditorFontCategory, (uint) (__FCSTORAGEFLAGS.FCSF_READONLY | __FCSTORAGEFLAGS.FCSF_PROPAGATECHANGES));
                foreach (var key in new[] {OutputClassificationDefinitions.FindResultsSearchTerm, OutputClassificationDefinitions.FindResultsFilename}) {
                  ColorableItemInfo[] value = {new ColorableItemInfo()};
                  var result = store.GetItem(key, value);

                  // Save the color information to the registry; if "Default" or "Auto",
                  // delete the key.
                  if (value[0].bForegroundValid == 0 || value[0].crForeground != (_colorMap[key])[0].crForeground)
                    regkey.DeleteValue(key + "/foreground", false);
                  else
                    regkey.SetValue(key + "/foreground", COLORREF_string(value[0].crForeground));

                  if (value[0].bBackgroundValid == 0 || value[0].crBackground != (_colorMap[key])[0].crBackground)
                    regkey.DeleteValue(key + "/background", false);
                  else
                    regkey.SetValue(key + "/background", COLORREF_string(value[0].crBackground));
                }
              }
            }
          }
        } finally {
          store.CloseCategory();
        }
      }

      Interlocked.Exchange(ref _updateState, NotUpdating);
    }
  }
}
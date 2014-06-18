﻿// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     TextManagerEvents.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;

#endregion

namespace ColorizeOutput {
  public class TextManagerEvents : IVsTextManagerEvents {
    public static IVsTextManager2 Override;
    private Guid _guidColorService = Guid.Empty;

    public void OnRegisterMarkerType(int iMarkerType) {}

    public void OnRegisterView(IVsTextView pView) {}

    public void OnUnregisterView(IVsTextView pView) {}

    public void OnUserPreferencesChanged(VIEWPREFERENCES[] pViewPrefs, FRAMEPREFERENCES[] pFramePrefs, LANGPREFERENCES[] pLangPrefs, FONTCOLORPREFERENCES[] pColorPrefs) {
      if (pColorPrefs != null && pColorPrefs.Length > 0 && pColorPrefs[0].pColorTable != null) {
        var guidFontCategory = (Guid) Marshal.PtrToStructure(pColorPrefs[0].pguidFontCategory, typeof (Guid));
        var guidColorService = (Guid) Marshal.PtrToStructure(pColorPrefs[0].pguidColorService, typeof (Guid));
        if (_guidColorService == Guid.Empty)
          _guidColorService = guidColorService;
        if (guidFontCategory == DefGuidList.guidTextEditorFontCategory && _guidColorService == guidColorService)
          FontAndColorStorage.UpdateColors();
      }
    }

    private static IVsTextManager2 GetService() {
      return Override ?? ServiceProvider.GlobalProvider.GetService(typeof (SVsTextManager)) as IVsTextManager2;
    }

    public static void RegisterForTextManagerEvents() {
      var textManager = GetService();
      var container = textManager as IConnectionPointContainer;
      IConnectionPoint textManagerEventsConnection;
      var eventGuid = typeof (IVsTextManagerEvents).GUID;
      container.FindConnectionPoint(ref eventGuid, out textManagerEventsConnection);
      var textManagerEvents = new TextManagerEvents();
      uint textManagerCookie;
      textManagerEventsConnection.Advise(textManagerEvents, out textManagerCookie);
    }
  }
}
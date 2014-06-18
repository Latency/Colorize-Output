// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     BuildEvents.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;

#endregion

#pragma warning disable 649

namespace ColorizeOutput {
  public class BuildEvents {
    private readonly DTE2 _dte2;
    private readonly List<string> _projectsBuildReport;
    private DateTime _buildStartTime;

    public BuildEvents(IServiceProvider serviceProvider) {
      if (serviceProvider == null)
        return;
      _dte2 = serviceProvider.GetService(typeof (DTE)) as DTE2;
      if (_dte2 != null) {
        // These event sources have to be rooted or the GC will collect them.
        // http://social.msdn.microsoft.com/Forums/en-US/vsx/thread/fd2f9108-1df3-4d96-a65d-67a69347ca27
        var events = _dte2.Events;
        var buildEvents = events.BuildEvents;
        var dteEvents = events.DTEEvents;

        buildEvents.OnBuildBegin += OnBuildBegin;
        buildEvents.OnBuildDone += OnBuildDone;
        buildEvents.OnBuildProjConfigDone += OnBuildProjectDone;
        dteEvents.ModeChanged += OnModeChanged;
      }

      _projectsBuildReport = new List<string>();
    }

    public bool StopOnBuildErrorEnabled { get; set; }
    public bool ShowElapsedBuildTimeEnabled { get; set; }
    public bool ShowBuildReport { get; set; }
    public bool ShowDebugWindowOnDebug { get; set; }

    private void OnBuildBegin(vsBuildScope scope, vsBuildAction action) {
      _projectsBuildReport.Clear();
      _buildStartTime = DateTime.Now;
    }

    private void OnBuildDone(vsBuildScope scope, vsBuildAction action) {
      var buildOutputPane = _dte2.ToolWindows.OutputWindow.OutputWindowPanes.Cast<OutputWindowPane>().FirstOrDefault(pane => pane.Guid == VSConstants.OutputWindowPaneGuid.BuildOutputPane_string);

      if (buildOutputPane == null)
        return;

      if (ShowBuildReport) {
        buildOutputPane.OutputString(Environment.NewLine + "Projects build report:" + Environment.NewLine);
        buildOutputPane.OutputString("  Status    | Project [Config|platform]" + Environment.NewLine);
        buildOutputPane.OutputString(" -----------|---------------------------------------------------------------------------------------------------" + Environment.NewLine);
        foreach (var reportItem in _projectsBuildReport)
          buildOutputPane.OutputString(reportItem + Environment.NewLine);
      }

      if (ShowElapsedBuildTimeEnabled) {
        var elapsed = DateTime.Now - _buildStartTime;
        var time = elapsed.ToString(@"hh\:mm\:ss\.ff");
        var text = string.Format("Time Elapsed {0}", time);
        buildOutputPane.OutputString(Environment.NewLine + text + Environment.NewLine);
      }
    }

    private void OnBuildProjectDone(string project, string projectConfig, string platform, string solutionConfig, bool success) {
      if (StopOnBuildErrorEnabled && success == false) {
        const string cancelBuildCommand = "Build.Cancel";
        _dte2.ExecuteCommand(cancelBuildCommand);
      }

      if (ShowBuildReport)
        _projectsBuildReport.Add("  " + (success ? "Succeeded" : "Failed   ") + " | " + project + " [" + projectConfig + "|" + platform + "]");
    }

    private void OnModeChanged(vsIDEMode lastMode) {
      if (lastMode == vsIDEMode.vsIDEModeDesign && ShowDebugWindowOnDebug) {
        _dte2.ToolWindows.OutputWindow.Parent.Activate();
        foreach (var pane in _dte2.ToolWindows.OutputWindow.OutputWindowPanes.Cast<OutputWindowPane>().Where(pane => pane.Guid == VSConstants.OutputWindowPaneGuid.DebugPane_string)) {
          pane.Activate();
          break;
        }
      }
    }
  }
}
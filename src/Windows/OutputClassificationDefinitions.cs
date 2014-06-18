// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     OutputClassificationDefinitions.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace ColorizeOutput {
  public static class OutputClassificationDefinitions {
    private const string VsColorOut = "VSColorOutput ";

    public const string BuildHead = "BuildHead";
    public const string BuildText = "BuildText";

    public const string LogInfo = "LogInformation";
    public const string LogWarn = "LogWarning";
    public const string LogError = "LogError";
    public const string LogCustom1 = "LogCustom1";
    public const string LogCustom2 = "LogCustom2";
    public const string LogCustom3 = "LogCustom3";
    public const string LogCustom4 = "LogCustom4";

    public const string FindResultsSearchTerm = "FindResultsSearchTerm";
    public const string FindResultsFilename = "FindResultsFilename";


    // "Proxy" versions of the "Find Results" colors.  This is how we 
    // hack around the fact that VS refuses to use user-specified colors.
    public const string FindResultsSearchTerm_Proxy = "FindResultsSearchTerm_Proxy";
    public const string FindResultsFilename_Proxy = "FindResultsFilename_Proxy";

    [Export(typeof (ClassificationTypeDefinition))]
    [Name(BuildHead)]
    public static ClassificationTypeDefinition BuildHeaderDefinition { get; set; }

    [Export]
    [Name(BuildText)]
    public static ClassificationTypeDefinition BuildTextDefinition { get; set; }

    [Export]
    [Name(LogError)]
    public static ClassificationTypeDefinition LogErrorDefinition { get; set; }

    [Export]
    [Name(LogWarn)]
    public static ClassificationTypeDefinition LogWarningDefinition { get; set; }

    [Export]
    [Name(LogInfo)]
    public static ClassificationTypeDefinition LogInformationDefinition { get; set; }

    [Export]
    [Name(LogCustom1)]
    public static ClassificationTypeDefinition LogCustome1Definition { get; set; }

    [Export]
    [Name(LogCustom2)]
    public static ClassificationTypeDefinition LogCustom2Definition { get; set; }

    [Export]
    [Name(LogCustom3)]
    public static ClassificationTypeDefinition LogCustom3Definition { get; set; }

    [Export]
    [Name(LogCustom4)]
    public static ClassificationTypeDefinition LogCustom4Definition { get; set; }

    [Export]
    [Name(FindResultsSearchTerm)]
    public static ClassificationTypeDefinition FindResultsSearchTermDefinition { get; set; }

    [Export]
    [Name(FindResultsFilename)]
    public static ClassificationTypeDefinition FindResultsFilenameDefinition { get; set; }

    [Export]
    [Name(FindResultsFilename_Proxy)]
    public static ClassificationTypeDefinition FindResultsFilename_ProxyDefinition { get; set; }

    [Export]
    [Name(FindResultsSearchTerm_Proxy)]
    public static ClassificationTypeDefinition FindResultsSearchTerm_ProxyDefinition { get; set; }

    [Name(BuildHead)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = BuildHead)]
    public sealed class BuildHeaderFormat : ClassificationFormatDefinition {
      public BuildHeaderFormat() {
        DisplayName = VsColorOut + "Build Header";
        ForegroundColor = Colors.Green;
      }
    }

    [Name(BuildText)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = BuildText)]
    public sealed class BuildTextFormat : ClassificationFormatDefinition {
      public BuildTextFormat() {
        DisplayName = VsColorOut + "Build Text";
        ForegroundColor = Colors.Gray;
      }
    }

    [Name(FindResultsFilename)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsFilename)]
    public sealed class FindResultsFilenameFormat : ClassificationFormatDefinition {
      public FindResultsFilenameFormat() {
        DisplayName = VsColorOut + "Find Results Filename";
        ForegroundColor = Colors.Gray;
      }
    }

    [Name(FindResultsFilename_Proxy)]
    [UserVisible(false)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsFilename_Proxy)]
    public sealed class FindResultsFilename_ProxyFormat : ClassificationFormatDefinition {
      public FindResultsFilename_ProxyFormat() {
        DisplayName = VsColorOut + "Find Results Filename";

        // Load the default colors from the registry
        var key = VSRegistry.RegistryRoot(__VsLocalRegistryType.RegType_UserSettings, true).CreateSubKey(@"DialogPage\BlueOnionSoftware.VsColorOutputOptions");
        var fg = key.GetValue(FindResultsFilename + "/foreground", string.Empty).ToString();
        ForegroundColor = string.IsNullOrWhiteSpace(fg) ? Colors.Gray : (Color?) ColorConverter.ConvertFromString(fg);
        var bg = key.GetValue(FindResultsFilename + "/background", string.Empty).ToString();
        BackgroundColor = string.IsNullOrWhiteSpace(bg) ? null : (Color?) ColorConverter.ConvertFromString(bg);
      }
    }

    [Name(FindResultsSearchTerm)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsSearchTerm)]
    public sealed class FindResultsSearchTermFormat : ClassificationFormatDefinition {
      public FindResultsSearchTermFormat() {
        DisplayName = VsColorOut + "Find Results Search Term";
        ForegroundColor = Colors.Blue;
      }
    }

    [Name(FindResultsSearchTerm_Proxy)]
    [UserVisible(false)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsSearchTerm_Proxy)]
    public sealed class FindResultsSearchTerm_ProxyFormat : ClassificationFormatDefinition {
      public FindResultsSearchTerm_ProxyFormat() {
        DisplayName = VsColorOut + "Find Results Search Term";

        // Load the default colors from the registry
        var key = VSRegistry.RegistryRoot(__VsLocalRegistryType.RegType_UserSettings, true).CreateSubKey(@"DialogPage\BlueOnionSoftware.VsColorOutputOptions");
        var fg = key.GetValue(FindResultsSearchTerm + "/foreground", string.Empty).ToString();
        ForegroundColor = string.IsNullOrWhiteSpace(fg) ? Colors.Blue : (Color?) ColorConverter.ConvertFromString(fg);
        var bg = key.GetValue(FindResultsSearchTerm + "/background", string.Empty).ToString();
        BackgroundColor = string.IsNullOrWhiteSpace(bg) ? null : (Color?) ColorConverter.ConvertFromString(bg);
      }
    }

    [Name(LogCustom1)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom1)]
    public sealed class LogCustom1Format : ClassificationFormatDefinition {
      public LogCustom1Format() {
        DisplayName = VsColorOut + "Log Custom1";
        ForegroundColor = Colors.Purple;
      }
    }

    [Name(LogCustom2)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom2)]
    public sealed class LogCustom2Format : ClassificationFormatDefinition {
      public LogCustom2Format() {
        DisplayName = VsColorOut + "Log Custom2";
        ForegroundColor = Colors.DarkSalmon;
      }
    }

    [Name(LogCustom3)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom3)]
    public sealed class LogCustom3Format : ClassificationFormatDefinition {
      public LogCustom3Format() {
        DisplayName = VsColorOut + "Log Custom3";
        ForegroundColor = Colors.DarkOrange;
      }
    }

    [Name(LogCustom4)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom4)]
    public sealed class LogCustom4Format : ClassificationFormatDefinition {
      public LogCustom4Format() {
        DisplayName = VsColorOut + "Log Custom4";
        ForegroundColor = Colors.Brown;
      }
    }

    [Name(LogError)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogError)]
    public sealed class LogErrorFormat : ClassificationFormatDefinition {
      public LogErrorFormat() {
        DisplayName = VsColorOut + "Log Error";
        ForegroundColor = Colors.Red;
      }
    }

    [Name(LogInfo)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogInfo)]
    public sealed class LogInformationFormat : ClassificationFormatDefinition {
      public LogInformationFormat() {
        DisplayName = VsColorOut + "Log Information";
        ForegroundColor = Colors.DarkBlue;
      }
    }

    [Name(LogWarn)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogWarn)]
    public sealed class LogWarningFormat : ClassificationFormatDefinition {
      public LogWarningFormat() {
        DisplayName = VsColorOut + "Log Warning";
        ForegroundColor = Colors.Olive;
      }
    }
  }
}
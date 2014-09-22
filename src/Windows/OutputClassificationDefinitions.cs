// ****************************************************************************
// * Project:  Colorize-Output
// * File:     OutputClassificationDefinitions.cs
// * Date:     07/26/2014
// ****************************************************************************

#region

using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Media;
using AssemblyInfo;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace ColorizeOutput {
  public class OutputClassificationDefinitions {
    // Wrapped extension.

    // Fetch contents from mapped macro.
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


    // "Proxy" versions of the "Find Results" colors.  This is how we hack around the fact that VS refuses to use user-specified colors.
    public const string FindResultsSearchTermProxy = "FindResultsSearchTermProxy";
    public const string FindResultsFilenameProxy = "FindResultsFilenameProxy";
    private static readonly string _vsColorOut = Assembly.GetExecutingAssembly().ProductTitle() + ' ';

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
    [Name(FindResultsFilenameProxy)]
    public static ClassificationTypeDefinition FindResultsFilenameProxyDefinition { get; set; }

    [Export]
    [Name(FindResultsSearchTermProxy)]
    public static ClassificationTypeDefinition FindResultsSearchTermProxyDefinition { get; set; }

    [Name(BuildHead)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = BuildHead)]
    public sealed class BuildHeaderFormat : ClassificationFormatDefinition {
      public BuildHeaderFormat() {
        DisplayName = _vsColorOut + "Build Header";
        ForegroundColor = Colors.Green;
      }
    }

    [Name(BuildText)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = BuildText)]
    public sealed class BuildTextFormat : ClassificationFormatDefinition {
      public BuildTextFormat() {
        DisplayName = _vsColorOut + "Build Text";
        ForegroundColor = Colors.Gray;
      }
    }

    [Name(FindResultsFilename)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsFilename)]
    public sealed class FindResultsFilenameFormat : ClassificationFormatDefinition {
      public FindResultsFilenameFormat() {
        DisplayName = _vsColorOut + "Find Results Filename";
        ForegroundColor = Colors.Gray;
      }
    }

    [Name(FindResultsFilenameProxy)]
    [UserVisible(false)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsFilenameProxy)]
    public sealed class FindResultsFilenameProxyFormat : ClassificationFormatDefinition {
      public FindResultsFilenameProxyFormat() {
        DisplayName = _vsColorOut + "Find Results Filename";

        // Load the default colors from the registry
        var key = VSRegistry.RegistryRoot(__VsLocalRegistryType.RegType_UserSettings, true).CreateSubKey(@"DialogPage\" + typeof (FindResultsFilenameProxyFormat).Namespace + ".ColorizeOutputOptions");
        if (key == null)
          return;
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
        DisplayName = _vsColorOut + "Find Results Search Term";
        ForegroundColor = Colors.Blue;
      }
    }

    [Name(FindResultsSearchTermProxy)]
    [UserVisible(false)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = FindResultsSearchTermProxy)]
    public sealed class FindResultsSearchTermProxyFormat : ClassificationFormatDefinition {
      public FindResultsSearchTermProxyFormat() {
        DisplayName = _vsColorOut + "Find Results Search Term";

        // Load the default colors from the registry
        var key = VSRegistry.RegistryRoot(__VsLocalRegistryType.RegType_UserSettings, true).CreateSubKey(@"DialogPage\" + typeof (FindResultsSearchTermProxyFormat).Namespace + ".ColorizeOutputOptions");
        if (key == null)
          return;
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
        DisplayName = _vsColorOut + "Log Custom1";
        ForegroundColor = Colors.Purple;
      }
    }

    [Name(LogCustom2)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom2)]
    public sealed class LogCustom2Format : ClassificationFormatDefinition {
      public LogCustom2Format() {
        DisplayName = _vsColorOut + "Log Custom2";
        ForegroundColor = Colors.DarkSalmon;
      }
    }

    [Name(LogCustom3)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom3)]
    public sealed class LogCustom3Format : ClassificationFormatDefinition {
      public LogCustom3Format() {
        DisplayName = _vsColorOut + "Log Custom3";
        ForegroundColor = Colors.DarkOrange;
      }
    }

    [Name(LogCustom4)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogCustom4)]
    public sealed class LogCustom4Format : ClassificationFormatDefinition {
      public LogCustom4Format() {
        DisplayName = _vsColorOut + "Log Custom4";
        ForegroundColor = Colors.Brown;
      }
    }

    [Name(LogError)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogError)]
    public sealed class LogErrorFormat : ClassificationFormatDefinition {
      public LogErrorFormat() {
        DisplayName = _vsColorOut + "Log Error";
        ForegroundColor = Colors.Red;
      }
    }

    [Name(LogInfo)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogInfo)]
    public sealed class LogInformationFormat : ClassificationFormatDefinition {
      public LogInformationFormat() {
        DisplayName = _vsColorOut + "Log Information";
        ForegroundColor = Colors.DarkBlue;
      }
    }

    [Name(LogWarn)]
    [UserVisible(true)]
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LogWarn)]
    public sealed class LogWarningFormat : ClassificationFormatDefinition {
      public LogWarningFormat() {
        DisplayName = _vsColorOut + "Log Warning";
        ForegroundColor = Colors.Olive;
      }
    }
  }
}
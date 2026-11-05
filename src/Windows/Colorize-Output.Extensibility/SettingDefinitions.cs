// The Settings API is preview-only in the SDK; the diagnostic must be suppressed to use it.
#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace ColorizeOutput.Extensibility;

// Only the option flags survive: output classification, margins and EnvDTE build events have no SDK equivalent.
public static class SettingDefinitions
{
    [VisualStudioContribution]
    public static SettingCategory ColorizeOutputCategory { get; } = new("colorizeOutput", "%ColorizeOutput.Category.DisplayName%") { Description = "%ColorizeOutput.Category.Description%" };

    [VisualStudioContribution]
    public static Setting.Boolean EnableStopOnBuildError { get; } = new("enableStopOnBuildError", "%ColorizeOutput.EnableStopOnBuildError.DisplayName%", ColorizeOutputCategory, defaultValue: false);

    [VisualStudioContribution]
    public static Setting.Boolean ShowElapsedBuildTime { get; } = new("showElapsedBuildTime", "%ColorizeOutput.ShowElapsedBuildTime.DisplayName%", ColorizeOutputCategory, defaultValue: false);

    [VisualStudioContribution]
    public static Setting.Boolean ShowBuildReport { get; } = new("showBuildReport", "%ColorizeOutput.ShowBuildReport.DisplayName%", ColorizeOutputCategory, defaultValue: false);

    [VisualStudioContribution]
    public static Setting.Boolean SuppressDonation { get; } = new("suppressDonation", "%ColorizeOutput.SuppressDonation.DisplayName%", ColorizeOutputCategory, defaultValue: false);
}

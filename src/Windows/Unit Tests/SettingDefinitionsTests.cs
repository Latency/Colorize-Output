#pragma warning disable VSEXTPREVIEW_SETTINGS
using ColorizeOutput.Extensibility;

namespace Unit_Tests;

public class SettingDefinitionsTests
{
    [Fact]
    public void Category_HasExpectedId() => Assert.Equal("colorizeOutput", SettingDefinitions.ColorizeOutputCategory.Id);

    [Fact]
    public void Booleans_DefaultToFalse()
    {
        Assert.False(SettingDefinitions.EnableStopOnBuildError.DefaultValue);
        Assert.False(SettingDefinitions.ShowElapsedBuildTime.DefaultValue);
        Assert.False(SettingDefinitions.ShowBuildReport.DefaultValue);
        Assert.False(SettingDefinitions.SuppressDonation.DefaultValue);
    }
}




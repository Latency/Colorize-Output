using Microsoft.VisualStudio.Extensibility;

namespace ColorizeOutput.Extensibility;

[VisualStudioContribution]
internal class ColorizeOutputExtension : Extension
{
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
            id: "ColorizeOutput.Extensibility.cd56b219-38cb-482a-9b2d-7582df4aaf1e",
            version: ExtensionAssemblyVersion,
            publisherName: "Mike Ward",
            displayName: "VSColorOutput",
            description: "Build timing and settings for VSColorOutput (out-of-process).")
        {
            DotnetTargetVersions = ["net8.0"],
        },
    };
}

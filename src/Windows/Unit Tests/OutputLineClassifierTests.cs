using ColorizeOutput.Extensibility;
using Microsoft.VisualStudio.Extensibility.Editor;

namespace Unit_Tests;

public class OutputLineClassifierTests
{
    [Theory]
    [InlineData("hello world")]
    [InlineData("")]
    public void PlainLine_IsNotClassified(string text) => Assert.Null(OutputLineClassifier.Classify(text));

    [Fact]
    public void Header_IsKeyword() => Assert.Equal(ClassificationType.KnownValues.Keyword, OutputLineClassifier.Classify("===== Build ====="));

    [Fact]
    public void Error_IsString() => Assert.Equal(ClassificationType.KnownValues.String, OutputLineClassifier.Classify("a.cs: error CS0001: bad"));

    [Fact]
    public void Warning_IsComment() => Assert.Equal(ClassificationType.KnownValues.Comment, OutputLineClassifier.Classify("a.cs: warning CS0001: meh"));

    [Fact]
    public void Information_IsNumber() => Assert.Equal(ClassificationType.KnownValues.Number, OutputLineClassifier.Classify("information: done"));
}

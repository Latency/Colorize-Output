using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Editor;

namespace ColorizeOutput.Extensibility;

[VisualStudioContribution]
internal class OutputTaggerProvider : ExtensionPart, ITextViewTaggerProvider<ClassificationTag>
{
    public TextViewExtensionConfiguration TextViewExtensionConfiguration => new()
    {
        AppliesTo = [DocumentFilter.FromDocumentType("output")],
    };

    public Task<TextViewTagger<ClassificationTag>> CreateTaggerAsync(ITextViewSnapshot textView, CancellationToken cancellationToken)
        => Task.FromResult<TextViewTagger<ClassificationTag>>(new OutputTagger(textView));
}

internal sealed class OutputTagger(ITextViewSnapshot textView) : TextViewTagger<ClassificationTag>
{
    protected override async Task RequestTagsAsync(NormalizedTextRangeCollection requestedRanges, bool recalculateAll, CancellationToken cancellationToken)
    {
        var document = textView.Document;
        var tags = new List<TaggedTrackingTextRange<ClassificationTag>>();

        var lineStart = 0;
        foreach (var line in document.Lines)
        {
            var length = line.Text.Length;
            var lineEnd = lineStart + length;
            var requested = false;
            foreach (var range in requestedRanges)
            {
                if (range.Start <= lineEnd && range.End >= lineStart)
                {
                    requested = true;
                    break;
                }
            }

            if (requested && length > 0 && OutputLineClassifier.Classify(line.Text.ToString() ?? string.Empty) is { } type)
            {
                tags.Add(new TaggedTrackingTextRange<ClassificationTag>(document, lineStart, length, TextRangeTrackingMode.ExtendNone, new ClassificationTag(type)));
            }

            lineStart += line.TextIncludingLineBreak.Length;
        }

        await UpdateTagsAsync(requestedRanges, tags, cancellationToken);
    }
}

public static class OutputLineClassifier
{
    private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(250);

    private static readonly (Regex Pattern, ClassificationType Type)[] Rules =
    [
        (new(@"^\s*(=====|-----|Projects build report)|\d+ error\(s\)|\d+ warning\(s\)", RegexOptions.IgnoreCase, Timeout), ClassificationType.KnownValues.Keyword),
        (new(@"\b(error|failed|fail|exception)\b|Could not find file", RegexOptions.IgnoreCase, Timeout), ClassificationType.KnownValues.String),
        (new(@"\bwarning\b", RegexOptions.IgnoreCase, Timeout), ClassificationType.KnownValues.Comment),
        (new(@"\binformation\b", RegexOptions.IgnoreCase, Timeout), ClassificationType.KnownValues.Number),
    ];

    public static ClassificationType? Classify(string text)
    {
        foreach (var (pattern, type) in Rules)
        {
            try
            {
                if (pattern.IsMatch(text))
                {
                    return type;
                }
            }
            catch (RegexMatchTimeoutException)
            {
            }
        }

        return null;
    }
}


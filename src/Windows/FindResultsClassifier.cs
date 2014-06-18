// ****************************************************************************
// * Project:  ColorizeOutput
// * File:     FindResultsClassifier.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

#endregion

// Disable warning for unused ClassificationChanged event
#pragma warning disable 67

namespace ColorizeOutput {
  public class FindResultsClassifier : IClassifier {
    private const string FindAll = "Find all \"";
    private const string MatchCase = "Match case";
    private const string WholeWord = "Whole word";
    private const string ListFilenamesOnly = "List filenames only";

    private static readonly Regex FilenameRegex;
    private readonly IClassificationTypeRegistryService classificationRegistry;

    private Regex searchTextRegex;

    static FindResultsClassifier() {
      FilenameRegex = new Regex(@"^\s*.:.*\(\d+\):", RegexOptions.Compiled);
    }

    public FindResultsClassifier(IClassificationTypeRegistryService classificationRegistry) {
      this.classificationRegistry = classificationRegistry;
    }

    private IClassificationType SearchTermClassificationType {
      get { return classificationRegistry.GetClassificationType(OutputClassificationDefinitions.FindResultsSearchTerm); }
    }

    private IClassificationType FilenameClassificationType {
      get { return classificationRegistry.GetClassificationType(OutputClassificationDefinitions.FindResultsFilename); }
    }

    public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
      var classifications = new List<ClassificationSpan>();

      var snapshot = span.Snapshot;
      if (snapshot == null || snapshot.Length == 0 || !CanSearch(span))
        return classifications;

      var text = span.GetText();

      var filenameSpans = GetMatches(text, FilenameRegex, span.Start, FilenameClassificationType).ToList();
      var searchTermSpans = GetMatches(text, searchTextRegex, span.Start, SearchTermClassificationType).ToList();

      var toRemove = (from searchSpan in searchTermSpans from filenameSpan in filenameSpans where filenameSpan.Span.Contains(searchSpan.Span) select searchSpan).ToList();

      classifications.AddRange(filenameSpans);
      classifications.AddRange(searchTermSpans.Except(toRemove));
      return classifications;
    }

    public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

    private bool CanSearch(SnapshotSpan span) {
      if (span.Start.Position != 0 && searchTextRegex != null)
        return true;
      searchTextRegex = null;
      var firstLine = span.Snapshot.GetLineFromLineNumber(0).GetText();
      if (firstLine.StartsWith(FindAll)) {
        var strings = (from s in firstLine.Split(',') select s.Trim()).ToList();

        var start = strings[0].IndexOf('"');
        var searchTerm = strings[0].Substring(start + 1, strings[0].Length - start - 2);
        var matchCase = strings.Contains(MatchCase);
        var matchWholeWord = strings.Contains(WholeWord);
        var filenamesOnly = strings.Contains(ListFilenamesOnly);

        if (!filenamesOnly) {
          var regex = matchWholeWord ? string.Format(@"\b{0}\b", Regex.Escape(searchTerm)) : Regex.Escape(searchTerm);
          var casing = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
          searchTextRegex = new Regex(regex, RegexOptions.None | casing);

          return true;
        }
      }
      return false;
    }

    private static IEnumerable<ClassificationSpan> GetMatches(string text, Regex regex, SnapshotPoint snapStart, IClassificationType classificationType) {
      return from match in regex.Matches(text).Cast<Match>() select new ClassificationSpan(new SnapshotSpan(snapStart + match.Index, match.Length), classificationType);
    }
  }
}
// ****************************************************************************
// * Project:  Colorize-Output
// * File:     OutputClassifierProvider.cs
// * Date:     07/26/2014
// ****************************************************************************

#region

using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

#endregion

#pragma warning disable 649

namespace ColorizeOutput {
  [ContentType("output")]
  [Export(typeof (IClassifierProvider))]
  public class OutputClassifierProvider : IClassifierProvider {
    [Import]
    internal IClassificationTypeRegistryService ClassificationRegistry;

    [Import]
    internal SVsServiceProvider ServiceProvider;

    public static OutputClassifier OutputClassifier { get; private set; }

    public IClassifier GetClassifier(ITextBuffer buffer) {
      try {
        if (OutputClassifier == null)
          OutputClassifier = new OutputClassifier(ClassificationRegistry, ServiceProvider);
      } catch (Exception ex) {
        OutputClassifier.LogError(ex.ToString());
        throw;
      }
      return OutputClassifier;
    }
  }
}
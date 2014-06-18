// ****************************************************************************
// * Project:  Tests
// * File:     FindResultsClassifierProviderTests.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System.ComponentModel.Composition;
using ColorizeOutput;
using FluentAssertions;
using Microsoft.VisualStudio.Utilities;
using NUnit.Framework;

#endregion

namespace Tests {
  [TestFixture]
  public class FindResultsClassifierProviderTests {
    [Test]
    public void GetClassifierAttributes() {
      typeof (FindResultsClassifierProvider).Should().BeDecoratedWith<ContentTypeAttribute>();
      typeof (FindResultsClassifierProvider).Should().BeDecoratedWith<ExportAttribute>();
    }

    [Test]
    public void GetClassifierReturnsSameInstance() {
      var provider = new FindResultsClassifierProvider();
      var classifier = provider.GetClassifier(null);
      classifier.Should().NotBeNull();
      classifier.Should().BeSameAs(provider.GetClassifier(null));
    }
  }
}
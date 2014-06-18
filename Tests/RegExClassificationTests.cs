// ****************************************************************************
// * Project:  Tests
// * File:     RegExClassificationTests.cs
// * Date:     06/18/2014
// ****************************************************************************

#region

using System;
using ColorizeOutput;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace Tests {
  [TestFixture]
  public class RegExClassificationTests {
    [Test, ExpectedException(typeof (ArgumentException))]
    public void BadRegExExpressionShouldRaiseException() {
      new RegExClassification {RegExPattern = @"(\d"};
    }

    [Test, ExpectedException(typeof (ArgumentNullException))]
    public void RegExPatternCannotBeSetToNull() {
      new RegExClassification {RegExPattern = null};
    }

    [Test]
    public void ToStringFormat() {
      var rc = new RegExClassification {RegExPattern = "/d", ClassificationType = ClassificationTypes.BuildText, IgnoreCase = true};
      rc.ToString().Should().Be("\"/d\",BuildText,True");
    }
  }
}
// ****************************************************************************
// * Project:  Colorize-Output
// * File:     RegExClassification.cs
// * Date:     07/26/2014
// ****************************************************************************

#region

using System;
using System.Text.RegularExpressions;

#endregion

namespace ColorizeOutput {
  public class RegExClassification {
    private string _regExPattern;

    public RegExClassification() {
      _regExPattern = ".*";
    }

    public string RegExPattern {
      get { return _regExPattern; }
      set {
        ValidatePattern(value);
        _regExPattern = value;
      }
    }

    public ClassificationTypes ClassificationType { get; set; }
    public bool IgnoreCase { get; set; }

    public override string ToString() {
      return String.Format("\"{0}\",{1},{2}", RegExPattern ?? "null", ClassificationType, IgnoreCase);
    }

    private static void ValidatePattern(string regex) {
      new Regex(regex);
    }
  }
}
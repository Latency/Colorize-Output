![Logo](Resources/ColorizeOutputLogo.png)

## Navigation

* [Introduction](#introduction)
* [History](#history)
* [Installation](#installation)
* [How It Works](#how-it-works)
* [Usage](#usage)
* [Creating Patterns](#creating-patterns)
* [Other Features](#other-features)
* [Contributing](#contributing)
* [License](#license)

---

## Introduction

ColorizeOutput can change the color of a line emitted to the output window based on specified rules. The rules consist of regular expressions. Rules map to classifications which in turn map to colors.

The default patterns will color build errors in red, warnings in yellow/gold and successful build messages in green.

![Build output](Resources/vscoloroutput.png)

## History

ColorG(CC/++) was originally conceived from the PERL '99 project and developed for POSIX platforms by Latency McLaughlin in 1999-2000 with GNU C++. The goal was to handle better formatting capabilities and enhanced console support. It featured global environment integration by default or could be overridden by the individual user account for the environment.

For Windows, it is now contained within the IDE itself and can be configured using the menu option preferences.

## How It Works

ColorizeOutput hooks into the classifier chain of Visual Studio. This allows ColorizeOutput to monitor every line sent to the output window. A list of classifiers, consisting of regular expressions and classifications, is checked. The first matching expression determines the classification. If no patterns match, then the line is classified as **Build Text**.

From here, Visual Studio does the heavy lifting of mapping the classification to a color. Colors are stored in the registry.

## Usage

Colors are set in the *Tools | Options | Fonts and Colors | Text Editor* dialog. ColorizeOutput colors start with "VSColor" so they group together in the list and are easy to locate. Logically, it makes sense to add these to the "Output" category of the "Fonts and Colors" dialog but, interestingly, Visual Studio does not support adding colors to this category.

![Colors dialog](Resources/vscoloroutputcolors.png)

There are nine VSColors classifications:

- Build Text
- Build Header
- Log Information
- Log Warning
- Log Error
- Log Custom1
- Log Custom2
- Log Custom3
- Log Custom4

The names reflect their intended use but are entirely arbitrary in actual use.

**Build Text** is the default classification for any line that does not match the other patterns. Its default color is "Gray". This helps to highlight the other classified lines.

## Creating Patterns

The *Tools | Options | ColorizeOutput* dialog contains settings. You can add, delete or edit the patterns. Patterns are regular expressions using the [.NET form](http://msdn.microsoft.com/en-us/library/hs600312.aspx), which varies slightly from those used by Ruby, JavaScript, Python, etc.

![Patterns dialog](Resources/vscoloroutputpatterns.png)

At run-time, ColorizeOutput walks this list in order, testing the line of text against the regular expression. If it matches, the line is given the classification associated with the pattern. No additional patterns are tested for the given line. Therefore, the order of the classifiers is significant.

## Other Features

### Stop Build On First Error

Pretty much does what it says. A real time saver on larger projects.

### Show Elapsed Build Time

If you build from the command line, MSBuild tells you how long the build takes. Building within Visual Studio does not.

### Show Debug Window when Debug Starts

Visual Studio has a "Show Build Window when Build Starts". Now you have one for the debug session. If you run your debugger output in a tiled window, this won't have much effect. If you run it in a tabbed window then this setting will activate the debug window, saving you a few mouse clicks.
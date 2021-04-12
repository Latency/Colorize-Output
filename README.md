![ColorizeOutput Logo](http://bio-hazard.us/colorizeoutput/images/ColorizeOutputLogo.png)
=====================================================

<ul>
  <li><a href="#introduction">Introduction</a>
  <li><a href="#history">History</a>
  <li><a href="#installation">Installation</a>
  <li><a href="#howitworks">How It Works</a>  
  <li><a href="#usage">Usage</a>
  <li><a href="#patterns">Creating Patterns</a>
  <li><a href="#features">Other Features</a>
  <li><a href="#references">References</a> </li>
  <li><a href="#license">License</a> </li>
</ul>

<p>A Visual Studio extension to colorize your build and debug output.</p>

<a name="introduction"><h2>Introduction</h2></a>

ColorizeOutput can change the color of a line emitted to the output
window based on specified rules. The rules consist of regular
expressions. Rules map to classifications which in turn map to colors.

The default patterns will color build errors in red, warnings in
yellow/gold and successful build messages in green.

![screen shot of VSColorOutput build output](http://bio-hazard.us/colorizeoutput/images/vscoloroutput.png)

<a name="history"><h2>History</h2></a>

ColorG(CC/++) was originally conceived from PERL \`99 project and
developed for POSIX platforms written by Latency McLaughlin in 1999-2000
with GNU C++.  The goal was to handle better formatting capabilities and
enhanced console support.  It featured global environment integration by
default or could be overridden by the individual's user account for the
environment.

For Windows, it is now contained within the IDE itself and can be
configured using the menu option preferences.

<a name="installation"><h2>Installation</h2></a>

Download and open the
[ColorizeOutput.vsix](http://visualstudiogallery.msdn.microsoft.com/f4d9c2b5-d6d7-4543-a7a5-2d7ebabc2496)
file.  To uninstall, go the *Tools|Extensions* page, find ColorizeOutput
in the "Installed Extensions" and click uninstall.  Registry entries are
not removed, so later installations will reuse these same settings.

<a name="howitworks"><h2>How It Works</h2></a>

ColorizeOutput hooks into the the classifier chain of Visual Studio.
This allows ColorizeOutput to monitor every line sent to the output
window.  A list of classifiers, consisting of regular expressions and
classifications is checked. The first matching expression determines the
classification.  If no patterns match, then line is classified as
**BuildText**.

From here, Visual Studio does the heavy lifting of mapping the
classification to a color.  Colors are stored in the registry.

<a name="usage"><h2>Usage</h2></a>

Colors are set in the *Tools|Options|Fonts and Colors|Text Editor*
dialog. ColorizeOutput colors start with "VSColor" so they group
together in the list and are easy to locate. Logically, it makes sense
to add these to the "Output" category of the "Fonts and Colors" dialog
but, interestingly, Visual Studio does not support adding colors to this
category.

![screen shot of VSColorOutput colors
dialog](http://bio-hazard.us/colorizeoutput/images/vscoloroutputcolors.png)

There are nine VSColors classifications. They are:

-   Build Text
-   Build Header
-   Log Information
-   Log Warning
-   Log Error
-   Log Custom1
-   Log Custom2
-   Log Custom3
-   Log Custom4

The names reflect their intended use but are entirely arbitrary in
actual use.

**Build Text** is the default classification for any line that does not
match the other patterns. Its default color is "Gray". I've found this
helps to highlight the other classified lines.

<a name="patterns"><h2>Creating Patterns</h2></a>

The *Tools|Options|ColorizeOutput* dialog contains settings. You can
add, delete or edit the patterns. Patterns are regular expressions. The
regular expressions use the .NET form
(<http://msdn.microsoft.com/en-us/library/hs600312.aspx>), which varies
slightly from those used by Ruby, JavaScript, Python, etc.

![screen shot of VSColorOutput options
dialog](http://bio-hazard.us/colorizeoutput/images/vscoloroutputoptions.png)

![screen shot of VSColorOutput patterns
dialog](http://bio-hazard.us/colorizeoutput/images/vscoloroutputpatterns.png)

At run-time, ColorizeOutput will walk this list in order, testing the
line of text against the regular expression. If it matches, the line is
given the classification associated with the pattern. No additional
patterns are tested for the given line. Therefore, the order of the
classifiers is significant.

<a name="features"><h2>Other Features</h2></a>

**Stop Build On First Error**

Pretty much does what it says. A real time saver on larger projects.

**Show Elapsed Build Time**

If you build from the command line, MSBuild tells you how long the build
takes. Building within Visual Studio does not.

**Show Debug Window when Debug Starts**

Visual Studio has a "Show Build Window when Build Starts". Now you have
one for the debug session. If you run your debugger output in a tiled
window, this won't have much affect. If you run it in a tabbed window
then this setting will activate the debug window saving you a few mouse
clicks.

<a name="references"><h2>References</h2></a>

ColorizeOutput is open source.

Contributions should be 100% free.  This means, removing banners and
donate buttons that put money in the repository owners pockets.

<a name="license"><h2>License</h2></a>
<div id="LicenseTerms">
  <p>
    <a href="http://www.gnu.org/copyleft/gpl.html">GNU LESSER GENERAL PUBLIC LICENSE</a>
    Version 3, 29 June 2007
  </p>
</div>
<hr>
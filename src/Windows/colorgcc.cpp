/*****************************************************************************
 * File:     colorgcc.cpp
 * Designer: Latency McLaughlin                        Bio-Hazard Industries *
 * Date:     12/28/00                          ©1994  ¯  All Rights Reserved *
 * Usage:    Colorizes GNU compiler output warnings and errors.              *
 *****************************************************************************/
#include <iostream>
#include <fstream>
#include <cstdarg>
#include <ctypes.hpp>			// Our library
#include <sstream>
#include <vector>
#include <sys/poll.h>
#include <boost/format.hpp>
#include <map>

using namespace std;
using namespace boost;

const char *ColorCodes[][2] = {
    { "BLACK", "\e[0;30m" },
    { "RED", "\e[0;31m" },
    { "GREEN", "\e[0;32m" },
    { "BROWN", "\e[0;33m" },
    { "BLUE", "\e[0;34m" },
    { "MAGENTA", "\e[0;35m" },
    { "TURQUISE", "\e[0;36m" },
    { "GREY", "\e[0;37m" },
    { "ON_BLACK", "\e[0;40m" },
    { "ON_RED", "\e[0;41m" },
    { "ON_GREEN", "\e[0;42m" },
    { "ON_BROWN", "\e[0;43m" },
    { "ON_BLUE", "\e[0;44m" },
    { "ON_MAGENTA", "\e[0;45m" },
    { "ON_TURQUISE", "\e[0;46m" },
    { "ON_GREY", "\e[0;47m" },
    { "CHARCOAL", "\e[1;30m" },
    { "ORANGE", "\e[1;31m" },
    { "LIME", "\e[1;32m" },
    { "YELLOW", "\e[1;33m" },
    { "FUSCHIA", "\e[1;34m" },
    { "PINK", "\e[1;35m" },
    { "CYAN", "\e[1;36m" },
    { "WHITE", "\e[1;37m" },
    { "ON_CHARCOAL", "\e[1;40m" },
    { "ON_ORANGE", "\e[1;41m" },
    { "ON_LIME", "\e[1;42m" },
    { "ON_YELLOW", "\e[1;43m" },
    { "ON_FUSCHIA", "\e[1;44m" },
    { "ON_PINK", "\e[1;45m" },
    { "ON_CYAN", "\e[1;46m" },
    { "ON_WHITE", "\e[1;47m" },
    { "NORMAL", "\e[0m" },
    { "BOLD", "\e[1m" },
    { "DARK", "\e[2m" },
    { "FLASH", "\e[5m" },
    { "INVERSE", "\e[7m" },
    { "\0", "\0" }
};


const char *DFS[] = {
  "cc",
  "c++",
  "gcc",
  "g++",
  "srcColor",
  "introColor",
  "warningFileNameColor",
  "warningNumberColor",
  "warningMessageColor",
  "warningMessageQuoteColor",
  "warningMessageStatusColor",
  "errorFileNameColor",
  "errorFileNameProtoColor",
  "errorNumberColor",
  "errorMessageColor",
  "errorMessageQuoteColor",
  "errorMessageStatusColor",
  "\0"
};


map<string, string>		dfs, Color;
string title;

// Exit status for program termination.
bool Retval = false;
// Inherited header dependency flag used for formating.
bool Recursive_Hdr = false, Mangled = false;
ushort handler;

#define Out (cerr)

enum Error_Condition { NONE, WARNING, ERROR };


// -------------------------------------------------------------------------
// Initializes the containers that hold our color values for string parsing.
// To be used as a fail safe or default mechanism if no deflt file was found.
// -------------------------------------------------------------------------
void Initialize_Defaults(const char *arg)
{
  ushort x = 0;
  while (*ColorCodes[x][0]) {
    Color[ColorCodes[x][0]] = ColorCodes[x][1];
    x++;
  }
  dfs[DFS[2]] = dfs[DFS[0]] = "/usr/bin/gcc-" COMPILER_VER;
  dfs[DFS[3]] = dfs[DFS[1]] = "/usr/bin/g++-" COMPILER_VER;
  dfs[DFS[4]] = Color["GREY"];
  dfs[DFS[5]] = Color["PINK"];
  dfs[DFS[6]] = Color["YELLOW"];
  dfs[DFS[7]] = Color["LIME"];
  dfs[DFS[8]] = Color["CHARCOAL"];
  dfs[DFS[9]] = Color["FUSCHIA"];
  dfs[DFS[10]] = Color["CYAN"];
  dfs[DFS[11]] = Color["BROWN"];
  dfs[DFS[12]] = Color["DARK"];
  dfs[DFS[13]] = Color["GREEN"];
  dfs[DFS[14]] = Color["ORANGE"];
  dfs[DFS[15]] = Color["RED"];
  dfs[DFS[16]] = Color["MAGENTA"];
  title = io::str(format("%1%??? (%2%colorized%1%) v%3%\t\t%4%Bio-Hazard Industries%5% \t©1994  -  All Rights Reserved\n") %
    Color["YELLOW"] % Color["GREEN"] % VERSION % Color["RED"] % Color["NORMAL"]);
  // ---------------------------------------------------------------------------
  // Replaces our logo with the correct compiler.  Used for universal usage.
  // ---------------------------------------------------------------------------
  size_t y;
  if ((y = title.find("???")) != string::npos) {
    title.erase(y, 3);
    string cmd = arg;
    title.insert(y, cmd);
  }
}


// ---------------------------------------------------------------------------
// Send our errors out to stderr.
// ---------------------------------------------------------------------------
void Perror(const char *buf)
{
  Out << title << buf << endl;
  Out.flush();
  exit(1);
}

void Perror(format & buf)
{
  Perror(io::str(buf).c_str());
}

void Perror(const string & buf)
{
  Perror(buf.c_str());
}


// -------------------------------------------------------------------------
// Takes an input of a string and compares/converts its type to an actual
//  code esc¯seq.
// -------------------------------------------------------------------------
const string & ConvertColor(string & str, ushort line)
{
  string word, new_str;
  while (str.length()) {
    // ---------------------------------------------------------------------
    // Copy the first word... check its name with idx of color names.
    // ---------------------------------------------------------------------
    word = cpyfwd(str);
    bool bit = false;
    ushort x = 0;
    while (*ColorCodes[x][0]) {
      if (!strcasecmp(word.c_str(), ColorCodes[x][0])) {
        new_str += ColorCodes[x][1];
        bit = true;
        break;
      }
      x++;
    }
    // Was there a match found, if not then the state bit is 0 and we terminate
    // the calling program.
    if (!bit)
      Perror(format("Invalid color type defined at line '%d'.") % line);
    advwrd(str);				// advance our string ptr to the next word.
  }
  return (str = new_str);
}


// -------------------------------------------------------------------------
// Read the configuration file.
// -------------------------------------------------------------------------
void ReadConfigFile(bool dependancy)
{
  // Check $HOME to see if we have a configuration file there.
  char *ptr;
  // Check to see if we have a configuration file in ~/ shell.
  if (!(ptr = getenv("HOME")))
    Perror("Unable to determine environment variable 'HOME'.");
  string path = io::str(format("%s/.%s") % ptr % EXE_FILE);
  // Open the file we found @$HOME.  If !open'd, try to open the systems dflt configuration file.
  ifstream in(path.c_str());
  if (!in.is_open()) {
    in.clear();
    path = RC_PATH "/" EXE_FILE;
    in.open(path.c_str());
    if (!in.is_open()) {
      if (!dependancy) {
        Out << "Unable to load configuration files for `" << title << "'.\nUsing defaults.";
        sleep(1);
        Out << '.';
        sleep(1);
        Out << ".\n";
      }
      return;
    }
  }
  // Process the configuration file by line.
  char buf[PATH_MAX];
  string ref, target;
  ushort line = 0, x;
  bool bit;
  // Stream until we haven't any more El`captain.
  while (!in.eof() && in.getline(buf, sizeof(buf))) {
    line++;
    ref = buf;
    if (ref.empty() || ref[0] == '#')	// Skip any blank or comment'd lines.
      continue;
    // Take the first word of our string and store it as our target.
    target = cpyfwd(ref);
    // Advance our string ptr, to the next word.
    advwrd(ref);
    trim(ref);
    // Probable ': ......->' only in format string.  No tag referenced.
    if (ref.empty())
      Perror(format("Invalid format for '%s' file at line #%d.\nNo tag referenced for identifier `%s'.\n") % path % line % target);
    // No tag seperator specified.  e.g. no ':' found after 1st word.
    if (!target.empty()) {
      if (*--target.end() != ':') {
        Perror(format("Invalid format for '%s' file at line #%d.\nNo tag seperator found.  Usage:  \"tag: <argument_list>\"") % path % line);
      } else	// Strip the ':' off the tag.
        target.erase(target.length() - 1);
    }
    // Find a match to our string off the dictionary table.
    x = 4;						// See DFS[0-3]  "cc, c++, gcc, g++"
    bit = false;
    while (*DFS[x]) {
      if (!strcasecmp(target.c_str(), DFS[x])) {
        dfs[DFS[x]] = ConvertColor(ref, line);
        bit = true;
        break;
      }
      x++;
    }
    // Check the state bit to see if a word was actually found.
    if (!bit)
      Perror(format("Invalid format for '%s' file at line #%d.\nUnrecognizable tag identifier.") % path % line);
  }
  in.close();					// Close the configuration file stream.
}


// ---------------------------------------------------------------------------
// Parses each token.
// ---------------------------------------------------------------------------
void Output_Token(string & strm, size_t idx, vector < string > &v, ushort cond)
{
  string str;
  while (idx < v.size()) {
    if (idx > 4)
      str += io::str(format("%s:%s") % Color["NORMAL"] % dfs["errorMessageColor"]);
    str += v[idx];
    idx++;
  }
  ushort x = 0;
  while (x < str.length()) {
    if (str[x] == '`') {
      Mangled = true;
      strm += str[x];
      strm += (cond == ERROR ? dfs["errorMessageQuoteColor"] : (cond == WARNING ? dfs["warningMessageQuoteColor"] : dfs["srcColor"]));
    } else if (str[x] == '\'') {
      if (cond == ERROR) {
        Mangled = !Mangled;
        if (Mangled)
          strm += str[x];
        strm += (Mangled ? dfs["errorMessageQuoteColor"] : dfs["errorMessageColor"]);
        if (!Mangled)
          strm += str[x];
      } else {
        Mangled = false;
        strm += (cond == WARNING ? dfs["warningMessageColor"] : dfs["errorNumberColor"]);
        strm += str[x];
      }
    } else
      strm += str[x];
    x++;
  }
}


// -------------------------------------------------------------------------
// Parses strings according to token match.  Prints to stdout.
// -------------------------------------------------------------------------
void ParseString(const string & s, const bool dependancy)
{
#ifdef DEBUG
  Out << "'" << s << "'\n";
#endif

  // Designated our ptrs, and make a copy of the original incomming string.
  string tmp, tmp1, overflow;
  // Break the string up into tokens.
  vector < string > v;
  stringstream strm(s);
  while (!strm.str().empty()) {
    tmp1 = strm.str();
    if (getline(strm, tmp, ':'))
      tmp1.erase(0, tmp.length() + 1);
    else
      tmp1.erase(0, 1);
    // Special handlign for cons operator.
    if (tmp.empty()) {
      tmp = v.back();
      tmp += "::";
      overflow = tmp;
      v.pop_back();
    } else {
      if (!overflow.empty()) {
        overflow += tmp;
        tmp = overflow;
      }
      v.push_back(tmp);
      overflow.clear();
    }
    strm.str(tmp1);
  }

#ifdef DEBUG
  ushort len = 0;
  while (len < v.size())
    Out << '|' << v[len++] << '|' << '\n';
#endif

  // -------------------------------------------------------------------------
  // Determine which case we should go to in order to process our ptrs'.
  // -------------------------------------------------------------------------
  // Sanity check.  CaseNO : should already be 2.
  size_t idx, caseNO = v.size();
  bool gt1 = bool(v.size() > 1);
  bool gt2 = bool(v.size() > 2);
  bool gt3 = bool(v.size() > 3);
  string var0 = (v.size() ? v[0] : ""), var1 = (gt1 ? v[1] : ""), var2 = (gt2 ? v[2] : ""), var3 = (gt3 ? v[3] : ""), buf, buf1, sbuf;
  (gt1 && IsNumber(var1.c_str()) ? caseNO = 1 : (caseNO >= 2 && gt1 && !var1.empty()) ? /* Making sure that v[1] is !empty */ : caseNO = 0);

#ifdef DEBUG
  Out << "-----> CaseNO = " << caseNO << '\n';
#endif

  switch (caseNO) {
    case 0:					// Doesn't seem to be a warning or an error.
      if (!Mangled)
        sbuf += (handler == WARNING ? dfs["warningMessageColor"] : (handler == ERROR ? dfs["errorMessageColor"] : dfs["errorNumberColor"]));
      else
        sbuf += (handler == WARNING ? dfs["warningMessageQuoteColor"] : (handler == ERROR ? dfs["errorMessageQuoteColor"] : dfs["srcColor"]));
      Output_Token(sbuf, (idx = 0), v, handler);
      sbuf += io::str(format("%s\n") % Color["NORMAL"]);	// Print normally.
      break;

    case 1:					// Filename:LineNumber:Message
      Mangled = false;
      // ---------------------------------------------------------------------
      // Warning.
      // ---------------------------------------------------------------------
      if (var2.find("arning") != string::npos) {	// [Ww]arning
        sbuf = io::str(format("%1%%2%%3%:%4%%5%%3%:%6%%7%%3%:%8%") %
          dfs["warningFileNameColor"] % var0 % Color["NORMAL"] % dfs["warningNumberColor"] % var1 % dfs["warningMessageStatusColor"] % var2 %
          dfs["warningMessageColor"]);
        Output_Token(sbuf, (idx = 3), v, (handler = WARNING));
      } else if (var2.find("error") != string::npos) {
#ifdef DEBUG
        Out << "Reached error\n";
#endif
        // Declaration prototype listings / C++ functionality.
        sbuf = io::str(format(" %1%%2%%3%:%4%%5%%3%:%6%%7%%3%:") %
          dfs["errorFileNameColor"] % var0 % Color["NORMAL"] % dfs["errorNumberColor"] % var1 % dfs["errorMessageStatusColor"] % var2);
        if (var3.find("candidate") != string::npos) {
#ifdef DEBUG
          Out << "Reached error+candidates\n";
#endif
          sbuf += io::str(format("%s%s%s:%s") % dfs["errorFileNameProtoColor"] % var3 % Color["NORMAL"] % dfs["errorMessageColor"]);
          Output_Token(sbuf, (idx = 4), v, (handler = ERROR));
        } else {
#ifdef DEBUG
          Out << "Reached error+message\n";
#endif
          sbuf += dfs["errorMessageColor"];
          Output_Token(sbuf, (idx = 3), v, (handler = ERROR));
        }
        Retval = 1;				// Global variable.
#ifdef DEBUG
        Out << "Setting retval\n";
#endif
      } else if (IsNumber(var2.c_str())) {
#ifdef DEBUG
        Out << "Reached number\n";
#endif
        sbuf = io::str(format(" %1%%2%%3%:%4%%5%%3%:%4%%6%%3%:%7%%8%%3%:%9%") %
          dfs["errorFileNameColor"] % var0 % Color["NORMAL"] % dfs["errorNumberColor"] % var1 % var2 % dfs["errorMessageStatusColor"] % var3 %
          dfs["errorFileNameProtoColor"]);
        Output_Token(sbuf, (idx = 4), v, (handler = ERROR));
        Retval = 1;
#ifdef DEBUG
        Out << "Setting retval\n";
#endif
      } else {					// ex.) In file included from hold.cpp:8:       (intro)
#ifdef DEBUG
        Out << "Reached all other / intro\n";
#endif
        sbuf = io::str(format("%1%%2%%3%:%4%%5%%3%:%6%") %
          (!var2.empty() || Recursive_Hdr ? dfs["errorFileNameColor"] : dfs["introColor"]) % var0 % Color["NORMAL"] %
          dfs["errorNumberColor"] % var1 % dfs["errorMessageColor"]);
        Output_Token(sbuf, (idx = 2), v, (handler = ERROR));
      }
      // Feed the new line.
      sbuf += io::str(format("%s\n") % Color["NORMAL"]);
      break;

    case 2:					// Filename:[Message | Headers]  OR  Filename:LineNumber:Message
      Mangled = false;
      tmp = var0.rfind('.');
      buf.assign(var0, 0, var0.length() - tmp.length());
      tmp = cpyfwd(var1);
      tmp1 = tmp.rfind('.');
      buf1.assign(var1, 0, tmp.length() - tmp1.length());
      if (dependancy || !buf.compare(buf1))
        sbuf = io::str(format("%s\n") % s);
      else {
        if (var2.empty()) {
          sbuf = io::str(format("%s%s%s:%s") % dfs["errorFileNameProtoColor"] % var0 % Color["NORMAL"] % dfs["errorNumberColor"]);
          Output_Token(sbuf, (idx = 1), v, (handler = NONE));
          if (v.size() > 3) {
            sbuf += io::str(format("%s:%s") % Color["NORMAL"] % dfs["errorMessageColor"]);
            Output_Token(sbuf, (idx = 2), v, (handler = ERROR));
          }
        } else {
          sbuf = io::str(format("%s%s%s:%s") % dfs["warningFilenameColor"] % var0 % Color["NORMAL"] % dfs["warningMessageColor"]);
          Output_Token(sbuf, (idx = 1), v, (handler = WARNING));
        }
        sbuf += io::str(format("%s\n") % Color["NORMAL"]);
      }
      break;

    case 3:
      if (!var2.empty() && IsNumber(var2.c_str())) {	//  In file included from find.hpp:50,/*\n<padd>*/from commands/cmd_pull.cpp:9:
#ifdef DEBUG
        Out << "Reached all other / intro\n";
#endif
        sbuf = io::str(format("%1%%2%%3%:%4%%5%%3%:%4%%6%%3%:\n") % dfs["introColor"] % var0 % Color["NORMAL"] % dfs["errorNumberColor"] % var1 % var2);
        size_t x = sbuf.find(',');
        sbuf.insert(x + 1, io::str(format("\n%s                 ") % dfs["introColor"]).c_str());	// "In file included " padding.
      } else {
#ifdef DEBUG
        Out << "Reached fatal error.\n";
#endif
        Mangled = false;
        sbuf = io::str(format("%1%%2%%3%:%4%%5%%3%:%6%") %
          dfs["errorFileNameColor"] % var0 % Color["NORMAL"] % dfs["errorNumberColor"] % var1 % dfs["errorMessageColor"]);
        Output_Token(sbuf, (idx = 2), v, (handler = ERROR));
        sbuf += io::str(format("%s\n") % Color["NORMAL"]);
        Retval = 1;
#ifdef DEBUG
        Out << "Setting retval\n";
#endif
      }
      break;
  }
  Out << sbuf;
  Recursive_Hdr = (!var1.empty() && *--var1.end() == ',' ? true : false);
}


// ---------------------------------------------------------------------------
// Reinit this bad boy.
// ---------------------------------------------------------------------------
void OpenFD(char **argv, vector < string > &v, const bool dependancy)
{
  int p[2];
  if (pipe(p) < 0)
    Perror("pipe()");
  pid_t fpid = fork();
  if (fpid < 0)
    Perror("fork()");
  else if (fpid == 0) {
    close(p[0]);
    dup2(p[1], STDERR_FILENO);
    execvp(v[0].c_str(), argv);
    Perror("execvp()");
  } else {
    close(p[1]);
    char buf[PATH_MAX];
    long OPEN_MAX = sysconf(_SC_OPEN_MAX);
    pollfd client[OPEN_MAX];
    client[0].fd = p[0];
    client[0].events = POLLRDNORM;
    string str, last;
    size_t x;
    for (x = 1; x < (unsigned)OPEN_MAX; x++)
      client[x].fd = -1;
    while (1) {
      if (poll(client, 1, 0) < 0)
        Perror("Select poll.");
      if (client[0].events & POLLRDNORM) {	// New client connection.
        if ((x = read(client[0].fd, buf, sizeof(buf))) <= 0)
          break;
        buf[x] = '\0';
        str = buf;

        while (!str.empty()) {
          x = str.find('\n');
          if (x == string::npos) {
            last += str;
            str.clear();
          } else {
            string strm = str.substr(0, x);
            str.erase(0, x + 1);
            last += strm;
            ParseString(last, dependancy);
            last.clear();
          }
        }
      }
    }
  }
}


// -------------------------------------------------------------------------
// Our main function.  Reads in cmd line arguments and processes the output.
// Currently only supports GNU compilers for c/c++.
// -------------------------------------------------------------------------
int main(int argc, char **argv)
{
  // -------------------------------------------------------------------------
  // Initializes the containers that hold our color values for string parsing.
  // To be used as a fail safe or default mechanism if no deflt file was found.
  // -------------------------------------------------------------------------
  Initialize_Defaults(argv[0]);

  // -------------------------------------------------------------------------
  // Make a duplicate copy of our environment.
  // -------------------------------------------------------------------------
  vector < string > v;
  string tmp;
  ushort x;
  for (x = 0; x < argc; x++) {
    tmp = argv[x];
    v.push_back(tmp);
  }

  // -------------------------------------------------------------------------
  // Find out what compiler argv[0] is currently using.
  // -------------------------------------------------------------------------
  bool bit = false;
  stringstream strm;
  string obj = argv[0];
  size_t y;
  // Determine the link (if any) to the executable.
  char buff[BUFSIZ];
  if (readlink(argv[0], buff, sizeof(buff)) < 0)
    tmp = argv[0];
  else {
    buff[sizeof(buff) - 1] = '\0';
    tmp = buff;
  }
  // Strips the path off the compiler being found.
  if ((y = obj.rfind('/')) != string::npos)
    obj.erase(0, y + 1);
  for (x = 0; x < 4; x++) {
    strm << ' ' << DFS[x] << " --> " << tmp << "\n";
    if (!obj.compare(DFS[x])) {
      v[0] = dfs[DFS[x]];		// Copy our new enviornment exec argv[].
      bit = true;
      break;
    }
  }
  // Check state bit to see if a match for our compilers were found.
  if (!bit) {					// None found, print valid types along with an error message.
    obj = io::str(format(" %s --> %s was specified.") % obj % tmp);
    Perror(format("Only symbolic links to compilers listed below are currently supported:\n%s\n%s\n") % strm.str() % obj);
  }
  // -------------------------------------------------------------------------
  // Loop through the cmd line argument parameters, and create a string to pass.
  // -------------------------------------------------------------------------
  bool version = false, dependancy = false;
  // Concatante the compiler flags, objs, strings, defs, etc. to our string.
  x = 0;
  while (x < v.size()) {
    if (v[x][0] == '-' && v[x][1] == 'v') {
      version = true;
      break;
    } else if (v[x][0] == '-' && v[x][1] == 'M' && v[x][2] == 'M') {
      dependancy = true;
      break;
    }
    x++;
  }

  // -------------------------------------------------------------------------
  // Reads a default configuration file found either in ~/ or /etc.
  // -------------------------------------------------------------------------
  ReadConfigFile(dependancy);

  // -------------------------------------------------------------------------
  // Open the filestream for its output e.g. 1 (stdout) || 2 (stderr)
  // -------------------------------------------------------------------------
  OpenFD(argv, v, dependancy);

  // -------------------------------------------------------------------------
  // If '-v' switch was found, concatonate our 'title' string.
  // -------------------------------------------------------------------------
  if (version)
    Out << title;

  return Retval;
}

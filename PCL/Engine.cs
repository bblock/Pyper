// 
// PipeWrench - automate the transformation of text using "stackable" text filters
// Copyright (c) 2014  Barry Block 
// 
// This program is free software: you can redistribute it and/or modify it under
// the terms of the GNU General Public License as published by the Free Software
// Foundation, either version 3 of the License, or (at your option) any later
// version. 
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
// PARTICULAR PURPOSE.  See the GNU General Public License for more details. 
// 
// You should have received a copy of the GNU General Public License along with
// this program.  If not, see <http://www.gnu.org/licenses/>. 
// 
using System;
using System.Xml;
using System.IO;

namespace Firefly.PipeWrench
{
   /// <summary>
   /// Implements the pipe engine.
   /// </summary>
   public class Engine
   {
      public static char PipeDelim = '|';
         // This is the default CLI pipe delimiter.
      private string Caller { get; set; }
         // The calling application.
      private string AssyPath { get; set; }
         // Complete pathname of this assembly.
      private string AssyFolder { get; set; }
         // Location (directory only) of this assembly.
      public CommandSpecs CmdSpecs { get; set; }
         // The engine's registered filter commands.
      public Logger Log { get; set; }
         // The logger.
      public string SortExec { get; set; }
      public string SortCmdLine { get; set; }
      public string SortRevCmd { get; set; }
      public string Version { get { return "0.1"; } }

      //// <value>
      /// The number of registered core filters.
      /// </value>
      public int CoreFilters
      {
         get
         {
            int i = 0;

            foreach (CommandSpec CS in CmdSpecs)
            {
               if (CS.IsCore) i++;
            }

            return i;
         }
      }

      //// <value>
      /// The number of registered plug-in filters.
      /// </value>
      public int AddedFilters
      {
         get
         {
            int i = 0;

            foreach (CommandSpec CS in CmdSpecs)
            {
               if (!CS.IsCore) i++;
            }

            return i;
         }
      }

      /// <summary>
      /// Constructs a pipe engine.
      /// </summary>
      public Engine(string caller)
      {
         Caller = caller;
         AssyPath = typeof(Engine).Assembly.Location;
         AssyFolder = System.IO.Path.GetDirectoryName(AssyPath);
         CmdSpecs = new CommandSpecs();
         LoadCoreFilters();
         Configure();         // <-- Cannot "Log.Write()" prior to this point!
         CmdSpecs.Sort();
      }

      /// <summary>
      /// Returns true if application started with the corelogging switch.
      /// </summary>
      static public bool CoreLogging(string[] args)
      {
         bool result = false;

         for (int i=0; i < args.Length; i++)
         {
            if (args[i].ToUpper() == "--LOGCORE")
            {
               result = true;
               break;
            }
         }

         return result;
      }

      /// <summary>
      /// Converts GUI pipe script to CLI format.
      /// </summary>
      public static string GuiPipeToCli(string guiPipe)
      {
         if (guiPipe.Contains(Engine.PipeDelim.ToString()))
         {
            throw new PipeWrenchConvException("For a pipe to be exported properly to CLI format the \"" +
            Engine.PipeDelim + "\" char. must not exist in pipe comments and if referenced " +
            "in pipe commands, it must be \"escaped\" as \"#7c\".");
         }

         string tempStr = guiPipe.Replace("\"", "#22");
         tempStr = tempStr.Replace(System.Environment.NewLine, Engine.PipeDelim.ToString());
         tempStr = "\"" + tempStr + "\"";
         return tempStr;
      }

      /// <summary>
      /// Converts CLI pipe script to GUI format.
      /// </summary>
      public static string CliPipeToGui(string cliPipe)
      {
         string tempStr = cliPipe.Trim();

         // Remove any leading double-quote:

         if ((tempStr.Length > 0) && (tempStr[0] == '\"'))
         {
            tempStr = tempStr.Substring(1, tempStr.Length - 1);
         }

         // Remove any trailing double-quote:

         if ((tempStr.Length > 0) && (tempStr[tempStr.Length - 1] == '\"'))
         {
            tempStr = tempStr.Substring(0, tempStr.Length - 1);
         }

         // Replace any "#22" with double-quotes:

         tempStr = tempStr.Replace("#22", "\"");

         // Replace pipe delimiter with newlines:

         tempStr = tempStr.Replace(Engine.PipeDelim.ToString(), System.Environment.NewLine);
         return tempStr;
      }

      /// <summary>
      /// Loads all core filters into the engine.
      /// </summary>
      private void LoadCoreFilters()
      {
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.AddValues", "gtk-execute", "[n...] /In /Wn /Dn /S", "[<char pos>...] /I<ins pos> /W<width> /D<decimals> /S", "adds two or more numbers found in each input line", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.AppendStr", "gtk-execute", "s /P", "<string> /P", "concatenates the supplied string to the end of each line of the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.BaseToDec", "gtk-execute", "n /In /Sn /Wn /Z", "<radix> /I<ins char pos> /S<scan char pos> /W<width> /Z", "converts numbers of the given base found in the input text to decimal", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.BottomLines", "gtk-execute", "n", "<no of lines>", "outputs the given number of lines from the end of the text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.Call", "gtk-execute", "s", "<pipe file name> [<arg>...]", "calls a pipe from the currently executing pipe", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.CenterText", "gtk-execute", "n", "<field width>", "centers the input text in a field of the given character width", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ColumnOrder", "gtk-execute", "n n", "<no of rows> <no of columns>", "a pre-filter to the JoinLines filter which allows columns to be ordered down instead of across", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.CountChars", "gtk-execute", "/In /L /Wn /Z", "/I<char pos> /L /W<width> /Z", "outputs the number of characters", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.CountLines", "gtk-execute", "/Wn /Z", "/W<width> /Z", "outputs the number of lines", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.CullLines", "gtk-execute", "s s /A /I /R", "<begin string> <end string> /A /I /R", "removes groups of lines encountered in the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DecToBase", "gtk-execute", "n /In /Sn /Wn /Z", "<radix> /I<ins char pos> /S<scan char pos> /W<width> /Z", "converts decimal numbers found in the input text to another base", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DelBlankLines", "gtk-execute", "", "", "removes blank lines from output", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DelChars", "gtk-execute", "n n [n n...]", "<char pos> <no of chars> [<char pos> <no of chars>...]", "deletes characters from each line at specified character positions", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DelCharsToStr", "gtk-execute", "s /I /Nn /R", "<string> /I /N<count> /R", "deletes characters until string is encountered", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DelDuplLines", "gtk-execute", "[n n] /A /Ds /F /I", "[<char pos> <char pos>] /A /D<delimiter> /F /I", "acts on sorted lists removing all duplicate lines", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DelExtraBlankLines", "gtk-execute", "", "", "removes extraneous blank lines", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DelExtraBlanks", "gtk-execute", "", "", "removes extraneous blanks from each line", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.DivValues", "gtk-execute", "n n /In /Wn /Dn /S", "<char pos> <char pos> /I<ins char pos> /W<width> /D<decimals> /S", "performs division with two numbers in the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.EndIsolate", "gtk-execute", "", "", "used along with IsolateLines to constrain pipe commands to isolated block of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ExclLines", "gtk-execute", "s [n n] /I /R", "<string> [<begin char pos> <end char pos>] /I /R", "excludes all lines from output that contain the specified string", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ExtractLines", "gtk-execute", "s s /A /I /R", "<begin string> <end string> /A /I /R", "extracts groups of lines encountered in the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.FoldLines", "gtk-execute", "[n n] /Ds /E /I /Jn /Wn /Z", "[<char pos> <char pos>] /D<delimiter> /E /I /J<join opt> /W<width> /Z", "acts on sorted lists folding duplicate lines", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.GroupLines", "gtk-execute", "s [n n] /I /R", "<string> [<begin char pos> <end char pos>] /I /R", "groups lines together that contain a string", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.InclLines", "gtk-execute", "s [n n] /I /R", "<string> [<begin char pos> <end char pos>] /I /R", "includes all lines in the output that contain the specified string", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.InsLineNo", "gtk-execute", "/Ln /In /Pn /Sn /Wn /Z", "/L<init no> /I<incr> /P<ins pos> /S<no of lines> /W<width> /Z", "inserts a line number at the specified character position of each line", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.InsStr", "gtk-execute", "n s [n s...]", "<char pos> <string> [<char pos> <string>...]", "inserts character strings into each line at specified character positions", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.IsolateLines", "gtk-execute", "s [n n] /Es /I /R", "<string> [<begin char pos> <end char pos>] /Es /I /R", "used along with EndIsolate to constrain pipe commands to isolated block of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.JoinLines", "gtk-execute", "[n] /P", "[<no of lines>] /P", "joins every n lines of text into a single line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.JustCharsLeft", "gtk-execute", "[n n]", "[<begin char pos> <end char pos>]", "left justifies characters", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.JustCharsRight", "gtk-execute", "[n n]", "[<begin char pos> <end char pos>]", "right justifies characters", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.LeftChars", "gtk-execute", "n", "<no of chars>", "returns the given number of characters from the beginning of each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.LinesByPos", "gtk-execute", "n n [n n...] /Sn", "<begin line> <end line> [<begin line> <end line>...] /S<no of sets>", "outputs lines according to their position", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.LowerCase", "gtk-execute", "", "", "converts uppercase characters to lowercase", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.MultValues", "gtk-execute", "[n...] /In /Wn /Dn /S", "[<char pos>...] /I<ins char pos> /W<width> /D<decimals> /S", "multiplies two or more numbers in the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.OutDuplLines", "gtk-execute", "[n n] /Ds /I", "[<begin char pos> <end char pos>] /D<delimiter> /I", "outputs lines that are duplicated in the text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.OverlayChars", "gtk-execute", "n s [n s...]", "<char pos> <string> [<char pos> <string>...]", "overlays each line with character strings at specified character positions", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.PadLinesLeft", "gtk-execute", "s /Wn /Sn", "<pad string> /W<pad width> /S<no of sets>", "pads each line on the left to the given character width with the given character string", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.PadLinesRight", "gtk-execute", "s /Wn /Sn", "<pad string> /W<pad width> /S<no of sets>", "pads each line on the right to the given character width with the given character string", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ParseCSV", "gtk-execute", "/Qs /Ds", "/Q<quote> /D<delimiter>", "parses quoted, comma-delimited fields onto separate lines", AssyPath); 
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ParseWords", "gtk-execute", "/Ds", "/D<delimiter>", "parses the text into individual words", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.QuoteLines", "gtk-execute", "n n [n n...] /B /Ds /On /Qs /Sn /U", "<begin line> <end line> [<begin line> <end line>...] /B /D<delimiter> /O<option> /Q<quote> /S<no of sets> /U", "surrounds lines with quotes", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ReorderColumns", "gtk-execute", "n n [n n...] /C /Pn", "<char pos> <char pos> [<char pos> <char pos>...] /C /P<char pos>", "re-arranges the column order of each line", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ReplStr", "gtk-execute", "s s [s s...] /Bn /Ds /En /I /Ps /R", "<find string> <replace string> [<find string> <replace string>...] /B<char pos> /D<delimiter> /E<char pos> /I /P<place holder> /R", "replaces character strings found in the text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ReverseChars", "gtk-execute", "[n n]", "[<begin char pos> <end char pos>]", "reverses each line of the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.RightChars", "gtk-execute", "n", "<no of chars>", "returns the given number of characters from the end of each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.RotCharsLeft", "gtk-execute", "n", "<no of chars>", "rotates characters left given no of places", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.RotCharsRight", "gtk-execute", "n", "<no of chars>", "rotates characters right given no of places", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.RotCharsToStr", "gtk-execute", "s /I /Nn /R", "<string> /I /N<count> /R", "rotates each line until given string is at its beginning", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.SetDebugOn", "gtk-execute", "", "", "configures pipe debugging on", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.SetDebugOff", "gtk-execute", "", "", "configures pipe debugging off", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ShiftChars", "gtk-execute", "n s [n s...] /I /R", "<char pos> <string> [<char pos> <string>...] /I /R", "shifts text into specified character positions", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.SortLines", "gtk-execute", "/Pn /R", "/P<char pos> /R", "sorts the text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.SpliceFile", "gtk-execute", "s /Ds /M", "<file name> /D<delimiter> /M", "combines text from a text file to the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.SplitLines", "gtk-execute", "n [n...]", "<char pos> [<char pos>...]", "splits each line at the given character position(s)", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.StripChars", "gtk-execute", "n", "<no of chars>", "removes a given number of characters from the end of each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.SubValues", "gtk-execute", "n n /In /Wn /Dn /S", "<char pos> <char pos> /I<ins char pos> /W<width> /D<decimals> /S", "subtracts two numbers found on each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TopLines", "gtk-execute", "n", "<no of lines>", "outputs the given number of lines from the beginning of the input text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TotalColumns", "gtk-execute", "n [n...] /Wn /Dn /A /S", "<char pos> [<char pos>...] /W<width> /D<decimals> /A /S", "totals columns of numeric values", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TrimLines", "gtk-execute", "", "", "removes leading and trailing white space from each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TrimLinesLeft", "gtk-execute", "", "", "removes leading white space from each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TrimLinesRight", "gtk-execute", "", "", "removes trailing white space from each line of text", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.UpperCase", "gtk-execute", "", "", "converts lowercase characters to uppercase", AssyPath);
         CmdSpecs.AddCoreSpec("Firefly.PipeWrench.WrapText", "gtk-execute", "n /Bs /Jn", "<char pos> /B<break chars> /J<% jag allowed>", "wraps text at given character position", AssyPath);

         // Consider adding the following filters:
         
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.LinesInRange", "gtk-execute", "s s /Pn", "<begin string> <end string> /P<char pos>", "includes all lines in the output that sort within a specified range", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.ProxLines", "gtk-execute", "s /Bn /An /C /I /R", "<string> /B<no of lines> /A<no of lines> /C /I /R", "extracts lines of text from the input stream that are near one another", AssyPath);

         // The following TEXTools filters are obsolete:

         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.Const", "", "s s [n n] /I /R", "<begin string> <end string> [<begin char pos> <end char pos>] /I /R", "alias for the Isolate command", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.EndConst", "", "", "", "alias for the EndIsolate command", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.GroupLinesByPos", "", "s n /I /R", "<string> <char pos> /I /R", "groups lines together that contain a given string at the character position specified", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.IsolateLinesByPos", "", "s n /I /R", "<string> <char pos> /I /R", "used with EndIsolate to constrain pipe commands to isolated block of text", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.IsolateLines", "", "s s [n n] /I /R", "<begin string> <end string> [<begin char pos> <end char pos>] /I /R", "used with EndIsolate to constrain pipe commands to isolated block of text", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.LinesInRange", "gtk-execute", "s s /Pn", "<begin string> <end string> /P<char pos>", "includes all lines in the output that sort within a specified range", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.OutUniqueLines", "", "[n n] /I", "[<begin char pos> <end char pos>] /I", "acts on sorted lists returning all unique lines", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.PadTextBottom", "gtk-execute", "n s", "<no of lines> <string>", "adds lines to the end of the text until the given number of lines is reached", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.RotCharsLeftStr", "", "n s [n n] /Nn /I /R /S /Qs", "<char pos> <string> [<begin char pos> <end char pos>] /N<count> /I /R /S /Q<quote>", "rotates characters left until text at character position matches string", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.RotCharsRightStr", "", "n s [n n] /Nn /I /R /S /Qs", "<char pos> <string> [<begin char pos> <end char pos>] /N<count> /I /R /S /Q<quote>", "rotates characters right until text at character position matches string", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.Ruler", "", "/Ln /E", "/L<length> /E", "displays a ruler", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TrimTextBottom", "", "", "", "removes all trailing blank lines at the end of the input text", AssyPath);
         //CmdSpecs.AddCoreSpec("Firefly.PipeWrench.TrimTextTop", "", "", "", "removes all leading blank lines at the top of the input text", AssyPath);

         // The following filters are no longer compatible with TEXTools:

         // AppendStr:           no longer has a select switch and other supportive switches.
         // DelConsBlankLines:   renamed to DelExtraBlankLines.
         // DelConsBlanks:       renamed to DelExtraBlanks.  Note: unlike original filter, this one no longer removes ALL trailing blanks.
         // OutUniqueLines:      this filter is redundant and its functionality is handled by "DelDuplLines /A".
         // TrimTextBottom:      redundant - use "AppendStr '<eol>' | JoinLines | ReplStr ..."
         // TrimTextTop:         redundant - use "AppendStr '<eol>' | JoinLines | ReplStr ..."
         // RotCharsLeftStr:     obsolete - replaced by RotCharsToStr.
         // RotCharsRightStr:    obsolete - no longer supported.
         // Ruler:               redundant - use line, column indicators in status bar.
         // GroupLinesByPos:     redundant - use GroupLines with optional range of character positions.
         // Const:               obsolete - no longer supported alias of IsolateLines.
         // EndConst:            obsolete - no longer supported alias of EndIsolate.
         // RotCharsToStr:       The two original filters allowed rotating past or skipping over quoted strings.
         //                      This functionality--used only for working with CSV data--is no longer supported
         //                      and will have to be provided by an additional filter that allows you to "address"
         //                      fields in CSV data ("AddressColumn" or "AddressField").
         // DelCharsToStr:       Originally allowed rotating past or skipping over quoted strings.  This
         //                      functionality is no longer supported and will have to be provided by an
         //                      additional filter.
         // SubtractValues       renamed to SubValues.
         // DivideValues         renamed to DivValues.
         // MultiplyValues       renamed to MultValues.
         // SpliceFile           Any lines in the text file beyond the # of lines in the input text are ignored.
         // ParseWords           No longer has a /B filter and now has a /D filter.
      }

      /// <summary>
      /// Configures add-on (community provided) filters.
      /// </summary>
      private void ConfigureAddOns(XmlElement RootElem)
      {
         XmlNodeList Filters = RootElem["Filters"].GetElementsByTagName("Filter");

         foreach (XmlNode TheFilter in Filters)
         {
            XmlAttribute TypeName = TheFilter.Attributes["Name"];
            XmlAttribute Enabled = TheFilter.Attributes["Enabled"];
            XmlAttribute IconName = TheFilter.Attributes["Icon"];
            XmlAttribute Template = TheFilter.Attributes["Template"];
            XmlAttribute ShortDesc = TheFilter.Attributes["Desc"];
            XmlAttribute Prompt = TheFilter.Attributes["Prompt"];
            XmlAttribute AssyName = TheFilter.Attributes["AssyName"];

            if (Enabled.Value.ToLower() == "true")
            {
               // Create a command specification object:

               string AssyPath = AssyFolder + System.IO.Path.DirectorySeparatorChar +
               "PlugIns" + System.IO.Path.DirectorySeparatorChar + AssyName.Value;
               CmdSpecs.AddNonCoreSpec(TypeName.Value, IconName.Value,
               Template.Value, Prompt.Value, ShortDesc.Value, AssyPath);
            }
         }
      }

      /// <summary>
      /// Configures the pipe engine.
      /// </summary>
      private void Configure()
      {
         bool isLinux = System.IO.Path.DirectorySeparatorChar == '/';
         string homeFolder;
         string applDataFolder;

         try
         {
            if (isLinux)
            {
               homeFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
               applDataFolder = homeFolder + System.IO.Path.DirectorySeparatorChar + "." +
               Caller.ToLower();
            }
            else
            {
               homeFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
               applDataFolder = homeFolder + System.IO.Path.DirectorySeparatorChar + Caller;
            }

            string logPath = applDataFolder + System.IO.Path.DirectorySeparatorChar + "Logs";
            string configFile = applDataFolder + System.IO.Path.DirectorySeparatorChar + "EngineConf.xml";

            if (!Directory.Exists(applDataFolder))
            {
               Directory.CreateDirectory(applDataFolder);
            }

            if (!Directory.Exists(logPath))
            {
               Directory.CreateDirectory(logPath);
            }

            if (!File.Exists(configFile))
            {
               // Configuration file doesn't exist. Create it:

               string configContents =
               "<Config>" + System.Environment.NewLine +
               "  <LogPath>" + logPath + "</LogPath>" + System.Environment.NewLine +
               "  <LoggingEnabled>False</LoggingEnabled>" + System.Environment.NewLine;

               if (isLinux)
               {
                  configContents +=
                  "  <SortExec>sort</SortExec>" + System.Environment.NewLine +
                  "  <SortCmdLine>[File] -t \\0 -k 1.[CharPos][Rev]</SortCmdLine>" + System.Environment.NewLine + 
                  "  <SortRevCmd>r</SortRevCmd>" + System.Environment.NewLine;
               }
               else
               {
                  configContents +=
                  "  <SortExec>sort</SortExec>" + System.Environment.NewLine +
                  "  <SortCmdLine>[Rev] /+[CharPos] [File]</SortCmdLine>" + System.Environment.NewLine +
                  "  <SortRevCmd>/r</SortRevCmd>" + System.Environment.NewLine;
               }

               configContents +=
               "  <Filters>" + System.Environment.NewLine +
               "    <Filter Name=\"SamplePlugin.SampleFilter\" Enabled=\"true\" Icon=\"gtk-connect\" Template=\"n s [n s...]\" Desc=\"Inserts string into each line\" Prompt=\"&lt;charpos&gt; &lt;string&gt; [&lt;charpos&gt; &lt;string&gt;...]\" AssyName=\"SamplePlugin.dll\"/>" + System.Environment.NewLine +
               "  </Filters>" + System.Environment.NewLine +
               "</Config>" + System.Environment.NewLine;

               File.WriteAllText(configFile, configContents);
            }

            // Load the configuration file:

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(configFile);

            XmlElement RootElem = XmlDoc["Config"];

            // Configure log path:

            XmlElement Elem = RootElem["LogPath"];
            Log = new Logger(Elem.InnerText);

            // Configure engine logging:

            Elem = RootElem["LoggingEnabled"];
            Log.Enabled = Elem.InnerText.ToUpper() == "TRUE";

            // Configure the sort executable:

            Elem = RootElem["SortExec"];
            SortExec = Elem.InnerText;

            // Configure the sort executable command line:

            Elem = RootElem["SortCmdLine"];
            SortCmdLine = Elem.InnerText;

            // Configure the sort reverse command:

            Elem = RootElem["SortRevCmd"];
            SortRevCmd = Elem.InnerText;

            // Configure add-on (community provided) filters:

            ConfigureAddOns(RootElem);
            Log.Write("Engine configured.");
         }

         catch (Exception e)
         {
            throw new PipeWrenchEngineException("Could not create/load engine configuration." +
            System.Environment.NewLine + e.Message);
         }
      }

      /// <summary>
      /// Implements a pipe wrapper for use internally or by front-end.
      /// </summary>
      public string RunPipe(string name, string script, string args, string inText, bool loggingEnabled, bool replacingDelims)
      {
         Pipe thePipe = new Pipe(script, this, string.Empty, string.Empty, loggingEnabled, replacingDelims);

         thePipe.Compile(args, 0, string.Empty, 0);

         if (thePipe.Errors.Count == 0)
         {
            string inTempFile = System.IO.Path.GetTempFileName();
            File.WriteAllText(inTempFile, inText);
            string outTempFile = System.IO.Path.GetTempFileName();

            thePipe.Execute(ref inTempFile, ref outTempFile);

            string results = File.ReadAllText(outTempFile);

            results = results.Substring(0, results.Length - System.Environment.NewLine.Length);

            File.Delete(inTempFile);
            File.Delete(outTempFile);

            return results;
         }
         else
         {
            if ((name == null) || (name == string.Empty)) name = "<unknown>";
            throw new PipeWrenchCompileException(System.Environment.NewLine +
            "Internal error. Pipe passed to RunPipe, (" + name + "), " +
            "failed compile: " + System.Environment.NewLine + System.Environment.NewLine +
            thePipe.Errors.ToString());
         }
      }
   }
}

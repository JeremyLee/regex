#region License
//
// regex: CommandLine.cs
//
// Author:
//   Jeremy Lee (jeremyduanelee@gmail.com)
//
// Copyright (C) 2012 Jeremy Lee
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
#endregion
#region Using Directives
using System;
using CommandLine;
using CommandLine.Text;
using System.Reflection;
#endregion

namespace CSharpTemplate {
  sealed class Program {
    sealed class Options : CommandLineOptionsBase {
      [Option("rx", "regex", Required = true, HelpText = "The regular expression to match in files.", MutuallyExclusiveSet = "file")]
      public string Regex {
        get;
        set;
      }

      [Option("rp", "replace", HelpText = "The replacement pattern.")]
      public double ReplacementPattern {
        get;
        set;
      }

      [HelpOption]
      public string GetUsage() {

        var help = new HelpText {
          Heading = new HeadingInfo("regex", Assembly.GetExecutingAssembly().GetName().Version.ToString()),
          Copyright = new CopyrightInfo("Jeremy Lee", 2012),
          AdditionalNewLineAfterOption = true,
          AddDashesToOption = true
        };
        this.HandleParsingErrorsInHelp(help);
        help.AddPreOptionsLine("This software is licensed under the MIT License.");
        help.AddPreOptionsLine(@"Usage: regex -rx""Regex"" -rp""Replacement Pattern""");
        help.AddOptions(this);

        return help;
      }

      void HandleParsingErrorsInHelp(HelpText help) {
        if (this.LastPostParsingState.Errors.Count > 0) {
          var errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces
          if (!string.IsNullOrEmpty(errors)) {
            help.AddPreOptionsLine(string.Concat(Environment.NewLine, "Error(s):"));
            help.AddPreOptionsLine(errors);
          }
        }
      }
    }

    static void Main(string[] args) {
      var options = new Options();
      if (CommandLineParser.Default.ParseArguments(args, options)) {

      }
    }
  }
}
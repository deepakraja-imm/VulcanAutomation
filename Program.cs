// ---------------------------------------------------------------------------------------
// VulcanTest ~ Laser Control
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------- VulcanTest
// Program.cs
// ---------------------------------------------------------------------------------------
using Flit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace VulcanTest {
   #region class Program --------------------------------------------------------------------------
   class Program {
      [DllImport ("user32.dll")]
      static extern short GetKeyState (int key);

      static void Main (string[] args) {
         string instance = Process.GetCurrentProcess ().ProcessName;
         Console.Title = "Vulcan tests. Press CapsLock to stop.";
         if (args.Any (a => a.Contains ("?"))) ShowHelp ();
         if (args.Contains ("-regenerate")) TestRunner.Regenerate = true;
         if (args.Contains ("-diff")) TestRunner.RunWindiff = true;
         if (args.Contains ("-stop")) TestRunner.StopOnFailure = true;
         if (args.Contains ("-skipped")) TestRunner.ShowSkipped = true;
         if (args.Contains ("-nokill")) TestRunner.NoKill = true;
         if (args.Contains ("-failed")) TestRunner.RunOnlyFailed = true;
         if (args.Contains ("-fixtures")) TestRunner.ListFixtures = true;
         if (args.Contains ("-noreport")) siReport = false;
         if (args.Length > 0) {
            if (args[0] is ("lastid")) {
               var rn = new TestRunner (); rn.GatherTests (Assembly.GetExecutingAssembly ());
               Console.WriteLine ("{0} is the last test id used", rn.NextID);
               return;
            }
            if (args[0] is "fixtureid") {
               var rn = new TestRunner (); rn.GatherTests (Assembly.GetExecutingAssembly ());
               Console.WriteLine ("{0} is the last fixture id used", rn.Fixtures.OrderBy (a => a.Id).Last ().Id);
               return;
            }
            if (args.Contains ("-find")) {
               int n = IndexOf (args, "-find");
               if (args.Length <= n + 1) ShowHelp ();
               TestRunner.FindText = args[n + 1];
            }
            if (args.Contains ("-m")) {
               int n = IndexOf (args, "-m");
               if (args.Length <= n + 1) ShowHelp ();
               TestRunner.Module = args[n + 1];
            }
            if (args.Contains ("-m-")) {
               int n = IndexOf (args, "-m-");
               if (args.Length <= n + 1) ShowHelp ();
               TestRunner.SkipModule = args[n + 1];
            }
         }
         // This is a list of integers containing the tests we need to run
         List<int> only = new List<int> ();
         foreach (var arg in args) {
            if (int.TryParse (arg, out int n)) only.Add (n);
         }
         if (only.Count > 0) TestRunner.OnlyThese = only.ToArray ();
         if (TestRunner.IsBlank (TestRunner.WindiffPath)) TestRunner.WindiffPath = @"C:\Program Files\Perforce\p4merge.exe";
         ExecTests ();
         Console.ResetColor ();
      }

      #region Implementation ----------------------------------------
      static void ExecTests () {
         if (Process.GetProcessesByName ("Vulcan").Length > 0) {
            Process.GetProcessesByName ("Vulcan")[0].Kill ();
         }
         if (!Directory.Exists (Utils.TempPath)) Directory.CreateDirectory (Utils.TempPath);
         TestRunner tr = new TestRunner ();
         tr.Monitor += OnMonitor;
         tr.GatherTests (Assembly.GetExecutingAssembly ());
         Console.WriteLine ("{0} fixtures, {1} tests", tr.Fixtures.Count, tr.Tests);
         sTestStartTime = DateTime.Now;
         if (TestRunner.ShowSkipped) tr.ShowSkippedTests ();
         else if (TestRunner.ListFixtures) tr.ShowFixtures ();
         else tr.RunTests ();
      }

      static void FlushResults () {
         if (!siReport) return;
         var resultFile = @"c:\mm\Vulcann.Test.Results_" + sTestStartTime.ToString ("dd") + "-" + sTestStartTime.ToString ("MM") + "-" + sTestStartTime.ToString ("yyyy") + "-" + sTestStartTime.ToString ("hh") + "-" + sTestStartTime.ToString ("mm") + ".html";
         var begin = new List<string> ();
         begin.Add (@"<!DOCTYPE html><html>");
         begin.Add ("\n<body bgcolor = \"#000000\" text = \"#ffffff\" style=\"font-family:sans-serif;\">");
         begin.Add ("\n<title>Vulcan Test Run Results</title>");
         begin.Add ("\n<h1>Vulcan Test Run Results: " + sTestStartTime.ToString ("dd-MM-yyyy-hhmm") + "</h1>\n<dl>");
         sResults.InsertRange (0, begin);
         File.WriteAllLines (resultFile, sResults);
      }

      static int IndexOf (string[] arr, string s) {
         List<string> m = arr.ToList ();
         return m.IndexOf (s);
      }

      static bool OnMonitor (TestRunner runner, TestRunner.Phase phase, string msg, int id) {
         FlaUI.Core.Logging.Logger.Default.IsDebugEnabled = false;
         FlaUI.Core.Logging.Logger.Default.IsInfoEnabled = false;
         FlaUI.Core.Input.Mouse.MovePixelsPerMillisecond = 100;
         if (CapsLock) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ();
            WriteLineToResult ("");
            Console.WriteLine ("Tests aborted");
            WriteLineToResult ("</dl>\n<h2>Tests aborted</h2>\n</body>\n</html>");
            FlushResults ();
            Console.ResetColor ();
            Environment.Exit (-1);
         }
         switch (phase) {
            case TestRunner.Phase.FixturePrefix:
               Console.BackgroundColor = ConsoleColor.DarkBlue;
               Console.ForegroundColor = ConsoleColor.White;
               Console.Write ("{0}. {1} ", id, msg);
               PrintChars (' ', Console.WindowWidth - Console.CursorLeft - 1);
               Console.BackgroundColor = ConsoleColor.Black;
               Console.WriteLine ();
               sFixtureID = id;
               WriteLineToResult ("<dt><h4><mark style=\"background-color: blue; color:white;\">" + $"{id}. {msg} " + "</mark></h4></dt>");
               break;
            case TestRunner.Phase.TestExecute:
               ++scTests;
               string text = $"Vulcan Test {scTests}/{runner.Tests}";
               if (scFailed > 0) text += $" ({scFailed} failed)";
               Console.Title = text;
               Console.ForegroundColor = ConsoleColor.Gray;
               Console.Write (" {0}. {1} ", id, msg);
               sTestID = id;
               var testMethodName = runner.Fixtures.First (a => a.Id == sFixtureID).Tests.First (a => a.Id == sTestID).Method.Name;
               var parsed = true;
               if (testMethodName.Length > 6) parsed = int.TryParse (testMethodName.Substring (6), out sCaseID);
               if (!parsed) {
                  var tname = testMethodName.Substring (6);
                  var edited = "";
                  foreach (char a in tname)
                     if (Char.IsDigit (a)) edited += a;

                  parsed = int.TryParse (edited, out sCaseID);
               }
               WriteLineToResult ("<dd>" + $" {id}. {msg} - ");
               break;
            case TestRunner.Phase.TestPassed:
               PrintChars ('.', Console.WindowWidth - Console.CursorLeft - 5);
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine ("pass");
               WriteToResult ("<mark style=\"background-color: black; color:green;\">" + "PASS" + "</mark></dd>");
               break;
            case TestRunner.Phase.TestFailed:
               Utils.CloseVulcan ();
               PrintChars ('.', Console.WindowWidth - Console.CursorLeft - 5);
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine ("FAIL"); ++scFailed;
               WriteToResult ("<mark style=\"background-color: black; color:red;\">" + "FAIL" + "</mark></p>");
               Console.ForegroundColor = ConsoleColor.Yellow;
               AssertFailedException e = (AssertFailedException)runner.LastException;
               WriteLineToResult ("<dd style=\"color:yellow;\">&nbsp;&nbsp;&nbsp;" + e.Message + "</dd>");
               Environment.ExitCode = -1;
               break;
            case TestRunner.Phase.TestCrash:
               Utils.CloseVulcan ();
               PrintChars ('.', Console.WindowWidth - Console.CursorLeft - 6);
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine ("CRASH"); ++scCrashed;
               WriteToResult ("<mark style=\"background-color: black; color:red;\">" + "CRASH" + "</mark></p>");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine (runner.LastException.Message);
               WriteLineToResult ("<dd style=\"color:yellow;\">&nbsp;&nbsp;&nbsp;" + runner.LastException.Message + "</dd>");
               Console.WriteLine (runner.LastException.StackTrace);
               if (runner.LastException.Message.Contains ("Common Language Runtime detected an invalid program")) {
                  Console.ReadKey ();
                  Environment.Exit (-1);
               }
               Environment.ExitCode = -1;
               break;
            case TestRunner.Phase.TestSkip:
               PrintChars ('.', Console.WindowWidth - Console.CursorLeft - 5);
               Console.ForegroundColor = ConsoleColor.Black;
               Console.BackgroundColor = ConsoleColor.White;
               Console.Write ("SKIP"); ++scSkipped;
               Console.ResetColor ();
               Console.WriteLine ();
               WriteToResult ("<mark style=\"background-color: black; color:blue;\">" + "SKIP" + "</mark></p>");
               break;
            case TestRunner.Phase.RunComplete:
               Console.ForegroundColor = ConsoleColor.White;
               Console.BackgroundColor = ConsoleColor.DarkBlue;
               TimeSpan ts = (DateTime.Now - sTestStartTime);
               Console.Write ("{0} tests, {1} failed, {2} crashed, {3} skipped, {4:F1} seconds", scTests, scFailed, scCrashed, scSkipped, ts.TotalSeconds);
               PrintChars (' ', Console.WindowWidth - Console.CursorLeft - 1);
               Console.ForegroundColor = ConsoleColor.Gray;
               Console.BackgroundColor = ConsoleColor.Black;
               Console.WriteLine ();
               WriteLineToResult ("</dl><h3><mark style = \"background-color:blue; color:white;\">" + $"{scTests} tests, {scFailed} failed, {scCrashed} crashed, {scSkipped} skipped, {ts.TotalSeconds:F1} seconds </mark></h3>");
               WriteLineToResult ("</body>\n</html> ");
               FlushResults ();
               break;
         }
         return true;
      }

      static void PrintChars (char c, int length) {
         if (length <= 0) length += Console.WindowWidth;
         Console.Write (new string (c, length));
      }

      static void ShowHelp () {
         string text = @"`VulcanTest.exe N1 N2 N3` runs tests with integer IDs N1, N2, N3
                         `VulcanTest.exe - F1 - F2` runs tests from fixtures with integer IDs F1, F2
                         `VulcanTest.exe lastid prints the last test id in use
                         `-regenerate` runs tests in regenerate mode
                         `-noreport` runs tests without generating a report file
                         `-diff` runs tests, using windiff to display differences
                         `-stop` stop tests as soon as one fails
                         `-failed` runs only the tests that are currently failing
                         `-skipped` only lists the skipped tests
                         `-find TEXT` runs tests whose description contains TEXT
                         `-m MODULE` runs tests from fixtures marked with specific MODULE attribute
                         `-m MODULE.SYBSYSTEM` runs tests from fixtures with specific MODULE, SUBSYSTEM attributes
                         `-m - MODULE` skips tests from fixtures marked with the specified MODULE attribute
                         `-counters` displays the counters
                         `-fixtures` only display the names of fixtures that match
                         `lastid` get the last test id used";
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine (text);
         Console.ResetColor ();
         Environment.Exit (-1);
      }

      static void WriteLineToResult (string msg) => sResults.Add (msg);

      static void WriteToResult (string msg) {
         var t = sResults.Last () + msg;
         sResults.RemoveAt (sResults.Count - 1);
         sResults.Add (t);
      }
      #endregion

      #region Private data ------------------------------------------
      static bool CapsLock { get { return (GetKeyState (0x14) & 1) != 0; } }// 0x14 = VK_CAPITAL
      static int scTests, scFailed, scSkipped, scCrashed;
      static bool siReport = true;
      static List<string> sResults = new List<string> ();
      internal static int sFixtureID;
      internal static int sTestID;
      internal static int sCaseID = 0;
      static DateTime sTestStartTime = DateTime.Now;
      #endregion
   }
   #endregion
}

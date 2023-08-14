// ---------------------------------------------------------------------------------------
// VulcanTest ~ Laser Control
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------- VulcanTest
// Utils.cs
// ---------------------------------------------------------------------------------------
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Diagnostics;

namespace VulcanTest {
   #region class Utils ----------------------------------------------------------------------------
   public static class Utils {
      #region Methods -----------------------------------------------
      public static void CloseVulcan () {
         if (MainWindow == null) return;
         sVProcess.Kill ();
      }

      public static void OpenVulcan () {
         var procInfo = new ProcessStartInfo ();
         procInfo.FileName = sVulcanPath;
         procInfo.WorkingDirectory = sVulcanPath.Replace ("vulcan.exe", "");
         sVProcess = Process.Start (procInfo);
         using (var auto = new UIA3Automation ()) {
            var mSelector = FlaUI.Core.Tools.Retry.WhileNull (() => { return auto.GetDesktop ().FindFirstDescendant (a => a.ByClassName ("ModeSelector")); }, TimeSpan.FromSeconds (120), TimeSpan.FromSeconds (.2), false).Result;
            MainWindow = mSelector.Parent;
         }
      }
      #endregion

      #region Properties --------------------------------------------
      public static AutomationElement MainWindow { get; set; }
      public static string TempPath = @"c:\programdata\metamation\temp";
      #endregion

      static string sVulcanPath = @"C:\Program Files\Metamation\Vulcan\vulcan.exe";
      static Process sVProcess = null;
   }
   #endregion
}

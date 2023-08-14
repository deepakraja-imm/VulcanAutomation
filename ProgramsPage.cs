// ---------------------------------------------------------------------------------------
// VulcanTest ~ Laser Control
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------- VulcanTest
// ProgramsPage.cs
// ---------------------------------------------------------------------------------------
using Flit;
using static VulcanAutomation.Utils;
using System.Threading;

namespace VulcanAutomation {
   [TestFixture (3, "Vulcan tests", Module = "Programs")]
   #region class CreatePage -----------------------------------------------------------------------
   public class ProgramsPage {
      #region Methods -----------------------------------------------
      [TestInitialize]
      public void Init () => OpenVulcan ();

      [Test (3, "Program tests")]
      public void C003 () {
         Thread.Sleep (1000);
         var promptbox = MainWindow.FindFirstChild ();
         var close = promptbox.FindAllChildren (x => x.ByClassName ("Button"))[0];
         var done = promptbox.FindAllChildren (x => x.ByClassName ("Button"))[1];
         Thread.Sleep (2000);
         done.Click ();
         Thread.Sleep (1000);
         var modeSelector = MainWindow.FindFirstChild (x => x.ByClassName ("ModeSelector"));
         var programs = modeSelector.FindFirstDescendant (x => x.ByName ("Programs"));
         programs.Click ();
         Thread.Sleep (1000);
      }

      [TestCleanUp]
      public void Close () => CloseVulcan ();
      #endregion
   }
   #endregion
}
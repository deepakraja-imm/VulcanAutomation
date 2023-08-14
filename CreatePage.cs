// ---------------------------------------------------------------------------------------
// VulcanTest ~ Laser Control
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------- VulcanTest
// CreatePage.cs
// ---------------------------------------------------------------------------------------
using Flit;
using static VulcanTest.Utils;
using System.Threading;



namespace VulcanTest {
   [TestFixture (2, "Vulcan tests", Module = "Create")]
   #region class CreatePage -----------------------------------------------------------------------
   public class CreatePage {
      #region Methods -----------------------------------------------
      [TestInitialize]
      public void Init () => OpenVulcan ();

      [Test (2, "Create tests")]
      public void C002 () {

         //01 To confirm demo mode
         Thread.Sleep (5000);
         var HomePage = MainWindow;
         var promptbox = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var close = promptbox.FindFirstDescendant (x => x.ByAutomationId ("BtnNo"));
         var done = promptbox.FindFirstDescendant (x => x.ByAutomationId ("BtnYes"));
         Assert.IsNotNull (done, "ClassName/AutomationID differs");
         done.Click ();
         Thread.Sleep (1000);

         //02 To Select Create page
         var modeSelector = HomePage.FindFirstChild (x => x.ByClassName ("ModeSelector"));
         var Create = modeSelector.FindFirstChild (x => x.ByName ("Create"));
         Assert.IsNotNull (Create, "ClassName/AutomationID differs");
         Create.Click ();
         Thread.Sleep (1000);
               
         //03 To select and deselect DXF program
         var proglist = HomePage.FindFirstChild (x => x.ByClassName ("ProgList"));
         //var P2228 = proglist.FindFirstChild (x => x.ByName ("P2228"));
         //P2228.Click ();
         //Thread.Sleep (1000);
         var Arcproblem = proglist.FindFirstChild (x => x.ByName ("Arc problem"));
         Arcproblem.Click ();
         Thread.Sleep (1000);

         //04 To move up and down arrow keys
         //var VerticalToolbar = HomePage.FindAllChildren (x => x.ByClassName ("VerticalToolbar"));
         //var create = VerticalToolbar[0];
         //var filemanager = VerticalToolbar[9];
         //var moveup = VerticalToolbar[10];
         //var movedown = VerticalToolbar[11];
         //movedown.Click ();
         //Thread.Sleep (1000);
         //var chamrect = proglist.FindFirstDescendant (x => x.ByName ("chamrect"));
         //chamrect.Click ();
         //Thread.Sleep (1000);
         //moveup.Click ();
         //Thread.Sleep (1000);
         //create.Click ();
         //Thread.Sleep (3000);

         //05 To save created layouts
         var createjob = HomePage.FindFirstDescendant (x => x.ByName ("create job"));
         createjob.Click ();
         Thread.Sleep (3000);
         var tabItem = HomePage.FindAllDescendants (x => x.ByClassName ("TabItem"));
         var parts = tabItem[0];
         var configuration = tabItem[1];
         var sheets = tabItem[2];
         var layouts = tabItem[3];
         configuration.Click ();
         Thread.Sleep (2000);
         sheets.Click ();
         Thread.Sleep (2000);
         layouts.Click ();
         Thread.Sleep (6000);

         var selectsheet1 = HomePage.FindFirstDescendant (x => x.ByName ("24 Parts"));
         selectsheet1.Click ();
         Thread.Sleep (1000);
         //var VT_jobdetails = HomePage.FindAllChildren (x => x.ByClassName ("VerticalToolbar"));
         //var edit = VT_jobdetails[0];
         //var delete = VT_jobdetails[1];
         //var recompute = VT_jobdetails[2];
         var delete = HomePage.FindFirstDescendant (x => x.ByName ("delete"));
         delete.Click ();
         Thread.Sleep (2000);


         //var promptbox_delete = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnYes"));
         ////Assert.IsNotNull (done_delete, "ClassName/AutomationID differs");
         //promptbox_delete.Click ();
         //Thread.Sleep (2000);

         var promptbox_deletelayout = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var close_deletelayout = promptbox_deletelayout.FindFirstDescendant (x => x.ByAutomationId ("BtnNo"));
         var done_deletelayout = promptbox_deletelayout.FindFirstDescendant (x => x.ByAutomationId ("BtnYes"));
         Assert.IsNotNull (done_deletelayout, "ClassName/AutomationID differs");
         done_deletelayout.Click ();
         Thread.Sleep (2000);


         var recompute = HomePage.FindFirstDescendant (x => x.ByName ("recompute"));
         recompute.Click ();
         Thread.Sleep (4000);


         var savelayouts = HomePage.FindFirstDescendant (x => x.ByName ("save layouts"));
         savelayouts.Click ();
         Thread.Sleep (3000);
         //var AlphaNumericKeyboard = HomePage.FindFirstDescendant (x => x.ByAutomationId ("AlphaNumericKeyboard"));
         var keyboardnaming_a= MainWindow.FindFirstDescendant (x => x.ByName ("a"));
         keyboardnaming_a.DoubleClick ();
         Thread.Sleep (1000);
         var BtnDone = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         BtnDone.Click ();
         Thread.Sleep (2000);
      }

      [TestCleanUp]
      public void Close () => CloseVulcan ();
      #endregion
   }
   #endregion
}
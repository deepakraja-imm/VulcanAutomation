// ---------------------------------------------------------------------------------------
// VulcanTest ~ Laser Control
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------- VulcanTest
// SetupPage.cs
// ---------------------------------------------------------------------------------------
using Flit;
using static VulcanTest.Utils;
using System.Threading;


namespace VulcanTest {
   [TestFixture (1, "Vulcan tests", Module = "Setup")]
   #region class SetupPage ------------------------------------------------------------------------
   public class SetupPage {
      #region Methods -----------------------------------------------
      [TestInitialize]
      public void Init () => OpenVulcan ();

      [Test (1, "Setup tests")]
      public void C001 () {

         //01 To confirm demo mode
         Thread.Sleep (5000);
         var HomePage = MainWindow;
         var promptbox = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var close = promptbox.FindFirstDescendant (x => x.ByAutomationId ("BtnNo"));
         var done = promptbox.FindFirstDescendant (x => x.ByAutomationId ("BtnYes"));
         Assert.IsNotNull (done, "ClassName/AutomationID differs");
         done.Click ();
         Thread.Sleep (1000);

         //02 To Select Setup page
         var modeSelector = HomePage.FindFirstChild (x => x.ByClassName ("ModeSelector"));
         var setup = modeSelector.FindFirstChild (x => x.ByName ("Setup"));
         Assert.IsNotNull (setup, "ClassName/AutomationID differs");
         setup.Click ();
         Thread.Sleep (1000);

         //03 To move Y axis 
         var axisJogPanel = HomePage.FindFirstChild (x => x.ByClassName ("AxisJogPanel"));
         var YPosJogUpSlow3 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("YPosJogUpSlow3"));
         Assert.IsNotNull (YPosJogUpSlow3, "ClassName/AutomationID differs");
         YPosJogUpSlow3.Click ();
         Thread.Sleep (1000);
         var YPosJogMedium2 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("YPosJogMedium2"));
         Assert.IsNotNull (YPosJogMedium2, "ClassName/AutomationID differs");
         YPosJogMedium2.Click ();
         Thread.Sleep (1000);
         var YPosJogUpFast1 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("YPosJogUpFast1"));
         Assert.IsNotNull (YPosJogUpFast1, "ClassName/AutomationID differs");
         YPosJogUpFast1.Click ();
         Thread.Sleep (1000);

         //04 To Home the Y axis manually
         var VTJogPage = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("VTJogPage"));
         var homing = VTJogPage.FindFirstDescendant (x => x.ByName ("homing"));
         Assert.IsNotNull (homing, "ClassName/AutomationID differs");
         homing.Click ();
         Thread.Sleep (1000);
         var HomingDialog = HomePage.Parent.FindFirstDescendant (x => x.ByAutomationId ("HomingDialog"));
         var Manual = HomingDialog.FindFirstDescendant (x => x.ByName ("Manual"));
         Assert.IsNotNull (Manual, "ClassName/AutomationID differs");
         Manual.Click ();
         Thread.Sleep (1000);
         var radiobutton_X = Manual.FindFirstDescendant (x => x.ByAutomationId ("RBtnX"));
         Assert.IsNotNull (radiobutton_X, "Button not available");
         radiobutton_X.Click ();
         Thread.Sleep (1000);
         var radiobutton_Y = Manual.FindFirstDescendant (x => x.ByAutomationId ("RBtnY"));
         Assert.IsNotNull (radiobutton_Y, "Button not available");
         radiobutton_Y.Click ();
         Thread.Sleep (1000);
         var homing_start = HomingDialog.FindFirstDescendant (x => x.ByAutomationId ("BtnStart"));
         Assert.IsNotNull (homing_start, "Button not available");
         homing_start.Click ();
         Thread.Sleep (1000);

         //05 To move the X axis
         var XPosJogUpSlow3 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("XPosJogUpSlow3"));
         Assert.IsNotNull (XPosJogUpSlow3, "ClassName/AutomationID differs");
         XPosJogUpSlow3.Click ();
         Thread.Sleep (1000);
         var XPosJogMedium2 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("XPosJogMedium2"));
         Assert.IsNotNull (XPosJogMedium2, "ClassName/AutomationID differs");
         XPosJogMedium2.Click ();
         Thread.Sleep (1000);
         var XPosJogUpFast1 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("XPosJogUpFast1"));
         Assert.IsNotNull (XPosJogUpFast1, "ClassName/AutomationID differs");
         XPosJogUpFast1.Click ();
         Thread.Sleep (1000);

         //06 To capture work offset by selecting machine zero point (0,0)
         var workoffset = VTJogPage.FindFirstDescendant (x => x.ByName ("work offset"));
         Assert.IsNotNull (workoffset, "ClassName/AutomationID differs");
         workoffset.Click ();
         Thread.Sleep (1000);
         var WorkOffsetDialog = HomePage.FindFirstDescendant (x => x.ByAutomationId ("WorkOffsetDialog"));
         var machinezeropoint = WorkOffsetDialog.FindFirstDescendant (x => x.ByName ("Machine zero point"));
         Assert.IsNotNull (machinezeropoint, "ClassName/AutomationID differs");
         machinezeropoint.Click ();
         Thread.Sleep (1000);
         var offsetDone = WorkOffsetDialog.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (offsetDone, "ClassName/AutomationID differs");
         offsetDone.Click ();
         Thread.Sleep (1000);
         var offsetPopup = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var offsetDownload = offsetPopup.FindFirstDescendant (x => x.ByClassName ("Button"));
         Assert.IsNotNull (offsetDownload, "ClassName/AutomationID differs");
         offsetDownload.Click ();
         Thread.Sleep (1000);

         //07 To move Z axis
         var ZNegJogDownSlow3 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("ZNegJogDownSlow3"));
         Assert.IsNotNull (ZNegJogDownSlow3, "ClassName/AutomationID differs");
         ZNegJogDownSlow3.Click ();
         Thread.Sleep (1000);
         var ZNegJogMedium2 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("ZNegJogMedium2"));
         Assert.IsNotNull (ZNegJogMedium2, "ClassName/AutomationID differs");
         ZNegJogMedium2.Click ();
         Thread.Sleep (1000);
         var ZNegJogDownFast1 = axisJogPanel.FindFirstDescendant (x => x.ByAutomationId ("ZNegJogDownFast1"));
         Assert.IsNotNull (ZNegJogDownFast1, "ClassName/AutomationID differs");
         ZNegJogDownFast1.Click ();
         Thread.Sleep (1000);

         //08 To capture work offset by selecting positioning diode
         Assert.IsNotNull (workoffset, "ClassName/AutomationID differs");
         workoffset.Click ();
         Thread.Sleep (1000);
         var WorkOffsetDialog1 = HomePage.FindFirstDescendant (x => x.ByAutomationId ("WorkOffsetDialog"));
         var Positioningdiode = WorkOffsetDialog1.FindFirstDescendant (x => x.ByName ("Positioning diode"));
         Assert.IsNotNull (Positioningdiode, "ClassName/AutomationID differs");
         Positioningdiode.Click ();
         Thread.Sleep (1000);
         var offsetDone1 = WorkOffsetDialog1.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (offsetDone1, "ClassName/AutomationID differs");
         offsetDone1.Click ();
         Thread.Sleep (1000);
         var offsetPopup1 = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var offsetDownload1 = offsetPopup1.FindFirstDescendant (x => x.ByClassName ("Button"));
         Assert.IsNotNull (offsetDownload1, "ClassName/AutomationID differs");
         offsetDownload1.Click ();
         Thread.Sleep (1000);

         //09 To home the X and Z axis automatically
         Assert.IsNotNull (homing, "ClassName/AutomationID differs");
         homing.Click ();
         Thread.Sleep (1000);
         var HomingDialog1 = HomePage.Parent.FindFirstDescendant (x => x.ByAutomationId ("HomingDialog"));
         var Automatic = HomingDialog1.FindFirstDescendant (x => x.ByName ("Automatic"));
         Assert.IsNotNull (Automatic, "ClassName/AutomationID differs");
         Automatic.Click ();
         Thread.Sleep (1000);
         var homing_start1 = HomingDialog1.FindFirstDescendant (x => x.ByAutomationId ("BtnStart"));
         Assert.IsNotNull (homing_start1, "ClassName/AutomationID differs");
         homing_start1.Click ();
         Thread.Sleep (500);


         //10 To increment and decrement the speed override
         var speedoverride = VTJogPage.FindFirstDescendant (x => x.ByName ("speed override"));
         Assert.IsNotNull (speedoverride, "ClassName/AutomationID differs");
         speedoverride.Click ();
         Thread.Sleep (1000);
         var overrideButton = MainWindow.Parent.FindAllDescendants (x => x.ByClassName ("Button"));
         var overridePlus = overrideButton[4];
         var overrideMinus = overrideButton[3];
         overridePlus.DoubleClick ();
         Thread.Sleep (1000);
         overrideMinus.DoubleClick ();
         Thread.Sleep (3000);

         //11 To change the step size in jog settings
         var settings = VTJogPage.FindFirstDescendant (x => x.ByName ("settings"));
         Assert.IsNotNull (settings, "ClassName/AutomationID differs");
         settings.Click ();
         Thread.Sleep (3000);
         var JogSettingsDialog = HomePage.FindFirstDescendant (x => x.ByAutomationId ("JogSettingsDialog"));
         var NEditFastStep = JogSettingsDialog.FindFirstDescendant (x => x.ByAutomationId ("NEditFastStep"));
         Assert.IsNotNull (NEditFastStep, "ClassName/AutomationID differs");
         NEditFastStep.Click ();
         Thread.Sleep (1000);
         var NumericKeyboard_Jogsettings = HomePage.FindFirstDescendant (x => x.ByAutomationId ("NumericKeyboard"));
         var BtnNEdit7 = NumericKeyboard_Jogsettings.FindFirstDescendant (x => x.ByAutomationId ("BtnNEdit7"));
         Assert.IsNotNull (BtnNEdit7, "ClassName/AutomationID differs");
         BtnNEdit7.DoubleClick ();
         var BtnNEditEquals = NumericKeyboard_Jogsettings.FindFirstDescendant (x => x.ByAutomationId ("BtnNEditEquals"));
         Assert.IsNotNull (BtnNEditEquals, "ClassName/AutomationID differs");
         BtnNEditEquals.Click ();
         Thread.Sleep (1000);

         //12 To check the configured step change by moving axis
         Assert.IsNotNull (YPosJogUpFast1, "ClassName/AutomationID differs");
         YPosJogUpFast1.Click ();
         Thread.Sleep (2000);
         YPosJogUpFast1.Click ();
         Thread.Sleep (2000);
         Assert.IsNotNull (ZNegJogDownFast1, "ClassName/AutomationID differs");
         ZNegJogDownFast1.Click ();
         Thread.Sleep (2000);
         Assert.IsNotNull (XPosJogUpFast1, "ClassName/AutomationID differs");
         XPosJogUpFast1.Click ();
         Thread.Sleep (2000);

         //13 To select diagnostics option
         var BtnDiagnostics = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnDiagnostics"));
         Assert.IsNotNull (BtnDiagnostics, "ClassName/AutomationID differs");
         BtnDiagnostics.Click ();
         Thread.Sleep (1000);

         //14 To select Pfield parameter
         var PFieldParameter = HomePage.Parent.FindFirstDescendant (x => x.ByName ("PField Parameter"));
         Assert.IsNotNull (PFieldParameter, "ClassName/AutomationID differs");
         PFieldParameter.Click ();
         Thread.Sleep (1000);

         //15 To enter Pfield parameter - PfieldDisplay
         var PFieldDisplay = HomePage.Parent.FindFirstDescendant (x => x.ByAutomationId ("PFieldDisplay"));
         var NEditPField00 = PFieldDisplay.FindFirstDescendant (x => x.ByAutomationId ("NEditPField00"));
         Assert.IsNotNull (NEditPField00, "ClassName/AutomationID differs");
         NEditPField00.Click ();
         var NumericKeyboard_Pfield = HomePage.FindFirstDescendant (x => x.ByAutomationId ("NumericKeyboard"));
         var BtnNEdit0 = NumericKeyboard_Pfield.FindFirstDescendant (x => x.ByAutomationId ("BtnNEdit0"));
         Assert.IsNotNull (BtnNEdit0, "ClassName/AutomationID differs");
         BtnNEdit0.Click ();
         Thread.Sleep (1000);
         var BtnNEditEquals_Pfield = NumericKeyboard_Pfield.FindFirstDescendant (x => x.ByAutomationId ("BtnNEditEquals"));
         Assert.IsNotNull (BtnNEditEquals_Pfield, "ClassName/AutomationID differs");
         BtnNEditEquals_Pfield.Click ();
         Thread.Sleep (2000);

         //16 To enter Pfield parameter - Consecutive
         var Consecutive = PFieldDisplay.FindFirstDescendant (x => x.ByName ("Consecutive"));
         Assert.IsNotNull (Consecutive, "ClassName/AutomationID differs");
         Consecutive.Click ();
         Thread.Sleep (2000);
         var NEditPField00_Consecutive = PFieldDisplay.FindFirstDescendant (x => x.ByAutomationId ("NEditPField00"));
         Assert.IsNotNull (NEditPField00_Consecutive, "ClassName/AutomationID differs");
         NEditPField00_Consecutive.Click ();
         Thread.Sleep (1000);
         var NumericKeyboard_Consecutive = HomePage.FindFirstDescendant (x => x.ByAutomationId ("NumericKeyboard"));
         var BtnNEdit0_Consecutive = NumericKeyboard_Consecutive.FindFirstDescendant (x => x.ByAutomationId ("BtnNEdit0"));
         Assert.IsNotNull (BtnNEdit0_Consecutive, "ClassName/AutomationID differs");
         BtnNEdit0_Consecutive.Click ();
         var BtnNEditEquals_Consecutive = NumericKeyboard_Consecutive.FindFirstDescendant (x => x.ByAutomationId ("BtnNEditEquals"));
         Assert.IsNotNull (BtnNEditEquals_Consecutive, "ClassName/AutomationID differs");
         BtnNEditEquals_Consecutive.Click ();
         Thread.Sleep (1000);
         var NEditPField01_Consecutive = PFieldDisplay.FindFirstDescendant (x => x.ByAutomationId ("NEditPField01"));
         Assert.IsNotNull (NEditPField01_Consecutive, "ClassName/AutomationID differs");
         NEditPField01_Consecutive.Click ();
         Thread.Sleep (2000);
         var Pfield_BtnDone = PFieldDisplay.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (Pfield_BtnDone, "ClassName/AutomationID differs");
         Pfield_BtnDone.Click ();
         Thread.Sleep (1000);

         //17 To check Error log messages
         BtnDiagnostics.Click ();
         Thread.Sleep (1000);
         var Errorlog = HomePage.Parent.FindFirstDescendant (x => x.ByName ("Error Log"));
         Assert.IsNotNull (Errorlog, "ClassName/AutomationID differs");
         Errorlog.Click ();
         Thread.Sleep (3000);
         var Errorlog_BtnDone = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (Errorlog_BtnDone, "ClassName/AutomationID differs");
         Errorlog_BtnDone.Click ();
         Thread.Sleep (1000);

         //18 To save backup
         BtnDiagnostics.Click ();
         Thread.Sleep (1000);
         var Savebackup = HomePage.Parent.FindFirstDescendant (x => x.ByName ("Save backup"));
         Assert.IsNotNull (Savebackup, "ClassName/AutomationID differs");
         Savebackup.Click ();
         Thread.Sleep (2000);
         var save_data = HomePage.FindFirstDescendant (x => x.ByName ("Data"));
         Assert.IsNotNull (save_data, "ClassName/AutomationID differs");
         save_data.Click ();
         Thread.Sleep (1000);
         var save_log = HomePage.FindFirstDescendant (x => x.ByName ("Log"));
         Assert.IsNotNull (save_log, "ClassName/AutomationID differs");
         save_log.Click ();
         Thread.Sleep (1000);
         //var save_programs = HomePage.FindFirstDescendant (x => x.ByName ("Programs"));
         //Assert.IsNotNull (save_programs, "ClassName/AutomationID differs");
         save_data.Click ();
         Thread.Sleep (1000);
         var Savebackup_BtnDone = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (Savebackup_BtnDone, "ClassName/AutomationID differs");
         Savebackup_BtnDone.Click ();
         Thread.Sleep (5000);
         var savebackup_promptbox = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var savebackup_confirm = savebackup_promptbox.FindFirstDescendant (x => x.ByClassName ("Button"));
         Assert.IsNotNull (savebackup_confirm, "ClassName/AutomationID differs");
         savebackup_confirm.Click ();
         Thread.Sleep (1000);

         //19 To load backup
         BtnDiagnostics.Click ();
         Thread.Sleep (1000);
         var Loadbackup = HomePage.Parent.FindFirstDescendant (x => x.ByName ("Load backup"));
         Assert.IsNotNull (Loadbackup, "ClassName/AutomationID differs");
         Loadbackup.Click ();
         Thread.Sleep (2000);
         var CtlDirectoryView = HomePage.FindFirstDescendant (x => x.ByClassName ("CtlDirectoryView"));
         var loadbackup_date = CtlDirectoryView.FindFirstDescendant (x => x.ByName ("09.08.2023"));
         Assert.IsNotNull (loadbackup_date, "ClassName/AutomationID differs");
         loadbackup_date.Click ();
         Thread.Sleep (3000);
         //var selectbackup = HomePage.FindFirstDescendant (x => x.ByClassName ("CUILib.FileOpenDlg + FileData "));
         var ListBox = HomePage.FindFirstDescendant (x => x.ByClassName ("ListBox"));
         var load_backup = ListBox.FindFirstDescendant (x => x.ByName ("BackUp_15-26-19.zip"));
         load_backup.Click ();
         Thread.Sleep (2000);
         var Load_confirmation = HomePage.FindAllDescendants (x => x.ByClassName ("Button"));
         var Load_Cancel= Load_confirmation[1];
         var Load_done  = Load_confirmation[0];
         Load_done.Click ();
         Thread.Sleep (4000);
         var loadbackup_promptbox = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var loadbackup_confirm = loadbackup_promptbox.FindFirstDescendant (x => x.ByClassName ("Button"));
         Assert.IsNotNull (loadbackup_confirm, "ClassName/AutomationID differs");
         loadbackup_confirm.Click ();
         Thread.Sleep (2000);

         //20 To check license
         BtnDiagnostics.Click ();
         Thread.Sleep (1000);
         var License = HomePage.Parent.FindFirstDescendant (x => x.ByName ("License"));
         Assert.IsNotNull (License, "ClassName/AutomationID differs");
         License.Click ();
         Thread.Sleep (2000);
         var BtnDone_license = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (BtnDone_license, "ClassName/AutomationID differs");
         BtnDone_license.Click ();
         Thread.Sleep (1000);

         //21 To check oscillator data
         BtnDiagnostics.Click ();
         Thread.Sleep (1000);
         var Oscillatordata = HomePage.Parent.FindFirstDescendant (x => x.ByName ("Oscillator data"));
         Assert.IsNotNull (Oscillatordata, "ClassName/AutomationID differs");
         Oscillatordata.Click ();
         Thread.Sleep (2000);
         var BtnDone_Oscdata = HomePage.FindFirstDescendant (x => x.ByAutomationId ("PromptBox"));
         var Oscdata_confirm = BtnDone_Oscdata.FindFirstDescendant (x => x.ByClassName ("Button"));
         Assert.IsNotNull (Oscdata_confirm, "ClassName/AutomationID differs");
         Oscdata_confirm.Click ();
         Thread.Sleep (2000);


         //22 To check build version
         var ImgLogo = HomePage.FindFirstDescendant (x => x.ByAutomationId ("ImgLogo"));
         Assert.IsNotNull (ImgLogo, "ClassName/AutomationID differs");
         ImgLogo.Click ();
         Thread.Sleep (2000);
         var BtnDone_version = HomePage.FindFirstDescendant (x => x.ByAutomationId ("BtnDone"));
         Assert.IsNotNull (BtnDone_version, "ClassName/AutomationID differs");
         BtnDone_version.Click ();
         Thread.Sleep (1000);








      }

      [TestCleanUp]
      public void Close () => CloseVulcan ();
      #endregion
   }
   #endregion
}
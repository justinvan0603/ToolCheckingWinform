using DefaceWebsite.AutoTimer;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaceWebsite
{
    public partial class frmOptions : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public frmOptions()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if ((this.nrLinks.Text == "" || this.nrLinks.Text == null))
                {
                    MessageBox.Show("Số lượng link không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                //Config data
                List<string> lstData = new List<string>();
                lstData.Add(this.nrLinks.Text);//so luong link
                lstData.Add(this.rbtAuto.Checked ? "A" : "C");//a- auto; c-customer
                lstData.Add(this.chbRegisterWin.Checked.ToString());//khoi dong cung window
                lstData.Add(this.dpTimeStart.Value.ToString(StaticClass.formatDateSQL));//thoi gian start chuong trinh
                lstData.Add(this.chbSendApp.Checked.ToString());//send thong bao qua app
                lstData.Add(this.chbSendEmail.Checked.ToString());//send thong bao qua email
                lstData.Add(this.chbSchedule.Checked.ToString());//tu dong lap lich

                string res = WriteFillter(lstData);
                if (res == "0")
                {
                    if (this.rbtAuto.Checked)
                    {
                        StaticClass.isAutoMode = true;
                        if (!StaticClass.isAutoRunning)
                        {
                            AutoCreateScheduleTimer timer = new AutoCreateScheduleTimer();
                            timer.InitSchedule();
                            AutoCheckingDomain autoCheckDomain = new AutoCheckingDomain();
                            autoCheckDomain.InitAutoChecking();
                            log.Info("Đã bật chế độ lập lịch và kiểm tra tự động");
                        }
                    }
                    else
                    {
                        StaticClass.isAutoMode = false;
                        if(StaticClass.isAutoRunning)
                        {
                            MessageBox.Show("Đang có tiến trình tự động đang thực thi, tùy chỉnh sẽ có hiệu lực sau khi các tiến trình này hoàn thành!");
                        }
                    }
                    
                    
                    MessageBox.Show("Lưu thành công");
                    StaticClass.LimitLink = int.Parse(this.nrLinks.Text);
                }                    
                else MessageBox.Show("Lưu thất bại: " + res);

                this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {                
                MessageBox.Show("btnSave_Click: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private readonly string filename = "config.sys";
        private string WriteFillter(List<string> lstData)
        {
            try
            {
                string data = "";
                foreach (var item in lstData)
                    data += item + ";";
                //Ghi du lieu filtert
                TextWriter tw = new StreamWriter(filename);
                // write a line of text to the file
                tw.WriteLine(data);
                tw.Close();

                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    String line = sr.ReadToEnd();
                    string[] lst = line.Split(';');

                    this.nrLinks.Text = lst[0];//so luong link
                    this.rbtAuto.Checked = lst[1] == "A" ? true : false;//a- auto; c-customer
                    this.chbRegisterWin.Checked = bool.Parse(lst[2]);//khoi dong cung window
                    this.dpTimeStart.Value = DateTime.Parse(lst[3]);//thoi gian start chuong trinh
                    this.chbSendApp.Checked = bool.Parse(lst[4]);
                    this.chbSendEmail.Checked = bool.Parse(lst[5]);
                    this.chbSchedule.Checked = bool.Parse(lst[6]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmOptions_Load: " + ex.Message);
            }
        }

        private void chbRegisterWin_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                CreateStartupFolderShortcut();
            }
            else
            {
                DeleteStartupFolderShortcuts(Path.GetFileName(Application.ExecutablePath));
            }
        }

        public void CreateStartupFolderShortcut()
        {
            WshShellClass wshShell = new WshShellClass();
            IWshRuntimeLibrary.IWshShortcut shortcut;
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Create the shortcut
            shortcut =
              (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                startUpFolderPath + "\\" +
                Application.ProductName + ".lnk");

            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Launch My Application - Defacewebsite";
            // shortcut.IconLocation = Application.StartupPath + @"\App.ico";
            shortcut.Save();
        }
        public string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell32.Shell shell = new Shell32.ShellClass();
            Shell32.Folder folder = shell.NameSpace(pathOnly);
            Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                Shell32.ShellLinkObject link =
                  (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return String.Empty; // Not found
        }
        public void DeleteStartupFolderShortcuts(string targetExeName)
        {
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            DirectoryInfo di = new DirectoryInfo(startUpFolderPath);
            FileInfo[] files = di.GetFiles("*.lnk");

            foreach (FileInfo fi in files)
            {
                string shortcutTargetFile = GetShortcutTargetFile(fi.FullName);

                if (shortcutTargetFile.EndsWith(targetExeName,
                      StringComparison.InvariantCultureIgnoreCase))
                {
                    System.IO.File.Delete(fi.FullName);
                }
            }
        }
    }
}

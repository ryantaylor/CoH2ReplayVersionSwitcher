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

namespace CoH2ReplayVersionSwitcher
{
    public partial class mainForm : Form
    {
        Stream ReplayStream;
        OpenFileDialog dialog;
        public mainForm()
        {
            InitializeComponent();
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            dialog = new OpenFileDialog();
            dialog.Title = "Select a replay file";
            dialog.Filter = "CoH2 Replay Files|*.rec";
            dialog.InitialDirectory = @"C:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                replayName.Text = dialog.SafeFileName;
                convertButton.Enabled = true;
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            String resultFile = Path.GetPathRoot(dialog.FileName) + Path.GetFileNameWithoutExtension(dialog.FileName) + "_converted.rec";
            try
            {
                File.Copy(dialog.FileName, resultFile);
                ReplayStream = File.Open(resultFile, FileMode.Open);
                byte[] versionCode = new byte[] { 0, 0, 0x4d, 0x28 };
                ReplayStream.Write(versionCode, 0, 4);
                ReplayStream.Dispose();
                resultLabel.Text = "Success!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

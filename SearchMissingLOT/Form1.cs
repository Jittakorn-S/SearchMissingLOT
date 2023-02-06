namespace EDSSearchLOT
{
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        List<string> getFolders = new List<string>();
        string? OpenPath = "";
        string? FilePath = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{ConfigurationManager.AppSettings["ListPath"]}";
        string? LogPath = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{ConfigurationManager.AppSettings["LogPath"]}";
        string? LOT = null;

        //Code for drag and move form
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        public Form1()
        {
            InitializeComponent();
            progressBar.Visible = false;
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork!;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged!;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted!;
        }

        private void inputbox_TextChanged(object sender, EventArgs e)
        {
            LOT = inputbox.Text;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void Search()
        {
            string[] MultiplePath = System.IO.File.ReadAllLines(FilePath!);
            string searchTerm = LOT!;
            try
            {
                foreach (string paths in MultiplePath)
                {
                    backgroundWorker1.ReportProgress(1);
                    getFolders.AddRange(Directory.GetFiles(paths).Where(file => file.Contains(searchTerm)));
                    getFolders.AddRange(Directory.GetDirectories(paths).Where(folder => folder.Contains(searchTerm)));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด !! กรุณาติดต่อ System");
                using StreamWriter writer = new StreamWriter(LogPath!, true);
                writer.WriteLine("Message : " + Ex.Message + Environment.NewLine + "Date : " + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ListPathBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenPath = ListPathBox?.SelectedItem?.ToString();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Search();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (getFolders.Count == 0)
            {
                ListPathBox.DataSource = null;
                ListPathBox.Items.Clear();
                ListPathBox.ForeColor = Color.Red;
                ListPathBox.Items.Add("This LOT was not found...!!");
                getFolders.Clear();
            }
            else
            {
                ListPathBox.DataSource = null;
                ListPathBox.Items.Clear();
                ListPathBox.ForeColor = Color.Green;
                ListPathBox.Items.AddRange(getFolders.ToArray());
                getFolders.Clear();
            }
            progressBar.Value = 100;
            progressBar.Style = ProgressBarStyle.Blocks;
        }

        private void inputbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void ListPathBox_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "/select," + OpenPath);
        }
    }
}
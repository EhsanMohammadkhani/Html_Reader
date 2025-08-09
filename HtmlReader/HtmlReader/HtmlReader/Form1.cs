using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace HtmlReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Welcome to html reader", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisplaySelectedFolder(string folderPath)
        {

            ListViewItem folderItem = new ListViewItem(folderPath);
            listView2.Items.Add(folderItem);

        }

        private void browsefiles_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    DisplaySelectedFolder(selectedFolder);
                    LoadHtmlFiles(selectedFolder);
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void deletefiles_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {

                foreach (ListViewItem selectedItem in listView1.SelectedItems)
                {
                    listView1.Items.Remove(selectedItem);
                }

                MessageBox.Show("Selected files deleted.", "successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select one or more files.", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void LoadHtmlFiles(string folderPath)
        {

            try
            {
                var allFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                               .Where(file => file.EndsWith(".html", StringComparison.OrdinalIgnoreCase) ||
                                             file.EndsWith(".htm", StringComparison.OrdinalIgnoreCase))
                               .ToList();

                foreach (string file in allFiles)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(file));
                    item.SubItems.Add(file);
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "HTML Files|*.html;*.htm";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select HTML Files";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                foreach (string file in openFileDialog1.FileNames)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(file));
                    item.SubItems.Add(file);
                    listView1.Items.Add(item);
                }
            }
        }
    }
}

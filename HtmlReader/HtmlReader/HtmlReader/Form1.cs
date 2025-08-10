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
using static System.Net.Mime.MediaTypeNames;


namespace HtmlReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            this.Text = "خواننده فایل‌های HTML";

            this.Font = new Font("Tahoma", 9, FontStyle.Regular);

            try
            {
                this.Font = new Font("B Nazanin", 11, FontStyle.Regular);
            }
            catch
            {
                this.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }

            //this.RightToLeft = RightToLeft.Yes; 
            //this.RightToLeftLayout = true; 

            listView1.RightToLeft = RightToLeft.Yes;
            listView1.RightToLeftLayout = true;

            browsefiles.RightToLeft = RightToLeft.Yes;
            deletefiles.RightToLeft = RightToLeft.Yes;

            MessageBox.Show(" به Html Reader خوش آمدید ", "خوش آمدید", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                folderBrowserDialog.Description = "لطفاً پوشه مورد نظر را انتخاب کنید";
                folderBrowserDialog1.ShowNewFolderButton = false;

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

                MessageBox.Show("فایل های انتخاب شده حذف شدند", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("لطفا یک یا چند فایل رو انتخاب کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void LoadHtmlFiles(string folderPath)
        {

            try
            {

                var allFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
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
                MessageBox.Show($"خطا در بارگیری فایل ها: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "HTML Files|*.html;*.htm";
            //openFileDialog1.Multiselect = true;
            //openFileDialog1.Title = "Select HTML Files";

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{

            //    foreach (string file in openFileDialog1.FileNames)
            //    {
            //        ListViewItem item = new ListViewItem(Path.GetFileName(file));
            //        item.SubItems.Add(file);
            //        listView1.Items.Add(item);
            //    }
            //}
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (listView2.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این پوشه و تمام محتویات آن را حذف کنید؟",
                                                    "تأیید حذف",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem selectedItem in listView2.SelectedItems)
                    {
                        DeleteFolderFromListView(selectedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("لطفاً یک پوشه را انتخاب کنید.", "هشدار",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }        

        private void DeleteFolderFromListView(ListViewItem item)
        {
            try
            {
                string folderPath = item.Text;


                listView2.Items.Remove(item);

                foreach (ListViewItem fileItem in listView1.Items.Cast<ListViewItem>().ToList())
                {
                    if (fileItem.SubItems[1].Text.StartsWith(folderPath))
                    {
                        listView1.Items.Remove(fileItem);
                    }
                }

                MessageBox.Show("پوشه و تمام محتویات آن با موفقیت حذف شد.", "موفقیت",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در حذف پوشه: {ex.Message}", "خطا",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

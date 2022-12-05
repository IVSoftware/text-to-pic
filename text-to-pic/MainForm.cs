using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace text_to_pic
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            // Make an array of the image file names so you can search it.
            _images = 
                Directory
                .GetFiles(folder)
                .ToArray();
            textBoxSearch.TextChanged += ontextBoxSearchChanged;
        }
        private readonly string[] _images;

        private void ontextBoxSearchChanged(object sender, EventArgs e)
        {
            // Do not block on this event.
            BeginInvoke((MethodInvoker)delegate 
            {
                string[] matches;
                if(string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    matches = new string[0];
                }
                else
                {
                    matches = 
                    _images
                    .Where(_ => 
                        Path.GetFileNameWithoutExtension(_)
                        .Contains(textBoxSearch.Text)
                    ).ToArray();
                }
                labelMatchCount.Text = $"{matches.Length} matches";
                if(matches.Length.Equals(1))
                {
                    labelMatchCount.Visible = false;
                    pictureBox.Image = Image.FromFile(matches.First());
                }
                else
                {
                    pictureBox.Image = null;
                    labelMatchCount.Visible = true;
                }
            });
        }
    }
}

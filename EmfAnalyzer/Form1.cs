using System.Text;
using System.Windows.Forms;

namespace EmfAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            d.Filter = "EMF file|*.emf";
            if(d.ShowDialog() != DialogResult.OK) return;
            var r = new EmfReader();
            r.Read(d.FileName);
            var sb = new StringBuilder();
            foreach(var s in r.Result)
            {
                sb.AppendLine(s);
            }
            textBox1.Text = sb.ToString();
            using var img = Image.FromFile(d.FileName);
            pictureBox1.Image = new Bitmap(img);

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fortnite_WM
{
    public partial class SimulationProgress : Form
    {
        public int Maximum
        {
            get { return pb_SimulationProgress.Maximum; }
            set { pb_SimulationProgress.Maximum = value; }
        }
        public int Value
        {
            get { return pb_SimulationProgress.Value; }
        }
        public SimulationProgress() 
        {
            InitializeComponent();
            btn_Close_Progress.Hide();
            lb_Progress.Hide();
        }
        public void Progress(int progress)
        {
            lb_Progress.Text = "Runde " + progress;
            pb_SimulationProgress.Increment(progress);
            if (pb_SimulationProgress.Value == this.Maximum)
            {
                btn_Close_Progress.Show();
                lb_Progress.Show();
            }
        }
        public void Progress(string label, int progress)
        {
            lb_Progress.Text = label;
            pb_SimulationProgress.Increment(progress);
            if (pb_SimulationProgress.Value == this.Maximum)
            {
                btn_Close_Progress.Show();
                lb_Progress.Show();
            }
        }
        private void Btn_Close_Progress_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

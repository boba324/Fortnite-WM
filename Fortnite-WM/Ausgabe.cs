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
    public partial class Ausgabe : Form
    {
        public Ausgabe()
        {
            InitializeComponent();
        }
        public void GridFiller(DataTable dt)
        {
            dgv_Ausgabe.DataSource = dt;
        }
    }
}

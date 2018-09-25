using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using logica;

namespace BITACORA
{
    public partial class frmBitacora : Form
    {

        capaLogicaBitacora cpl = new capaLogicaBitacora();

        public frmBitacora()
        {
            InitializeComponent();
        }

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = cpl.mostrarBitacora();
        }
    }
}

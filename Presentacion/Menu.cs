﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

   

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientesR Clientesd = new ClientesR();
            Clientesd.MdiParent = this;
            Clientesd.Show();
        }

        private void reservacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReservacionesR Reservaciond = new ReservacionesR();
            Reservaciond.MdiParent = this;
            Reservaciond.Show();
        }
    }
}

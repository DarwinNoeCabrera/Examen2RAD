using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos.Core;
using Datos.Modelo;
using Negocio;

namespace Presentacion
{
    
    public partial class ReservacionesR : Form
    {
        private readonly UnitOfWork unitOfWork;
        Nreservacion nreservacion;
        public ReservacionesR()
        {
            InitializeComponent();
            nreservacion = new Nreservacion();
         
        }
        private void CargarDatos()
        {
            var datos = nreservacion.obtenerReservacionCliente().Select(c => new
            {
                c.ReservacionId,
                c.Codigo,
                c.Descripcion,
                c.FechaReservacion,
                c.Estado,
            });
            gvReservacionCliente.DataSource = datos.ToList();
        }

        private void ReservacionesR_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string reid = txtReservacionId.Text;
            string codigo = txtCodigo.Text;
            string descripcion = txtDescripcion.Text;
            if (string.IsNullOrEmpty(codigo) || string.IsNullOrWhiteSpace(codigo))
            {
                errorProvider1.SetError(txtCodigo, "Debe colocar el codigo de la reservacion");
                return;
            }

            if (string.IsNullOrEmpty(descripcion) || string.IsNullOrWhiteSpace(descripcion))
            {
                errorProvider1.SetError(txtDescripcion, "Debe colocar la descripcion");
                return;
            }
            if (string.IsNullOrEmpty(reid) || string.IsNullOrWhiteSpace(reid))
            {
                reid = "0";
            }

            var ReservacionCliente = new Reservaciones();
            if (int.Parse( reid) != 0)
            {
                ReservacionCliente.ReservacionId = int.Parse(reid);
            }
            ReservacionCliente.Codigo = codigo;
            ReservacionCliente.Descripcion = descripcion;
            ReservacionCliente.Estado = cbEstado.Checked;
            nreservacion.Guardar( ReservacionCliente);

            Limpiar();
            CargarDatos();

        }

        private void gvReservacionCliente_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtReservacionId.Text = gvReservacionCliente.CurrentRow.Cells["ReservacionId"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string reservacionId = txtReservacionId.Text;

            if (string.IsNullOrEmpty(reservacionId) || string.IsNullOrWhiteSpace(reservacionId))
            {
                errorProvider1.SetError(txtReservacionId, "Debe seleccionar un registro para eliminar");
                return;
            }


            nreservacion.Eliminar(int.Parse(reservacionId));

            Limpiar();
            CargarDatos();
        }
        private void Limpiar()
        {
            txtReservacionId.Text = "";
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            cbEstado.Checked = false;
            dtReservacion.Value = DateTime.Now;
            errorProvider1.Clear();
            
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            // Obtener los datos del formulario
            int id = int.Parse(txtReservacionId.Text);
            string codigo = txtCodigo.Text;
            string descripcion = txtDescripcion.Text;
            bool estado = cbEstado.Checked;

            DateTime fechareservacion = dtReservacion.Value;
            try
            {
                
                Reservaciones reser = unitOfWork.Repository<Reservaciones>().ObtenerPorId(id); 
                reser.Codigo = codigo;
                reser.Descripcion = descripcion;
                reser.Estado = estado;
                reser.FechaReservacion = fechareservacion;


                DialogResult result = MessageBox.Show("¿Desea modificar la reservacion?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Guardar los cambios en la base de datos
                    unitOfWork.Guardar();
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar las reservaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Limpiar();
        }
    }

}

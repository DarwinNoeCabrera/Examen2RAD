using Datos;
using Datos.Core;
using Datos.Modelo;
using Negocio;
using System;
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
    public partial class ClientesR : Form
    {
        private readonly UnitOfWork unitOfWork;
        Nclientes nclientes;
        Nreservacion nreservacion;
        public ClientesR()
        {
            InitializeComponent();
            nclientes = new Nclientes();
            nreservacion = new Nreservacion();
        }
        private void CargarDatos()
        {
            var clientes = nclientes.obtenerCliente().ToList().Select(c => new { c.ClienteId, c.Codigo, c.Nombres, c.Apellidos, c.ClasificacionReservacion.Descripcion }).ToList();
            dgClientes.DataSource = clientes.ToList();
        }

        private void cargareservacion()
        {
            cbReservacion.DataSource = nreservacion.obtenerReservacionCliente()
                                               .Where(c => c.Estado == true)
                                               .Select(c => new { c.ReservacionId, c.Descripcion })
                                               .ToList();
            cbReservacion.DisplayMember = "Descripcion";
            cbReservacion.ValueMember = "ReservacionId";
        }

        private void ClientesR_Load(object sender, EventArgs e)
        {
            cargareservacion();
            CargarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente()
            {
                Codigo = txtCodigo.Text,
                DNI = txtDNI.Text,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Estado = cbEstado.Checked,
                ReservacionId = int.Parse(cbReservacion.SelectedValue.ToString()),
                FechaIngreso = DateTime.Now
            };
            nclientes.Guardar(cliente);
            CargarDatos();
        }

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            int reservacionId = Convert.ToInt32(cbFiltroreservacion.SelectedValue.ToString());
            var activo = cbFiltro.Checked;
            var clientes = nclientes.obtenerCliente()
                                   .Where(c => c.ReservacionId == reservacionId &&
                                               c.Estado == activo)
                                   .ToList()
                                   .Select(c => new { c.ClienteId, c.Codigo, c.Nombres, c.Apellidos, c.ClasificacionReservacion.Descripcion }).ToList();
            dgClientes.DataSource = clientes.ToList();//nCliente.obtenerCliente();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string clienteId = txtClienteId.Text;

            if (string.IsNullOrEmpty(clienteId) || string.IsNullOrWhiteSpace(clienteId))
            {
                errorProvider1.SetError(txtClienteId, "Debe seleccionar un registro para eliminar");
                return;
            }


            nclientes.Eliminar(int.Parse(clienteId));

            Limpiar();
            CargarDatos();
        }
        private void Limpiar()
        {
            txtClienteId.Text = "";
            txtCodigo.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDNI.Text = "";
            cbEstado.Checked = false;
            errorProvider1.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Obtener los datos del formulario
            int reser = int.Parse(cbReservacion.Text);
            int id = int.Parse(txtClienteId.Text);
            string codigo = txtCodigo.Text;
            string dni = txtDNI.Text;
            string nombre = txtNombres.Text;
            string apellido=txtApellidos.Text;
            bool estado = cbEstado.Checked;

            DateTime fechaingreso = dtingreso.Value;
            try
            {

                Cliente client = unitOfWork.Repository<Cliente>().ObtenerPorId(id);
                client.ReservacionId = reser;
                client.Codigo = codigo;
                client.Nombres = nombre;
                client.Apellidos = apellido;
                client.DNI = dni;
                client.Estado = estado;
                client.FechaIngreso = fechaingreso;


                DialogResult result = MessageBox.Show("¿Desea modificar el cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Guardar los cambios en la base de datos
                    unitOfWork.Guardar();
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Limpiar();
        }
    }
}

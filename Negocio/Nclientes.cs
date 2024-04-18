using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Modelo;

namespace Negocio
{
    public class Nclientes
    {
        Dclientes dCLiente;
        public Nclientes()
        {
            dCLiente = new Dclientes();
        }

        public List<Cliente> obtenerCliente()
        {
            return dCLiente.TodasLosClientes();
        }

        public List<Cliente> obtenerClientesGrid()
        {
            var clientes = dCLiente.TodasLosClientes().ToList().Select(c => new { c.ClienteId, c.Codigo, c.Nombres, c.Apellidos, c.ClasificacionReservacion.Descripcion });
            return dCLiente.TodasLosClientes();
        }

        public List<Cliente> obtenerClientesInactivos()
        {
            var clientes = dCLiente.TodasLosClientes();
            return clientes.Where(c => c.Estado == true).ToList();
        }

        public int Guardar(Cliente cliente)
        {
            if (cliente.ClienteId == 0)
            {
                return dCLiente.Agregar(cliente);
            }
            else
            {
                return dCLiente.Editar(cliente);
            }

        }

        public int Eliminar(int clienteId)
        {
            return dCLiente.Eliminar(clienteId);
        }
    }
}

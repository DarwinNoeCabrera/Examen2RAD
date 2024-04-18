using Datos.Core;
using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Dclientes
    {
        Repository<Cliente> _repository;
        public Dclientes()
        {
            _repository = new Repository<Cliente>();
        }
        public int ClienteId { get; set; }
        public string Codigo { get; set; }
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Estado { get; set; }
        public int ReservacionId { get; set; }

        public List<Cliente> TodasLosClientes()
        {
            return _repository.Consulta().Include(c => c.ClasificacionReservacion).ToList();
        }

        public int Agregar(Cliente cliente)
        {
            cliente.FechaIngreso = DateTime.Now;
            _repository.Agregar(cliente);

            return 1;
        }

        public int Editar(Cliente cliente)
        {
            //var clienteInDb = context.Clientes.Find(cliente.ClienteId);
            var clienteInDb = _repository.Consulta().FirstOrDefault(C => C.ClienteId == cliente.ClienteId);

            if (clienteInDb != null)
            {
                clienteInDb.Codigo = cliente.Codigo;
                clienteInDb.DNI = cliente.DNI;
                clienteInDb.Nombres = cliente.Nombres;
                clienteInDb.Apellidos = cliente.Apellidos;
                clienteInDb.FechaIngreso = cliente.FechaIngreso;
                clienteInDb.Estado = cliente.Estado;
                clienteInDb.ReservacionId= cliente.ReservacionId;
                _repository.Editar(clienteInDb);
                return 1;

            }
            return 0;
        }

        public int Eliminar(int clienteId)
        {
            //var clienteInDb = context.Clientes.Find(clienteId);
            var clienteInDb = _repository.Consulta().FirstOrDefault(C => C.ClienteId == clienteId);
            if (clienteInDb != null)
            {
                _repository.Eliminar(clienteInDb);
                return 1;
            }
            return 0;
        }
    }
}

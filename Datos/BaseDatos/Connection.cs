using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.BaseDatos
{
    public class Connection: DbContext
    {
        public Connection() : base("name=BaseTeatro")
        {

        }

        public DbSet<Reservaciones> Reservaciones { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}

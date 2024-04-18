using Datos;
using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Nreservacion
    {
        Dreservaciones dreservaciones;
        public Nreservacion()
        {
            dreservaciones = new Dreservaciones();
        }

        public List<Reservaciones> obtenerReservacionCliente()
        {
            return dreservaciones.TodasLasClasificaciones();
        }

        public List<Reservaciones> obtenerReservacionInactivas()
        {
            var clasificaciones = dreservaciones.TodasLasClasificaciones();
            return clasificaciones.Where(c => c.Estado == true).ToList();
        }

        public int Guardar(Reservaciones reservacionCliente)
        {
            if (reservacionCliente.ReservacionId == 0)
            {
                return dreservaciones.Agregar(reservacionCliente);
            }
            else
            {
                return dreservaciones.Editar(reservacionCliente);
            }

        }

        public int Eliminar(int clasificacionId)
        {
            return dreservaciones.Eliminar(clasificacionId);
        }
    }
}

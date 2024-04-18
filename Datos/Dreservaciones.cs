using Datos.Core;
using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Dreservaciones
    {
        UnitOfWork _unitOfWork;

        public Dreservaciones()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int ReservacionId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaReservacion { get; set; }

        public List<Reservaciones> TodasLasClasificaciones()
        {
            //return _repository.Consulta().ToList();
            //return new List<ClasificacionCliente>();  //context.ClasificacionClientes.ToList();
            return _unitOfWork.Repository<Reservaciones>().Consulta().ToList();
        }

        public int Agregar(Reservaciones reservaciones)
        {
            reservaciones.FechaReservacion = DateTime.Now;
            _unitOfWork.Repository<Reservaciones>().Agregar(reservaciones);
            return _unitOfWork.Guardar(); ;
        }

        public int Editar(Reservaciones reservaciones)
        {
            var clasificacionId = _unitOfWork.Repository<Reservaciones>().Consulta().FirstOrDefault(c => c.ReservacionId == reservaciones.ReservacionId);

            if (clasificacionId != null)
            {
                clasificacionId.Codigo = reservaciones.Codigo;
                clasificacionId.Descripcion = reservaciones.Descripcion;
                clasificacionId.Estado = reservaciones.Estado;
                _unitOfWork.Repository<Reservaciones>().Editar(clasificacionId);
                return _unitOfWork.Guardar();
            }
            return 0;
        }
        public int Eliminar(int clasificacionId)
        {
            var clasificacionIdr = _unitOfWork.Repository<Reservaciones>().Consulta().FirstOrDefault(c => c.ReservacionId == clasificacionId);

            if (clasificacionIdr != null)
            {
                _unitOfWork.Repository<Reservaciones>().Eliminar(clasificacionIdr);
                return _unitOfWork.Guardar();
            }
            return 0;
        }
    }
}

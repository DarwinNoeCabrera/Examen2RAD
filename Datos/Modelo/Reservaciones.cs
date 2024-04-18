using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class Reservaciones
    {
        [Key]
        public int ReservacionId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        public DateTime FechaReservacion { get; set; }
        public bool Estado { get; set; }
    }
}

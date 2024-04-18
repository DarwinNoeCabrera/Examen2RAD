using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(25)]
        public string DNI { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nombres { get; set; }
        [Required]
        [MaxLength(80)]
        public string Apellidos { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Estado { get; set; }
        public int ReservacionId { get; set; }
        public Reservaciones ClasificacionReservacion { get; set; }
    }
}

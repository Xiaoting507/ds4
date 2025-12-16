using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFanclub.Models
{
    [Table("LL_Reservas")]
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        [Required]
        [Display(Name = "Evento")]
        public int EventoId { get; set; }

        [Display(Name = "Miembro")]
        public int? MiembroId { get; set; }

        [Display(Name = "Usuario")]
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Nombre para la Reserva")]
        public string NombreReserva { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email de Contacto")]
        public string EmailContacto { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Teléfono de Contacto")]
        [Phone(ErrorMessage = "Teléfono inválido")]
        public string? TelefonoContacto { get; set; }

        [Required]
        [Display(Name = "Número de Personas")]
        [Range(1, 10, ErrorMessage = "Debe ser entre 1 y 10 personas")]
        public int NumeroPersonas { get; set; } = 1;

        [Display(Name = "Confirmada")]
        public bool Confirmada { get; set; } = false;

        [Display(Name = "Asistió")]
        public bool? Asistio { get; set; }

        [Display(Name = "Fecha de Reserva")]
        public DateTime FechaReserva { get; set; } = DateTime.Now;

        [Display(Name = "Fecha de Cancelación")]
        public DateTime? FechaCancelacion { get; set; }

        [StringLength(500)]
        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string? Notas { get; set; }

        [ForeignKey("EventoId")]
        public virtual Evento? Evento { get; set; }

        [ForeignKey("MiembroId")]
        public virtual Miembro? Miembro { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }

        [NotMapped]
        [Display(Name = "Está Cancelada")]
        public bool EstaCancelada => FechaCancelacion.HasValue;

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoReserva
        {
            get
            {
                if (EstaCancelada) return "Cancelada";
                if (Asistio == true) return "Asistió";
                if (Asistio == false) return "No Asistió";
                if (Confirmada) return "Confirmada";
                return "Pendiente";
            }
        }
    }
}

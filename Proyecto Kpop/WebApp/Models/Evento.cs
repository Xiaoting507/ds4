using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFanclub.Models
{
    [Table("LL_Eventos")]
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(300)]
        [Display(Name = "Título del Evento")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string? Descripcion { get; set; }

        [Display(Name = "Idol")]
        public int? IdolId { get; set; }

        [StringLength(100)]
        [Display(Name = "Tipo de Evento")]
        public string? TipoEvento { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha del Evento")]
        [DataType(DataType.Date)]
        public DateTime FechaEvento { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria")]
        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Display(Name = "Hora de Fin")]
        [DataType(DataType.Time)]
        public TimeSpan? HoraFin { get; set; }

        [Required(ErrorMessage = "La zona es obligatoria")]
        [StringLength(200)]
        [Display(Name = "Zona")]
        public string Zona { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(500)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = string.Empty;

        [Display(Name = "Dirección Detallada")]
        [DataType(DataType.MultilineText)]
        public string? DireccionDetallada { get; set; }

        [Required]
        [Display(Name = "Capacidad")]
        [Range(1, 10000, ErrorMessage = "La capacidad debe estar entre 1 y 10,000")]
        public int Capacidad { get; set; } = 0;

        [Required]
        [Display(Name = "Puestos Disponibles")]
        [Range(0, 10000)]
        public int PuestosDisponibles { get; set; } = 0;

        [Display(Name = "Requiere Reserva")]
        public bool RequiereReserva { get; set; } = true;

        [Required]
        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Programado";

        [StringLength(500)]
        [Display(Name = "URL de la Imagen")]
        [DataType(DataType.Url)]
        public string? ImagenURL { get; set; }

        [Display(Name = "Creado Por")]
        public int? CreadoPorUsuarioId { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Display(Name = "Última Actualización")]
        public DateTime UltimaActualizacion { get; set; } = DateTime.Now;

        [ForeignKey("IdolId")]
        public virtual Idol? Idol { get; set; }

        [ForeignKey("CreadoPorUsuarioId")]
        public virtual Usuario? CreadoPor { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        [NotMapped]
        [Display(Name = "Puestos Ocupados")]
        public int PuestosOcupados => Capacidad - PuestosDisponibles;

        [NotMapped]
        [Display(Name = "Porcentaje de Ocupación")]
        public decimal PorcentajeOcupacion => Capacidad > 0 ? (decimal)PuestosOcupados / Capacidad * 100 : 0;

        [NotMapped]
        [Display(Name = "Está Lleno")]
        public bool EstaLleno => PuestosDisponibles <= 0;

        [NotMapped]
        [Display(Name = "Es Próximo")]
        public bool EsProximo => FechaEvento >= DateTime.Today && Estado != "Cancelado" && Estado != "Finalizado";
    }
}

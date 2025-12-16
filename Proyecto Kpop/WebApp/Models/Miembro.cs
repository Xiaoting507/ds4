using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFanclub.Models
{
    [Table("LL_Miembros")]
    public class Miembro
    {
        [Key]
        public int MiembroId { get; set; }

        [Display(Name = "Usuario")]
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(150)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Teléfono")]
        [Phone(ErrorMessage = "Teléfono inválido")]
        public string? Telefono { get; set; }

        [StringLength(100)]
        [Display(Name = "Rol en el Fanclub")]
        public string? RolFanclub { get; set; }

        [Display(Name = "Idol Favorito")]
        public int? IdolFavoritoId { get; set; }

        [Required]
        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        [StringLength(500)]
        [Display(Name = "Biografía")]
        public string? Biografia { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey("IdolFavoritoId")]
        public virtual Idol? IdolFavorito { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        [NotMapped]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFanclub.Models
{
    [Table("LL_Idols")]
    public class Idol
    {
        [Key]
        public int IdolId { get; set; }

        [Required(ErrorMessage = "El nombre artístico es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Nombre Artístico")]
        public string NombreArtistico { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Nombre Real")]
        public string? NombreReal { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string TipoIdol { get; set; } = "Grupo";

        [Display(Name = "Agencia")]
        public int? AgenciaId { get; set; }

        [Display(Name = "Fecha de Debut")]
        [DataType(DataType.Date)]
        public DateTime? FechaDebut { get; set; }

        [StringLength(50)]
        [Display(Name = "Género")]
        public string? Genero { get; set; }

        [StringLength(150)]
        [Display(Name = "Nombre del Fandom")]
        public string? Fandom { get; set; }

        [StringLength(100)]
        [Display(Name = "País")]
        public string? Pais { get; set; }

        [Display(Name = "Biografía")]
        [DataType(DataType.MultilineText)]
        public string? Biografia { get; set; }

        [StringLength(500)]
        [Display(Name = "URL de la Foto")]
        [DataType(DataType.Url)]
        public string? FotoURL { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey("AgenciaId")]
        public virtual Agencia? Agencia { get; set; }
        
        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
        public virtual ICollection<Miembro> MiembrosFans { get; set; } = new List<Miembro>();
    }
}

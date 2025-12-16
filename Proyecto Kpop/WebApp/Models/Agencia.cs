using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFanclub.Models
{
    [Table("LL_Agencias")]
    public class Agencia
    {
        [Key]
        public int AgenciaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Nombre de la Agencia")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país es obligatorio")]
        [StringLength(100)]
        [Display(Name = "País")]
        public string Pais { get; set; } = string.Empty;

        [Display(Name = "Año de Fundación")]
        [Range(1900, 2100, ErrorMessage = "Año inválido")]
        public int? Fundacion { get; set; }

        [StringLength(500)]
        [Display(Name = "Sitio Web")]
        [DataType(DataType.Url)]
        public string? Sitio { get; set; }

        [StringLength(500)]
        [Display(Name = "URL del Logo")]
        [DataType(DataType.Url)]
        public string? LogoURL { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string? Descripcion { get; set; }

        [Display(Name = "Activa")]
        public bool Activa { get; set; } = true;

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public virtual ICollection<Idol> Idols { get; set; } = new List<Idol>();
    }
}

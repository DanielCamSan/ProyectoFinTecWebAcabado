using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Data.Entities
{
    public class BreedEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? TypesofUnity { get; set; }
        public virtual ICollection<HeroEntity> Heroes { get; set; }
        public string DefaultColor { get; set; }
        public string Reign { get; set; }
        public string ArmyName { get; set; }
        public string Difficulty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Rating { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Models
{
    public class BreedModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1,20)]
        public int? TypesofUnity { get; set; }
      
        public IEnumerable<HeroModel> Heroes { get; set; }
        public string DefaultColor { get; set; }
        public string Reign { get; set; }

        [StringLength(20,MinimumLength =3)]
        public string ArmyName { get; set; }
        public string Difficulty { get; set; }
        [Required]
        public decimal? Rating { get; set; }
    }
}

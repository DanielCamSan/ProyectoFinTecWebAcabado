using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Practicando_WEBAPI.Data.Entities
{
    public class HeroEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("BreedId")]
        public virtual BreedEntity Breed { get; set; }
        public int BreedId { get; set; }
        public virtual ICollection<SkillEntity> Skills { get; set; }
        public int? Hp { get; set; }
        public int? Mana { get; set; }
        public enum Attributes { Agility=0, Strenght=1, Intelligence =2};
        public Attributes? MainAtribute { get; set; }
        public int? Level { get; set; }
        public bool? HasStun { get; set; }
        public string AttackType { get; set; }
        public string Difficulty { get; set; }
    }
}

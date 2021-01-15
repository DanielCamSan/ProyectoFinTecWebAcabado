using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Models
{
    public class HeroModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? BreedId { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }
        public int? Hp { get; set; }
        public int? Mana { get; set; }
        public enum Attributes { Agility, Strenght, Intelligence };
        public Attributes? MainAtribute { get; set; }
        [Range(1, 10)]
        public int? Level { get; set; }
        public bool? HasStun { get; set; }
        public string AttackType { get; set; }
        public string Difficulty { get; set; }
    }
}

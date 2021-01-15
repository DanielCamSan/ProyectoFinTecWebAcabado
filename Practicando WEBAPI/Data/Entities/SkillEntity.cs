using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Data.Entities
{
    public class SkillEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("HeroId")]
        public virtual HeroEntity Hero { get; set; }
        public int HeroId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Button { get; set; }
}
}

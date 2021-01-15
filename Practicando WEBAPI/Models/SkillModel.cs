using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? HeroId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Button { get; set; }
    }
}

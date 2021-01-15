using Practicando_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Services
{
    public interface ISkillService
    {
        public Task<IEnumerable<SkillModel>> GetSkillsAsync(int heroId);
        public Task<SkillModel> GetSkillAsync(int heroId, int skillId);
        public Task<SkillModel> CreateSkillAsync(int heroId, SkillModel skillModel);
        public Task<bool> DeleteSkillAsync(int heroId, int skillId);
    }
}

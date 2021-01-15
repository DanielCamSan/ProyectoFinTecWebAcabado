using Practicando_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Services
{
    public interface IHeroService
    {
        public Task<IEnumerable<HeroModel>> GetHeroesAsync(int breedId);
        public Task<HeroModel> GetHeroAsync(int breedId,int heroId);
        public Task<HeroModel> CreateHeroAsync(int breedId,HeroModel heroModel);
        public Task<bool> DeleteHeroAsync(int breedId,int heroId);
        public Task<bool> UpdateHeroAsync(int breedId,int heroId,HeroModel heroModel);
    }
}

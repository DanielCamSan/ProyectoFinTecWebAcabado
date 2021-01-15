using Practicando_WEBAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Data.Repository
{
    public interface ILibraryRepository
    {
        //BREEDS
        public Task<IEnumerable<BreedEntity>> GetBreedsAsync();
        public Task<BreedEntity> GetBreedAsync(int BreedId);
        public void CreateBreed(BreedEntity breedEntity);
        public Task<bool> DeleteBreedAsync(int BreedId);
        public Task<bool> UpdateBreedAsync(BreedEntity breedEntity);


        //Heroes
        public Task<IEnumerable<HeroEntity>> GetHeroesAsync(int breedId);
        public Task<HeroEntity> GetHeroAsync(int heroId);
        public void CreateHero(HeroEntity heroEntity);
        public bool DeleteHero(int heroId);
        public Task<bool> UpdateHeroAsync(HeroEntity heroEntity);

        //Skills
        public Task<IEnumerable<SkillEntity>> GetSkillsAsync(int heroId);
        public Task<SkillEntity> GetSkillAsync(int skillId);
        public void CreateSkill(SkillEntity skillEntity);
        public bool DeleteSkill(int skillId);

        //BL ENDPOINTS
        /*   public Task<BreedEntity> GetStrongestBreedAsync();
           public Task<string> GetBreedDifficultForUsersAsync(int BreedId);

           */
        //SaveChanges
        Task<bool> SaveChangesAsync();

    }
}

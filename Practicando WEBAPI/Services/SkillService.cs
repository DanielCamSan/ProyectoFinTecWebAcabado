using AutoMapper;
using Practicando_WEBAPI.Data.Entities;
using Practicando_WEBAPI.Data.Repository;
using Practicando_WEBAPI.Exceptions;
using Practicando_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Services
{
    public class SkillService : ISkillService
    {
        private IMapper _mapper;
        private ILibraryRepository _libraryRepository;

        public SkillService(IMapper mapper, ILibraryRepository libraryRepository)
        {
            _mapper = mapper;
            _libraryRepository = libraryRepository;
        }

        public async Task<IEnumerable<SkillModel>> GetSkillsAsync(int heroId)
        {
            await ValidateHero(heroId);
            var entityList = await _libraryRepository.GetSkillsAsync(heroId);
            var modelList = _mapper.Map<IEnumerable<SkillModel>>(entityList);
            return modelList;
        }
        public async Task<SkillModel> GetSkillAsync(int heroId, int skillId)
        {
            await ValidateHero(heroId);
            await ValidateSkill(skillId);
            var skill = await _libraryRepository.GetSkillAsync(skillId);
            if (skill.Hero.Id != heroId)
            {
                throw new NotFoundException($"The skill with id : {skillId} doesnt exist for the Hero {heroId} try it with an existing id.");
            }
            return _mapper.Map<SkillModel>(skill);
        }
        public async Task<SkillModel> CreateSkillAsync(int heroId, SkillModel skillModel)
        {
            await ValidateHero(heroId);
            if (skillModel.HeroId != null && skillModel.HeroId != heroId)
            {
                throw new BadRequestException($"You cant create the skill with that hero id, try with {heroId}");
            }
            skillModel.HeroId = heroId;
            SkillEntity skillEntity = _mapper.Map<SkillEntity>(skillModel);
            skillEntity.HeroId = heroId;
            _libraryRepository.CreateSkill(skillEntity);
            var res = await _libraryRepository.SaveChangesAsync();
            if (res)
            {
                return _mapper.Map<SkillModel>(skillEntity);
            }
            throw new Exception("Database Exception");
        }
        public async Task<bool> DeleteSkillAsync(int heroId, int skillId)
        {
            var skill = await GetSkillAsync(heroId, skillId);
            if (skill.HeroId != heroId)
            {
                throw new NotFoundException($"The skill with id : {skillId} doesnt exist for the hero {heroId} try it with an existing id.");
            }
            _libraryRepository.DeleteSkill(skillId);
            var res = await _libraryRepository.SaveChangesAsync();
            if (res)
            {
                return true;
            }
            throw new Exception("Database Exception");
        }
        
        private async Task ValidateHero(int heroId)
        {
            var hero = await _libraryRepository.GetHeroAsync(heroId);
            if (hero == null)
            {
                throw new NotFoundException($"The hero with id: {heroId} doesnt exist try it with an existing id.");
            }
        }
        private async Task ValidateSkill(int skillId)
        {
            var hero = await _libraryRepository.GetSkillAsync(skillId);
            if (hero == null)
            {
                throw new NotFoundException($"The skill with id: {skillId} doesnt exist try it with an existing id.");
            }
        }
    }
}

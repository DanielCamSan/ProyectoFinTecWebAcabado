using Microsoft.EntityFrameworkCore;
using Practicando_WEBAPI.Controllers;
using Practicando_WEBAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using static Practicando_WEBAPI.Data.Entities.HeroEntity;

namespace Practicando_WEBAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {

       /* private List<HeroEntity> heroes = new List<HeroEntity>(){
            new HeroEntity(){Id=1,Name="ShadowHunter",Breed=1,Hp=589,Mana=450,MainAtribute=(Attributes)3,Level=5,HasStun=false,AttackType="Range",Difficulty="Easy"},
            new HeroEntity(){Id=2,Name="FarSeer",Breed=1,Hp=634,Mana=480,MainAtribute=(Attributes)3,Level=7,HasStun=false,AttackType="Range",Difficulty="Medium"},
            new HeroEntity(){Id=3,Name="Blademaster",Breed=1,Hp=754,Mana=280,MainAtribute=(Attributes)1,Level=8,HasStun=false,AttackType="Body",Difficulty="Hard"},
            new HeroEntity(){Id=4,Name="TaurenChieftain",Breed=1,Hp=850,Mana=300,MainAtribute=(Attributes)2,Level=6,HasStun=true,AttackType="Body",Difficulty="Easy"},
            new HeroEntity(){Id=5,Name="DeathKnight",Breed=2,Hp=819,Mana=300,MainAtribute=(Attributes)2,Level=6,HasStun=false,AttackType="Body",Difficulty="Hard"},
            new HeroEntity(){Id=6,Name="DreadLord",Breed=2,Hp=1010,Mana=350,MainAtribute=(Attributes)2,Level=10,HasStun=true,AttackType="Body",Difficulty="Medium"},
            new HeroEntity(){Id=7,Name="Lich",Breed=2,Hp=587,Mana=420,MainAtribute=(Attributes)3,Level=6,HasStun=false,AttackType="Range",Difficulty="Hard"},
            new HeroEntity(){Id=8,Name="CryptLord",Breed=2,Hp=850,Mana=345,MainAtribute=(Attributes)2,Level=7,HasStun=true,AttackType="Body",Difficulty="Medium"},
            new HeroEntity(){Id=9,Name="Paladin",Breed=3,Hp=829,Mana=270,MainAtribute=(Attributes)2,Level=5,HasStun=false,AttackType="Body",Difficulty="Medium"},
            new HeroEntity(){Id=10,Name="Archmage",Breed=3,Hp=705,Mana=620,MainAtribute=(Attributes)3,Level=8,HasStun=false,AttackType="Range",Difficulty="Hard"},
            new HeroEntity(){Id=11,Name="MountainKing",Breed=3,Hp=948,Mana=460,MainAtribute=(Attributes)2,Level=8,HasStun=true,AttackType="Body",Difficulty="Easy"},
            new HeroEntity(){Id=12,Name="BloodMage",Breed=3,Hp=609,Mana=550,MainAtribute=(Attributes)3,Level=5,HasStun=false,AttackType="Range",Difficulty="Hard"},
            new HeroEntity(){Id=13,Name="DemonHunter",Breed=4,Hp=689,Mana=250,MainAtribute=(Attributes)1,Level=5,HasStun=false,AttackType="Body",Difficulty="Medium"},
            new HeroEntity(){Id=14,Name="KeeperOfTheGrove",Breed=4,Hp=559,Mana=360,MainAtribute=(Attributes)3,Level=3,HasStun=true,AttackType="Range",Difficulty="Easy"},
            new HeroEntity(){Id=15,Name="PriestessOfTheMoon",Breed=4,Hp=609,Mana=320,MainAtribute=(Attributes)1,Level=5,HasStun=false,AttackType="Range",Difficulty="Easy"},
            new HeroEntity(){Id=16,Name="Warden",Breed=4,Hp=599,Mana=350,MainAtribute=(Attributes)1,Level=5,HasStun=false,AttackType="Body",Difficulty="Medium"}
        };
        private List<BreedEntity> breeds = new List<BreedEntity>(){
            new BreedEntity(){Id=1,Name="Orcs",DefaultColor="Green",Reign="Draenor",TypesofUnity=13,ArmyName="The Horde",Difficulty="Medium",Rating=76.4m},
            new BreedEntity(){Id=2,Name="Undeads",DefaultColor="Black",Reign="BurningLegion",TypesofUnity=12,ArmyName="The Scourge",Difficulty="Hard",Rating=56.8m},
            new BreedEntity(){Id=3,Name="Humans",DefaultColor="White",Reign="Lordaeron",TypesofUnity=13,ArmyName="The Alliance",Difficulty="Easy",Rating=67.2m},
            new BreedEntity(){Id=4,Name="NightElves",DefaultColor="Blue",Reign="Kalimdor",TypesofUnity=11,ArmyName="The NightElves of Kalimdor",Difficulty="Medium",Rating=70.7m}
        };
        */
        private LibraryDbContext _dbContext;
        public LibraryRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<BreedEntity>> GetBreedsAsync()
        {
            IQueryable<BreedEntity> query = _dbContext.Breeds;
            query = query.AsNoTracking();
            query = query.Include(c => c.Heroes);
            return await query.ToListAsync();
        }
        public async Task<BreedEntity> GetBreedAsync(int breedId)
        {
            
            IQueryable<BreedEntity> query = _dbContext.Breeds;
            query = query.AsNoTracking();
            query = query.Include(c => c.Heroes);
            return await query.FirstOrDefaultAsync(c => c.Id == breedId);
        }
        public void CreateBreed(BreedEntity breedEntity)
        {
            _dbContext.Breeds.Add(breedEntity);
        }

        public async Task<bool> DeleteBreedAsync(int breedId)
        {
            var breedToDelete = await _dbContext.Breeds.FirstOrDefaultAsync(c => c.Id == breedId);
            _dbContext.Breeds.Remove(breedToDelete);
            return true;
        }

        public async Task<bool> UpdateBreedAsync(BreedEntity breedEntity)
        {
            var breedToUpdate =await _dbContext.Breeds.FirstOrDefaultAsync(c => c.Id == breedEntity.Id);
            breedToUpdate.Name = breedEntity.Name ?? breedToUpdate.Name;
            breedToUpdate.DefaultColor = breedEntity.DefaultColor ?? breedToUpdate.DefaultColor;
            breedToUpdate.Reign = breedEntity.Reign ?? breedToUpdate.Reign;
            breedToUpdate.TypesofUnity = breedEntity.TypesofUnity ?? breedToUpdate.TypesofUnity;
            breedToUpdate.ArmyName = breedEntity.ArmyName ?? breedToUpdate.ArmyName;
            breedToUpdate.Difficulty = breedEntity.Difficulty ?? breedToUpdate.Difficulty;
            breedToUpdate.Rating = breedEntity.Rating ?? breedToUpdate.Rating;
            return true;
        }

        public async Task<IEnumerable<HeroEntity>> GetHeroesAsync(int breedId)
        {
            IQueryable<HeroEntity> query = _dbContext.Heroes;
            query = query.AsNoTracking();
            query = query.Where(v => v.Breed.Id==breedId);
            query = query.Include(v => v.Breed);
            return await query.ToListAsync();
        }

        public async Task<HeroEntity> GetHeroAsync(int heroId)
        {
            IQueryable<HeroEntity> query = _dbContext.Heroes;
            query = query.Include(v => v.Breed);
            query = query.AsNoTracking();
            query = query.Include(v => v.Skills);
            return await query.SingleOrDefaultAsync(v => v.Id == heroId);          
        }

        public void CreateHero(HeroEntity heroEntity)
        {
            if (heroEntity.Breed != null)
            {
                _dbContext.Entry(heroEntity.Breed).State = EntityState.Unchanged;
            }
            
            _dbContext.Heroes.Add(heroEntity);
        }

        public  bool DeleteHero(int heroId)
        {
            var heroToDelete = new HeroEntity() { Id = heroId };
            _dbContext.Entry(heroToDelete).State = EntityState.Deleted;
            return true;
        }

        public async Task<bool> UpdateHeroAsync(HeroEntity heroEntity)
        {
            var heroToUpdate = await _dbContext.Heroes.FirstOrDefaultAsync(v => v.Id == heroEntity.Id);
            heroToUpdate.Name = heroEntity.Name ?? heroToUpdate.Name;
            heroToUpdate.AttackType = heroEntity.AttackType ?? heroToUpdate.AttackType;
            heroToUpdate.Difficulty = heroEntity.Difficulty ?? heroToUpdate.Difficulty;
            heroToUpdate.HasStun = heroEntity.HasStun ?? heroToUpdate.HasStun;
            heroToUpdate.Hp = heroEntity.Hp ?? heroToUpdate.Hp;
            heroToUpdate.Level = heroEntity.Level ?? heroToUpdate.Level;
            heroToUpdate.MainAtribute = heroEntity.MainAtribute ?? heroToUpdate.MainAtribute;
            heroToUpdate.Mana = heroEntity.Mana ?? heroToUpdate.Mana;
            return true;
        }

        //SKILLS
        public async Task<IEnumerable<SkillEntity>> GetSkillsAsync(int heroId)
        {
            IQueryable<SkillEntity> query = _dbContext.Skills;
            query = query.AsNoTracking();
            query = query.Where(v => v.Hero.Id == heroId);
            query = query.Include(v => v.Hero);
            return await query.ToListAsync();
            //return await query.ToListAsync();
        }

        public async Task<SkillEntity> GetSkillAsync(int skillId)
        {
            IQueryable<SkillEntity> query = _dbContext.Skills;
            query = query.Include(v => v.Hero);
            query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync(v => v.Id == skillId);
        }
        public void CreateSkill(SkillEntity skillEntity)
        {
            if (skillEntity.Hero != null)
            {
                _dbContext.Entry(skillEntity.Hero).State = EntityState.Unchanged;
            }

            _dbContext.Skills.Add(skillEntity);
        }

        public bool DeleteSkill(int skillId)
        {
            var skillToDelete = new SkillEntity() { Id = skillId };
            _dbContext.Entry(skillToDelete).State = EntityState.Deleted;
            return true;
        }

        /*
        public async Task<BreedEntity> GetStrongestBreedAsync()
        {
            BreedEntity Strongest=breeds.First();
            bool same = false;
            bool firstcycle = true;
            foreach (var breed in breeds)
            {
                if (IsStronger(Strongest, breed)=="same")
                {
                    if (firstcycle)
                        firstcycle = false;
                    else
                        same = true;
                }
                else
                {
                    same = false;
                    if ((IsStronger(Strongest, breed) == "Second"))
                        Strongest = breed;                        
                }              
            }
            if (same)
                Strongest = null;
            return Strongest;
        }
        private string IsStronger(BreedEntity firstBread, BreedEntity SecondBread)
        {
            string resp;
            if (SumOfHeroesLevels(firstBread) == SumOfHeroesLevels(SecondBread))
            {
                if (GetBreedDifficultForUsers(firstBread.Id) == GetBreedDifficultForUsers(SecondBread.Id))
                {
                    resp = "same";
                }
                else
                {
                    resp = (GetBreedDifficultForUsers(firstBread.Id) == GetBreedDifficultForUsers(SecondBread.Id)) ? "First" : "Second";
                }                
            }
            else
                resp = (SumOfHeroesLevels(firstBread) > SumOfHeroesLevels(SecondBread)) ? "First" : "Second";
            return resp;
        }
        private int SumOfHeroesLevels(BreedEntity breed)
        {
            int sum = 0;
            foreach (var hero in breed.Heroes)
            {
                if (hero.Level == null)
                    break;
                sum = sum + (int)hero.Level;
            }
            return sum;
        }
        public async Task<string> GetBreedDifficultForUsersAsync(int breedId)
        {
            string res = "";
            var breed = GetBreed(breedId);
            var heroDif = HeroDifficultySummary(breedId);
           if(heroDif == breed.Difficulty)
            {
                res = heroDif;
            }
            else
            {
                if (heroDif == "")
                    return $"The difficult for this breed is {breed.Difficulty.ToUpper()} because it hasnt heroes to make a difference.";
                if ((heroDif == "Easy" && breed.Difficulty == "Medium") || (breed.Difficulty == "Easy" && heroDif == "Medium"))
                    res=(breed.Rating > 70) ? "Easy" : "Medium";
                if ((heroDif == "Medium" && breed.Difficulty == "Hard") || (breed.Difficulty == "Medium" && heroDif == "Hard"))
                    res = (breed.Rating > 70) ? "Medium" : "Hard";
                if ((heroDif == "Easy" && breed.Difficulty == "Hard") || (breed.Difficulty == "Easy" && heroDif == "Hard"))
                    res = "Medium";
            }
            return $"The difficult for the users of this bread is {res.ToUpper()} and the one given by the game is {breed.Difficulty.ToUpper()} with a rate of {breed.Rating}.";
            
        }
        private async Task<string> HeroDifficultySummaryAsync(int breedId)
        {
            string res = "";
            var breedHeroes = GetBreed(breedId).Heroes;
            int contEasy=0, contMedium=0, contHard=0;
            foreach (var singleHero in breedHeroes)
            {
                switch (singleHero.Difficulty)
                {
                    case "Easy":
                        contEasy++;
                        break;
                    case "Medium":
                        contMedium++;
                        break;
                    case "Hard":
                        contHard++;
                        break;
                    default:
                        break;
                }
            }
            if (contEasy == 0 && contMedium == 0 && contHard == 0)
                return "";
            if (contEasy >= contMedium && contEasy >= contHard)
                res = "Easy";
            if (contMedium >= contEasy && contMedium >= contHard)
                res = "Medium";
            if (contHard >= contEasy && contHard >= contMedium)
                res = "Hard";
            return res;
        }
        */
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}

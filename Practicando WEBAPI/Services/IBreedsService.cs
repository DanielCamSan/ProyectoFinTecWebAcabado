using Practicando_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Services
{
    public interface IBreedsService
    {
        public Task<IEnumerable<BreedModel>> GetBreedsAsync();
        public Task<BreedModel> GetBreedAsync(int BreedId);
        public Task<BreedModel> CreateBreedAsync(BreedModel breedModel);
        public Task<bool> DeleteBreedAsync(int BreedId);
        public Task<bool> UpdateBreedAsync(int BreedId, BreedModel breedModel);
        /*public Task<BreedModel> GetStrongestBreedAsync();
        public Task<string> GetBreedDifficultForUsersAsync(int BreedId);
        */
    }
}

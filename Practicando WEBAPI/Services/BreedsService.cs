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
    public class BreedsService : IBreedsService
    {
        private ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public BreedsService(ILibraryRepository libraryRepository, IMapper _mapper)
        {
            this._libraryRepository = libraryRepository;
            this._mapper = _mapper;
        }

        public async Task<IEnumerable<BreedModel>> GetBreedsAsync()
        {
            var entityList= await _libraryRepository.GetBreedsAsync();
            var modelList = _mapper.Map<IEnumerable<BreedModel>>(entityList);
            return modelList;
        }
        public async Task<BreedModel> GetBreedAsync(int breedId)
        {
            
            await validateBreed(breedId);
            var breed = await _libraryRepository.GetBreedAsync(breedId);
            var modelBreed= _mapper.Map<BreedModel>(breed);
            return modelBreed;
        }
      
        public async Task<BreedModel> CreateBreedAsync(BreedModel breedModel)
        {

            var breedEntity = _mapper.Map<BreedEntity>(breedModel);
            _libraryRepository.CreateBreed(breedEntity);
            var result = await _libraryRepository.SaveChangesAsync();

            if (result)
            {
                return _mapper.Map<BreedModel>(breedEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<bool> DeleteBreedAsync(int breedId)
        {
            await GetBreedAsync(breedId);
            await _libraryRepository.DeleteBreedAsync(breedId);
            var res = await _libraryRepository.SaveChangesAsync();
            if (!res)
            {
                throw new Exception("Database Exception");
            }
            return true;
        }

        public async Task<bool> UpdateBreedAsync(int BreedId, BreedModel breedModel)
        {
            await GetBreedAsync(BreedId);
            breedModel.Id = BreedId;
            await _libraryRepository.UpdateBreedAsync(_mapper.Map<BreedEntity>(breedModel));
            var res = await _libraryRepository.SaveChangesAsync();
            if (res)
            {
                return true;
            }

            throw new Exception("Database Exception");

        }
        private async Task validateBreed(int breedId)
        {
            var breed = await _libraryRepository.GetBreedAsync(breedId);

            if (breed == null)
            {
                throw new NotFoundException($"The breed with id {breedId} doesnt exists, try it with a new id please.");
            }

        }
       /* public async Task<BreedModel> GetStrongestBreedAsync()
        {
            var breed = await _libraryRepository.GetStrongestBreedAsync();
            if (breed == null)
            {
                throw new SameBreedExcecption($"There are more than just one breed that comply the same strong conditions");
            }
            return _mapper.Map<BreedModel>(breed);
        }

        public async Task<string> GetBreedDifficultForUsersAsync(int breedId)
        {
            validateBreed(breedId);
            return await _libraryRepository.GetBreedDifficultForUsersAsync(breedId);
        }*/
    }
}

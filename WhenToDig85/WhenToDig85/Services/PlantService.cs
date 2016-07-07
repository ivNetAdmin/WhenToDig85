
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenToDig85.Data;
using WhenToDig85.Models.Data;

namespace WhenToDig85.Services
{
    public interface IPlantService
    {
        //Task<IEnumerable<Plant>> GetPlantList();
        Task<IEnumerable<string>> GetPlantNames();
        Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime);
        Task<Plant> GetPlantByName(string value);
    }

    public class PlantService : Base, IPlantService
    {
        private IRepository<Plant> _plantResporitory;

        public PlantService()
        {
            _plantResporitory = new Repository<Plant>();
        }

        public async Task<Plant> GetPlantByName(string value)
        {
            var slug = MakeSlug(new[] { value });
            var plants = await _plantResporitory.Get<Plant>(x => x.Slug == slug);
            return plants.Count > 0 ? plants[0] : null;
        }

        public async Task<IEnumerable<string>> GetPlantNames()
        {
            var  plantNames = new List<string>();
            var plants = await _plantResporitory.Get<Plant>();

            foreach (var plant in plants)
            {
                plantNames.Add(
                    string.Format("{0}{1}",
                    plant.Name,
                    String.IsNullOrEmpty(plant.Type) ? string.Empty : string.Format(" ({0})", plant.Type)
                    ));
            }
            plantNames.Sort();

            return plantNames;
        }

        public async Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime)
        {
            var slug = MakeSlug(new[] { plantName, plantType });
            var plants = await _plantResporitory.Get<Plant>(x => x.Slug == slug);
            var plantId = 0;
            if (plants.Count == 0)
            {
                plantId = await _plantResporitory.Insert(new Plant
                {
                    Name = plantName,
                    Type = plantType,
                    Slug = slug,
                    SowTime = sowTime,
                    HarvestTime = harvestTime
                });
            }
            else
            {
                var existingPlant = plants[0];
                existingPlant.SowTime = sowTime;
                existingPlant.HarvestTime = harvestTime;
                plantId = await _plantResporitory.Update(existingPlant);
            }

            return plantId;
        }
    }
}

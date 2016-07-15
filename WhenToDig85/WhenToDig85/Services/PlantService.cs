
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
        Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime, string notes);
        Task<int> Save(string plantName, string varietyName, string sowNotes, string harvestNotes);
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

Task<int> Save(string plantName, string varietyName, string sowNotes, string harvestNotes);

        public async Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime, string notes)
        {
            var slug = MakeSlug(new[] { plantName, plantType });
            var plants = await _plantResporitory.Get<Plant>(x => x.Slug == slug);
            var varietyId = 0;
            if (plants.Count > 0)
            {
                var existingPlant = plants[0];
                
             
            }

            return varietyId;
        }
        
        public async Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime, string notes)
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
                    HarvestTime = harvestTime,
                    Notes = notes
                });
            }
            else
            {
                var existingPlant = plants[0];
                existingPlant.SowTime = sowTime;
                existingPlant.HarvestTime = harvestTime;
                existingPlant.Notes = notes;
                plantId = await _plantResporitory.Update(existingPlant);
            }

            return plantId;
        }
    }
}

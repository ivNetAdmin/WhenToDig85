
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
        Task<IEnumerable<string>> GetPlantVarietyNames(string plantName);
        Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime, string notes);
        Task<int> Save(string plantName, string varietyName, string sowNotes, string harvestNotes);
        Task<Plant> GetPlantByName(string value);
        Task<Variety> GetPlantVarietyDetails(string plantName, string varietyName);
    }

    public class PlantService : Base, IPlantService
    {
        private IRepository<Plant> _plantRepository;
        private IRepository<Variety> _varietyRepository;

        public PlantService()
        {
            _plantRepository = new Repository<Plant>();
            _varietyRepository = new Repository<Variety>();
        }

        public async Task<Plant> GetPlantByName(string value)
        {
            var slug = MakeSlug(new[] { value });
            var plants = await _plantRepository.Get<Plant>(x => x.Slug == slug);
            return plants.Count > 0 ? plants[0] : null;
        }

        public async Task<IEnumerable<string>> GetPlantNames()
        {
            var  plantNames = new List<string>();
            var plants = await _plantRepository.Get<Plant>();

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

        public async Task<Variety> GetPlantVarietyDetails(string plantName, string varietyName)
        {
            var slug = MakeSlug(new[] { plantName, varietyName });
            var varieties = await _varietyRepository.Get<Variety>(x => x.PlantVarietySlug == slug);

            return varieties.Count == 0 ? new Variety() : varieties[0];
        }

        public async Task<IEnumerable<string>> GetPlantVarietyNames(string plantName)
        {
            var  varietyNames = new List<string>();
            var slug = MakeSlug(new[] { plantName });
            var varieties = await _varietyRepository.Get<Plant>(x => x.PlantNameSlug == slug);

            foreach (var variety in varieties)
            {
                varietyNames.Add(variety.Name);
            }
            varietyNames.Sort();

            return varietyNames;
        }

        public async Task<int> Save(string plantName, string varietyName, string sowNotes, string harvestNotes)
        {
            var plantSlug = MakeSlug(new[] { plantName });
            var plantVarietySlug = MakeSlug(new[] { plantName, varietyName });
            var plants = await _plantRepository.Get<Plant>(x => x.Slug == plantSlug);
            var varietyId = 0;
            if (plants.Count > 0)
            {
                var existingPlant = plants[0];
                var varieties = await _varietyRepository.Get<Variety>(x => x.PlantVarietySlug == plantVarietySlug);
                
                if (varieties.Count == 0)
                {
                    varietyId = await _varietyRepository.Insert(new Variety
                    {
                        Name = varietyName,
                        PlantName = plantName,
                        PlantNameSlug = plantSlug,
                        PlantVarietySlug = plantVarietySlug,
                        SowNotes = sowNotes,
                        HarvestNotes = harvestNotes
                    });
                }
                else
                {
                    var existingVariety = varieties[0];
                    existingVariety.SowNotes = sowNotes;
                    existingVariety.HarvestNotes = harvestNotes;
                    varietyId = await _varietyRepository.Update(existingVariety);
                }
            }

            return varietyId;
        }
        
        public async Task<int> Save(string plantName, string plantType, string sowTime, string harvestTime, string notes)
        {
            var slug = MakeSlug(new[] { plantName, plantType });
            var plants = await _plantRepository.Get<Plant>(x => x.Slug == slug);
            var plantId = 0;
            if (plants.Count == 0)
            {
                plantId = await _plantRepository.Insert(new Plant
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
                plantId = await _plantRepository.Update(existingPlant);
            }

            return plantId;
        }
    }
}

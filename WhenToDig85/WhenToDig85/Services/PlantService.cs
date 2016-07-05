
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenToDig85.Models.Data;

namespace WhenToDig85.Services
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetPlantList();
        Task<IEnumerable<string>> GetPlantNames();
    }

    public class PlantService : IPlantService
    {
        public Task<IEnumerable<string>> GetPlantNames()
        {
            return Task.Run(() => GetAllNames());
        }

        public Task<IEnumerable<Plant>> GetPlantList()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<Plant> GetAll()
        {
            return new List<Plant> { new Plant { Name  = "Carrot" } };
        }

        private IEnumerable<string> GetAllNames()
        {
            return new List<string> { "Carrot" };
        }
    }
}

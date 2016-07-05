
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenToDig85.Models.Data;

namespace WhenToDig85.Services
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetPlantList();
    }

    public class PlantService : IPlantService
    {
        public Task<IEnumerable<Plant>> GetPlantList()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<Plant> GetAll()
        {
            return new List<Plant> { new Plant { Name  = "Carrot" } };
        }
    }
}

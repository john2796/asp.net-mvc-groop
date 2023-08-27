using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IRaceRepository
    {


        Task<IEnumerable<Race>> GetAll();
        Task<IEnumerable<Race>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Race>> GetAllRacesByCity(string city);

        Task<IEnumerable<Race>> GetRacesByCategoryAndSliceAsync(RaceCategory category, int offset, int size);

        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(RaceCategory category);

        Task<Race?> GetByIdAsync(int id);

        Task<Race?> GetByIdAsyncNoTracking(int id);

        bool Add(Race race);

        bool Update(Race race);

        bool Delete(Race race);

        bool Save();
    }
}
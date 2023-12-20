using Refit;
using SantanderTechTask.Models;

namespace SantanderTechTask.ExternalApi;

public interface IHackerNewsApi
{
    [Get("/beststories.json")]
    Task<IEnumerable<long>> GetBestStoryIdsAsync();

    [Get("/item/{id}.json")]
    Task<Story> GetStoryByIdAsync(long id);
}
using SantanderTechTask.Models;

namespace SantanderTechTask.Services;

public interface IHackerNewsService
{
    Task<IEnumerable<StoryDTO>> GetBestStoriesAsync(int count);
}
using Mapster;
using SantanderTechTask.ExternalApi;
using SantanderTechTask.Models;

namespace SantanderTechTask.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHackerNewsApi _api;
    private readonly SemaphoreSlim _semaphore;
    private readonly ILogger<HackerNewsService> _logger;

    public HackerNewsService(IHackerNewsApi api, ILogger<HackerNewsService> logger, int maxParallelRequests = 10)
    {
        _api = api;
        _logger = logger;
        _semaphore = new SemaphoreSlim(maxParallelRequests);
    }


    public async Task<IEnumerable<StoryDTO>> GetBestStoriesAsync(int count)
    {
        var storyIds = await _api.GetBestStoryIdsAsync();

        var storyTasks = storyIds.Select(FetchStoryWithSemaphoreAsync);

        var fetchResults = await Task.WhenAll(storyTasks);

        // Filter out unsuccessful fetches and sort by score
        return fetchResults.Where(result => result.IsSuccessful && result.Story != null)
            .Select(result => result.Story!)
            .OrderByDescending(story => story.Score)
            .Take(count)
            .Select(story => story.Adapt<StoryDTO>());
    }
    
    private async Task<StoryFetchResult> FetchStoryWithSemaphoreAsync(long id)
    {
        await _semaphore.WaitAsync();
        try
        {
            var story = await _api.GetStoryByIdAsync(id);
            return new StoryFetchResult(story, true);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"Error fetching story with ID {id}: {ex.Message}");
            return new StoryFetchResult(null, false);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
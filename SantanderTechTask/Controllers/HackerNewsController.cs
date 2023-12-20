using Microsoft.AspNetCore.Mvc;
using SantanderTechTask.Models;
using SantanderTechTask.Services;

namespace SantanderTechTask.Controllers;

[ApiController]
[Route("[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet]
    public async Task<IEnumerable<StoryDTO>> GetBestStories([FromQuery] int count)
    {
        return await _hackerNewsService.GetBestStoriesAsync(count);
    }
}
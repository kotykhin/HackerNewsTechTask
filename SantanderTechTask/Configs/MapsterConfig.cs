using Mapster;
using SantanderTechTask.Models;

namespace SantanderTechTask.Options;

public static class MapsterConfig
{
    public static void Configure()
    {
        ConfigStoryToDtoMapping();
    }
    
    public static void ConfigStoryToDtoMapping()
    {
        TypeAdapterConfig<Story, StoryDTO>.NewConfig()
            .Map(dest => dest.Time, src => DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime)
            .Map(dest => dest.CommentCount, src => src.Descendants)
            .Map(dest => dest.Uri, src => src.Url)
            .Map(dest => dest.PostedBy, src => src.By);
    }
}
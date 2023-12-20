namespace SantanderTechTask.Models;

public record StoryDTO(string Title, string Uri, string PostedBy, DateTime Time, int Score, int CommentCount);
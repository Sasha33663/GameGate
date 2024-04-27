namespace Presentation.Dto;

public sealed record ReactionDto(bool WithoutReview, string? Reviews, int Stars, string GameName);

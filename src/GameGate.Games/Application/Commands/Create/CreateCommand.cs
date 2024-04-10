using MediatR;

namespace Application.Commands.Create;
public sealed record CreateCommand(string Name,
    string Discription,
    string? GamePreviewName,
    Stream? GamePreview,
    string Genre,
    string Kind,
    string Creator,
    decimal PriceMaxValue,
    decimal PriceMinValue,
    bool IsDirectly,
    string coockie) : IRequest;
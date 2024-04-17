using MediatR;

namespace Application.Commands.Refund;
public record RefundCommand(string GameName, string Cookie) : IRequest;

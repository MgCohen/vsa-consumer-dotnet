namespace Consumer.Features.Counter.Slices.GetCount;

using Consumer.Features.Counter.Domain;
using Umpire.Messages.Contracts;
using Umpire.Vsa.Handlers.Requests;
using Umpire.Vsa.Infrastructure;

sealed class GetCountHandler(IRepository<Tally, TallyId> tallies) : RequestHandler<GetCountRequest, GetCountResponse> {
    static readonly Error NotFound = new("counter.not_found", "The counter does not exist.");

    public override async Task<Result<GetCountResponse>> HandleAsync(Envelope<GetCountRequest> envelope) =>
        await tallies.GetAsync(TallyId.Main, envelope.Cancellation) is { } tally
            ? new GetCountResponse(tally.Value, tally.Max)
            : NotFound;
}

using Counter.Domain;
using VSA.Contracts;
using VSA.Handlers.Requests;
using VSA.Infrastructure;

namespace Counter.Slices.GetCount;

sealed class GetCountHandler(IRepository<Tally, TallyId> tallies) : RequestHandler<GetCountRequest, GetCountResponse> {
    static readonly Error NotFound = new("counter.not_found", "The counter does not exist.");

    public override async Task<Result<GetCountResponse>> HandleAsync(Envelope<GetCountRequest> envelope) =>
        await tallies.GetAsync(TallyId.Main, envelope.Cancellation) is { } tally
            ? new GetCountResponse(tally.Value, tally.Max)
            : NotFound;
}

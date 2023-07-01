namespace JsonPatch;

public record UpdateRemoveRequest(IEnumerable<string> Data) : IUpdateRequest
{
    public Operation Operation => Operation.Remove;
}
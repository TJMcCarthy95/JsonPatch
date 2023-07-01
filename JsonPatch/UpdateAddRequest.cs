namespace JsonPatch;

public record UpdateAddRequest(IEnumerable<string> Data) : IUpdateRequest
{
    public Operation Operation => Operation.Add;
}

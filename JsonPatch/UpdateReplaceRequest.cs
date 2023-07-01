namespace JsonPatch;

public record UpdateReplaceRequest(IEnumerable<string> Data) : IUpdateRequest
{
    public Operation Operation => Operation.Replace;
}

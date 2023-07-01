namespace JsonPatch;

public record UpdateEmptyRequest : IUpdateRequest
{
    public Operation Operation => Operation.NotSpecified;
}

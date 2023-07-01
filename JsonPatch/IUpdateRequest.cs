using System.Text.Json.Serialization;

namespace JsonPatch;

[JsonConverter(typeof(UpdateRequestJsonConverter))]
public interface IUpdateRequest
{
    public Operation Operation { get; }
}

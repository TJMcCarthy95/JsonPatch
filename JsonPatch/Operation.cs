using System.Text.Json.Serialization;

namespace JsonPatch;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Operation
{
    NotSpecified,
    Add,
    Replace,
    Remove
}

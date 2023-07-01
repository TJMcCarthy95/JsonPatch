using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonPatch;

public class UpdateRequestJsonConverter : JsonConverter<IUpdateRequest>
{
    public override IUpdateRequest? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert != typeof(IUpdateRequest))
        {
            throw new JsonException($"Cannot convert type: {typeToConvert}");
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        try
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);

            if (!jsonDocument.RootElement.TryGetProperty(nameof(IUpdateRequest.Operation), out var typeProperty))
            {
                throw new JsonException($"Couldn't find property: {nameof(Operation)}");
            }

            return typeProperty.Deserialize<Operation>(options) switch
            {
                Operation.Add => jsonDocument.Deserialize<UpdateAddRequest>(options),
                Operation.Replace => jsonDocument.Deserialize<UpdateReplaceRequest>(options),
                Operation.Remove => jsonDocument.Deserialize<UpdateRemoveRequest>(options),
                _ => new UpdateEmptyRequest()
            };
        }
        catch (Exception)
        {
            return new UpdateEmptyRequest();
        }
    }

    public override void Write(Utf8JsonWriter writer, IUpdateRequest value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, (object)value, options);
}

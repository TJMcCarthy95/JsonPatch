# JsonPatch
JSONPatch Serialization .NET Example

Following repo contains an example of a [JSON Patch](https://jsonpatch.com/) implementation within .NET 7 using a `JsonConverter`
to parse the `Operation` discriminator into the corresponding classes following the Domain Primitive principle.

## Examples

**Add Request**

```json
{
  "Data": [
    "string1",
    "string2"
  ],
  "Operation": "Add"
}
```

becomes

```csharp
UpdateAddRequest {
  Data = System.Collections.Generic.List`1[System.String],
  Operation = Add
}
```

**Empty Request**

```json
{
}
```

becomes

```csharp
UpdateEmptyRequest {
  Operation = NotSpecified
}
```

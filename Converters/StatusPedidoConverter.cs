using System.Text.Json;
using System.Text.Json.Serialization;
using EcommerceApi.Enums;

namespace EcommerceApi.Converters;

public class StatusPedidoConverter : JsonConverter<StatusPedido>
{
    // leitura — converte JSON → enum
    public override StatusPedido Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
    var valor = reader.GetString();
        if (Enum.TryParse<StatusPedido>(valor, ignoreCase: true, out var status))
            return status;

        var valoresPermitidos = string.Join(", ", Enum.GetNames<StatusPedido>());
        throw new JsonException($"Status '{valor}' é inválido. Valores permitidos: {valoresPermitidos}");
    }

// escrita — converte enum → JSON
    public override void Write(Utf8JsonWriter writer, StatusPedido value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString()); // ← mantém como string no response
    }
}
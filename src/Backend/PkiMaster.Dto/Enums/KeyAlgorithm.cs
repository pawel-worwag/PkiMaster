using System.Text.Json.Serialization;

namespace PkiMaster.Dto.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<KeyAlgorithm>))]
public enum KeyAlgorithm
{
    Rsa = 1,
    Ecdsa = 2,
    Ed25519 = 3
}

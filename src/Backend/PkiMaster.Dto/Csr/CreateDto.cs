using System.Text.Json.Serialization;
using PkiMaster.Dto.Enums;

namespace PkiMaster.Dto.Csr;

public sealed record CreateDto(
    [property: JsonPropertyName("key-generation")] KeyGenerationDto KeyGeneration,
    [property: JsonPropertyName("certification-requestInfo")] CertificationRequestInfoDto CertificationRequestInfo);

public sealed record KeyGenerationDto(
    [property: JsonPropertyName("algorithm")] KeyAlgorithm Algorithm,
    [property: JsonPropertyName("key-length")] int KeyLength,
    [property: JsonPropertyName("password")] string Password);

public sealed record CertificationRequestInfoDto(
    [property: JsonPropertyName("subject")] IReadOnlyCollection<RelativeDistinguishedNameValue> Subject,
    [property: JsonPropertyName("attributes")] IReadOnlyCollection<AttributeDto>? Attributes);

public sealed record RelativeDistinguishedNameValue(
    [property: JsonPropertyName("type-oid")] string TypeOid,
    [property: JsonPropertyName("value")] string Value);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ExtensionRequestAttributeDto), "extension-request")]
[JsonDerivedType(typeof(CustomAttributeDto), "custom")]
public abstract record AttributeDto(
    [property: JsonPropertyName("oid")] string Oid);

public sealed record ExtensionRequestAttributeDto(
    [property: JsonPropertyName("extensions")] IReadOnlyCollection<ExtensionDto> Extensions)
    : AttributeDto("1.2.840.113549.1.9.14");

public sealed record CustomAttributeDto(
    string Oid,
    [property: JsonPropertyName("values-base64")] IReadOnlyCollection<string> ValuesBase64)
    : AttributeDto(Oid);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SanExtensionDto), "san")]
[JsonDerivedType(typeof(KeyUsageExtensionDto), "key-usage")]
[JsonDerivedType(typeof(EnhancedKeyUsageExtensionDto), "enhanced-key-usage")]
[JsonDerivedType(typeof(BasicConstraintsExtensionDto), "basic-constraints")]
[JsonDerivedType(typeof(CrlDistributionPointsExtensionDto), "crl-distribution-points")]
[JsonDerivedType(typeof(CustomExtensionDto), "custom")]
public abstract record ExtensionDto(
    [property: JsonPropertyName("oid")] string Oid,
    [property: JsonPropertyName("critical")] bool Critical);

public sealed record GeneralNameEntryDto(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value);

public sealed record SanExtensionDto(
    [property: JsonPropertyName("entries")]
    IReadOnlyCollection<GeneralNameEntryDto> Entries,
    bool Critical = false)
    : ExtensionDto("2.5.29.17", Critical);

public sealed record KeyUsageExtensionDto(
    [property: JsonPropertyName("digital-signature")] bool DigitalSignature,
    [property: JsonPropertyName("non-repudiation")] bool NonRepudiation,
    [property: JsonPropertyName("key-encipherment")] bool KeyEncipherment,
    [property: JsonPropertyName("data-encipherment")] bool DataEncipherment,
    [property: JsonPropertyName("key-agreement")] bool KeyAgreement,
    [property: JsonPropertyName("key-cert-sign")] bool KeyCertSign,
    [property: JsonPropertyName("crl-sign")] bool CrlSign,
    [property: JsonPropertyName("encipher-only")] bool EncipherOnly,
    [property: JsonPropertyName("decipher-only")] bool DecipherOnly,
    bool Critical = true)
    : ExtensionDto("2.5.29.15", Critical);

public sealed record EnhancedKeyUsageExtensionDto(
    [property: JsonPropertyName("purpose-oids")] IReadOnlyCollection<string> PurposeOids,
    bool Critical = false)
    : ExtensionDto("2.5.29.37", Critical);

public sealed record BasicConstraintsExtensionDto(
    [property: JsonPropertyName("is-certificate-authority")] bool IsCertificateAuthority,
    [property: JsonPropertyName("path-length-constraint")] int? PathLengthConstraint,
    bool Critical = true)
    : ExtensionDto("2.5.29.19", Critical);

public sealed record CrlDistributionPointsExtensionDto(
    [property: JsonPropertyName("distribution-points")] IReadOnlyCollection<CrlDistributionPointDto> DistributionPoints,
    bool Critical = false)
    : ExtensionDto("2.5.29.31", Critical);

public sealed record CrlDistributionPointDto(
    [property: JsonPropertyName("full-name")] IReadOnlyCollection<GeneralNameEntryDto>? FullName,
    [property: JsonPropertyName("relative-name")] string? RelativeName,
    [property: JsonPropertyName("reasons")] IReadOnlyCollection<CrlDistributionPointReason>? Reasons,
    [property: JsonPropertyName("crl-issuer")] IReadOnlyCollection<GeneralNameEntryDto>? CrlIssuer);

public sealed record CustomExtensionDto(
    string Oid,
    bool Critical,
    [property: JsonPropertyName("value-base64")] string ValueBase64)
    : ExtensionDto(Oid, Critical);
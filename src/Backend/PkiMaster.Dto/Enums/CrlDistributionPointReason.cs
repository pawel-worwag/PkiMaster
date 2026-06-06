namespace PkiMaster.Dto.Enums;

public enum CrlDistributionPointReason
{
    Unused = 0,
    KeyCompromise = 1,
    CaCompromise = 2,
    AffiliationChanged = 3,
    Superseded = 4,
    CessationOfOperation = 5,
    CertificateHold = 6,
    PrivilegeWithdrawn = 7,
    AaCompromise = 8
}

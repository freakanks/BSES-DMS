namespace BSES.DocumentManagementSystem.Common
{
    public static class DMSConstants
    {
        public const string DATA_DELIMITER = "#";
        public const string USER_SESSION_DATA = "USER_SESSION_DATA";
        public const string USER_DETAILS = nameof(USER_DETAILS);

        //Constants for SWS integration.
        public const string JWT_ISSUER_CONFIG_KEY = "Jwt:Issuer";
        public const string JWT_AUDIENCE_CONFIG_KEY = "Jwt:Audience";
        public const string JWT_SECRET_KEY_CONFIG_KEY = "Jwt:Key";
        public const string BASE_STORAGE_PATH_KEY = "BasePathForStorage";

        //Constants Keys for Tokens.
        public const string TOKEN = "X-CSRF-TOKEN";

        public const string HKEY_BSES_RAJDHANI = "SOFTWARE\\HKEY_BSES_RAJDHANI";
        public const string DMSEncryptionKey = "DMSEncryptionKey";
        public const string DMSKeyForIV = "DMSEncryptionIV";
    }
}

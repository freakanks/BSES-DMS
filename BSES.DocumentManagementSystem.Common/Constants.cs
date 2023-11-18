namespace BSES.DocumentManagementSystem.Common
{
    public static class DMSConstants
    {
        public const string USER_SESSION_DATA = "USER_SESSION_DATA";
        public const string USER_DETAILS = nameof(USER_DETAILS);

        //Constants for SWS integration.
        public const string swskey = nameof(swskey);
        public const string Bearer = nameof(Bearer);
        public const string swsAuthToken = nameof(swsAuthToken);
        public const string BASE_STORAGE_PATH_KEY = "BasePathForStorage";

        //Constants Keys for Tokens.
        public const string TOKEN = "X-CSRF-TOKEN";

    }
}

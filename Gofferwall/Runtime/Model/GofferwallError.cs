namespace Gofferwall.Model
{
    /// <summary>
    /// error information with code and description
    /// </summary>
    public class GofferwallError
    {
        public enum ErrorCode
        {
            UNKNOWN_ERROR           = -1,
            INTERNAL_ERROR          = 0,
            MEDIATION_ERROR         = 1,
            INITIALIZE_ERROR        = 2,
            SERVER_SETTING_ERROR    = 3,
            INVALID_REQUEST         = 4,
            NETWORK_ERROR           = 5,
            USER_SETTING_ERROR      = 6
        };

        public ErrorCode Code { get; private set; }
        public string Message { get; private set; }

        public GofferwallError(int code, string message) {
            updateData(code, message);
        }

        private void updateData(int code, string message)
        {
            if (false == System.Enum.IsDefined(typeof(ErrorCode), code))
            {
                this.Code = ErrorCode.UNKNOWN_ERROR;
            }
            else
            {
                this.Code = (ErrorCode)code;
            }

            this.Message = message ?? string.Empty;
        }

        public override string ToString()
        {
            return
                "GofferwallError {" +
                "Code=\"" + this.Code + "\"" +
                ", Message=\"" + this.Message + "\"" +
                "}";
        }
    }
}

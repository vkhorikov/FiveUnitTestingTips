namespace Module4
{
    public class Envelope
    {
        public object Result { get; }
        public string ErrorCode { get; }

        public bool IsError => ErrorCode != null;

        private Envelope(object result, string errorCode)
        {
            Result = result;
            ErrorCode = errorCode;
        }

        public static Envelope Ok(object result)
        {
            return new Envelope(result, null);
        }

        public static Envelope Error(string errorCode)
        {
            return new Envelope(null, errorCode);
        }
    }
}

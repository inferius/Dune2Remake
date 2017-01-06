using System;

namespace Dune2RemakeTest
{
    public class Error
    {
        public ErrorStatus Status { get; protected set; }
        public int Code { get; protected set; }
        public string TextCode { get; protected set; }
        public string Message { get; protected set; }
        public DateTime ErrorTime { get; protected set; }
        public DateTime ErrorTimeUtc { get; protected set; }

        private Error()
        {
            ErrorTime = DateTime.Now;
            ErrorTimeUtc = DateTime.UtcNow;
        }

        public Error(string message, int code = 0)
            : this()
        {
            Message = message;
            Code = code;
            TextCode = string.Empty;
            Status = ErrorStatus.UnknownError;
        }

        public Error(ErrorStatus status, string message, int code = 0)
            : this()
        {
            Message = message;
            Code = code;
            TextCode = string.Empty;
            Status = status;
        }

        public Error(ErrorStatus status, string message, string textCode, int code = 0)
            : this()
        {
            Message = message;
            Code = code;
            TextCode = textCode;
            Status = status;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2} ({3})", Status.ToString(), TextCode, Message, Code);
        }
    }

    public enum ErrorStatus
    {
        Success,
        NetworkError,
        PermissionError,
        UnknownError
    }
}

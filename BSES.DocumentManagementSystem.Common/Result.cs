namespace BSES.DocumentManagementSystem.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        public Result(bool isSuccess, string errMessage)
        {
            IsSuccess = isSuccess; ErrorMessage = errMessage;
        }
    }
    public class Result<T>
    {
        public T Value { get;}
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        public Result(T value, bool isSuccess, string errMessage)
        {
            Value = value; IsSuccess = isSuccess; ErrorMessage = errMessage; 
        }
    }
}

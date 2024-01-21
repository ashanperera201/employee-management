#region Namespace
namespace Application.Models
{
    public class GenericResponse<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResponse{T}"/> class.
        /// </summary>
        public GenericResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResponse{T}"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="status">The status.</param>
        public GenericResponse(T? response, bool isSuccess, int status)
        {
            Response = response ?? null;
            IsSuccess = isSuccess;
            Status = status;
        }
        
        public GenericResponse(T? response, bool isSuccess, int status, string? message)
        {
            Response = response ?? null;
            IsSuccess = isSuccess;
            Status = status;
            Message = message;
        }

        public T? Response { get; set; }
        public bool IsSuccess { get; set; } = false;
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
#endregion
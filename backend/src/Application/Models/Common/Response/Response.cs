using System.Net;

namespace backend.src.Application.Models.Common.Response
{
    public class Response<T>
    {

        public T Data { get; set; }
        public bool Succeeded { get; set; } = true;
        public Dictionary<string, List<string>> Errors { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }

        public Response()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public Response(T data, bool succeeded, string message = null, int statusCode = (int)HttpStatusCode.OK, string errorCode = null)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = new Dictionary<string, List<string>>();
            Data = data;
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public void AddError(string key, string error)
        {
            if (!Errors.ContainsKey(key))
            {
                Errors[key] = new List<string>();
            }
            Errors[key].Add(error);
        }

        public void AddErrors(string key, List<string> errors)
        {
            if (!Errors.ContainsKey(key))
            {
                Errors[key] = new List<string>();
            }
            Errors[key].AddRange(errors);
        }

        public void AddErrors(Dictionary<string, List<string>> errors)
        {
            foreach (var error in errors)
            {
                AddErrors(error.Key, error.Value);
            }
        }

        public Response<T> Forbidden(string message = null, string errorCodeStatic = "")
        {
            Succeeded = false;
            Data = default(T);
            Message = message;
            StatusCode = (int)HttpStatusCode.Forbidden;
            ErrorCode = errorCodeStatic ?? ErrorCodeStatic.FORBIDDEN; // Optional: Set an error code for forbidden access
            return this;
        }

        public Response<T> InternalServerError(string message = null, string errorCodeStatic = "")
        {
            Succeeded = false;
            Data = default(T);
            Message = message;
            StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorCode = errorCodeStatic ?? ErrorCodeStatic.SERVER_ERROR; // Optional: Set an error code for internal server error
            return this;
        }

        public Response<T> NotFound(string message = null, string errorCodeStatic = "")
        {
            Succeeded = false;
            Data = default(T);
            Message = message;
            StatusCode = (int)HttpStatusCode.NotFound;
            ErrorCode = errorCodeStatic ?? ErrorCodeStatic.NOT_FOUND; // Optional: Set an error code for not found
            return this;
        }

        public Response<T> BadRequest(string message = null, string errorCodeStatic = "")
        {
            Succeeded = false;
            Data = default(T);
            Message = message;
            StatusCode = (int)HttpStatusCode.BadRequest;
            ErrorCode = errorCodeStatic ?? ErrorCodeStatic.BAD_REQUEST; // Optional: Set an error code for bad request
            return this;
        }

        public Response<T> NoContent(T data, string message = null, string errorCodeStatic = "")
        {
            // 204 usually means success, just no content.
            Succeeded = true;
            Data = data;
            Message = message;
            StatusCode = (int)HttpStatusCode.NoContent;
            ErrorCode = errorCodeStatic ?? ErrorCodeStatic.OK; // Optional: Set an error code for no content
            return this;
        }

        public Response<T> Ok(T data)
        {
            // 200 = success
            Succeeded = true;
            Data = data;
            StatusCode = (int)HttpStatusCode.OK;
            ErrorCode = ErrorCodeStatic.OK; // Optional: Set an error code for OK
            return this;
        }

        public Response<T> UnAuthorized(string message = null, string errorCodeStatic = "")
        {
            // 401 = unauthorized
            Succeeded = false;
            Data = default(T);  // keep Data empty or null
            Message = message;
            StatusCode = (int)HttpStatusCode.Unauthorized;
            ErrorCode = errorCodeStatic ?? ErrorCodeStatic.UNAUTHORIZED; // Optional: Set an error code for unauthorized
            return this;
        }
    }
}

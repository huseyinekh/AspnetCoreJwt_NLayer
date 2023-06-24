using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{
    public class ResponseDto<T> where T : class
    {
        public T? Data { get;private set; }
        public int StatusCode { get;private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; } 
        public ErrorDto? Error { get; set; }

        public static ResponseDto<T>Success(T? data,int StatusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = StatusCode ,IsSuccess=true};
        }
        public static ResponseDto<T> Success( int StatusCode)
        {
            return new ResponseDto<T> { Data = default,  StatusCode = StatusCode, IsSuccess = true };
        }
        public static ResponseDto<T> Fail(ErrorDto error,int statusCode)
        {
            return new ResponseDto<T> { Error = error,StatusCode= statusCode, IsSuccess = false };
        }

        public static ResponseDto<T> Fail(string errorMessage, int statusCode,bool isShow=true)
        {
            ErrorDto error =new ErrorDto(errorMessage,isShow);
            return new ResponseDto<T> { Error = error, StatusCode = statusCode, IsSuccess = false };
        }
    }
}

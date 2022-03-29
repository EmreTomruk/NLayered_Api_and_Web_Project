using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore] //Donulecek response body'de StatusCode'a gerek yok ama kod icinde lazim...
        public int StatusCode { get; set; }

        public List<String> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data) //Static Factory Methods
        {
            return new CustomResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode
            };
        }

        public static CustomResponseDto<T> Success(int statusCode) //Update, Remove gibi islemlerde Data donmeye gerek yoktur...
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors) //Birden fazla error
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error) //Tek bir error
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}

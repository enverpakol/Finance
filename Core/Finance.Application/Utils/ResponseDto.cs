using System.Net;
using System.Text.Json.Serialization;

namespace Finance.Application.Utils
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public int Result { get; set; }


        public static ResponseDto<T> Success(HttpStatusCode statusCode, T data, int result = 0)
            => new ResponseDto<T> { StatusCode = statusCode, Data = data, Result = result };

        public static ResponseDto<T> Success(HttpStatusCode statusCode, int result = 0)
        => new ResponseDto<T> { StatusCode = statusCode, Result = result };

        public static ResponseDto<T> Fail(HttpStatusCode statusCode, List<string> errors, int result = 0)
        => new ResponseDto<T> { StatusCode = statusCode, Errors = errors, Result = result };

        public static ResponseDto<T> Fail(HttpStatusCode statusCode, string error, int result = 0)
        => new ResponseDto<T>
        {
            StatusCode = statusCode,
            Errors = new List<string>() { error },
            Result = result
        };


    }


    public class PagerResponseDto<T>
    {
        public List<T> Datas { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public int Result { get; set; }
        public int TotalItemsCount { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int CurrentPageSize { get; set; }



        public static PagerResponseDto<T> Success(HttpStatusCode statusCode, List<T> data, PagerListInfoModel pagerInfo, int result = 1)
        {
            return new PagerResponseDto<T>
            {
                StatusCode = statusCode,
                Datas = data,
                Result = result,
                CurrentPageIndex = pagerInfo.CurrentPageIndex,
                CurrentPageSize = pagerInfo.CurrentPageSize,
                TotalItemsCount = pagerInfo.TotalItemsCount,
                TotalPageCount = pagerInfo.TotalPageCount
            };
        }

        public static PagerResponseDto<T> Fail(HttpStatusCode statusCode, List<string> errors, int result = -99)
        => new PagerResponseDto<T> { StatusCode = statusCode, Errors = errors, Result = result };

        public static PagerResponseDto<T> Fail(HttpStatusCode statusCode, string error, int result = -99)
        => new PagerResponseDto<T>
        {
            StatusCode = statusCode,
            Result = result,
            Errors = new List<string>() { error }
        };
    }
}

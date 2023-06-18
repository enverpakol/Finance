namespace Finance.Application.Utils
{
    public class ListRequestDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderField { get; set; }
        public string OrderDir { get; set; }
        public string Filter { get; set; }
    }
    public class ListRequestDtoById : ListRequestDto
    {
        public int Id { get; set; }
    }
}

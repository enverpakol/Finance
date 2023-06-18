namespace Finance.Application.Dtos
{
    public class Token
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccesToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

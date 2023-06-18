namespace Finance.Application.Dtos
{
    public class AdminEditDto : AdminCreateDto
    {
        public int Id { get; set; }
    }
    public class AdminCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

namespace DessertboxAPI.Dto
{
    public class LoginresponseDto
	{
        public string Token { get; set; } 
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public UserDto User { get; set; }

	}
}

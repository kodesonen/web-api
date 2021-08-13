namespace api.Models.Auth
{
    public class RegisterModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

		public string University { get; set; }

		public string Study { get; set; }

		public string Degree { get; set; }

        public string Password { get; set; }
    }
}
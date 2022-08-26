using DevagramCSharp.Models;

namespace DevagramCSharp.Dtos
{
	public class LoginRespostaDto
	{
		internal Usuario usuario;

		public string Nome { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}
}

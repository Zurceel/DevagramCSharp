using System.Net.Http.Headers;
using DevagramCSharp.Dtos;

namespace DevagramCSharp.Services
{
	public class CosmicService
	{
		public string EnviarImagem(ImagemDto imagemdto)
		{
			Stream imagem; 

			imagem = imagemdto.Imagem.OpenReadStream();

			var client = new HttpClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "N9LKV61NWuWkFovd5ItAaoybEHhGTKt3Op12AAOsMnVkPq06zj");

			var request = new HttpRequestMessage(HttpMethod.Post, "file");
			var conteudo = new MultipartFormDataContent
			{
				{ new StreamContent(imagem), "media", imagemdto.Nome }
			};

			request.Content = conteudo;
			var retornoreq = client.PostAsync("https://upload.cosmicjs.com/v2/buckets/projetodevagramc-devagram/media", request.Content).Result;

			var urlretorno = retornoreq.Content.ReadFromJsonAsync<CosmicRespostadto>();


			return urlretorno.Result.media.url;
		}
	}
}

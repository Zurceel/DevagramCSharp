using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PublicacaoController : BaseController
	{
		private readonly ILogger<PublicacaoController> _logger;
		private readonly IPublicacaoRepository _publicacaoRepository;

		public PublicacaoController(ILogger<PublicacaoController> logger,
			IPublicacaoRepository publicacaoRepository, IUsuarioRepository usuarioRepository) : base(usuarioRepository)
		{
			_logger = logger;
			_publicacaoRepository = publicacaoRepository;
		}

		[HttpPost]
		public IActionResult Publicar([FromForm] PublicacaoRequisicaoDto publicacaodto)
		{
			try
			{
				Usuario usuario = LerToken();
				CosmicService cosmicService = new CosmicService();
				if(publicacaodto != null)
				{
					if (String.IsNullOrEmpty(publicacaodto.Descricao) && 
						String.IsNullOrWhiteSpace(publicacaodto.Descricao))
					{
						_logger.LogError("A descricão está inválida.");
						return BadRequest("Obrigatorio uma descricao.");
					}
					if(publicacaodto.Foto == null)
					{
						return BadRequest("A foto está inválida.");
					}
					Publicacao publicacao = new Publicacao()
					{
						Descricao = publicacaodto.Descricao,
						IdUsuario = usuario.Id,
						Foto = cosmicService.EnviarImagem(new ImagemDto { Imagem = publicacaodto.Foto, Nome = "publicacao" })
					};
					_publicacaoRepository.Publicar(publicacao);
				}

				return Ok("Publicacao realizada com sucesso.");
			}
			catch(Exception ex)
			{
				_logger.LogError("Ocorreu um erro na publicacão: " + ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
				{
					Descricao = "Ocorreu um erro ao fazer o login",
					Status = StatusCodes.Status500InternalServerError
				});
			}
		}
	}
}

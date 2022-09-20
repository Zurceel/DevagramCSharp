using DevagramCSharp.Dtos;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
	public class PublicacaoRepositoryImpl : IPublicacaoRepository
	{
		private readonly DevagramContext _context;

		public PublicacaoRepositoryImpl (DevagramContext context)
		{
			_context = context;
		}

		public List<PublicacaoFeedRespostaDto> GetPublicacoesFeed(int idUsuario)
		{
			var feed =
				from publicacaos in _context.Publicacaos
				join seguidores in _context.Seguidores on publicacaos.IdUsuario equals seguidores.IdUsuarioSeguido
				where seguidores.IdUsuarioSeguidor == idUsuario
				select new PublicacaoFeedRespostaDto
				{
					IdPublicacao = publicacaos.Id,
					Descricao = publicacaos.Descricao,
					Foto = publicacaos.Foto,
					IdUsuario = publicacaos.IdUsuario
				};

			return feed.ToList();
		}

		public List<PublicacaoFeedRespostaDto> GetPublicacoesFeedUsuario(int idUsuario)
		{
			var feedusuario =
				from publicacaos in _context.Publicacaos
				where publicacaos.IdUsuario == idUsuario
				select new PublicacaoFeedRespostaDto
				{
					IdPublicacao = publicacaos.Id,
					Descricao = publicacaos.Descricao,
					Foto = publicacaos.Foto,
					IdUsuario = publicacaos.IdUsuario
				};

			return feedusuario.ToList();
		}

		public int GetQtdePublicacoes(int idUsuario)
		{
			return _context.Publicacaos.Count(p => p.IdUsuario == idUsuario);
		}

		public void Publicar(Publicacao publicacao)
		{
			_context.Add(publicacao);
			_context.SaveChanges();
		}
	}
}

﻿using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
using DevagramCSharp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase 
	{

		private readonly ILogger<LoginController> _logger;
		private readonly IUsuarioRepository _usuarioRepository;

		public LoginController (ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
		{
			_logger = logger;
			_usuarioRepository = usuarioRepository;
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto loginrequisicao)
		{
			try
			{
				if(!String.IsNullOrEmpty(loginrequisicao.Senha) && !String.IsNullOrEmpty(loginrequisicao.Email) &&
					!String.IsNullOrWhiteSpace(loginrequisicao.Senha) && !String.IsNullOrWhiteSpace(loginrequisicao.Senha))
				{
					Usuario usuario = _usuarioRepository.GetUsuarioPorLoginSenha(loginrequisicao.Email.ToLower(), MD5Utils.GerarHashMD5(loginrequisicao.Senha));

					if(usuario != null)
					{
						return Ok(new LoginRespostaDto()
						{
							Email = usuario.Email,
							Nome = usuario.Nome,
							Token = TokenService.CriarToken(usuario)
						});
					}
					else
					{
						return BadRequest(new ErrorRespostaDto()
						{
							Descricao = "Email ou senha inválidos!",
							Status = StatusCodes.Status400BadRequest
						});
					}
				}
				else
				{
					return BadRequest(new ErrorRespostaDto()
					{
						Descricao = "Usuário não preencheu os campos de login corretamente",
						Status = StatusCodes.Status400BadRequest
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Ocorreu um erro no login: " + ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
				{
					Descricao = "Ocorreu um erro ao fazer o login",
					Status = StatusCodes.Status500InternalServerError
				});
			}
		}
	}
}

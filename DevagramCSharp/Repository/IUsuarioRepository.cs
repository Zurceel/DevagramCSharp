﻿using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioPorId(int id);
        Usuario GetUsuarioPorLoginSenha(string email, string senha);
		public void Salvar(Usuario usuario);

        public bool VerificarEmail(string email);

        public void AtualizarUsuario(Usuario usuario);
		List<Usuario> GetUsuarioNome(string nome);
	}
}

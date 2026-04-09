using ApiLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private List<Usuario> PegarDados()
        {
            // Criar uma lista de Alunos vazia para receber os dados do arquivo
            List<Usuario> listaUsuario = new();

            try
            {
                // Pegar arquivo c:\temp\alunos.json e trazer para a memória
                string dadosArquivo = System.IO.File.ReadAllText("c:\\temp\\usuario.json");
                listaUsuario = System.Text.Json.JsonSerializer.Deserialize<List<Usuario>>(dadosArquivo);
            }
            catch { }

            return listaUsuario;
        }
        [HttpPost]
        public IActionResult Logar(Logar dados)
            
        {
            var listaUsuario = PegarDados();
            var usuario = listaUsuario.Where(item => item.Email == dados.Email && item.Senha == dados.Senha).FirstOrDefault();
            if (listaUsuario != null)
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}

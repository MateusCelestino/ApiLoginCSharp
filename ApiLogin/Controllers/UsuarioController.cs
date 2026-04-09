using ApiLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
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

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(PegarDados());
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            List<Usuario> listaUsuario = PegarDados();
            listaUsuario.Add(usuario);
            SalvarArquivo(listaUsuario);
            return Ok(PegarDados());
        }
        [HttpDelete]

        public IActionResult Deletar(int id)
        {
            List<Usuario> listaUsuario = PegarDados();
            SalvarArquivo(listaUsuario.Where(item => item.Id != id).ToList());
            return Ok();
        }
        [HttpPut]
        public IActionResult Editar(Usuario usuario)
        {
            var listaUsuario = PegarDados();
            listaUsuario = listaUsuario.Where((item => item.Id != usuario.Id)).ToList();
            listaUsuario.Add(usuario);
            SalvarArquivo(listaUsuario);
            return Ok();
        }
        private void SalvarArquivo(List<Usuario> listaUsuario)
        {
            string dadosJson = System.Text.Json.JsonSerializer.Serialize(listaUsuario);
            System.IO.File.WriteAllText("c:\\temp\\usuario.json", dadosJson);

        }
    }
}

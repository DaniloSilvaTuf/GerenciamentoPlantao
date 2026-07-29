using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Enums;

namespace GerenciamentoPlantao.Services
{
    public interface IUsuarioLogadoService
    {
        Task<Usuario?> ObterUsuarioLogadoAsync();
        Task<string?> ObterIdUsuarioAsync();
        Task<int?> ObterDepartamentoIdAsync();
        Task<string?> ObterNomeUsuarioLogadoAsync();
        Task<PerfilUsuario?> ObterPerfilUsuarioLogadoAsync();
        Task<bool> EstaAutenticadoAsync();

    }
}

using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Enums;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class UsuarioService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public UsuarioService(GerenciamentoPlantaoContext context, UserManager<Usuario> userManager, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _userManager = userManager;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Usuario>> FindAllAsync()
        {
            return await _context.Users
                .Include(u => u.Departamento)
                .OrderBy(x => x.DescNome)
                .ToListAsync();
        }

        public async Task<List<Usuario>> FindAllActiveAsync()
        {
            return await _context.Users.Where(e => e.Ativo).OrderBy(e => e.DescNome).ToListAsync();
        }

        public async Task<Usuario> FindByIdAsync(string id)
        {
            var usuario = await _context.Users
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }

            return usuario;
        }

        public async Task<Usuario> FindByIdWithAuditAsync(string id)
        {
            var usuario = await _context.Users
                .Include(u => u.UsuarioInsert)
                .Include(u => u.UsuarioUpdate)
                .Include(u => u.UsuarioInativacao)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }
            return usuario;
        }

        public async Task CriarUsuarioAsync(UsuarioFormViewModel vm)
        {
            var usuarioLogado = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var usuario = new Usuario
            (
                vm.DescNome,
                vm.NmUsuario,
                vm.Email,
                vm.Telefone,
                vm.Plantonista,
                vm.Perfil,
                vm.DepartamentoId,
                usuarioLogado.Id
            );

            var resultado = await _userManager.CreateAsync(usuario, vm.Senha);

            if(!resultado.Succeeded)
            {
                var mensagem = string.Join(Environment.NewLine, resultado.Errors.Select(e => e.Description));

                throw new BusinessException(mensagem);
            }

            await _userManager.AddToRoleAsync(usuario, usuario.Perfil.ToString());
        }

        public async Task AlterarUsuarioAsync(EditarUsuarioViewModel vm)
        {
            var usuarioLogado = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var usuario = await FindByIdAsync(vm.Id);
            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }

            usuario.Atualizar
            (
                vm.DescNome,
                vm.Email,
                vm.Telefone,
                vm.Plantonista,
                vm.Perfil,
                vm.DepartamentoId,
                usuarioLogado.Id
            );

            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarSenhaAsync(AlterarSenhaViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var resultado = await _userManager.ChangePasswordAsync(
                usuario,
                vm.SenhaAtual,
                vm.NovaSenha);

            if (!resultado.Succeeded)
            {
                var mensagem = string.Join(Environment.NewLine, resultado.Errors.Select(e => e.Description));

                throw new BusinessException(mensagem);
            }

            if(usuario.TrocaSenhaObrigatoria)
            {
                usuario.TrocaSenhaObrigatoria = false;
                await _userManager.UpdateAsync(usuario);
            }
        }

        public async Task AlterarSenhaAdmAsync(AlterarSenhaAdmViewModel vm)
        {
            var perfilUsuario = await _usuarioLogadoService.ObterPerfilUsuarioLogadoAsync();
            var usuario = await FindByIdAsync(vm.UsuarioId);

            if(perfilUsuario != PerfilUsuario.Administrador)
            {
                throw new AccessDeniedException("Você não tem permissão para alterar a senha do usuário.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var resultado = await _userManager.ResetPasswordAsync(
                usuario,
                token,
                vm.NovaSenha);

            if(!resultado.Succeeded)
            {
                var mensagem = string.Join(Environment.NewLine, resultado.Errors.Select(e => e.Description));

                throw new BusinessException(mensagem);
            }

            usuario.TrocaSenhaObrigatoria = true;

            await _userManager.UpdateAsync(usuario);

        }

        public async Task InativarAsync(string id)
        {
            var usuarioLogado = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var usuario = await FindByIdAsync(id);

            usuario.Inativar(usuarioLogado.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(string id)
        {
            var usuarioLogado = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var usuario = await FindByIdAsync(id);

            usuario.Ativar(usuarioLogado.Id);
            await _context.SaveChangesAsync();
        }
    }
}

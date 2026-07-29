using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class UsuarioService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioService(GerenciamentoPlantaoContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<Usuario?> FindByIdAsync(string id)
        {
            return await _context.Users
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(UsuarioFormViewModel vm)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
            {
                throw new Exception("Departamento não encontrado.");
            }

            var usuario = new Usuario
            {
                DescNome = vm.DescNome,
                UserName = vm.NmUsuario,
                DepartamentoId = vm.DepartamentoId,
                Email = vm.Email,
                PhoneNumber = vm.Telefone,
                Plantonista = vm.Plantonista,
                Ativo = true,
                Perfil = vm.Perfil
            };

            var resultado = await _userManager.CreateAsync(usuario, vm.Senha);

            if(!resultado.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, resultado.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(usuario, usuario.Perfil.ToString());
        }

        public async Task UpdateAsync(EditarUsuarioViewModel vm)
        {
            var usuario = await FindByIdAsync(vm.Id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.DescNome = vm.DescNome;
            usuario.UserName = vm.NmUsuario;
            usuario.Email = vm.Email;
            usuario.PhoneNumber = vm.Telefone;
            usuario.Plantonista = vm.Plantonista;
            usuario.Perfil = vm.Perfil;
            usuario.DepartamentoId = vm.DepartamentoId;

            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(string id)
        {
            var usuario = await _context.Users.FindAsync(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Inativar();
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(string id)
        {
            var usuario = await _context.Users.FindAsync(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Ativar();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(string id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }
    }
}

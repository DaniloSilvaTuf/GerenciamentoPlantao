using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class UsuarioService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public UsuarioService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> FindAllAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Departamento)
                .OrderBy(x => x.DescNome)
                .ToListAsync();
        }

        public async Task<List<Usuario>> FindAllActiveAsync()
        {
            return await _context.Usuarios.Where(e => e.Ativo).OrderBy(e => e.DescNome).ToListAsync();
        }

        public async Task<Usuario?> FindByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(UsuarioFormViewModel vm)
        {
            var departamento = await _context.Departamentos
                .Include(d => d.Usuarios)
                .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
            {
                throw new Exception("Departamento não encontrado.");
            }

            var usuario = new Usuario
            {
                DescNome = vm.DescNome,
                NmUsuario = vm.NmUsuario,
                DepartamentoId = vm.DepartamentoId,
                Email = vm.Email,
                Telefone = vm.Telefone,
                Plantonista = vm.Plantonista,
            };

            usuario.Ativar();
            departamento.AddUsuario(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditarUsuarioViewModel vm)
        {
            var usuario = await FindByIdAsync(vm.Id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.DescNome = vm.DescNome;
            usuario.Email = vm.Email;
            usuario.Telefone = vm.Telefone;
            usuario.Plantonista = vm.Plantonista;
            usuario.Perfil = vm.Perfil;
            usuario.DepartamentoId = vm.DepartamentoId;

            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Inativar();
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Ativar();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(x => x.Id == id);
        }
    }
}

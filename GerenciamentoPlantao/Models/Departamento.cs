namespace GerenciamentoPlantao.Models
{
    public class Departamento : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Canal> Canais { get; set; } = new List<Canal>();
        public ICollection<CategoriaAcionamento> CategoriasAcionamentos { get; set; } = new List<CategoriaAcionamento>();
        public ICollection<Solucao> Solucoes { get; set; } = new List<Solucao>();
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public Departamento(string nome, string usuarioId)
        {
            Nome = nome;
            RegistrarCriacao(usuarioId);
        }

        public void Atualizar(string nome, string usuarioId)
        {
            Nome = nome;
            RegistrarAtualizacao(usuarioId);
        }

        public Departamento() { }

        public void Ativar(string usuarioId)
        {
            RegistrarAtivacao(usuarioId);
        }

        public void Inativar(string usuarioId)
        {
            RegistrarInativacao(usuarioId);
        }

        public void AddCanal(Canal canal)
        {
            if (Canais.Any(c => c.Nome.Equals(canal.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe um setor com esse nome.");
            }

            Canais.Add(canal);
        }

        public void AddCategoria(CategoriaAcionamento categoria)
        {
            if (CategoriasAcionamentos.Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe uma categoria com esse nome.");
            }

            CategoriasAcionamentos.Add(categoria);
        }

        public void AddSolucao(Solucao solucao)
        {
            if (Solucoes.Any(s => s.Nome.Equals(solucao.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe uma solução com esse nome.");
            }

            Solucoes.Add(solucao);
        }
    }
}

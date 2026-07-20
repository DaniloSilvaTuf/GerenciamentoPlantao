namespace GerenciamentoPlantao.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;
        public ICollection<Canal> Canais { get; set; } = new List<Canal>();
        public ICollection<CategoriaAcionamento> CategoriasAcionamentos { get; set; } = new List<CategoriaAcionamento>();
        public ICollection<Solucao> Solucoes { get; set; } = new List<Solucao>();
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public Departamento(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        public Departamento() { }


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
            if (Canais.Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe um setor com esse nome.");
            }

            CategoriasAcionamentos.Add(categoria);
        }

        public void AddSolucao(Solucao solucao)
        {
            if (Canais.Any(s => s.Nome.Equals(solucao.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe um setor com esse nome.");
            }

            Solucoes.Add(solucao);
        }

        public void AddUsuario(Usuario usuario)
        {
            if (Usuarios.Any(u => u.NmUsuario.Equals(usuario.NmUsuario, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe um usuário com esse nome.");
            }
            Usuarios.Add(usuario);
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}

using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Escala;

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
        public ICollection<PostoEscala> PostosEscalas { get; set; } = new List<PostoEscala>();
        public ICollection<BlocoHorarioEscala> BlocosHoarioEscala { get; set; } = new List<BlocoHorarioEscala>();
        public ICollection<DataEspecialEscala> DatasEspeciaisEscalas { get; set; } = new List<DataEspecialEscala>();

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
                throw new BusinessException("Já existe um canal com esse nome.");
            }

            Canais.Add(canal);
        }

        public void AddCategoria(CategoriaAcionamento categoria)
        {
            if (CategoriasAcionamentos.Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("Já existe uma categoria com esse nome.");
            }

            CategoriasAcionamentos.Add(categoria);
        }

        public void AddSolucao(Solucao solucao)
        {
            if (Solucoes.Any(s => s.Nome.Equals(solucao.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("Já existe uma solução com esse nome.");
            }

            Solucoes.Add(solucao);
        }

        public void AddPostoEscala(PostoEscala posto)
        {
            if (PostosEscalas.Any(p => p.Nome.Equals(posto.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("Já existe um posto com esse nome.");
            }

            PostosEscalas.Add(posto);
        }

        public void AddBlocoHorarioEscala(BlocoHorarioEscala bloco)
        {
            if (BlocosHoarioEscala.Any(b => b.Nome.Equals(bloco.Nome, StringComparison.OrdinalIgnoreCase) && b.DiaSemana == bloco.DiaSemana))
            {
                throw new BusinessException("Já existe um bloco com esse nome neste dia da semana.");
            }

            BlocosHoarioEscala.Add(bloco);
        }

        public void AddDataEspecialEscala(DataEspecialEscala dataEspecial)
        {
            if (DatasEspeciaisEscalas.Any(d => d.Data == dataEspecial.Data))
            {
                throw new BusinessException("Já existe uma data especial com essa data.");
            }
            DatasEspeciaisEscalas.Add(dataEspecial);
        }
    }
}

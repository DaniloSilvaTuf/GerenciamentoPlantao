using GerenciamentoPlantao.Data;

namespace GerenciamentoPlantao.Services
{
    public class CanalService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public CanalService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }
    }
}

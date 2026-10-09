using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GerenciamentoPlantao.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum valor)
        {
            var membro = valor.GetType()
                .GetMember(valor.ToString())
                .FirstOrDefault();

            var display = membro?
                .GetCustomAttribute<DisplayAttribute>();

            return display?.Name ?? valor.ToString();
        }
    }
}
using GerenciamentoPlantao.Exceptions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GerenciamentoPlantao.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await TratarExcecaoAsync(context, ex);
            }
        }

        private Task TratarExcecaoAsync(HttpContext context, Exception ex)
        {
            string titulo;
            string mensagem;

            switch (ex)
            {
                case NotFoundException:
                    titulo = "Não encontrado";
                    mensagem = ex.Message;
                    break;

                case AccessDeniedException:
                    titulo = "Acesso negado";
                    mensagem = ex.Message;
                    break;

                case BusinessException:
                    titulo = "Atenção";
                    mensagem = ex.Message;
                    break;

                default:
                    _logger.LogError(ex, "Erro inesperado em {Path}. Usuario: {Usuario}", context.Request.Path, context.User?.Identity?.Name ?? "Não autenticado");
                    titulo = "Erro inesperado";
                    mensagem = "Ocorreu um erro inesperado. Por favor, tente novamente.";
                    break;
            }

            var tempDataFactory = context.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
            var tempData = tempDataFactory.GetTempData(context);

            tempData["ErroTitulo"] = titulo;
            tempData["ErroMensagem"] = mensagem;
            tempData.Save();

            var referer = context.Request.Headers.Referer.ToString();

            if(!string.IsNullOrEmpty(referer))
            {
                context.Response.Redirect(referer);
            }
            else
            {
                context.Response.Redirect("/");
            }

            return Task.CompletedTask;
        }
    }
}

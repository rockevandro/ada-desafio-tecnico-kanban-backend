using Ada.Kanban.Service.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ada.Kanban.Api.Filters
{
    public class LoggingFilter : IResultFilter
    {
        public async void OnResultExecuted(ResultExecutedContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<LoggingFilter>>();
            var httpMethod = context.HttpContext.Request.Method;

            string action;
            if (HttpMethods.IsPut(httpMethod))
            {
                action = "Alterado";
            }
            else if (HttpMethods.IsDelete(httpMethod))
            {
                action = "Removido";
            }
            else
            {
                return;
            }

            var card = await GetCardFromRequestBody(context.HttpContext.Request);
            var cardId = context.HttpContext.Request.RouteValues.GetValueOrDefault("cardId");
            var datetime = DateTime.UtcNow;
            logger.LogInformation("{Datetime} - Card {CardId} - {Titulo} - {Action}", datetime, cardId, card?.Titulo, action);
        }

        private async Task<UpdateCardModel?> GetCardFromRequestBody(HttpRequest request)
        {
            if (!HttpMethods.IsPut(request.Method))
            {
                return null;
            }

            request.Body.Position = 0;
            return await request.ReadFromJsonAsync<UpdateCardModel>();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            return;
        }
    }
}

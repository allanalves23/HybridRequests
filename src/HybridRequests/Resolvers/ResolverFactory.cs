using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HybridRequests.Resolvers
{
    internal static class ResolverFactory
    {
        public static Task Resolve(this ModelBindingContext modelBindingContext)
        {
            return Enum.Parse<HttpMethod>(modelBindingContext.ActionContext.HttpContext.Request.Method) switch 
            {
                HttpMethod.PUT => new PutResolver().Resolve(modelBindingContext),
                _ => throw new InvalidOperationException("Invalid HTTP Method, valid methods are POST, GET, PUT, PATCH and DELETE")
            };
        }
    }
}
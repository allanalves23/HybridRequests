using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HybridRequests.Resolvers
{
    internal interface IResolver
    {
        Task Resolve(ModelBindingContext modelBindingContext);
    }
}
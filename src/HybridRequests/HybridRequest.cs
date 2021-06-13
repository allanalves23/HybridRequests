using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HybridRequests.Resolvers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HybridRequests
{
    public class HybridRequest : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext modelBindingContext) =>
            ResolverFactory.Resolve(modelBindingContext);
    }
}
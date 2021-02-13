using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HybridRequests
{
    public class HybridRequest : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var routeKeys = bindingContext.ActionContext.RouteData.Values.Keys.Where(x => x.ToString() != "action" && x.ToString() != "controller").Select(x => x.ToString()).ToArray();
            var routeDatas = bindingContext.ActionContext.RouteData.Values.Values.Where(x => x.ToString() != "Post").Select(x => x.ToString()).ToArray();

            string body;

            using var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body);
            body = streamReader.ReadToEnd();

            var model = bindingContext.ModelMetadata.UnderlyingOrModelType;
            var modelInstance = JsonSerializer.Deserialize(body, model);

            for (int i = 0; i < routeKeys.Length; i++)
            {

                var key = routeKeys[i];
                var data = routeDatas[i];
                if (!string.IsNullOrWhiteSpace(key))
                {
                    var item = model.GetProperties().FirstOrDefault(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                    if (item is not null)
                    {
                        item.ForceStateValue(modelInstance, data);
                    }
                }
            }

            bindingContext.Result = ModelBindingResult.Success(modelInstance);
            return Task.CompletedTask;
        }
    }
}
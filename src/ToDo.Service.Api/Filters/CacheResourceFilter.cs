using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using ToDo.Service.Api.Extensions;

namespace ToDo.Service.Api.Filters
{
    public class CacheResourceFilter : Attribute, IResourceFilter
    {
        private readonly IDistributedCache _cache;

        public CacheResourceFilter(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var id = context.RouteData.Values["Id"];
            var result = _cache.GetString(id.ToString());

            if (result != null)
            {
                context.Result = new ContentResult() { Content = result };
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var result = (ObjectResult)context.Result;

            if (result.Value != null)
            {
                var key = context.RouteData.Values["Id"];
                _cache.SetString(key.ToString(), JsonConvert.SerializeObject(result.Value), new DistributedCacheEntryOptions().SetExpirationMinutes(1));
            }
        }

    }
}

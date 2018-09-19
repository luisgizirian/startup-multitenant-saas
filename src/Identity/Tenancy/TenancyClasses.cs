using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Identity.Tenancy
{

    public interface ITenantService
    {
        TenantConfiguration GetCurrentTenant();
    }

    public interface ITenantIdentificationService
    {
        TenantConfiguration GetCurrentTenant(HttpContext context);
    }

    public sealed class TenantService : ITenantService
    {
        private readonly HttpContext _httpContext;
        private readonly ITenantIdentificationService _service;

        public TenantService(IHttpContextAccessor accessor, ITenantIdentificationService service)
        {
            this._httpContext = accessor.HttpContext;
            this._service = service;
        }

        public TenantConfiguration GetCurrentTenant()
        {
            return this._service.GetCurrentTenant(this._httpContext);
        }
    }

    public class TenantConfiguration
    {
        public string InstanceName { get; set; }
        public int InstanceTenant { get; set; }

        [Obsolete("In future versions, decouple & allow different implementations")]
        public static List<TenantConfiguration> ListDummyConfigurations()
        {
            return new List<TenantConfiguration> {
                new TenantConfiguration { InstanceName = "default", InstanceTenant = 1000 }
                , new TenantConfiguration { InstanceName = "tenant-1", InstanceTenant = 1001 }
                , new TenantConfiguration { InstanceName = "tenant-2", InstanceTenant = 1002 }
            };
        }
    }

    public class TenantMapping
    {
        public string Default { get; set; }
        public Dictionary<string, string> Tenants { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    }

    public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService
    {

        private readonly List<TenantConfiguration> _tenants;

        public QueryStringTenantIdentificationService()
        {
            this._tenants = TenantConfiguration.ListDummyConfigurations();
        }

        public TenantConfiguration GetCurrentTenant(HttpContext context)
        {
            var query = context.Request.QueryString.Value;

            query = WebUtility.UrlDecode(WebUtility.UrlDecode(query.Substring(query.IndexOf("tenant%253A"))));
            query = query.Substring(0, query.IndexOf('&'));
            var tenant = query.Split(':')[1]?.ToLower() ?? "default";

            if (string.IsNullOrWhiteSpace(tenant) || this._tenants.FirstOrDefault(e => e.InstanceName == tenant) == null)
            {
                return this._tenants[0];
            }

            return this._tenants.First(e => e.InstanceName == tenant.ToLowerInvariant());
        }
    }

}
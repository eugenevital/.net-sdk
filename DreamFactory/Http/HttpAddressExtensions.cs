﻿namespace DreamFactory.Http
{
    using System;
    using System.Collections.Generic;
    using DreamFactory.Model.Database;

    internal static class HttpAddressExtensions
    {
        public static IHttpAddress WithSqlQuery(this IHttpAddress httpAddress, SqlQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (query.Ids != null)
            {
                httpAddress = httpAddress.WithParameter("ids", query.Ids);
            }

            if (query.Filter != null)
            {
                httpAddress = httpAddress.WithParameter("filter", query.Filter);
            }

            if (query.Order != null)
            {
                httpAddress = httpAddress.WithParameter("order", query.Order);
            }

            if (query.Offset.HasValue)
            {
                httpAddress = httpAddress.WithParameter("offset", query.Offset.Value);
            }

            if (query.Limit.HasValue)
            {
                httpAddress = httpAddress.WithParameter("limit", query.Limit.Value);
            }

            if (query.Fields != null)
            {
                httpAddress = httpAddress.WithParameter("fields", query.Fields);
            }

            if (query.Related != null)
            {
                httpAddress = httpAddress.WithParameter("related", query.Related);
            }

            if (query.IncludeCount.HasValue)
            {
                httpAddress = httpAddress.WithParameter("include_count", query.IncludeCount);
            }

            if (query.IncludeSchema.HasValue)
            {
                httpAddress = httpAddress.WithParameter("include_schema", query.IncludeSchema);
            }

            if (query.CustomParameters != null)
            {
                foreach (KeyValuePair<string, object> customParameter in query.CustomParameters)
                {
                    httpAddress = httpAddress.WithParameter(customParameter.Key, customParameter.Value);
                }
            }

            return httpAddress;
        }
    }
}

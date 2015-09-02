﻿namespace DreamFactory.Api.Implementation
{
    using System;
    using System.Threading.Tasks;
    using DreamFactory.Http;
    using DreamFactory.Model.User;

    internal partial class SystemApi
    {
        public async Task<Session> LoginAdminAsync(string applicationName, string applicationApiKey, string email, string password, int duration = 0)
        {
            if (applicationName == null)
            {
                throw new ArgumentNullException("applicationName");
            }

            if (applicationApiKey == null)
            {
                throw new ArgumentNullException("applicationApiKey");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (duration < 0)
            {
                throw new ArgumentOutOfRangeException("duration");
            }

            IHttpAddress address = baseAddress.WithResource("admin", "session");
            baseHeaders.AddOrUpdate(HttpHeaders.FolderNameHeader, applicationName);
            baseHeaders.AddOrUpdate(HttpHeaders.DreamFactoryApiKeyHeader, applicationApiKey);

            Login login = new Login { Email = email, Password = password, Duration = duration };
            string loginContent = contentSerializer.Serialize(login);
            IHttpRequest request = new HttpRequest(HttpMethod.Post,
                                                   address.Build(),
                                                   baseHeaders,
                                                   loginContent);

            IHttpResponse response = await httpFacade.RequestAsync(request);
            HttpUtils.ThrowOnBadStatus(response, contentSerializer);

            Session session = contentSerializer.Deserialize<Session>(response.Body);
            baseHeaders.AddOrUpdate(HttpHeaders.DreamFactorySessionTokenHeader, session.SessionId);

            return session;
        }
        public async Task<Session> GetAdminSessionAsync()
        {
            var address = baseAddress.WithResource("admin", "session");
            IHttpRequest request = new HttpRequest(HttpMethod.Get, address.Build(), baseHeaders);
            IHttpResponse response = await httpFacade.RequestAsync(request);
            HttpUtils.ThrowOnBadStatus(response, contentSerializer);

            return contentSerializer.Deserialize<Session>(response.Body);
        }

        public async Task<bool> LogoutAdminAsync()
        {
            IHttpAddress address = baseAddress.WithResource("admin", "session");
            IHttpRequest request = new HttpRequest(HttpMethod.Delete, address.Build(), baseHeaders);

            IHttpResponse response = await httpFacade.RequestAsync(request);
            HttpUtils.ThrowOnBadStatus(response, contentSerializer);

            baseHeaders.Delete(HttpHeaders.DreamFactorySessionTokenHeader);

            var logout = new { success = false };
            return contentSerializer.Deserialize(response.Body, logout).success;
        }
    }
}

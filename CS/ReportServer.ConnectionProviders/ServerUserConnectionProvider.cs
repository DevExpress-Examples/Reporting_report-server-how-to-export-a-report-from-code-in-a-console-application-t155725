using System;
using System.Threading.Tasks;
using DevExpress.ReportServer.Printing;
using DevExpress.ReportServer.ServiceModel.Client;

namespace DevExpress.Futures.ReportServer.ServiceModel.ConnectionProviders {
    public class ServerUserConnectionProvider : ConnectionProvider {
        readonly string userName;
        readonly string password;

        public ServerUserConnectionProvider(string serverAddress, string userName, string password)
            : base(serverAddress) {
            if(string.IsNullOrEmpty(userName))
                throw new ArgumentException("userName");
            if(string.IsNullOrEmpty(password))
                throw new ArgumentException("password");
            this.userName = userName;
            this.password = password;
        }

        protected override Task<bool> LoginAsync(FormsAuthenticationEndpointBehavior cookieBehavior) {
            return LoginCoreAsync(cookieBehavior, "AuthenticationService.svc", AuthenticationType.Forms, userName, password);
        }
    }
}

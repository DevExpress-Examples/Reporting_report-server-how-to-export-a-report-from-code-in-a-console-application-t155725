using System.Threading.Tasks;
using DevExpress.ReportServer.Printing;
using DevExpress.ReportServer.ServiceModel.Client;

namespace DevExpress.Futures.ReportServer.ServiceModel.ConnectionProviders {
    public class GuestConnectionProvider : ConnectionProvider {
        public GuestConnectionProvider(string serverAddress)
            : base(serverAddress) {
        }

        protected override Task<bool> LoginAsync(FormsAuthenticationEndpointBehavior cookieBehavior) {
            return LoginCoreAsync(cookieBehavior, "AuthenticationService.svc", AuthenticationType.Guest, null, null);
        }
    }
}

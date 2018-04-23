using System.Threading.Tasks;
using DevExpress.ReportServer.Printing;
using DevExpress.ReportServer.ServiceModel.Client;

namespace DevExpress.Futures.ReportServer.ServiceModel.ConnectionProviders {
    public class WindowsUserConnectionProvider : ConnectionProvider {
        public WindowsUserConnectionProvider(string serverAddress)
            : base(serverAddress) {
        }

        protected override Task<bool> LoginAsync(FormsAuthenticationEndpointBehavior cookieBehavior) {
            return LoginCoreAsync(cookieBehavior, "WindowsAuthentication/AuthenticationService.svc", AuthenticationType.Windows, null, null);
        }
    }
}

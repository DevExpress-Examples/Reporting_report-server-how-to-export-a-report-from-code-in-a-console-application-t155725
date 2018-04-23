using System;
using System.Security;
using System.ServiceModel;
using System.Threading.Tasks;
using DevExpress.ReportServer.Printing;
using DevExpress.ReportServer.ServiceModel.Client;

namespace DevExpress.Futures.ReportServer.ServiceModel.ConnectionProviders {
    public abstract class ConnectionProvider {
        readonly Uri serverUri;

        public ConnectionProvider(string serverAddress) {
            serverUri = new Uri(serverAddress, UriKind.Absolute);
        }

        public Task<IReportServerClient> ConnectAsync() {
            FormsAuthenticationEndpointBehavior cookieBehavior = new FormsAuthenticationEndpointBehavior();
            return LoginAsync(cookieBehavior)
                .ContinueWith(task => {
                    if(!task.Result)
                        throw new SecurityException("Invalid user name or password.");
                    return CreateClient(cookieBehavior);
                });
        }

        protected abstract Task<bool> LoginAsync(FormsAuthenticationEndpointBehavior cookieBehavior);

        protected Task<bool> LoginCoreAsync(FormsAuthenticationEndpointBehavior cookieBehavior, string authServiceRelativeAddress, AuthenticationType authType, string userName, string password) {
            EndpointAddress authServiceAddress = GetEndpointAddress(authServiceRelativeAddress);
            AuthenticationServiceClientFactory clientFactory = new AuthenticationServiceClientFactory(authServiceAddress, authType);
            clientFactory.ChannelFactory.Endpoint.Behaviors.Add(cookieBehavior);
            IAuthenticationServiceProxy proxy = new AuthenticationServiceProxy(clientFactory.ChannelFactory.CreateChannel());
            return proxy.Login(userName, password, null);
        }

        protected IReportServerClient CreateClient(FormsAuthenticationEndpointBehavior cookieBehavior) {
            EndpointAddress serviceFacadeAddress = GetEndpointAddress("ReportServerFacade.svc");
            ReportServerClientFactory clientFactory = new ReportServerClientFactory(serviceFacadeAddress);
            clientFactory.ChannelFactory.Endpoint.Behaviors.Add(cookieBehavior);
            IReportServerClient client = clientFactory.Create();
            return client;
        }

        EndpointAddress GetEndpointAddress(string epRelativeAddress) {
            Uri epUri = new Uri(serverUri, epRelativeAddress);
            return new EndpointAddress(epUri);
        }
    }
}

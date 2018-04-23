using System.Threading.Tasks;
using DevExpress.ReportServer.ServiceModel.Client;
using DevExpress.Utils;

namespace DevExpress.Futures.ReportServer.ServiceModel.ConnectionProviders {
    interface IAuthenticationServiceProxy {
        Task<bool> Login(string userName, string password, object asyncState);
    }

    class AuthenticationServiceProxy : IAuthenticationServiceProxy {
        readonly IAuthenticationServiceAsync channel;

        public AuthenticationServiceProxy(IAuthenticationServiceAsync channel) {
            Guard.ArgumentNotNull(channel, "channel");
            this.channel = channel;
        }

        public Task<bool> Login(string userName, string password, object asyncState) {
            return Task.Factory.FromAsync<string, string, bool>(channel.BeginLogin, channel.EndLogin, userName, password, asyncState);
        }
    }
}

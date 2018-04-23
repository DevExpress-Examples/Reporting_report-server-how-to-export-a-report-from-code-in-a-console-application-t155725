using System.IO;
using System.Threading.Tasks;
using DevExpress.DocumentServices.ServiceModel;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
using DevExpress.ReportServer.ServiceModel.Client;
using DevExpress.ReportServer.ServiceModel.ConnectionProviders;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.XtraPrinting;

namespace T155725 {
    class Program {
        static void Main(string[] args) {
            const string serverAddress = "https://reportserver.devexpress.com/";
            const string targetFileName = @"c:\temp\CustomerOrderHistory.pdf";
            const int reportId = 1113;
            ReportParameter parameter = new ReportParameter() { Name = "@CustomerID", Path = "@CustomerID", Value = "ALFKI" };

            ExportToPdf(serverAddress, targetFileName, reportId, parameter);
        }

        static void ExportToPdf(string serverAddress, string fileName, int reportId, params ReportParameter[] parameters) {
            ServiceOperationBase.DelayerFactory = new ThreadingTimerDelayerFactory();

            IReportServerClient client =
                new GuestConnectionProvider(serverAddress).ConnectAsync().Result;
            //new ServerUserConnectionProvider(serverAddress, "demo", "demo").ConnectAsync().Result;
            //new WindowsUserConnectionProvider(serverAddress).ConnectAsync().Result;

            Task<byte[]> exportTask = Task.Factory.ExportReportAsync(client, new ReportIdentity(reportId), new PdfExportOptions(), parameters, null);
            File.WriteAllBytes(fileName, exportTask.Result);
        }
    }
}

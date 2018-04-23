Imports System.IO
Imports System.Threading.Tasks
Imports DevExpress.DocumentServices.ServiceModel
Imports DevExpress.DocumentServices.ServiceModel.DataContracts
Imports DevExpress.ReportServer.ServiceModel.Client
Imports DevExpress.ReportServer.ServiceModel.ConnectionProviders
Imports DevExpress.ReportServer.ServiceModel.DataContracts
Imports DevExpress.XtraPrinting

Namespace T155725
    Friend Class Program
        Shared Sub Main(ByVal args() As String)
            Const serverAddress As String = "https://reportserver.devexpress.com/"
            Const targetFileName As String = "c:\temp\CustomerOrderHistory.pdf"
            Const reportId As Integer = 1113
            Dim parameter As New ReportParameter() With {.Name = "@CustomerID", .Path = "Northwind.CustOrderHist.@CustomerID", .Value = "ALFKI"}

            ExportToPdf(serverAddress, targetFileName, reportId, parameter)
        End Sub

        Private Shared Sub ExportToPdf(ByVal serverAddress As String, ByVal fileName As String, ByVal reportId As Integer, ParamArray ByVal parameters() As ReportParameter)
            Dim client As IReportServerClient = (New GuestConnectionProvider(serverAddress)).ConnectAsync().Result
            'new ServerUserConnectionProvider(serverAddress, "demo", "demo").ConnectAsync().Result;
            'new WindowsUserConnectionProvider(serverAddress).ConnectAsync().Result;

            Dim exportTask As Task(Of Byte()) = Task.Factory.ExportReportAsync(client, New ReportIdentity(reportId), New PdfExportOptions(), parameters, Nothing)
            File.WriteAllBytes(fileName, exportTask.Result)
        End Sub
    End Class
End Namespace

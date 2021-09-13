using System;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
//using Microsoft.WindowsAzure;
using System.Web;

namespace ServicioLocalContract
{
    public static class NtLinkClientFactory
    {
        public static IServicioLocal Cliente()
        {
            string uri = ConfigurationManager.AppSettings["ServicioLocal"];

            //uri = uri.Replace("#DeploymentID#", HttpContext.Current.Request.Url.Host);

            XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
            readerQuotas.MaxDepth = 32;
            readerQuotas.MaxStringContentLength = Int32.MaxValue;
            readerQuotas.MaxArrayLength = int.MaxValue;
            readerQuotas.MaxBytesPerRead = Int32.MaxValue;
            readerQuotas.MaxNameTableCharCount = Int32.MaxValue;

            WSHttpBinding httpbind = new WSHttpBinding();
            //httpbind.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            httpbind.Security.Mode = SecurityMode.None;
            httpbind.ReaderQuotas = readerQuotas;

            //httpbind.ReaderQuotas = readerQuotas;
            httpbind.ReceiveTimeout = TimeSpan.MaxValue;
            httpbind.SendTimeout = TimeSpan.MaxValue;
            httpbind.CloseTimeout = TimeSpan.MaxValue;
            httpbind.MaxReceivedMessageSize = Int32.MaxValue;
            //
            httpbind.MaxBufferPoolSize = Int32.MaxValue;
            httpbind.TransactionFlow = false;




            //NetTcpBinding tcpBinding = new NetTcpBinding();
            //tcpBinding.TransactionFlow = false;
            //tcpBinding.Security.Transport.ProtectionLevel =
            //    ProtectionLevel.EncryptAndSign;
            //tcpBinding.Security.Transport.ClientCredentialType =
            //    TcpClientCredentialType.Windows;
            //tcpBinding.Security.Mode = SecurityMode.None;
            //tcpBinding.ReaderQuotas = readerQuotas;
            //tcpBinding.ReceiveTimeout = TimeSpan.MaxValue;
            //tcpBinding.SendTimeout = TimeSpan.MaxValue;
            //tcpBinding.CloseTimeout = TimeSpan.MaxValue;
            //tcpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            //tcpBinding.MaxBufferSize = Int32.MaxValue;
            
            
                        
            EndpointAddress address = new EndpointAddress(uri);
            //ChannelFactory<IServicioLocal> factory = new ChannelFactory<IServicioLocal>(tcpBinding, address);
            ChannelFactory<IServicioLocal> factory = new ChannelFactory<IServicioLocal>(httpbind, address);
            
            foreach (OperationDescription op in factory.Endpoint.Contract.Operations)
            {
                var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            IServicioLocal cliente = factory.CreateChannel();
           
            return cliente;
        }
    }
}

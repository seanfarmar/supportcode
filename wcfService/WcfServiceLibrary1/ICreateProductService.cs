namespace WcfServiceLibrary1
{
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using Messages;

    [ServiceContract]
    public interface ICreateProductService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "products", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        int Create(CreateProductMessage message);
    }
}
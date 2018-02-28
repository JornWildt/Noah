using log4net;
using Microsoft.AspNet.SignalR.Hubs;

namespace Noah.Service.Hubs
{
  public class ErrorHandlingPipelineModule : HubPipelineModule
  {
    static ILog Logger = LogManager.GetLogger(typeof(ErrorHandlingPipelineModule));


    protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
    {
      Logger.Error(exceptionContext.Error);
      if (exceptionContext.Error.InnerException != null)
      {
        Logger.Error("=> Inner Exception " + exceptionContext.Error.InnerException.Message);
      }
      base.OnIncomingError(exceptionContext, invokerContext);
    }
  }
}

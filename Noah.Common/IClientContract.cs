using System;
using System.Threading.Tasks;

namespace Noah.Common
{
  public class NewMessageArgs
  {
    public DateTime Timestamp { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
  }

  public interface IClientContract
  {
    void NewMessage(NewMessageArgs args);
  }
}

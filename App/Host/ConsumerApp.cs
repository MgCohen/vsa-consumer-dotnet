namespace Consumer.App.Host;

using Umpire.Vsa;
using Umpire.Vsa.Hosts.Web;
using Umpire.Kernel;

public class ConsumerApp : App<Vsa<WebVsaHost>>;

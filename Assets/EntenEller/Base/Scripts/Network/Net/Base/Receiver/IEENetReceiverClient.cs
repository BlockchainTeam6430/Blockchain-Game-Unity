using System.ComponentModel;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IEENetReceiverClient
    {
        void ReceiveAsClient(object data);
    }
}
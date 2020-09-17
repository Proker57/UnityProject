using UnityEngine;

namespace NoSuchStudio.Common.Service
{
    /// <summary>
    /// Should be implemented by classes that are part of a Service.
    /// </summary>
    /// <remarks>
    /// Any class can access services for one time use. By implementing this
    /// interface, you make it explicit that the class requires the service
    /// to function properly.
    /// Your class will also have its <see cref="Connect{ST}"/> method called
    /// by the Service when there is a change in the service, i.e. when the
    /// service is loading initially or the global instance is changing.
    /// </remarks>
    /// <typeparam name="T">Type of the <see cref="Common.Service"/> the class is a component of.</typeparam>
    public interface IServiceComponent<T> where T : Service<T>
    {
        MonoBehaviour mono
        {
            get;
        }
        bool IsConnected<ST>() where ST : T;
        void Connect<ST>() where ST : T;
        void Disconnect<ST>() where ST : T;
    }
}

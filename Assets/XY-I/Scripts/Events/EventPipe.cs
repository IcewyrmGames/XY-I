using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventPipe<E, T> : MonoBehaviour
	where E : UnityEvent<T>, new()
{
	public E onEventReceived = new E();

	public void SendEvent( T f )
	{
		onEventReceived.Invoke( f );
	}
}

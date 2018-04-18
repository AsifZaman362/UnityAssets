using UnityEngine;


public class MessageClient : MonoBehaviour
{

    public MessageBus bus;

    protected virtual void Start()
    {
        Debug.Log("Called");
        bus = GameObject.FindObjectOfType<MessageBus>();
    }

    public virtual void OnMessageRecieve(string message)
    {

    }
}


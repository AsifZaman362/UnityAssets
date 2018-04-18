using System.Collections;
using UnityEngine;


public class MessageBus : MonoBehaviour
{

    public ArrayList clientScripts;

    void Start()
    {
        clientScripts = new ArrayList();
        MessageClient[] clients = GameObject.FindObjectsOfType<MessageClient>();
        foreach(MessageClient client in clients)
        {
            clientScripts.Add(client);
        }
    }

    public new void SendMessage(string message)
    {
        foreach(MessageClient client in clientScripts)
        {
            client.OnMessageRecieve(message);
        }
    }

}


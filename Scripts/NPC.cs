using UnityEngine;


public class NPC : MonoBehaviour{

    public string _name;
    /// <summary>
    /// The types of NPCs are : "Humans" , "Demons", "Overlords" 
    /// </summary>
    public string NPC_type;
    /// <summary>
    /// The behaviours are represented as 0 : Peaceful (They are scared easily), 1 : Hostile (They attack without a reason)
    /// </summary>
    public int behaviour;

    void Start(){

    }

    void Update(){

    }
}
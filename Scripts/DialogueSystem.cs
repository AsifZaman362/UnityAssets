using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;



[System.Serializable]
public struct Node{

    [Multiline]
    public string text;
    public string[] options;
    public int[] targetNodeIndices;
    public int thisIndex;
    public UnityEvent action;

}

[DisallowMultipleComponent]
public class DialogueSystem : MessageClient{

    [Header("UI Objects")]
    [SerializeField] private bool isUsingButtons = false;
    [SerializeField] private GameObject dialogueBoxObject;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TextMeshProUGUI[] dialogueReplyOptions;
    [SerializeField] private TextMeshProUGUI name_text;

    [Header("Dialogues")]
    public Node[] dialogues;

    [Header("Character")]
    public string Name;

    private Node currentNode;
    
    void Start(){
        base.Start();
        if(dialogues.Length>0){
            currentNode = dialogues[0];
            name_text.text = Name;
            dialogueBoxObject.SetActive(true);
            StartCoroutine(UpdateInterface());
            StartCoroutine(ChoseOption());
        }
    }

    IEnumerator UpdateInterface(bool isLast = false){
        

        base.bus.SendMessage("stopController");
        if (isUsingButtons)
        {
            foreach (Button button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (TextMeshProUGUI reply in dialogueReplyOptions)
            {
                reply.gameObject.SetActive(false);
            }
        }

        int i;
        char[] textArray = currentNode.text.ToCharArray();

        for(i=0;i<textArray.Length;i++){

            char c = textArray[i];

            if(c=='#'){
                i++;
                string tag = "";
                while(textArray[i]!='#'){
                    tag += textArray[i];
                    i++;
                }
                i++;
                dialogueBox.text += tag;
            }

            if(i>=textArray.Length)
                break;
            
            c = textArray[i];

            dialogueBox.text += c;
            WaitForSeconds waitTime = new WaitForSeconds( (c!='\n' ? 0.05f : 0.2f) ) ;
            yield return waitTime;
            
        }
        yield return new WaitForSeconds(0.5f);

        for (i = 0; i < currentNode.options.Length; i++)
        {
            if (isUsingButtons)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.options[i];
            }
            else
            {
                dialogueReplyOptions[i].gameObject.SetActive(true);
                dialogueReplyOptions[i].text = currentNode.options[i];
            }
        }

        if (isLast)
        {
            yield return new WaitForSeconds(0.5f);
            dialogueBoxObject.SetActive(false);
            base.bus.SendMessage("startController");
            this.enabled = false;
        }
        
    }

    public void selectNodeAt(int buttonIndex){
        Debug.Log(dialogues[currentNode.targetNodeIndices[buttonIndex]].text);
        dialogueBox.text = "";
        bool move_on = false;
        currentNode = dialogues[currentNode.targetNodeIndices[buttonIndex]];
        if(currentNode.targetNodeIndices.Length>0){
            currentNode.action.Invoke();
            move_on = true;
        }
        if(move_on){
            StartCoroutine(UpdateInterface());
            StartCoroutine(ChoseOption());
        }
        else
        {
            Debug.Log("Exit");
            if (isUsingButtons)
            {
                foreach (Button button in optionButtons)
                {
                    button.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (TextMeshProUGUI option in dialogueReplyOptions)
                {
                    option.gameObject.SetActive(false);
                }
            }

            StartCoroutine(UpdateInterface(true));

        }
    }

    IEnumerator ChoseOption()
    {
        bool isDone = false;
        int selection = 1;
        while (!isDone)
        {
            yield return null;
            for (int i = 0; i < dialogueReplyOptions.Length; i++)
            {
                if (i == selection - 1)
                {
                    dialogueReplyOptions[i].color = Color.white;
                }
                else
                {
                    dialogueReplyOptions[i].color = Color.grey;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selection = selection == 1 ? currentNode.options.Length : selection-1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selection = selection == currentNode.options.Length ? 1 : selection+1;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                selectNodeAt(selection - 1);
                isDone = true;
                break;
                
            }
            
        }
        yield return null;
    }

}
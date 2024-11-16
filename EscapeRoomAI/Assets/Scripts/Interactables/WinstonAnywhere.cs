/*
Josh, Wrote the class
Keoki, made the dialogue work with async and the LLM and Game master
*/

/*
Josh, Wrote the class
Keoki, made the dialogue work with async and the LLM and Game master
*/

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinstonAnywhere : MonoBehaviour, Dialoguer
{
    private GameObject player;
    private GameObject player;
    public Sprite dialogueIcon;
    private InputManager playerIM;
    private InputManager playerIM;
    private GameObject ChatObject;
    private GameObject GameMasterObject;
    private GameObject GameMasterObject;
    private LLMHandler LLM;
    private GameMaster GM;

    [Header("Trigger Settings")]
    [SerializeField] private KeyCode triggerKey = KeyCode.T; // Key to trigger Winston dialogue

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerIM = player?.GetComponent<InputManager>();
        playerIM = player?.GetComponent<InputManager>();

        ChatObject = GameObject.FindGameObjectWithTag("LLMObj");
        LLM = ChatObject?.GetComponent<LLMHandler>();

        LLM = ChatObject?.GetComponent<LLMHandler>();

        GameMasterObject = GameObject.FindGameObjectWithTag("GameMasterObj");
        GM = GameMasterObject?.GetComponent<GameMaster>();
        GM = GameMasterObject?.GetComponent<GameMaster>();
    }

    void Update()
    {
        // Trigger dialogue on key press
        if (Input.GetKeyDown(triggerKey))
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        if (playerIM != null && playerIM.playerCanMove)
        {
            playerIM.playerCanMove = false;
            Dialogue.OpenDialogue(this);
        }
    }

    public async Task<List<DialogueItem>> getDialogue()
    {
        string gameContext = GM?.GenerateGameContext() ?? "Default game context";
        // add in 
        string winstonMessage = await (LLM?.SendMessageToWinston("This is the current Game state:  " +gameContext) ?? Task.FromResult("Error retrieving message from LLM."));
        UnityEngine.Debug.Log("Winston: " + winstonMessage);
        return new List<DialogueItem>()
        {
            new DialogueItem() { name = "Prof. Winston", picture = dialogueIcon },
            new DialogueItem() { text = "Hello! My name is Professor Winston" },
            new DialogueItem() { text = "What can I help you with today?" },
            new DialogueItem() { text = winstonMessage },
            new DialogueItem() { action = () => { playerIM.playerCanMove = true; } }
        };
    }
}

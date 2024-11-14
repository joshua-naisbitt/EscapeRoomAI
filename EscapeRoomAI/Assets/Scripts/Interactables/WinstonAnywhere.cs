/*
Josh, Wrote the class
Keoki, made the dialogue work with async and the LLM and Game master
*/

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinstonAnywhere : Interactable, Dialoguer
{
    private GameObject player;
    public Sprite dialogueIcon;
    private InputManager playerIM;
    private GameObject ChatObject;
    private GameObject GameMasterObject;
    private LLMHandler LLM;
    private GameMaster GM;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerIM = player?.GetComponent<InputManager>();

        ChatObject = GameObject.FindGameObjectWithTag("LLMObj");
        LLM = ChatObject?.GetComponent<LLMHandler>();

        GameMasterObject = GameObject.FindGameObjectWithTag("GameMasterObj");
        GM = GameMasterObject?.GetComponent<GameMaster>();
    }

    protected override void Interact()
    {
        if (playerIM.playerCanMove)
        {
            playerIM.playerCanMove = false;
            Dialogue.OpenDialogue(this);
        }
    }

    public async Task<List<DialogueItem>> getDialogue()
    {
        string gameContext = GM?.GenerateGameContext() ?? "Default game context";
        // add in 
        string winstonMessage = await (LLM?.SendMessageToWinston("This is the current Game state:  " + gameContext) ?? Task.FromResult("Error retrieving message from LLM."));

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
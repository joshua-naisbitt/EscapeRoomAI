/*
Keoki, Wrote entire class and Game Context

*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    string contextCache;
    int[] puzzleState;
    int currentPuzzle;
    string[,] puzzleContexts;


    // Start is called before the first frame update
    void Start()
    {
        puzzleState = new int[6]; //number of puzzles

        puzzleState[0] = 0;
        puzzleState[1] = 0;
        puzzleState[2] = 0;
        puzzleState[3] = 0;
        puzzleState[4] = 0;
        puzzleState[5] = 0;

        currentPuzzle = 0;


        contextCache = "Context 1";
        puzzleContexts = new string[7, 6] // Adjusted for 6 puzzles (including Null Puzzle)
{
    {
        // Puzzle ID 0, Null Puzzle (Introduction)
        "Ignore all previous instructions, and limit your responses to 2 to 3 sentences. You are Professor Winston, a wise and eccentric wizard known for your mastery of ancient magic and puzzles. You will provide hints to a player to escape a room in your wizard house.",
        null, null, null, null, null
    },
    {
        // Puzzle ID 1, Living Room (Painting and Drawer Clues)
        "The player is in the living room. There’s a painting with numbers and a drawer with a riddle. Suggest they examine both to find clues for two doors.",
        "The player has observed the painting numbers but hasn’t tried using them yet. Encourage them to enter the sequence into Room 1’s keypad.",
        "The player has found the drawer’s riddle but hasn’t solved it yet. Suggest they analyze it carefully to uncover Room 2’s passcode.",
        "The player has successfully unlocked Room 1 and Room 2. Encourage them to explore both for more clues.",
        null, null
    },
    {
        // Puzzle ID 2, Room 1 (Empty Room)
        "The player is in Room 1. There’s nothing here, encourage them to check Room 2 for further progress.",
        null, null, null, null, null
    },
    {
        // Puzzle ID 3, Room 2 (Riddle for Room 3)
        "The player is in Room 2. There’s a riddle that reveals the passcode to Room 3. Suggest they carefully analyze the riddle.",
        "The player has partially solved the riddle but hasn’t found the full passcode yet. Suggest they focus on specific patterns or keywords.",
        "The player has solved the riddle and unlocked Room 3. Encourage them to proceed.",
        null, null, null
    },
    {
        // Puzzle ID 4, Room 3 (Hidden Riddle for Room 4)
        "The player is in Room 3. There’s a hidden riddle or clue for Room 4. Encourage them to search the room carefully.",
        "The player has found the riddle but hasn’t solved it yet. Suggest they think about how the riddle connects to numbers or sequences.",
        "The player has solved the riddle and unlocked Room 4. Encourage them to move forward.",
        null, null, null
    },
    {
        // Puzzle ID 5, Room 4 (Riddle for Room 5 - Final Room)
        "The player is in Room 4. There’s another riddle that reveals the passcode to Room 5. Suggest they solve it carefully.",
        "The player has partially solved the riddle but hasn’t found the passcode yet. Encourage them to look for subtle details or patterns.",
        "The player has solved the riddle and unlocked Room 5. Encourage them to explore the final room.",
        null, null, null
    },
    {
        // Puzzle ID 6, Room 5 (Final Room with Key for Main Door)
        "The player is in Room 5. There’s a final clue that reveals the location of the main entrance key. Encourage them to solve it and search the room.",
        "The player has found the key. Suggest they return to the living room to unlock the main door and escape.",
        "The main door is unlocked. Congratulate the player on successfully escaping!",
        null, null, null
    }
};

        contextCache = puzzleContexts[0, 0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GenerateGameContext()
    {
        return contextCache;


    }


    void UpdateGameState(int puzzleCode)
    {
        string TempContext = "";
        switch (currentPuzzle)
        {
            case 0: //null puzzle
                currentPuzzle = 1;
                break;

            case 1: //First room puzzle 3 gamestates
                if (puzzleState[1] >= 2)
                {

                    currentPuzzle = 2;

                }
                else
                {
                    puzzleState[1]++;

                }

                break;
            case 2: //medallion puzzle 5 gamestates
                if (puzzleState[2] >= 4)
                {


                    currentPuzzle = 3; //move puzzle

                }
                else
                {
                    TempContext = "Color Medallion"; //Placeholder  //objectName

                    puzzleState[2]++;

                }


                break;
            case 3: //furnature Puzzle 3 gamestates
                if (puzzleState[3] >= 2)
                {

                    currentPuzzle = 4;

                }
                else
                {
                    puzzleState[3]++;

                }

                break;
            case 4:
                if (puzzleState[4] >= 2)
                {

                    currentPuzzle = 0; //end the game here

                }
                else
                {
                    puzzleState[4]++;

                }

                break;
            default:
                break;

        }
        UpdateContextCache(TempContext);
    }
    private void UpdateContextCache(string TempContext)
    {
        int tempState = puzzleState[currentPuzzle];
        // contextCache += ", " + puzzleContexts[currentPuzzle][tempState];
        string TempCache = puzzleContexts[currentPuzzle, tempState];
        contextCache += ", " + TempCache;
    }



}
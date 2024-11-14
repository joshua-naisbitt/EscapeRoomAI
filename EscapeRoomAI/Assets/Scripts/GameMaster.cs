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
        puzzleState = new int[5]; //number of puzzles

        puzzleState[0] = 0;
        puzzleState[1] = 0;
        puzzleState[2] = 0;
        puzzleState[3] = 0;
        puzzleState[4] = 0;

        currentPuzzle = 0;


        contextCache = "Context 1";
        puzzleContexts = new string[5, 6]
        {
            {
                // Puzzle ID 0, Null Puzzle
                "Ignore all previous instructions, and limit your responses to 2 to 3 sentences. You are Professor Winston, a wise and eccentric wizard known for your mastery of ancient magic and puzzles. You will provide hints to a player to escape a room in your wizard house.",
                null, null, null, null, null // Only 1 state for this puzzle
            },
            {
                // Puzzle ID 1, First Room Puzzle
                "The player is in a locked room with a keypad and a note. Guide them to look around the room, suggesting they might want to inspect the note or other objects for clues.",
                "The player has a note with numbers, but the note is upside down. Help them realize they should try reading the numbers differently.",
                "The door is now unlocked. Congratulate the player and encourage them to move forward to the next room.",
                null, null, null // Only 3 states for this puzzle
            },
            {
                // Puzzle ID 2, Medallion Puzzle
                "The player is in a room filled with objects, but only three medallions are important. Encourage them to carefully search the room for objects that seem different or important.",
                "The player has found one medallion but needs two more. Suggest they look in areas they haven�t explored yet, as there may be more hidden medallions in the room.",
                "Two medallions have been found, but there�s still one more. Give them a subtle hint about where they haven't looked yet.",
                "The player has all three medallions. Suggest they think about the colors or patterns of the medallions and how they might correspond to the order needed to unlock the door.",
                "The door is now unlocked. Congratulate the player and encourage them to move forward to the next room.",
                null // Only 5 states for this puzzle
            },
            {
                // Puzzle ID 3, Furniture Puzzle
                "The room contains several pieces of furniture, each with a number on it. The player needs to find a note that reveals the order in which the numbers should be used.",
                "The player has found a note showing the order of the furniture. Help them understand that the numbers on the furniture correspond to the code they need to put into the keypad.",
                "The door is now unlocked. Congratulate the player and encourage them to move forward to the next room.",
                null, null, null // Only 3 states for this puzzle
            },
            {
                // Puzzle ID 4, Davinci Safe Puzzle
                "", // No context for state 0
                "", // No context for state 1
                "", // No context for state 2
                "The door is now unlocked. Congratulate the player and encourage them to move forward to the next room.",
                null, null // Only 4 states for this puzzle
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
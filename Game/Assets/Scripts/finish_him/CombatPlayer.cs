using UnityEngine;
using System.Collections;


public class CombatPlayer : MonoBehaviour {

    public int playerNumber = 1;
    public string InputPlayerString;

    public int commandIndex = 0;
    public bool releasedButton = true;

	// Use this for initialization
	void Start () {
        commandIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
        /*
         *  HANDLING MULTIPLAYER
         */
        if (playerNumber == 1)
        {
            InputPlayerString = "_P1";
        }
        if (playerNumber == 2)
        {
            InputPlayerString = "_P2";
        }


        /*
         *  COMMAND PART
         */
        bool goodCommand = false;
        bool commandPressed = false;

        float inputY = Input.GetAxisRaw("Vertical" + InputPlayerString);
        float inputX = Input.GetAxisRaw("Horizontal" + InputPlayerString);
        bool triggerAction = false;
        triggerAction = Input.GetButton("Action" + InputPlayerString);



        if (releasedButton)
        {
            if (inputY > 0.5 && inputX == 0 && !triggerAction)
            {
                // UP
                commandPressed = true;
                goodCommand = checkForGoodCommand('U');
            }
            else if (inputY < -0.5 && inputX == 0 && !triggerAction)
            {
                // DOWN
                commandPressed = true;
                goodCommand = checkForGoodCommand('D');
            }
            else if (inputX > 0.5 && inputY == 0 && !triggerAction)
            {
                // RIGHT
                commandPressed = true;
                goodCommand = checkForGoodCommand('R');
            }
            else if (inputX < -0.5 && inputY == 0 && !triggerAction)
            {
                // LEFT
                commandPressed = true;
                goodCommand = checkForGoodCommand('L');
            }
            else if (triggerAction)
            {
                // ACTION
                commandPressed = true;
                goodCommand = checkForGoodCommand('A');
            }
        }
        else if (!triggerAction && inputX == 0 && inputY == 0)
            releasedButton = true;



        if (commandPressed)
        {
            releasedButton = false;

            // CHANGE IN UI
            FinishHimManager.Instance.uiCreator.CommandUINoticed(goodCommand, commandIndex);


            if (goodCommand)
            {
                commandIndex++;

                if (commandIndex == FinishHimManager.Instance.commandToDo.Length)
                    GameManager.Instance.LevelEnd(true);

            }
            else
                GameManager.Instance.LevelEnd(false);
        }
	}

    bool checkForGoodCommand(char command)
    {
        if (FinishHimManager.Instance.commandToDo[commandIndex] == command)
            return true;
        else
            return false;

    }
}

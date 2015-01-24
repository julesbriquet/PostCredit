using UnityEngine;
using System.Collections;

public class FinishHimManager : MonoBehaviour {

    public static FinishHimManager Instance;

    /*
     * COMMAND:
     * 1 = UP
     * 2 = DOWN
     * 3 = LEFT
     * 4 = RIGHT
     * 
     * So LEFT DOWN RIGHT UP will be: 3241
     */

    public string commandToDo = "";

    public CommandUICreator uiCreator;

    void Awake()
    {
        if (null == FinishHimManager.Instance)
        {
            FinishHimManager.Instance = this;
        }
        else if (this != FinishHimManager.Instance)
        {
            FinishHimManager.Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {

        commandToDo = "";

        // Level 1 & 2
        if (GameManager.Instance.LevelDifficulty < 3)
        {
            commandToDo += generateStringCommand(GameManager.Instance.LevelDifficulty * 2);
        }
        else if (GameManager.Instance.LevelDifficulty == 3)
        {
            commandToDo += generateStringCommand(4);
            commandToDo += generateStringCommand(2);
        }

        uiCreator.GenerateUICommand(commandToDo);
	}
	
	// Update is called once per frame
	void Update () {

	}


    string generateStringCommand(int numberOfDirection)
    {
        string returnedCommand = "";

        for (int i = 0; i < numberOfDirection; i++)
        {
            int rand = Random.Range(1, 4);
            returnedCommand += numberToCharCommand(rand);
        }

        // Adding action command
        returnedCommand += 'A';

        return returnedCommand;
    }

    char numberToCharCommand(int commandNumber)
    {
        if (commandNumber == 1)
            return 'U';
        else if (commandNumber == 2)
            return 'D';
        else if (commandNumber == 3)
            return 'L';
        else if (commandNumber == 4)
            return 'R';

        return 'B';
    }
}

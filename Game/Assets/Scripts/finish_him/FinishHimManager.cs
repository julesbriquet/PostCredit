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


    public float[] timerForDifficulty;
    public float timeUntilEndGame;

    public Transform background;

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

        ResizeBackGround();

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

        timeUntilEndGame = timerForDifficulty[GameManager.Instance.LevelDifficulty - 1];
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilEndGame -= Time.deltaTime;

        if (timeUntilEndGame <= 0)
            GameManager.Instance.LevelEnd(false);
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


    void ResizeBackGround()
    {
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        background.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = background.localScale;
        xWidth.x = worldScreenWidth / width;
        background.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = background.localScale;
        yHeight.y = worldScreenHeight / height;
        background.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }
}

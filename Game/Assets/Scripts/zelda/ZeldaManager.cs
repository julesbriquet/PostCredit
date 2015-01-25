using UnityEngine;
using System.Collections;

public class ZeldaManager : MonoBehaviour {

    public int numberOfPlant = 0;

    public float timeUntilEndGame;

    public static ZeldaManager Instance;

	// Use this for initialization
	void Start () {

        ZeldaManager.Instance = this;

        numberOfPlant = GameObject.FindGameObjectsWithTag("ZeldaPlant").Length;

        TimerManager.Instance.StartTimer(timeUntilEndGame);
	}


    // Update is called once per frame
    void Update()
    {
        timeUntilEndGame -= Time.deltaTime;

        if (timeUntilEndGame <= 0)
            GameManager.Instance.LevelEnd(false);

        if (numberOfPlant <= 0)
        {
            GameManager.Instance.LevelEnd(true);
        }
    }
}

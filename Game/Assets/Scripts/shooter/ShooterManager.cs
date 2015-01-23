using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour {

    public EnnemyEntity[] ennemyList;

	// Use this for initialization
	void Start () {
        if (GameManager.Instance.LevelDifficulty == 1)
        {
        }
        else if (GameManager.Instance.LevelDifficulty == 2)
        {
        }
        else if (GameManager.Instance.LevelDifficulty == 3)
        {
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

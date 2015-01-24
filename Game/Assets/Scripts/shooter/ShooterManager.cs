using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour {

    public EnnemyEntity[] ennemyList;
    public Transform SpawnPoint;

	// Use this for initialization
	void Start () {
        
        Instantiate(ennemyList[GameManager.Instance.LevelDifficulty - 1], SpawnPoint.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

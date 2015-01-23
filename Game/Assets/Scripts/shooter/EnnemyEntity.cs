using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyEntity : MonoBehaviour {

    public int hitPointNumber;
    public List<GunEntity> gun;

    public int gunIndex = 0;

    public float secondsBetweenShoots = 0.5f;
    private float nextPossibleShoot = 0f;

    public float secondsBeforeSwitchGun = 1f;
    private float nextSwitchGunTime = 0f;

	// Use this for initialization
	void Start () {
        gun = new List<GunEntity>(this.GetComponentsInChildren<GunEntity>());
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManager.Instance.LevelDifficulty == 3 && gun.Count > 0)
        {
            // DELAY BETWEEN SHOOT
            if (gunIndex < gun.Count && nextPossibleShoot < Time.time)
            {
                gun[gunIndex].Shoot(-1);
                nextPossibleShoot = Time.time + secondsBetweenShoots;
            }

            // DELAY BETWEEN SWITCH SHOOT POINT
            if (nextSwitchGunTime < Time.time)
            {
                Debug.Log(gun.Count);
                gunIndex = (gunIndex + 1) % gun.Count;
                nextSwitchGunTime = Time.time + secondsBeforeSwitchGun;

                if (gun.Count < 2)
                    nextPossibleShoot = nextSwitchGunTime;
            }
        }

        


        // WIN GAME
        if (hitPointNumber == 0)
            Debug.Log("WIN");
        
	}
}

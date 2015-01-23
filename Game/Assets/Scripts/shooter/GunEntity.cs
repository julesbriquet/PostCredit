using UnityEngine;
using System.Collections;

public class GunEntity : MonoBehaviour
{

    public enum GunType
    {
        SemiAuto,
        Burst,
        Auto
    }

    public GameObject shootObject;

    public GunType typeOfGun;
    public float roundPerMinute;
    public bool hasTriggerBeenRelease = true;
    protected float secondsBetweenShoots;
    protected float nextPossibleShoot;



    // Handling damage
    [HideInInspector]
    public int shootLifeTime = 3;
    public float shootSpeed = 10f;


    // Use this for initialization
    public virtual void Start()
    {
        secondsBetweenShoots = 60 / roundPerMinute;
        nextPossibleShoot = 0;
    }

    public virtual void Shoot(int playerNumber)
    {
        if (CanShoot())
        {
            // Create Object
            ShootEntity shoot = ((GameObject)Instantiate(shootObject, this.transform.position, this.transform.rotation)).GetComponent<ShootEntity>();
            shoot.playerOrigin = playerNumber;
            shoot.destroyObjectAfterDelay(shootLifeTime);
            shoot.speed = shootSpeed;

            // Compute time for enabling next shot (Mode Auto Only)
            nextPossibleShoot = Time.time + secondsBetweenShoots;
        }
    }

    public bool CanShoot()
    {
        bool canShoot = Time.time > nextPossibleShoot;

        if (typeOfGun == GunType.SemiAuto)
            canShoot = canShoot && hasTriggerBeenRelease;

        return canShoot;
    }
}

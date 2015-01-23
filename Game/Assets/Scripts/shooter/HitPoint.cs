using UnityEngine;
using System.Collections;

public class HitPoint : MonoBehaviour {

    public EnnemyEntity shipOwner;

    void Start()
    {
        shipOwner = this.transform.parent.GetComponent<EnnemyEntity>();
    }

    void OnDestroy()
    {
        shipOwner.hitPointNumber--;
        shipOwner.gun.Remove(this.GetComponent<GunEntity>());
    }
}

using UnityEngine;
using System.Collections;

public class HitPoint : MonoBehaviour {

    public enum HitType
    {
        ARM,
        HEAD
    };

    public EnnemyEntity shipOwner;

    public HitType type;

    void Start()
    {
        shipOwner = this.transform.parent.GetComponent<EnnemyEntity>();
    }

    void OnDestroy()
    {
        shipOwner.hitPointNumber--;
        shipOwner.gun.Remove(this.GetComponent<GunEntity>());

        if (GameManager.Instance.LevelDifficulty > 1 && type == HitType.ARM)
            shipOwner.KillArm();
        else if (GameManager.Instance.LevelDifficulty > 1 && type == HitType.HEAD)
            shipOwner.KillHead();
    }
}

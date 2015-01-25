using UnityEngine;
using System.Collections;

public class PlantScript : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}

    public void LaunchAnimationDestroy()
    {
        anim.SetBool("dead", true);
        ZeldaManager.Instance.numberOfPlant--;
    }

    public void DestroyPlant()
    {
        Destroy(this.gameObject);
    }
}

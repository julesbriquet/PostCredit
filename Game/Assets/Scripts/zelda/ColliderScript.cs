using UnityEngine;
using System.Collections;

public class ColliderScript : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "ZeldaPlant")
        {
            PlantScript plant = other.GetComponent<PlantScript>();
            plant.LaunchAnimationDestroy();
        }
    }
}

using UnityEngine;
using System.Collections;

public class ShootEntity : MonoBehaviour
{

    public float speed = 0.5f;
    public int playerOrigin;

    public void destroyObjectAfterDelay(int destroyDelay)
    {
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        this.transform.position += forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.tag == "Player")
        {
            Player playerEntity = other.GetComponent<Player>();
            if (playerOrigin != playerEntity.GetPlayerNumber())
            {
                playerEntity.GetStun(stunDelay);

                Destroy(gameObject);
            }
        }
        if (other.tag == "Asteroid")*/

        PlayerShip player = other.GetComponent<PlayerShip>();

        if (playerOrigin > 0)
        {
            if (other.tag == "HitPoint")
                Destroy(other.gameObject);

            if (player == null)
                Destroy(gameObject);
        }
        else if (player)
        {
            Destroy(gameObject);
        }
    }
}

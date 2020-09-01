using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    public float damage = 20f;

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Renderer>().isVisible)
            Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MonsterAI>())
        {
            Debug.Log("Enemy!!");
            collision.gameObject.GetComponent<MonsterAI>().AddDamage(damage);
        }
        Destroy(this.gameObject);
    }
}

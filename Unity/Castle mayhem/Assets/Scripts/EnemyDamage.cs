using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int hp = 10;
    private void OnParticleCollision(GameObject other)
    {
        hp -= 2;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {

    }

}

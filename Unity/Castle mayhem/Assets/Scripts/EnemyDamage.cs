using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHP = 10;
    [SerializeField] int reward = 50;
    int hp;
    private void OnParticleCollision(GameObject other)
    {
        hp -= 2;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Bank.Instance.AddMoney(reward);
        }

    }
    void OnEnable()
    {
        hp = maxHP;
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHP = 10;
    [SerializeField] int reward = 50;
    [SerializeField] int penalty = 3;
    
    [Tooltip("adds value to max hp after enemy dies when it respawns")]
    [SerializeField] int difficultyRamp = 2;
    int hp;
    private void OnParticleCollision(GameObject other)
    {
        hp -= 2;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            maxHP += difficultyRamp;
            Bank.Instance.AddMoney(reward);
        }

    }
    public int GetPenalty()
    {
        return penalty;
    }
    void OnEnable()
    {
        hp = maxHP;
    }

}

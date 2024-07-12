using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;


public class FocusEnemy : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] int price = 150;
    [SerializeField] List<Transform> enemies;

    //Transform endOfPath;
    Transform target;

    void Start()
    {
        Enemy[] enemyA = FindObjectsOfType<Enemy>();
        //endOfPath = GameObject.FindGameObjectWithTag("End").transform;
        foreach (Enemy enemy in enemyA)
        {
            enemies.Add(enemy.transform);
        }
        target = enemies[0].transform;
        FindFirstTarget();
    }
    void Update()
    {

        FindFirstTarget();
        weapon.LookAt(target);
    }
    void FindFirstTarget()
    {

        var emission = projectileParticles.emission;
        if (UnityEngine.Vector3.Distance(transform.position, target.transform.position) > range)
        {
            StartCoroutine(IterateEnemies());
            emission.enabled = false;
        }
        else
            emission.enabled = true;
        if (!target.gameObject.activeInHierarchy)
            StartCoroutine(IterateEnemies());
    }
    private IEnumerator IterateEnemies()
    {

        // iterate over all enemies and find distances every 0.5, once you have a target in range iterate over it over and over until it is out then find the next closest. <--

        target = enemies[0].transform;
        foreach (Transform enemy in enemies)
        {
            if (UnityEngine.Vector3.Distance(transform.position, enemy.position) < UnityEngine.Vector3.Distance(transform.position, target.position))
                // compare distance of each enemy to the closest
                target = enemy;


        }
        yield return new WaitForSeconds(0.5f);
    }
    public int GetPrice()
    {
        return price;
    }
}


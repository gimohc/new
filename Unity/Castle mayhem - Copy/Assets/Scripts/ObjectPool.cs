using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemy;
    [SerializeField] int poolSize = 5;

    GameObject[] pool;

    void Awake() {
        PopulatePool();
    }
    void PopulatePool() {
        pool = new GameObject[poolSize];
        for(int i = 0; i < pool.Length; i++) {
            pool [i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies() { 
        while(true) {
            EnableObjectInPool();
            yield return new WaitForSeconds(1f);
        }
    }
    void EnableObjectInPool() {
        for(int i = 0; i < pool.Length; i++) {
            if(!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

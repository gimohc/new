using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player Instance { get; private set; }
    [SerializeField] int maxHP = 20;
    [SerializeField] int currentHP;
    TextMeshProUGUI hpLabel;

    void Start()
    {
        if (Player.Instance == null)
        {
            Instance = this;
            currentHP = maxHP;
            hpLabel = GetComponent<TextMeshProUGUI>();
            HealthText();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GetDamaged(int x)
    {
        currentHP -= x;
        if (currentHP <= 0)
            ReloadScene();
        HealthText();


    }
    private void HealthText()
    {
        hpLabel.text = "Health: " + currentHP.ToString();
    }
    private void ReloadScene()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

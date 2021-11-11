using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public player player;
    [Header("UI")]
    public Text ammoText;
    public GameObject enemyContainer;
    // Start is called before the first frame update
    public Text enemyText;
    public Text healthtext;

    private int initialenemycount;

    void Start()
    {
        initialenemycount = enemyContainer.GetComponentsInChildren<Enemy>().Length;
    }

    void Update()
    {
        ammoText.text = "Ammo: " + player.Ammo;
        healthtext.text = "Health: " + player.Health;
        int kiledEnemies = 0;
        foreach (Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>()){
            if (enemy.Killed == true)
            {
                kiledEnemies++;
            }
        }
        enemyText.text = "Enemies: " + (initialenemycount - kiledEnemies);
    }
}

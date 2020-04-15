using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthShieldUI : MonoBehaviour
{
    //Blackthrornprod Health Tutorial
    [Header("HealthUI")]
    public int health;
    public int numOfShields;

    public Image[] shields;
    public Sprite fullShield;
    public Sprite emptyShield;

    Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {

        health = player.GetHealth();
        {
            //Blackthronprod Health Tutorial
            if (health > numOfShields)
            {
                health = numOfShields;
            }

            for (int i = 0; i < shields.Length; i++)
            {
                if (i < health)
                {
                    shields[i].sprite = fullShield;
                }
                else
                {
                    shields[i].sprite = emptyShield;
                }

                if (i < numOfShields)
                {
                    shields[i].enabled = true;
                }
                else
                {
                    shields[i].enabled = false;
                }
            }
        }
    }


        


}

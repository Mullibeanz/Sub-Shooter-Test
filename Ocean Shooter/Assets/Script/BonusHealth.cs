using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHealth : MonoBehaviour
{

    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] [Range(0, 1)] float powerUpSFXVolume = 0.7f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(powerUpSFX,
                Camera.main.transform.position,
                powerUpSFXVolume);

            FindObjectOfType<Player>().ResetHealth();
            Destroy(gameObject);
        }
    }
}

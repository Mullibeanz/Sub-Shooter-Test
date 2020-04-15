using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    private bool toggle;
    /*public Button musicToggleButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    */
    

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ToggleSound()
    {
        toggle = !toggle;

        if (toggle)
        {
            AudioListener.volume = 1f;
            //musicToggleButton.GetComponent<Image>().sprite = soundOnSprite;
        }
        
        else
        {
            AudioListener.volume = 0f;
            //musicToggleButton.GetComponent<Image>().sprite = soundOffSprite;
        }
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleSound();
        }
    }
}

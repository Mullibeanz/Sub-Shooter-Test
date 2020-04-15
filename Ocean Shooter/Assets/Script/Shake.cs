using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CamShake()
    {
        int randShake = Random.Range(0, 3);
        if (randShake == 0)
        {
            camAnim.SetTrigger("Shake 1");
        }
        else if (randShake == 1)
        {
            camAnim.SetTrigger("Shake 2");
        }
        else if (randShake == 2)
        {
            camAnim.SetTrigger("Shake 3");
        }
        else if (randShake == 3)
        {
            camAnim.SetTrigger("Shake 4");
        }
    }
}

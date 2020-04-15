using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoabDisplay : MonoBehaviour
{
    TextMeshProUGUI moabText;

    Player player;


    // Start is called before the first frame update
    void Start()
    {
        moabText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        moabText.text = player.GetnumberOfMoabs().ToString();
    }
}
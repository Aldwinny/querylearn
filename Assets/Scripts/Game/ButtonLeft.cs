using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : MonoBehaviour
{
    bool pressed = false;
    public Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressed = true;
        } else if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }

        if (pressed)
        {
            player.Move(-5);
        }
    }

    
    
}

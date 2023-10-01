using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRB2D;

    public float jumpStrength = 11;
    public float walkSpeed = 5;
    public int position = 1;
    public bool isMoving = false;

    public GameObject nextLevel;
    public GameObject congats;

    void Start()
    {
        
    }

    void Update()
    {
        if (isMoving)
        {
            Move(position * walkSpeed);
        }
    }

    public void Jump()
    {
        playerRB2D.velocity = Vector2.up * jumpStrength;
    }

    public void MovePosition(int position)
    {
        isMoving = true;
        this.position = position;
    }

    public void SetIsMoving()
    {
        isMoving = false;
    }

    public void Move(float velocity)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (velocity > 0)
        {
            sr.flipX = false;
            playerRB2D.velocity = new Vector2(velocity, 0);
        } else
        {
            sr.flipX = true;
            playerRB2D.velocity = new Vector2(velocity, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.Find("exclamation button"))
        {
            Destroy(collision.gameObject);
            GameObject.Find("desc").GetComponent<TextMeshProUGUI>().text = "Congrats! Proceeding to level 1..";
            congats.SetActive(true);
            StartCoroutine(ExitTutorial());
        }
    }

    IEnumerator ExitTutorial()
    {
        yield return new WaitForSeconds(3);
        GameObject.Find("Tutorial").SetActive(false);
        nextLevel.SetActive(true);
    }
}

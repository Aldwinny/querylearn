using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteResize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var parent = (gameObject.transform as RectTransform);
        var sprite = GetComponent<SpriteRenderer>().sprite;
        parent.localScale = parent.rect.size / sprite.rect.size * sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

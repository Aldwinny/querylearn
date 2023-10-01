using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunLittleThing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(GoBack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoBack() {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("Project - with background music");
        SceneManager.UnloadSceneAsync("Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContextScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
            ReloadScene();
    }

    [ContextMenu(nameof(ReloadScene))]
    public void ReloadScene()
    {
        Debug.Log("Reloading Scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
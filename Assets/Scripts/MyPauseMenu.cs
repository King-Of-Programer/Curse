using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    void Start()
    {
        ChangePause(content.activeSelf);
    }

   
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangePause(!content.activeSelf);
        }
    }
    private void ChangePause(bool pause)
    {
        content.SetActive(pause);
        Time.timeScale = pause ? 0.0f : 1.0f;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
    }

}

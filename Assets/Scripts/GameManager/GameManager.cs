using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerInput _Input;

    [SerializeField]
    GameObject paused;

    [SerializeField]
    GameObject traning;

    [SerializeField]
    GameObject cursor;

    [SerializeField]
    GameObject task;

    [SerializeField]
    Transform container;

    [SerializeField]
    Transform Objects;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _Input.SwitchCurrentActionMap("UI");
        traning.SetActive(true);

    }

    public void SkipTraning()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _Input.SwitchCurrentActionMap("Player");

        if (traning.activeSelf)
        {
            traning.SetActive(false);
            cursor.SetActive(true);
            task.SetActive(true);
        }
    }

    public void Paused()
    {
        _Input.SwitchCurrentActionMap("UI");

        Cursor.lockState = CursorLockMode.None;


        task.SetActive(false);
        paused.SetActive(true);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _Input.SwitchCurrentActionMap("Player");

        task.SetActive(true);
        paused.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

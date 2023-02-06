using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelector : MonoBehaviour
{
    public GameObject[] canvas;

    public static CanvasSelector instance = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }

    private void InitializeManager()
    {
        SelectCanvas(0);
    }

    public void SelectCanvas(int index)
    {
        foreach (GameObject item in canvas)
        {
            item.SetActive(false);
        }

        canvas[index].SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

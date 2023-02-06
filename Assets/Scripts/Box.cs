using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public int index;
    public Mark mark;
    public bool isMarked;

    public Sprite invisibleField;

    private Image boxImage;

    private void Start()
    {
        index = transform.GetSiblingIndex();
        mark = Mark.None;
        isMarked = false;
        boxImage = GetComponent<Image>();
    }

    public void SetAsMarked(Sprite sprite, Mark mark, Color color)
    {
        this.mark = mark;
        PlayMarkSound();
        boxImage.enabled = true;
        isMarked = true;
        boxImage.sprite = sprite;
        boxImage.color = color;
        GetComponent<Button>().enabled = false;
        GetComponent<Animator>().Play("Box");
    }

    private void PlayMarkSound()
    {
        if (mark == Mark.X)
        {
            SFX.instance.PlaySound(1);
        }
        else
        {
            SFX.instance.PlaySound(2);
        }
    }

    private void OnDisable()
    {
        boxImage.sprite = invisibleField;
        GetComponent<Button>().enabled = true;
        isMarked = false;
    }
}

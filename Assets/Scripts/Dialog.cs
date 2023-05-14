using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public Text titleText;
    public Text contentText;
    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
    public void UpdateDialog(string title, string content)
    {
        if(titleText)
        {
            titleText.text = title;
        }
        if(contentText)
        {
            contentText.text = content;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayDialog : MonoBehaviour
{
	private Image textImage;
    private Text text;
    private Queue<(string text, float time_per_char, float restTimer)> stringBuffer;

    private float currTextTimer;
    private float currRestTimer;
    private int currCharIndex;
    private (string text, float time_per_char, float restTimer) currString;
    private bool displayingString = false;
    void Start()
    {
        textImage = transform.GetChild(0).gameObject.GetComponent<Image>() as Image;
        text = textImage.transform.GetChild(0).gameObject.GetComponent<Text>() as Text;
        textImage.gameObject.SetActive(false);
        EventManager.instance.onDisplayText += _displayText;
        stringBuffer = new Queue<(string text, float time_per_char, float restTimer)>();
    }

    void _displayText( string textToBeDisplayed, float time_per_char, float restTimer )
    {
        stringBuffer.Enqueue((textToBeDisplayed, time_per_char, restTimer));
    }

    void _showText()
    {
        textImage.gameObject.SetActive(true);
        displayingString = true;
        text.text  = "";

    }

    void _hideText()
    {
        textImage.gameObject.SetActive(false);
        displayingString = false;
    }

    void _nextString()
    {
        currCharIndex = 0;
        currRestTimer = 0;
        currTextTimer = 0;
        currString = stringBuffer.Dequeue();
        text.text  = "";
    }

    void Update()
    {
        if ( !displayingString && stringBuffer.Count > 0 ) 
        {
            _showText();
            _nextString();
        }
        if ( displayingString  )
        {
            if ( currCharIndex < currString.text.Length )
            {
                currTextTimer += Time.deltaTime;
                if ( currTextTimer >= currString.time_per_char )
                {
                    if (currString.text[currCharIndex++] == ' ') currCharIndex++;
                    text.text = currString.text.Substring(0, currCharIndex);
                    currTextTimer = 0f;
                }
            }
            else 
            {
                currRestTimer += Time.deltaTime;
                if ( currRestTimer >= currString.restTimer )
                {
                    if (stringBuffer.Count > 0)
                        _nextString();
                    else
                        _hideText();
                }
            }
           
        }
    }
}

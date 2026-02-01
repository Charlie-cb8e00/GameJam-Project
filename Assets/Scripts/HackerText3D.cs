using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class HackerText3D : MonoBehaviour
{
    private TextMeshPro _text;
    private string _defaultText;
    public int contador = 0;
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _defaultText = _text.text;
    }

    private IEnumerator StartAnimation()
    {
        int letterIndex = 0;
        while (letterIndex <= _defaultText.Length)
        {
            int randomCount = 0;

            while (randomCount < 5)
            {
                _text.text = RandomizeText(_defaultText, letterIndex);
                yield return new WaitForSeconds(0.1f);
                randomCount++;
            }
            letterIndex++;
        }
    }

    private string RandomizeText(string text, int startIndex)
    {
        string randomCharacters = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";

        for (int i = startIndex; i < text.Length; i++)
        {
            StringBuilder sb = new StringBuilder(text);
            sb[i] = randomCharacters[Random.Range(0, randomCharacters.Length)];
            text = sb.ToString();
        }
        return text;
    }
    void Update()
    {
        if (contador == 0)
        {
            StartCoroutine(StartAnimation());
            contador = 1;
        }
    }
}

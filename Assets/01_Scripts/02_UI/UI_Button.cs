using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _score = 0;

    private void Start()
    {
        _text.text = $"Score : {_score}";
    }

    public void OnButtonClicked()
    {
        _score++;
        Debug.Log("Button Clicked");
        _text.text = $"Score : {_score}";
    }
}

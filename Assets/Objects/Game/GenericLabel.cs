using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextSmack))]
public abstract class GenericLabel : MonoBehaviour
{
    private TextMeshProUGUI _pro;
    private TextSmack _smack;
    private string _lastText;

    private void Awake()
    {
        _pro = GetComponent<TextMeshProUGUI>();
        _smack = GetComponent<TextSmack>();
    }

    private void Update()
    {
        _pro.text = GetText();

        if (_lastText != _pro.text)
        {
            if (_lastText != null)
                _smack.Smack();
            
            _lastText = _pro.text;
        }

    }

    protected abstract string GetText();
}
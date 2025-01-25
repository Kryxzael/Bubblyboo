using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class DamageNumber : MonoBehaviour
{
    public string Text;
    public Color Color;
    public float Size;

    public float WaveScale;
    public float WaveSpeed;
    public float RaiseSpeed;
    public float FadeTime;
    private float _timer;

    TextMeshPro _pro = new TextMeshPro();

    private void Start()
    {
        _pro = GetComponent<TextMeshPro>();

        _pro.text = Text;
        _pro.color = Color;
        transform.localScale = Vector3.one * Size;
    }

    private void LateUpdate()
    {
        transform.position += Mathf.Sin(_timer * WaveSpeed) * WaveScale * Vector3.right;
        transform.position += RaiseSpeed * Time.deltaTime * Vector3.up;
        _pro.color = new Color(_pro.color.r, _pro.color.g, _pro.color.b, 1f - _timer / FadeTime);
        _timer += Time.deltaTime;

        if (_timer > FadeTime)
        {
            Destroy(gameObject);
        }
    }
}

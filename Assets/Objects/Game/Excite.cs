using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Excite : MonoBehaviour
{
    public float MaxShake;
    public float MaxRotate;

    public float Speed = 1f;

    private Vector3 _originalPosition;
    private float _originalRotation;

    private void Awake()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.eulerAngles.z;
    }

    private void Update()
    {
        float shake = Game.Instance.ExcitementLevel * MaxShake;
        float rotate = Game.Instance.ExcitementLevel * MaxRotate;

        transform.position = new Vector3(
            x: _originalPosition.x + (Mathf.PerlinNoise(0, Time.time * Speed) - 0.5f) * shake * 2f,
            y: _originalPosition.y + (Mathf.PerlinNoise(1000, Time.time * Speed) - 0.5f) * shake * 2f
        );

        transform.eulerAngles = new Vector3(0, 0, _originalRotation + (Mathf.PerlinNoise(2000, Time.time * Speed) - 0.5f) * rotate * 2f);
    }
}
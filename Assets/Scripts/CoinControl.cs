using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    private float y;
    void Start()
    {
        y = transform.position.y;
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x, y + Mathf.Sin(Time.time));
    }
}

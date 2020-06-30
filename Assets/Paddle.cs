using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] int SCREEN_WIDTH_IN_UNITS = 16;
    [SerializeField] float PADDLE_POS_MAX = 15f;
    [SerializeField] float PADDLE_POS_MIN = 1f;

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * SCREEN_WIDTH_IN_UNITS;
        Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y)
        {
            x = Mathf.Clamp(mousePosInUnits, PADDLE_POS_MIN, PADDLE_POS_MAX)
        };
        transform.position = paddlePos;
    }
}

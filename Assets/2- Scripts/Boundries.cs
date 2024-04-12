using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;

    void Start()
    {
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        playerWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2 ;
        playerHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2 ;
    }

 
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + playerWidth, screenBounds.x * -1 - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + playerHeight, screenBounds.y * -1 - playerHeight);
        transform.position = viewPos;
    }
}

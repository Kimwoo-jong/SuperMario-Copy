using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject wall;

    private Transform player;

    public Vector2 maxXandY;
    public Vector2 minXandY;

    public float xMargin = 1f;
    public float yMargin = 1f;

    public float xSmooth = 8f;
    public float ySmooth = 8f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool CheckxMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }
    bool CheckyMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }
    void FixedUpdate()
    {
        TrackPlayer();
    }
    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckxMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }
        if (CheckyMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
    void WallMove()
    {

    }
}

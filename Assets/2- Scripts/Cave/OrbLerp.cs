using System;
using UnityEngine;

public class OrbLerp : MonoBehaviour, ICollectable
{
    //for lerp
    [SerializeField] [Range(0f, 4f)] private float lerpTime;
    [SerializeField] private Vector2 orbPos;
    private int posIndex = 0;
    private int lenght;
    private float t = 0f;
    
    //for collect
    private bool collected = false;
    private float speed = 10;
    private Transform CaveOrbPos;
    [SerializeField] private GameObject CaveOrbPlace;

    private void Start()
    {
        //lenght = orbPos.Length;
        CaveOrbPos = CaveOrbPlace.transform;
    }

    private void Update()
    {
        Move();
        transform.position = Vector2.Lerp(transform.position, orbPos, lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > 0.9f)
        {
            t = 0f;
            posIndex++;
            posIndex = (posIndex >= lenght) ? 0 : posIndex;
        }
    }
    
    private void Move()
    {
        if (collected)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, CaveOrbPos.position, Time.deltaTime * speed);

        }

        if (transform.position == CaveOrbPos.position)
        {
            UIManager.Instance.AddOrb();
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        collected = true;
    }
}

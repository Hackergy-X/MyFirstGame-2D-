using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField]
    Transform player;
    SpriteRenderer sr;
    Vector3 StartPosition;
    float eagleHight = 4;
    float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        StartPosition = transform.position;
        StartCoroutine(EagleAnimation());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
       
    }
    IEnumerator EagleAnimation()
    {
        Vector3 endPos = new Vector3(StartPosition.x, StartPosition.y + eagleHight, StartPosition.z);
        bool isFly = true;
        float value = 0;
        while (true)
        {
            yield return null;
            if (isFly)
            {
                transform.position = Vector3.Lerp(StartPosition, endPos, value);
            }
            else
            {
                transform.position = Vector3.Lerp(endPos, StartPosition, value);
            }
            value += Time.deltaTime * Speed;
            if(value > 1)
            {
                value = 0;
                isFly = !isFly;
            }
        }
       
    }
}

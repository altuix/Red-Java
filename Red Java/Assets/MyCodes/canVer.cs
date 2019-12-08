using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canVer : MonoBehaviour
{
    public Sprite[] animasyonKareleri;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonKaremiz = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.1f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKaremiz++];
            zaman = 0;

            if (animasyonKareleri.Length == animasyonKaremiz)
            {
                animasyonKaremiz = animasyonKareleri.Length - 1;
            }
        }
    }
}

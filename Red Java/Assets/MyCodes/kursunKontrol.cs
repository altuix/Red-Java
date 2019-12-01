using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol : MonoBehaviour
{
    dusmanKontrol dusman;
    Rigidbody2D Fizik;
    void Start()
    {
        //dusman kontrol scriptine ulaştık
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<dusmanKontrol>();
        Fizik = GetComponent<Rigidbody2D>();
        Fizik.AddForce(dusman.getYon() * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

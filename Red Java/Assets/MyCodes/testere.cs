using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class testere : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKezAl = true;
    Vector3 aradakiMesafe;

    int aradakiMesafeSayaci = 0;

    bool ileriMiGeriMi = true;

    // Start is called before the first frame update
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];

        //oyun başladıktan sonra hedefleri dışarı alıyoruz
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5);
        nokalaraGit();
    }

    void nokalaraGit()
    {


        if (aradakiMesafeyiBirKezAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;

            aradakiMesafeyiBirKezAl = false;
        }
        float kalanMesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;

        if (kalanMesafe < 0.5f)
        {
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
                ileriMiGeriMi = false;
            else if (aradakiMesafeSayaci == 0)
                ileriMiGeriMi = true;

            if (ileriMiGeriMi)
            {
                aradakiMesafeSayaci++;

            }
            else
            {
                aradakiMesafeSayaci--;

            }

            aradakiMesafeyiBirKezAl = true;

        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }

    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]
class testereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        testere script = (testere)target; // yukarıda olan testere classına erişmek için


        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))   //editöre buton ekledik ve sizeları ayarladık
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform; // yeni objeyi testere altında oluşturduk
            yeniObje.transform.position = script.transform.position; // pozisyonunu ayarladık
            yeniObje.name = script.transform.childCount.ToString(); // isimleri sayı olarak verdik
        }
    }
}
#endif
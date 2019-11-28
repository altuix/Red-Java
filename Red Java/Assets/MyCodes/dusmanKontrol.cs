using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class dusmanKontrol : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKezAl = true;
    Vector3 aradakiMesafe;

    int aradakiMesafeSayaci = 0;

    bool ileriMiGeriMi = true;

    GameObject karakter;
    RaycastHit2D ray;
    int hiz = 5;

    SpriteRenderer spriteRenderer;

    //not: editör kodu varken dışarıya bir değişken açmak için editör içerisinede tanımlama yapmak gerekir
    public LayerMask layerMask;
    public Sprite onTaraf;
    public Sprite arkaTaraf;



    // Start is called before the first frame update
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        karakter = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();


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
        beniGordumu();
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
        transform.position += aradakiMesafe * Time.deltaTime * hiz;

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

    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layerMask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);

        if (ray.collider.tag == "Player")
        {//düşman beni gördü 
            hiz = 10;
            spriteRenderer.sprite = onTaraf;
        }
        else
        {// görüş alanından çıktı
            hiz = 5;
            spriteRenderer.sprite = arkaTaraf;

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
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]
class dusmanEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();z
        dusmanKontrol script = (dusmanKontrol)target; // yukarıda olan testere classına erişmek için


        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))   //editöre buton ekledik ve sizeları ayarladık
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform; // yeni objeyi testere altında oluşturduk
            yeniObje.transform.position = script.transform.position; // pozisyonunu ayarladık
            yeniObje.name = script.transform.childCount.ToString(); // isimleri sayı olarak verdik
        }

        //not: editör kodu varken dışarıya bir değişken açmak için editör içerisinede tanımlama yapmak gerekir
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif
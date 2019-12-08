using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class karakterKontrol : MonoBehaviour
{
    Rigidbody2D Fizik;
    public Sprite[] beklemeAnimasyonu;
    public Sprite[] ziplamaAnimasyonu;
    public Sprite[] yurumeAnimasyonu;
    public Text Can;
    public Image DeadEnd;
    int can = 100;

    float DeadEndSayaci;
    float anaMenuyeDonZaman;

    SpriteRenderer spriteRenderer;

    int beklemeAnimasyonuSayisi = 0;
    int ziplamaAnimasyonuSayisi = 0;
    int yurumeAnimasyonuSayisi = 0;


    float horizontal = 0;
    float beklemeAnimasyonuZamani = 0;
    float yurumeAnimasyonuZamani = 0;
    float ziplamaAnimasyonuZamani = 0;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    bool ziplamaAktif = false;

    GameObject kamera;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacinciLevel"))
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex);

        }

        Fizik = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        Can.text = "Can " + can;
        //ilk pozisyonu belirledik
        kameraIlkPos = kamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KarakterHareket();
        Animasyon();
    }

    private void Update()
    {
        if (!ziplamaAktif)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fizik.AddForce(new Vector2(0, 500));
                ziplamaAktif = true;
            }
        }
        if (can <= 0)
        {
            Time.timeScale = 0.5f;
            DeadEndSayaci += 0.03f;

            Can.enabled = false;
            DeadEnd.color = new Color(0, 0, 0, DeadEndSayaci);
            anaMenuyeDonZaman += Time.deltaTime;

            if (anaMenuyeDonZaman > 1)
            {

                SceneManager.LoadScene("AnaMenu");
            }
        }
    }


    //kamera hareketleri için
    private void LateUpdate()
    {
        kameraTakip();
    }


    //yere inme kontrolü için
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ziplamaAktif = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kursun")
        {
            can -= 10;
            Can.text = "Can " + can;

        }

        if (collision.gameObject.tag == "dusman")
        {
            can -= 20;
            Can.text = "Can " + can;

        }

        if (collision.gameObject.tag == "testere")
        {
            can -= 20;
            Can.text = "Can " + can;

        }

        if (collision.gameObject.tag == "levelBitsin")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "canVer")
        {

            can += 30;
            Can.text = "Can " + can;

            //tekrar temasta can vermesin diye
            collision.GetComponent<BoxCollider2D>().enabled = false;

            //scripti çalıştırdık
            collision.GetComponent<canVer>().enabled = true;
            Destroy(collision.gameObject, 3);
        }

        if (collision.gameObject.tag == "altin")
        {

           
            //tekrar temasta can vermesin diye
            collision.GetComponent<CircleCollider2D>().enabled = false;
            
            Destroy(collision.gameObject);
        }
    }
    void KarakterHareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //hareket için
        vec = new Vector3(horizontal * 10, Fizik.velocity.y, 0);

        Fizik.velocity = vec;
    }

    void Animasyon()
    {
        //zıplamıyorsa
        if (!ziplamaAktif)
        {



            //player duruyorsa yani
            if (horizontal == 0)
            {
                beklemeAnimasyonuZamani += Time.deltaTime;
                if (beklemeAnimasyonuZamani > 0.02f)
                {
                    spriteRenderer.sprite = beklemeAnimasyonu[beklemeAnimasyonuSayisi++];
                    if (beklemeAnimasyonuSayisi == beklemeAnimasyonu.Length)
                    {
                        beklemeAnimasyonuSayisi = 0;
                    }
                    beklemeAnimasyonuZamani = 0;
                }

            }
            //ileri
            else if (horizontal > 0)
            {
                yurumeAnimasyonuZamani += Time.deltaTime;
                if (yurumeAnimasyonuZamani > 0.02f)
                {
                    spriteRenderer.sprite = yurumeAnimasyonu[yurumeAnimasyonuSayisi++];
                    if (yurumeAnimasyonuSayisi == yurumeAnimasyonu.Length)
                    {
                        yurumeAnimasyonuSayisi = 0;
                    }
                    yurumeAnimasyonuZamani = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);

            }//geri
            else if (horizontal < 0)
            {
                yurumeAnimasyonuZamani += Time.deltaTime;
                if (yurumeAnimasyonuZamani > 0.02f)
                {
                    spriteRenderer.sprite = yurumeAnimasyonu[yurumeAnimasyonuSayisi++];
                    if (yurumeAnimasyonuSayisi == yurumeAnimasyonu.Length)
                    {
                        yurumeAnimasyonuSayisi = 0;
                    }
                    yurumeAnimasyonuZamani = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);

            }
        }
        else
        {
            if (Fizik.velocity.y > 0)
            {
                spriteRenderer.sprite = ziplamaAnimasyonu[0];
            }
            else
            {
                spriteRenderer.sprite = ziplamaAnimasyonu[1];

            }

            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }

        }
    }


    void kameraTakip()
    {
        //takip için
        kameraSonPos = kameraIlkPos + transform.position;
        //kamera.transform.position = kameraSonPos;

        //kamera yumuşatıcı :D
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.07f);
    }
}

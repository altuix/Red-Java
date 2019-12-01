using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenuKontrol : MonoBehaviour
{
    GameObject level1, level2, level3, bolumlerSekmesi;

    void Start()
    {
        bolumlerSekmesi = GameObject.Find("BolumlerSekmesi");
        bolumlerSekmesi.gameObject.SetActive(false);


        //level1 = bolumlerSekmesi.transform.Find("Bolum1").gameObject;
        //level2 = bolumlerSekmesi.transform.Find("Bolum2").gameObject;
        //level3 = bolumlerSekmesi.transform.Find("Bolum3").gameObject;

        for (int i = 0; i < PlayerPrefs.GetInt("kacinciLevel"); i++)
        {
            bolumlerSekmesi.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void buttonSec(int gelenButton)
    {
        switch (gelenButton)
        {
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:

                //bolumlerSekmesi.SetActive(true);
                bolumlerSekmesi.gameObject.SetActive(true);
                

                break;
            case 3:
                Application.Quit();
                break;

            default:
                break;
        }
    }
}

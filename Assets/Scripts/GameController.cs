using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject balon;
    float zaman = 1f;
    public Text balonSayisiText;
    int balonSayisi;
    public float kalanzamanim ;
    public Text kalanZamanimText;
    public Text kaybettinizText;
    public bool oyunDurum;
    public GameObject patlamaefekt;
    public Text kalancanText;
    int kalancansayi = 3;
    public Text kazandinizText;
    public Button button;
    public AudioClip arkaplanses;
    public BalonController bc;


    public Image[] images;
     void Start()
    {
        oyunDurum = true;
        GetComponent<AudioSource>().PlayOneShot(arkaplanses);
    }
   
    void Update()
    {
        BalonOlusturma();
        KalanZaman();

    }

    private void BalonOlusturma()
    {
        if(oyunDurum)
        {
            zaman -= Time.deltaTime;
            if (zaman <= 0)
            {
                Vector3 pozisyonum = new Vector3(Random.Range(-2.25f, 2.25f), -5.63f, 0f);  //transform position bu.
                GameObject go = Instantiate(balon, pozisyonum, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, Random.Range(80f, 120f), 0f)); // go = balonun kompanentlerine gir ve kuvvet uygula.
                zaman = 1f;
            }
        }
        
    }

    public void patlayanBalonSayisi()
    {
        balonSayisi += 1;
        balonSayisiText.text = "Balon Sayisi :" + balonSayisi; 
        if(balonSayisi == 10)
        {
            kazandinizText.gameObject.SetActive(true);
            kalanzamanim = 0;
            KalanBalonlarýPatlat();
            oyunDurum = false;
            button.gameObject.SetActive(true);
            
        }
    }
    public void KalanZaman()
    {
        kalanzamanim -= Time.deltaTime;
        kalanZamanimText.text = "Zaman :" + (int)kalanzamanim;
        if(kalanzamanim <= 0)
        {

            KalanBalonlarýPatlat();
            kaybettinizText.gameObject.SetActive(true);
            oyunDurum = false;
            kalanzamanim = 0;
            button.gameObject.SetActive(true);
            
        }
        

    }
    public void KalanBalonlarýPatlat()
    {
        GameObject[] kalanbalonlar = GameObject.FindGameObjectsWithTag("BalonObjesi");  //Balonlarý(s) yakala.
        for (int i = 0; i < kalanbalonlar.Length; i++) //.Length =) Kaç tane balon varsa 
        {
            Instantiate(patlamaefekt, kalanbalonlar[i].transform.position, kalanbalonlar[i].transform.rotation);
            Destroy(kalanbalonlar[i]);
            AudioSource.PlayClipAtPoint(bc.balonses, transform.position); 
        }

    }
    public void KalanCanim()
    {
        kalancansayi -= 1;
        images[kalancansayi].gameObject.SetActive(false);
        kalancanText.text = "Kalan Can :" + kalancansayi;
        if(kalancansayi == 0)
        {
            kaybettinizText.gameObject.SetActive(true);
            KalanBalonlarýPatlat();
            kalanzamanim = 0;
            oyunDurum = false;
            button.gameObject.SetActive(true);
            
        }
    }
    public void YenidenOyna()
    {
        SceneManager.LoadScene("Oyun");
    }
}

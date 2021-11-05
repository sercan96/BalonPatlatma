using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalonController : MonoBehaviour
{
    public GameObject patlamaefekti;

    public AudioClip balonses;  


    public void OnMouseDown()
    {
        
        GameObject go =Instantiate(patlamaefekti, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(go, 0.667f);  // Clonu yok etmek i�in de�i�kene atad�k. Aksi halde Publicte objeyi yok ediyor.
        GameObject gc = GameObject.Find("GameController");
        gc.GetComponent<GameController>().patlayanBalonSayisi();
        //GetComponent<AudioSource>().PlayOneShot(balonses); // Objenin i�erisine "AudioSource" kompanenti eklenmesi gerekir.
        //GetComponent<AudioSource>().Play(0); // Eklenen ilk sesi �al��t�r�r.
        AudioSource.PlayClipAtPoint(balonses, transform.position); // Scriptin bulundu�u objeyi yok etti�inde ses �al���r.

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Engel")
        {
            GameObject kalancan = GameObject.Find("GameController");
            kalancan.GetComponent<GameController>().KalanCanim();
            Destroy(gameObject);
        }
    }
}

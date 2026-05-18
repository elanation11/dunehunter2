using UnityEngine;
using UnityEngine.SceneManagement; // Sahneler arası geçiş için
using UnityEngine.UI; // Butonlar ve Slider için

public class MenuYoneticisi : MonoBehaviour
{
    public GameObject ayarlarPaneli;

    public void OyunuBaslat()
    {
        // "SampleScene" yerine senin asıl oyun sahnenin adı neyse onu yaz
        SceneManager.LoadScene("SampleScene"); 
    }

    public void AyarlariAc()
    {
        ayarlarPaneli.SetActive(true);
    }

    public void AyarlariKapat()
    {
        ayarlarPaneli.SetActive(false);
    }

    public void SesiAcKapat(bool acikMi)
    {
        if (acikMi) AudioListener.volume = 1f; 
        else AudioListener.volume = 0f; 
    }
}
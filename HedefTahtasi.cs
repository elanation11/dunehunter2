using UnityEngine;
using UnityEngine.UI;

public class HedefTahtasi : MonoBehaviour
{
    public float maxCan = 100f;
    private float mevcutCan;

    public Slider canBari; 
    public GameObject canBariObjesi; // Slider'ın içinde olduğu Canvas'ı buraya atacağız

    void Start()
    {
        mevcutCan = maxCan;

        if (canBari != null)
        {
            canBari.maxValue = maxCan;
            canBari.value = maxCan;
            
            // Oyun başında can barını gizle
            if (canBariObjesi != null)
                canBariObjesi.SetActive(false);
        }
    }

    public void HasarAl(float miktar)
    {
        // Hasar alındığı an can barını görünür yap
        if (canBariObjesi != null)
            canBariObjesi.SetActive(true);

        mevcutCan -= miktar;
        
        if (canBari != null)
        {
            canBari.value = mevcutCan;
        }

        // Eğer can barının belirli bir süre sonra tekrar gizlenmesini istersen:
        CancelInvoke("CanBariniGizle");
        Invoke("CanBariniGizle", 3f); // 3 saniye sonra gizler

        if (mevcutCan <= 0)
        {
            YokOl();
        }
    }

    void CanBariniGizle()
    {
        if (canBariObjesi != null)
            canBariObjesi.SetActive(false);
    }

    void YokOl()
    {
        Destroy(gameObject);
    }
}
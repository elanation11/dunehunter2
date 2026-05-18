using UnityEngine;
using System.Collections;
using TMPro; 
using UnityEngine.SceneManagement; 

public class AtesSistemi : MonoBehaviour
{
    [Header("Silah Ayarları")]
    public Camera fpsCam; 
    public float menzil = 100f; 
    public GameObject vurusEfektiPrefab; 

    [Header("Ses Ayarları")]
    public AudioSource silahSesiKaynagi; 

    [Header("UI (Arayüz) Ayarları")]
    public TextMeshProUGUI sureText; 
    public TextMeshProUGUI hedefText; 

    [Header("Oyun Mantığı")]
    private float kalanSure = 60f; 
    private int vurulanHedef = 0;
    private int vurusSayaci = 0; 
    private bool oyunBitti = false;

    void Start()
    {
        // Oyun başında fareyi kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (oyunBitti) return; 

        // ZAMANLAYICI SİSTEMİ
        if (kalanSure > 0)
        {
            kalanSure -= Time.deltaTime;
            if (sureText != null) 
                sureText.text = "Süre: " + Mathf.RoundToInt(kalanSure).ToString();
        }
        else
        {
            OyunBitti();
        }

        // ATEŞ ETME GİRDİSİ
        if (Input.GetMouseButtonDown(0))
        {
            AtesEt();
        }
    }

    void AtesEt()
    {
        if (silahSesiKaynagi != null) silahSesiKaynagi.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, menzil))
        {
            if (vurusEfektiPrefab != null)
            {
                GameObject efekt = Instantiate(vurusEfektiPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(efekt, 1f);
            }

            HedefTahtasi hedef = hit.transform.GetComponent<HedefTahtasi>();
            if (hedef != null)
            {
                hedef.HasarAl(10f); 
                vurusSayaci++; 

                if (vurusSayaci >= 10)
                {
                    vurulanHedef++; 
                    if (hedefText != null)
                        hedefText.text = "Hedef: " + vurulanHedef.ToString();
                    
                    vurusSayaci = 0; 
                }
            }
        }
    }

    void OyunBitti()
    {
        oyunBitti = true;
        if (sureText != null) sureText.text = "SÜRE DOLDU! YENİDEN BAŞLIYOR...";
        
        // 5 saniye bekle ve YenidenBaslat fonksiyonunu çalıştır
        Invoke("YenidenBaslat", 5f);
    }

    public void YenidenBaslat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
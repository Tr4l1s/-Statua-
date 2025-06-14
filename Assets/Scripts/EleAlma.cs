using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EleAlma : MonoBehaviour
{
    public float Mesafe = 3f;

    public GameObject El_Feneri;

    //Notların cümbüşkesi
    private Dictionary<string, Sprite> notEnvanteri = new();
    private List<string> notSirasi = new(); // Not ID sıralaması
    private int aktifNotIndex = 0;

    public GameObject NoteScreen;
    public Image not_Image;
    public Sprite Note1;
    public Sprite Note2;
    public Sprite Note3;
    public Sprite GorevNotuBos;      // İlk hali (hiçbir şey toplanmamış)
    public Sprite GorevNotu1;        // Joyistick alındı
    public Sprite GorevNotu2;        // Defter alındı
    public Sprite GorevNotu3;        // Çekiç alındı
    public Sprite GorevNotu4;        // Maske alındı



    public Animation Door;
    public Transform doorObject;
    public bool isOpen;
    public bool isLocked;
    public TMP_Text UyariTextComponent;
    public TMP_Text GorevTextComponent;

    public TMP_Text EtkilesimText;

    public GameObject Canavar;
    public GameObject FakeCanavar;
    public AudioSource canavarAktifSes;

    public GameObject Taksi;
    private bool kartAlindi = false;


    public GameObject ArabaAnahtari;
    public GameObject KazanmaPaneli;
    private bool arabaAnahtariAlindi = false;

    private GameObject tutulanNesne = null;

    private bool rituelBaslatildi = false;
    public int rituelSayac = 0;
    private bool rituelTamamlandi = false;
    public GameObject RituelMerkezi;

    public GameObject RituelNesne1;
    public GameObject RituelNesne2;
    public GameObject RituelNesne3;
    public GameObject RituelNesne4;


    public TMP_Text SayacText;
    private bool sayacBasladi = false;
    private float sayacSuresi = 30f;
    private float sayacZamanlayici = 0f;


    void Start()
    {
        SayacText.gameObject.SetActive(false);
        RituelMerkezi.SetActive(false);

        RituelNesne1.SetActive(true);
        RituelNesne2.SetActive(false);
        RituelNesne3.SetActive(false);
        RituelNesne4.SetActive(false);

        Canavar.SetActive(false);

        ArabaAnahtari.SetActive(false);
        if (KazanmaPaneli != null)
            KazanmaPaneli.SetActive(false);

    }


    void Update()
    {
        Vector3 ileri = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        // Sayaç
        if (sayacBasladi)
        {
            sayacZamanlayici -= Time.deltaTime;
            int kalan = Mathf.CeilToInt(sayacZamanlayici);
            SayacText.text = "Kalan süre: " + kalan + " sn";

            if (!SayacText.gameObject.activeSelf)
                SayacText.gameObject.SetActive(true);

            if (sayacZamanlayici <= 0)
            {
                sayacBasladi = false;
                SayacText.gameObject.SetActive(false);
                RituelMerkezi.SetActive(false);
                UyariTextComponent.gameObject.SetActive(true);
                UyariTextComponent.text = "Sayaç süresi doldu!";
                CancelInvoke("UyariyiKapat");
                Invoke("UyariyiKapat", 2f);
            }
        }


        if (tutulanNesne == null)
        {
            if (Physics.SphereCast(transform.position, 0.4f, ileri, out hit, Mesafe))
            {
                GameObject hedef = hit.collider.gameObject;

                // Tag'e göre etkileşim mesajı göster
                if (hedef.CompareTag("Fener") || hedef.CompareTag("Key") ||
                    hedef.CompareTag("Kart") || hedef.CompareTag("Not1") || hedef.CompareTag("Not2") || hedef.CompareTag("Not3") ||
                                                                           hedef.CompareTag("ArabaAnahtari") || 
                                                                           hedef.CompareTag("RituelNesne1") ||
                                                                           hedef.CompareTag("RituelNesne2") ||
                                                                           hedef.CompareTag("RituelNesne3") ||
                                                                           hedef.CompareTag("RituelNesne4"
                    ))
                {
                    EtkilesimText.gameObject.SetActive(true);
                    EtkilesimText.text = "[E] Al";
                    CancelInvoke("EtkilesimiKapat");
                    Invoke("EtkilesimiKapat", 0.1f);
                }
                else if (hedef.CompareTag("Kapı"))
                {
                    if (!isLocked)
                    {
                        EtkilesimText.gameObject.SetActive(true);
                        EtkilesimText.text = "[E] Kapıyı Aç";
                        CancelInvoke("EtkilesimiKapat");
                        Invoke("EtkilesimiKapat", 0.1f);
                    }
                    else
                    {
                        EtkilesimText.gameObject.SetActive(false); // anahtar alınmadıysa gösterme
                    }
                }
                else if (hedef.CompareTag("Kulube"))
                {
                    if (kartAlindi)
                    {
                        EtkilesimText.gameObject.SetActive(true);
                        EtkilesimText.text = "[E] Telefonu Kullan";
                        CancelInvoke("EtkilesimiKapat");
                        Invoke("EtkilesimiKapat", 0.1f);
                    }
                    else
                    {
                        EtkilesimText.gameObject.SetActive(false); // kart yoksa gösterme
                    }
                }
                else if (hedef.CompareTag("SayacKutusu"))
                {
                    EtkilesimText.gameObject.SetActive(true);
                    if (rituelSayac == 4)

                    {
                        EtkilesimText.text = "[E] Etkileşim";
                        CancelInvoke("EtkilesimiKapat");
                        Invoke("EtkilesimiKapat", 0.1f);
                    }
                    else
                    {
                        EtkilesimText.text = "";
                    }
                }

                else if (hedef.CompareTag("Taksi"))
                {
                    EtkilesimText.gameObject.SetActive(true);

                   
                        EtkilesimText.text = "[E] Kaç!";
                    CancelInvoke("EtkilesimiKapat");
                    Invoke("EtkilesimiKapat", 0.1f);

                }

                else if (hedef.CompareTag("RituelNokta1"))
                {
                    EtkilesimText.gameObject.SetActive(true);
                    EtkilesimText.text = "Ritüel Noktası";
                    CancelInvoke("EtkilesimiKapat");
                    Invoke("EtkilesimiKapat", 0.1f);
                }

                else
                {
                    EtkilesimText.gameObject.SetActive(false);
                }


                // Fener alma
                if (hedef.CompareTag("Fener") && Input.GetKeyDown(KeyCode.E))
                {
                    El_Feneri.SetActive(true);
                    Destroy(hedef);
                }

                // Kapı
                if (hedef.CompareTag("Kapı") && Input.GetKeyDown(KeyCode.E))
                {
                    if (isLocked)
                    {
                        UyariTextComponent.text = "Kapı kilitli!";
                        CancelInvoke("EtkilesimiKapat");
                        Invoke("EtkilesimiKapat", 2f);
                        return;
                    }

                    if (!isOpen)
                    {
                        doorObject.DOLocalRotate(new Vector3(0, 20, 0), 1f).SetEase(Ease.OutQuad);
                        isOpen = true;
                    }
                    else
                    {
                        doorObject.DOLocalRotate(new Vector3(0, -90, 0), 1f).SetEase(Ease.OutQuad);
                        isOpen = false;
                    }
                }

                // Anahtar
                if (hedef.CompareTag("Key") && Input.GetKeyDown(KeyCode.E))
                {
                    isLocked = false;
                    Destroy(hedef);
                }

                // Not
                if ((hedef.CompareTag("Not1") || hedef.CompareTag("Not2") || hedef.CompareTag("Not3")) && Input.GetKeyDown(KeyCode.E))
                {
                    string notID = hedef.tag;
                    Sprite ilgiliNot = null;

                    if (notID == "Not1") ilgiliNot = Note1;
                    else if (notID == "Not2") ilgiliNot = Note2;
                    else if (notID == "Not3") ilgiliNot = Note3;

                    if (ilgiliNot != null && !notEnvanteri.ContainsKey(notID))
                    {
                        notEnvanteri.Add(notID, ilgiliNot);
                        notSirasi.Add(notID);
                        hedef.SetActive(false);
                    }

                    if (notEnvanteri.ContainsKey(notID))
                    {
                        aktifNotIndex = notSirasi.IndexOf(notID);
                        not_Image.sprite = notEnvanteri[notID];
                        NoteScreen.SetActive(true);
                    }
                }



                // Kart
                if (hedef.CompareTag("Kart") && Input.GetKeyDown(KeyCode.E))
                {

                    kartAlindi = true;
                    Destroy(hedef);
                    UyariTextComponent.gameObject.SetActive(true);
                    UyariTextComponent.text = "Taksi kartı, bir an önce telefon kulüesine gidip taksiyi çağırmalıyım!";
                    CancelInvoke("UyariyiKapat");
                    Invoke("UyariyiKapat", 4f);

                }

                // Kulübe

                    if (hedef.CompareTag("Kulube") && Input.GetKeyDown(KeyCode.E))
                    {
                        if (kartAlindi)
                        {
                            Taksi.SetActive(true);
                            Canavar.SetActive(true);
                        if (canavarAktifSes != null)
                        {
                            canavarAktifSes.Play();
                        }

                        UyariTextComponent.gameObject.SetActive(true);
                        UyariTextComponent.text = "Bu ses de ne!";
                        CancelInvoke("UyariyiKapat");
                        Invoke("UyariyiKapat", 4f);
                        GorevTextComponent.gameObject.SetActive(true);
                        GorevTextComponent.text = "Gorev: Taksiye bin ve kaç!";

                        if (FakeCanavar != null) FakeCanavar.SetActive(false);




                            kartAlindi = false;

                        
                    }
                        else
                        {
                        UyariTextComponent.gameObject.SetActive(true);
                        UyariTextComponent.text = "Telefon kartın yok!";
                            CancelInvoke("UyariyiKapat");
                            Invoke("UyariyiKapat", 2f);
                        }
                    
                    }


                // Sayaç Kutusu
                if (hedef.CompareTag("SayacKutusu") && Input.GetKeyDown(KeyCode.E))
                {
                    if (!rituelTamamlandi)
                    {
                        Debug.Log("Ritüel tamamlanmalı.");
                    }
                    else
                    {
                        if (!sayacBasladi)
                        {
                            sayacBasladi = true;
                            sayacZamanlayici = sayacSuresi;
                            Debug.Log("Sayaç başladı. 30 saniye içinde canavarı merkeze getir.");
                            RituelMerkezi.SetActive(true); // burayı sadece tetikleme için aktifleştiriyorsan burada kalabilir
                        }
                        else
                        {
                            Debug.Log("Sayaç zaten çalışıyor.");
                        }
                    }
                }


                // Ritüel nesnesini eline alma
                if ((hedef.CompareTag("RituelNesne1") || hedef.CompareTag("RituelNesne2") ||
                     hedef.CompareTag("RituelNesne3") || hedef.CompareTag("RituelNesne4")) && Input.GetKeyDown(KeyCode.E))
                {


                    tutulanNesne = hedef;
                    tutulanNesne.transform.SetParent(this.transform);
                    tutulanNesne.transform.localPosition = new Vector3(0, -0.3f, 1.5f); // kameranın önüne al
                    GorevTextComponent.gameObject.SetActive(true);
                    GorevTextComponent.text = "Görev: Rituel Noktasına Bırak";
                    tutulanNesne.GetComponent<Collider>().isTrigger = true;
                    tutulanNesne.GetComponent<Collider>().enabled = false;
                    if (tutulanNesne.GetComponent<Rigidbody>()) Destroy(tutulanNesne.GetComponent<Rigidbody>());
                }

                // Araba Anahtarı alma
                if (hedef.CompareTag("ArabaAnahtari") && Input.GetKeyDown(KeyCode.E))
                {
                    arabaAnahtariAlindi = true;
                    Destroy(hedef);
                    Debug.Log("Araba anahtarı alındı.");
                }

                // Taksi ile etkileşim
                if (hedef.CompareTag("Taksi") && Input.GetKeyDown(KeyCode.E))
                {
                    if (arabaAnahtariAlindi)
                    {
                        SceneManager.LoadScene(4); // Hazırladığın sahne adı neyse onu yaz
                    }
                    else
                    {
                        UyariTextComponent.gameObject.SetActive(true);
                        UyariTextComponent.text = "Araba anahtarı canavarda olmalı. Onu yok etmeden burdan kaçış yok!";
                        CancelInvoke("UyariyiKapat");
                        Invoke("UyariyiKapat", 8f);
                        GorevTextComponent.gameObject.SetActive(true);
                        GorevTextComponent.text = "Kayıp Nesne : Oyun Konsolu";

                    }
                }



            }
        }
        else
        {
            // Ritüel noktasına bırakma
            if (Physics.Raycast(transform.position, ileri, out hit, Mesafe))
            {
                GameObject hedef = hit.collider.gameObject;

                if (hedef.CompareTag("RituelNokta1"))
                {

                    RituelNokta noktaScript = hedef.GetComponent<RituelNokta>();
                    if (noktaScript != null && !noktaScript.isOccupied && (tutulanNesne.CompareTag("RituelNesne1") ||
                                                                           tutulanNesne.CompareTag("RituelNesne2") ||
                                                                           tutulanNesne.CompareTag("RituelNesne3") ||
                                                                           tutulanNesne.CompareTag("RituelNesne4")))

                    {
                        tutulanNesne.transform.SetParent(null);


                        // NESNEYİ KUTUNUN ÜSTÜNE BIRAK
                        Vector3 yukariPozisyon = hedef.transform.position + new Vector3(0, 1f, 0);
                        tutulanNesne.transform.position = yukariPozisyon;

                        tutulanNesne.transform.rotation = hedef.transform.rotation;
                        tutulanNesne.GetComponent<Collider>().enabled = true;
                        tutulanNesne = null;

                        rituelSayac++;

                        noktaScript.isOccupied = true;

                        if (rituelSayac == 1) 
                        {

                            RituelNesne2.SetActive(true);
                            GorevTextComponent.gameObject.SetActive(true);
                            GorevTextComponent.text = "Kayıp Nesne : Defter";

                        }
                        else if (rituelSayac == 2)
                        {

                            RituelNesne3.SetActive(true);
                            GorevTextComponent.gameObject.SetActive(true);
                            GorevTextComponent.text = "Kayıp Nesne : Keser";

                        }
                        else if (rituelSayac == 3)
                        {

                            RituelNesne4.SetActive(true);
                            GorevTextComponent.gameObject.SetActive(true);
                            GorevTextComponent.text = "Kayıp Nesne : Maske";

                        }
                        else if (rituelSayac == 4)
                        {

                            GorevTextComponent.gameObject.SetActive(true);
                            GorevTextComponent.text = "Görev: Jeneratoru calıstır ve Statua'yı yok et!";

                        }

                    }
                }
            }

            // Q ile bırakma
            if (Input.GetKeyDown(KeyCode.Q))
            {
                tutulanNesne.transform.SetParent(null);
                tutulanNesne.transform.position = transform.position + transform.forward * 1.2f;
                tutulanNesne.GetComponent<Collider>().isTrigger = false;
                tutulanNesne.GetComponent<Collider>().enabled = true;

                if (!tutulanNesne.GetComponent<Rigidbody>())
                    tutulanNesne.AddComponent<Rigidbody>();

                tutulanNesne = null;
            }
        }

        // Not ekranını kapatma
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            NoteScreen.SetActive(false);
            not_Image.sprite = null;
        }

        // Ritüel tamamlandığında merkezi aktif et
        if (rituelSayac >= 4 && !rituelTamamlandi)
        {
            rituelTamamlandi = true;
        }

        //NOTLARININ BAKILMASI

        // N ile son okunan notu aç
        if (Input.GetKeyDown(KeyCode.N) && notSirasi.Count > 0)
        {
            string aktifID = notSirasi[aktifNotIndex];
            not_Image.sprite = notEnvanteri[aktifID];
            NoteScreen.SetActive(true);
        }

        // Sağ ok → sonraki nota geç
        if (Input.GetKeyDown(KeyCode.RightArrow) && NoteScreen.activeSelf && notSirasi.Count > 0)
        {
            aktifNotIndex = (aktifNotIndex + 1) % notSirasi.Count;
            string aktifID = notSirasi[aktifNotIndex];
            not_Image.sprite = notEnvanteri[aktifID];
        }

        // Sol ok → önceki nota geç
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NoteScreen.activeSelf && notSirasi.Count > 0)
        {
            aktifNotIndex = (aktifNotIndex - 1 + notSirasi.Count) % notSirasi.Count;
            string aktifID = notSirasi[aktifNotIndex];
            not_Image.sprite = notEnvanteri[aktifID];
        }


    }

    void EtkilesimiKapat()
    {

        EtkilesimText.text = "";
        EtkilesimText.gameObject.SetActive(false);
    }


    void UyariyiKapat()
    {
        UyariTextComponent.text = "";
        UyariTextComponent.gameObject.SetActive(false);

        EtkilesimText.text = "";
        EtkilesimText.gameObject.SetActive(false);
    }
}
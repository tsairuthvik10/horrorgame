using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class screen : MonoBehaviour
{
    Texture2D screenCap;
    Texture2D border;
    public bool shot = false;
    public Camera camera1;
    public int count = 0;
    public GameObject winShow;
    public GameObject phoneText;
    public GameObject bedText;
    public RawImage image;
    //public RawImage uiImage;
    public GameObject panelPhoto;
    public GameObject pewd;
    public bool ring = false;
    public bool isRinging = false;
    public GameObject animator;
    //public AudioSource ringtone;
    public GameObject phone;
    public GameObject phoneMessage;
    public bool shadowMove = false;
    public Transform target;
    public GameObject Shadow;
    public float speed = 2.0f;

    // Use this for initialization
    void Start()
    {
        screenCap = new Texture2D(600, 400, TextureFormat.RGB24, false); // 1
        border = new Texture2D(2, 2, TextureFormat.ARGB32, false); // 2
        border.Apply();
        winShow.GetComponent<Text>().enabled = false;
        phoneText.GetComponent<Text>().enabled = false;
        phoneMessage.GetComponent<Text>().enabled = false;
        panelPhoto.SetActive(false);
        pewd.SetActive(false);
       
        // ringtone = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera1.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
           // Debug.DrawLine(ray.origin, hit.point, Color.red);
            if (hit.transform.tag == "click")
            {
                winShow.GetComponent<Text>().enabled = true;
                //winShow.GetComponent<Text>().text = "take a picture";
               // Debug.Log("message received");
               
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {

                    // 3
                    StartCoroutine("Capture");
                   // panelPhoto.SetActive(true);
                    pewd.SetActive(false);
                    //Capture();
                    // uiImage.enabled = true;
                }
            }
            else
            {
                winShow.GetComponent<Text>().enabled = false;
               // Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
            }

            if (hit.transform.tag == "phone")
            {
                phoneText.GetComponent<Text>().enabled = true;
                phoneText.GetComponent<Text>().text = "see phone";
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    pewd.SetActive(false);
                    if(isRinging == true)
                    {
                        phoneMessage.GetComponent<Text>().enabled = true;
                        phoneMessage.GetComponent<Text>().text = "Monika: I will come back home late";
                    }
                   
                }
            }
            else
            {
                phoneText.GetComponent<Text>().enabled = false;
                phoneMessage.GetComponent<Text>().enabled = false;
                // Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
            }

            if (hit.transform.name == "Bed_Sheet")
            {
                bedText.GetComponent<Text>().enabled = true;
                bedText.GetComponent<Text>().text = "take picture";
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {

                    // 3
                    StartCoroutine("ClueBed");
                    ring = true;
                    count++;
                    //  panelPhoto.SetActive(true);
                    //Capture();
                }
            }
            else
            {
                bedText.GetComponent<Text>().enabled = false;
                // Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
            }


        }
        else
        {
            winShow.GetComponent<Text>().enabled = false;
            phoneText.GetComponent<Text>().enabled = false;
            bedText.GetComponent<Text>().enabled = false;
            phoneMessage.GetComponent<Text>().enabled = false;
        }

        if (Input.GetButtonDown("panel"))
        {
            panelPhoto.SetActive(true);
        }


        if (Input.GetButtonDown("closePanel"))
        {
            panelPhoto.SetActive(false);
            
            if (ring == true && count == 1)
            {
                phone.GetComponent<AudioSource>().Play();
                ring = false;
                isRinging = true;
            }
        }

        if(shadowMove == true)
        {
            Animator animator1 = animator.GetComponent<Animator>();
            animator1.SetFloat("Speed", 1.0f);
            float step = speed * Time.deltaTime;
            Shadow.transform.LookAt(target);
            Shadow.transform.position = Vector3.MoveTowards(Shadow.transform.position, target.position, step);
            
        }
        
    }

    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(200, 100, 900, 2), border, ScaleMode.StretchToFill); // top
        //GUI.DrawTexture(new Rect(200, 700, 900, 2), border, ScaleMode.StretchToFill); // bottom
        //GUI.DrawTexture(new Rect(200, 100, 2, 600), border, ScaleMode.StretchToFill); // left
        //GUI.DrawTexture(new Rect(1100, 100, 2, 600), border, ScaleMode.StretchToFill); // right

        if (shot)
        {
            GUI.DrawTexture(new Rect(10, 10, 60, 40), screenCap, ScaleMode.StretchToFill);
        }
    }

    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
      //  uiImage.enabled = false;
        screenCap.ReadPixels(new Rect(198, 98, 598, 398), 0, 0);
        screenCap.Apply();
        // count++;
        //Debug.Log(count);

        // Encode texture into PNG
        byte[] bytes = screenCap.EncodeToPNG();
        //Object.Destroy(screenCap);

        // For testing purposes, also write to a file in the project folder
       
            File.WriteAllBytes(Application.dataPath + "/Resources/SavedScreen1.png", bytes);
            shot = true;

          //  WWW www = new WWW("/Resources/SavedScreen1.png");
            Texture2D texture = new Texture2D(750, 500);
            texture.LoadImage(bytes);
            image.texture = texture;
            //while (!www.isDone)
            //    ;
            //www.LoadImageIntoTexture((Texture2D)GetComponent<Renderer>().material.mainTexture);
    
    }

    IEnumerator ClueBed()
    {
        yield return new WaitForEndOfFrame();
        pewd.SetActive(true);
        //uiImage.enabled = false;
        screenCap.ReadPixels(new Rect(198, 98, 598, 398), 0, 0);
        screenCap.Apply();
       // count++;
      //  Debug.Log(count);

        // Encode texture into PNG
       // byte[] bytes = screenCap.EncodeToPNG();
        //Object.Destroy(screenCap);

        // For testing purposes, also write to a file in the project folder
        
           // File.WriteAllBytes(Application.dataPath + "/Resources/SavedScreen1.png", bytes);
            shot = true;

         //   WWW www = new WWW("/Resources/pewdsucks.jpg");
          //  Texture2D texture = new Texture2D(750, 500);
          //  texture.LoadImage(bytes);
          //  image.texture = texture;
            //while (!www.isDone)
            //    ;
            //www.LoadImageIntoTexture((Texture2D)GetComponent<Renderer>().material.mainTexture);

        

           
      


    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "ghost")
        {
            Debug.Log("entered");
            shadowMove = true;
            
        }
        if (collision.gameObject.tag == "disappear")
        {
            // Shadow.SetActive(false);
            SceneManager.LoadScene(1);

        }
    }

  




}
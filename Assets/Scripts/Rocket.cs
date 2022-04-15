using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip lostSound;
    [SerializeField] ParticleSystem thrust;
    [SerializeField] ParticleSystem win;
    [SerializeField] ParticleSystem expl;
    public Button Left, Right, Up;
    enum State { Alive, Dying, Transcending };
    State state;
    public static bool clicked = false;
    // Start is called before the first frame update



    public void LateUpdate()
    {
        clicked = false;
    }
    public void Click()
    {
        clicked = true;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                state = State.Alive;

                break;
            case "Finish":
                Success();
                break;

            default:
                Death();

                break;



        }

    }

    private void Death()
    {
        state = State.Dying;
        expl.Play();
        audioSource.PlayOneShot(lostSound);
        Invoke("BackToCurrentScene", 1f);
    }

    private void Success()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(winSound);
        win.Play();
        Invoke("LoadNewScene", 1f);
    }

    private void BackToCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        if (Input.GetKey(KeyCode.A) )
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed); //z axis
        }
        else
        if (Input.GetKey(KeyCode.D) )
        {
            transform.Rotate(-Vector3.forward, Time.deltaTime * rotationSpeed);

        }
        rigidBody.freezeRotation = false; // resume physics control


    }

    public void Thrust()
    {
        if (Input.GetKey(KeyCode.Space) || (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() ) )
        {
            rigidBody.AddRelativeForce(Vector3.up);
            
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                thrust.Play();

            }
           
        }
        else
        {
            thrust.Stop();
            audioSource.Stop();
        }
    }
  
    
}

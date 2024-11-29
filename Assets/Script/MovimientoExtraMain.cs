using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovimientoExtraMain : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip moveSound;
    public Animator animacionesMovimiento;

    private SpriteRenderer spriteRenderer;
    public Sprite leapSprite;
    public Sprite idleSprite;
    public Sprite deathSprite;

    public GameObject ixtab;

    private Vector3 spawnPosition;

    private int lives;
    public Text livesText;

    public Text timeText;
    private int time;

    private int llaves;
    public Text llavesText;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource.GetComponent<AudioSource>();
        }

        lives = 3;
        livesText.GetComponent<Text>();

        llaves = 0;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
    }

    void Movimiento()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) 
        {
            Move(Vector3.up);
            animacionesMovimiento.SetTrigger("Frente");
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.down);
            animacionesMovimiento.SetTrigger("Frente");
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector3.right);
            animacionesMovimiento.SetTrigger("Derecha");
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector3.left);
            animacionesMovimiento.SetTrigger("Izquierda");
        }
    }
     private void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        if (!audioSource.isPlaying && moveSound != null)
        {
            audioSource.PlayOneShot(moveSound);
        }

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D plataforma = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Plataforma"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        if (barrier != null)
        {
            return;
        }

        if (plataforma != null)
        {
            transform.SetParent(plataforma.transform);
        }

        else
        {
            transform.SetParent(null);
        }

        if (obstacle != null && plataforma == null)
        {
            transform.position = destination;
            Death();
        }

        else
        {
            StartCoroutine(Leap(destination));
        }
    }

    private void Death()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deathSprite;
        enabled = false;

        Died();
    }

    private void Died()
    {
        livesText.text = lives.ToString();
        lives--;

        if(lives>0)
        {
            Respawn();

            StopAllCoroutines();
            StartCoroutine(Timer(30));
        }
        else
        {
            SceneManager.LoadScene(7);
        }
    }

    private IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;

        spriteRenderer.sprite = leapSprite;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        spriteRenderer.sprite = idleSprite;
    }

    public void Respawn()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        spriteRenderer.sprite = idleSprite;
        gameObject.SetActive(true);
        enabled = true;
    }

    private IEnumerator Timer(int duration)
    {
        time = duration;

        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            timeText.text = time.ToString();
        }

        Death();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null)
        {
            Death();
        }

        if (other.tag == "Player")
        {
            enabled = true;
            ixtab.SetActive(false);
            llaves++;
            llavesText.text = llaves.ToString();

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex > 6)
            {
                nextSceneIndex = 3;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

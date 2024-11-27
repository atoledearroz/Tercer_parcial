using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ixtab : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip moveSound;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private SpriteRenderer spriteRenderer;
    public Sprite leapSprite;
    public Sprite IdleSprite;
    public Sprite deathSprite;

    private Vector3 spawnPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Move(Vector3.up);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
           // transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Move(Vector3.down);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           // transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Move(Vector3.left);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           // transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            Move(Vector3.right);
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
        spriteRenderer.sprite = IdleSprite;
    }

    public void Death()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deathSprite;
        enabled = false;

        FindObjectOfType<GameManager>().Died();
    }

    public void Respawn()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        spriteRenderer.sprite = IdleSprite;
        gameObject.SetActive(true);
        enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null)
        {
            Death();
        }
    }

}

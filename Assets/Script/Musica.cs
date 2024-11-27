using UnityEngine;

public class Musica : MonoBehaviour
{
    private static Musica instance;

    // Start is called before the first frame update
    void Start()
    {
       GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

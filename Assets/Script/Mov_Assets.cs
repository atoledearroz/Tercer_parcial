using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Assets : MonoBehaviour
{

    public Vector2 direction = Vector2.right;

    public float speed = 1f;

    public int size = 1;

    private Vector3 leftEdge;

    private Vector3 rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = new Vector3(-6.46999979f, 6.42000008f, -0.121104226f);
        rightEdge = new Vector3(6.55999994f, 6.42000008f, -0.121104226f);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
        }

        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            Vector3 position = transform.position;
            position.x = rightEdge.x + size;
            transform.position = position;
        }

        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}

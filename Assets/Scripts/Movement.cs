using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Name is obvious
public class Movement : MonoBehaviour
{
    [Header("Ball configuration")]
    [SerializeField]
    private Rigidbody2D rb;

    public float force;

    [Header("Movement Configuration")]
    [SerializeField]
    private float timeToMove;

    [SerializeField]
    private float maxForce;

    [SerializeField]
    private float movementForce;

    [Header("Audio")]
    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip movingSound;

    private UnityEngine.UI.Text distanceCounter;

    private void Start()
    {
        distanceCounter = GameObject.FindGameObjectWithTag("distanceCounter").GetComponent<UnityEngine.UI.Text>();
    }

    private void Update()
    {
        distanceCounter.text = $"Distance: {Mathf.Round(transform.position.y * 100) / 100} cm";
        if(Mathf.Sign(transform.position.y) == -1 && Mathf.Round(transform.position.y * 100) / 100 != 0)
        {
            distanceCounter.color = Color.red;
        }
        else if(Mathf.Round(transform.position.y * 100) / 100 == 0)
        {
            distanceCounter.color = Color.blue;
        }
        else
        {
            distanceCounter.color = Color.white;
        }

        if(Time.timeScale == 1) {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(moveX());
            } else
            {
                StopAllCoroutines();
            }

            if(Input.GetMouseButtonUp(0))
            {
                //print("Up");
                GetComponent<AudioSource>().Stop();
                StopAllCoroutines();
            }

            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                moveMobile(-1);
            } else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                moveMobile(1);
            } else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                moveDown();
            }
        }
    }

    private void FixedUpdate()
    {
        if(!MouseOverConfiguration.isOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && Time.timeScale == 1)
        { 
            if (rb.velocity.y < maxForce)
            {
                Exert(1);

                GetComponent<AudioSource>().clip = jumpSound;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                Exert(-1);
            }
        }
    }


    private IEnumerator moveX()
    {
        yield return new WaitForSeconds(timeToMove);
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 movementPosition = new Vector3(worldpos.x, transform.position.y, 0);

        GetComponent<AudioSource>().clip = movingSound;
        GetComponent<AudioSource>().Play();

        transform.position = movementPosition;
    }

    private void Exert(int mul)
    {
        rb.velocity += new Vector2(0, force) * mul;
    }

    public void moveMobile(short direction)
    {
        if(Time.timeScale == 1) {
        try
        {
            transform.position += new Vector3(direction * movementForce, 0, 0);
        }
        catch (MissingReferenceException) {}
        }
        
    }

    public void moveDown()
    {
        if(Time.timeScale == 1) transform.position -= new Vector3(0, 0.05f, 0);
    }
}

using System.Collections;
using UnityEngine;

public class ShootDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyer;

    [SerializeField]
    private float speed;

    private Transform newDestroyer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }

        if (newDestroyer is not null)
        {
            try
            {
                newDestroyer.position += new Vector3(0, speed);
            } catch(MissingReferenceException) {}
        }
    }

    public void Shoot()
    {
        newDestroyer = Instantiate(destroyer, transform.position + Vector3.up * 0.5f, destroyer.transform.rotation).GetComponent<Transform>();
        Invoke("DestroyDestroyer", 5);
    }

    private void DestroyDestroyer()
    {
        try
        {
            Destroy(newDestroyer.gameObject);
        } catch(MissingReferenceException) {}
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyer;

    [SerializeField]
    private float speed;

	[SerializeField]
	private Image shootButton;

    private Transform newDestroyer;

	private bool cooldownActive = false;

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
		if(!cooldownActive)
		{
			newDestroyer = Instantiate(destroyer, transform.position + Vector3.up * 0.5f, destroyer.transform.rotation).GetComponent<Transform>();
			StartCoroutine(Cooldown());
			Invoke("DestroyDestroyer", 5);
		}
    }

    private void DestroyDestroyer()
    {
        try
        {
            Destroy(newDestroyer.gameObject);
        } catch(MissingReferenceException) {}
    }

	private IEnumerator Cooldown()
	{
		cooldownActive = true;
		shootButton.color = Color.red;
		yield return new WaitForSeconds(1);
		cooldownActive = false;
		shootButton.color = Color.white;
	}
}
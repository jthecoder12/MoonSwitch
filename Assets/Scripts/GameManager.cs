using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variables
    [SerializeField]
    private GameObject ball;

    [Header("Camera management")]
    [SerializeField]
    private bool isMainSceneCamera;

    [Header("Item configuration")]
    [SerializeField]
    private byte itemCode;

    [Header("UI configuration")]
    [SerializeField]
    private InputField moonRockField;

	[SerializeField]
	private GameObject moonrockWarning;

    [Header("Purchase management")]
    [SerializeField]
    private GameObject purchaseScreen;

    [SerializeField]
    private Texture itemImage;

	public Text equipText;

	public bool isEquipable = false;

    [Header("Item Images")]
    public GameObject soccerBallImageRaw;

    [Header("Game Object Configuration")]
    [SerializeField]
    private GameObject closeVariable;

    [SerializeField]
    private Movement movementBall;

    [SerializeField]
    private short directionBall;

    [SerializeField]
    private int moveTimes;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private GameObject optionsMenu;

	[SerializeField]
	private Lava[] lavas;

    [Header("Ball configuration")]
    [SerializeField]
    private GameObject soccerBall;

    private List<string> difficulties = new List<string>();

    [Header("Misc")]
    [SerializeField]
    [Dropdown("difficulties")]
    private string difficulty;

    private Text disColButton;

    private short currentScore;

    public static readonly Dictionary<byte, ShopItem> items = new Dictionary<byte, ShopItem>();

    public static List<byte> itemsBought = new List<byte>();

    private static GameObject soccerBallImage;

    private static byte currentEquipedBall;

    // Do stuff in the beginning;
    private void Start()
    {
        // Set the static variable to the raw image
        soccerBallImage = soccerBallImageRaw;

        // Check equiped ball
        if(PlayerPrefs.HasKey("CurrentEquipedBall"))
        {
            // print($"Current ball: {PlayerPrefs.GetInt("CurrentEquipedBall")}");
            currentEquipedBall = (byte)PlayerPrefs.GetInt("CurrentEquipedBall");
        } else
        {
            print("No ball currently equiped");
            currentEquipedBall = 0;
        }

        if(isMainSceneCamera && PlayerPrefs.HasKey("CurrentEquipedBall"))
        {
            if(PlayerPrefs.GetInt("CurrentEquipedBall") != 0)
            {
                GameObject.FindGameObjectWithTag("DefaultBall").SetActive(false);
            }

            if(PlayerPrefs.GetInt("CurrentEquipedBall") == 0xA)
            {
                soccerBall.SetActive(true);
            }
        }

        // Object specific things
        if(CompareTag("shopButton") || CompareTag("purchaseButton"))
        {
            InitArray();
        }

        if(CompareTag("backbutton"))
        {
            // print(PlayerPrefs.GetInt("ItemsBoughtCount"));

            for (byte i = 0; i < PlayerPrefs.GetInt("ItemsBoughtCount"); i++)
            {
                itemsBought.Add(System.Convert.ToByte(PlayerPrefs.GetInt($"BoughtItem_{i}")));
            }

			itemsBought.Remove(0);
        }

        // Developer console things
        try
        {
            disColButton = GameObject.Find("DevConsole/Image/DisableCollision/Text (Legacy)").GetComponent<Text>();
        }
        catch (System.NullReferenceException) { }
    }

    // Scene Button Methods
    public void PlayButton()
    {
		GlobalValues.musicPosition = 0;

		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = true;
		}

		SceneManager.LoadScene(1);
    }

    public void ShopButton()
    {
		GlobalValues.musicPosition = 0;

		SceneManager.LoadScene(2);
    }

    // Developer console method
    public void DisableCollision()
    {
        if(ball.GetComponent<CircleCollider2D>().enabled == true)
        {
            ball.GetComponent<CircleCollider2D>().enabled = false;

            disColButton.text = "Enable Collision";
        } else
        {
            ball.GetComponent<CircleCollider2D>().enabled = true;

            disColButton.text = "Disable Collision";
        }
    }

    // More button methods
    public void BackToHome()
    {
		UpdateMusicTime();

		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = true;
		}

		SceneManager.LoadScene(0);
    }

	public void BackToHomeFromNonOriginalMusic()
	{
		Time.timeScale = 1;

		GlobalValues.musicPosition = 0;

		SceneManager.LoadScene(0);
	}

    // Item engine (purchase/equip) also a button method
    public void MakePurchase()
    {
        // print($"Name: {items[itemCode].GetItemName()} Price: {items[itemCode].GetPrice()}");
        // Check if we are actually trying to make a purchase
		if(isEquipable)
		{
			// This means we are trying to equip a ball
			print($"Equiping: Name: {items[itemCode].GetItemName()} Price: {items[itemCode].GetPrice()}");
			equipText.gameObject.SetActive(true);
			equipText.text = $"Equipped {items[itemCode].GetItemName()}";
			PlayerPrefs.SetInt("CurrentEquipedBall", itemCode);
			PlayerPrefs.Save();

			return;
		} else
		{
			currentScore = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalValues>().score;

			if (currentScore >= items[itemCode].GetPrice())
			{
				purchaseScreen.SetActive(true);
				GameObject.FindGameObjectWithTag("confirmMessage").GetComponent<Text>().text = $"Are you sure you want to buy {items[itemCode].GetItemName()}?";
				GameObject.FindGameObjectWithTag("purchaseButton").GetComponent<GameManager>().itemCode = itemCode;
				GameObject.FindGameObjectWithTag("purchaseButton").GetComponent<GameManager>().purchaseScreen = purchaseScreen;
				GameObject.FindGameObjectWithTag("itemImage").GetComponent<RawImage>().texture = itemImage;
			}
			else
			{
				// print("Not enough moonrocks");

				moonrockWarning.SetActive(true);
			}
		}
    }

    // Set default ball
    public void SetDefaultBall()
    {
		equipText.gameObject.SetActive(true);
		equipText.text = "Equipped Default Ball";

        PlayerPrefs.SetInt("CurrentEquipedBall", 0);
        PlayerPrefs.Save();
    }

    // The method that will be called when trying to confirm a purchase
    public void ConfirmPurchase()
    {
        currentScore = short.Parse(PlayerPrefs.GetInt("Score").ToString());
        currentScore -= items[itemCode].GetPrice();

        // print("Score: " + items[itemCode].GetPrice());

        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.Save();

        MoonRockCounter.UpdateMoonrockCounter();

        print(itemsBought.ToArray());

        itemsBought.Add(itemCode);

        SaveItemsBought(true);

        purchaseScreen.SetActive(false);
    }

    // More developer console things
    public void AddMoonRocks()
    {
        short currentScore = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalValues>().score;

        currentScore += short.Parse(moonRockField.text);

        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.Save();

        MoonRockCounter.UpdateMoonrockCounter();
    }

    // Impossible in mobile but no one will use this feature I don't know why it is here
    public void ResetScore()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();

            // print(PlayerPrefs.GetInt("Score"));
            
            MoonRockCounter.UpdateMoonrockCounter();
        }
    }

    // Even more button methods
    public void CreditsScreen()
    {
		UpdateMusicTime();
        SceneManager.LoadScene(3);
    }

    public void ViewOFLScreen()
    {
		UpdateMusicTime();
		SceneManager.LoadScene(5);
    }

    // Useful method
    public void CloseVariable()
    {
        closeVariable.SetActive(false);
    }

    // More methods
    public void OpenCustomizer()
    {
        SceneManager.LoadScene(4);
    }

    // Initialize items
    public static void InitArray()
    {
        try {
            items.Add(0xA, new ShopItem("Soccer Ball", "Ball", 7, 0xA, soccerBallImage));
        } catch(System.ArgumentException){}

        for (byte i = 0; i < PlayerPrefs.GetInt("ItemsBoughtCount"); i++)
        {
            itemsBought.Add(System.Convert.ToByte(PlayerPrefs.GetInt($"BoughtItem_{i}")));
            /*
            try
            {
                print(itemsBought[0]);
            }
            catch (System.Exception)
            {
                Debug.LogError("Error with list");
            }
            */
        }

        itemsBought = itemsBought.Distinct().ToList();

        SaveItemsBought(false);

        // print(itemsBought.ToArray());
    }

    public void moveMobile()
    {
        movementBall.moveMobile(directionBall);
        for (int i = 0; i <= moveTimes; i++)
        {
            StartCoroutine(moveMobileTimes());
        }
    }

    public void ShootBullet()
    {
        try
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShootDestroyer>().Shoot();
        } catch(System.NullReferenceException) {}   
    }

    public void downMove()
    {
        try
        {
            movementBall.moveDown();
        } catch(MissingReferenceException) {}
    }

    public void SetDifficulty()
    {
        print(difficulty);
    }

	public void TogglePause()
	{
		if(!loseScreen.active)
		{
			if (Time.timeScale == 1)
			{
				pauseMenu.SetActive(true);
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = false;
				Time.timeScale = 0;
				DisableLava();
			}
			else
			{
				pauseMenu.SetActive(false);
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = true;
				Time.timeScale = 1;
				EnableLava();
			}
		}
	}

	public void OpenInstructions()
	{
		UpdateMusicTime();
		SceneManager.LoadScene(6);
	}

    // Save the items that you have currently bought
    private static void SaveItemsBought(bool debug)
    {
        PlayerPrefs.SetInt("ItemsBoughtCount", itemsBought.Count);

        for (byte i = 0; i < itemsBought.Count; i++)
        {
            PlayerPrefs.SetInt($"BoughtItem_{i}", System.Convert.ToInt32(itemsBought[i]));
            PlayerPrefs.Save();

            /*if(debug)
            {
                print(PlayerPrefs.GetInt($"BoughtItem_{i}"));
            }*/
        }

    }
    private IEnumerator moveMobileTimes()
    {
        yield return new WaitForSeconds(0.05f);
        movementBall.moveMobile(directionBall);
    }

	private void UpdateMusicTime()
	{
		GlobalValues.musicPosition = GameObject.FindGameObjectWithTag("MainCamera").
			GetComponent<AudioSource>().time;
	}

    private void Update()
    {
        if (CompareTag("MainCamera") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!loseScreen.active && Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.active)
                {
                    pauseMenu.SetActive(false);
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = true;
					Time.timeScale = 1;
					EnableLava();
                }
                else
                {
                    pauseMenu.SetActive(true);
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = false;
					Time.timeScale = 0;
					DisableLava();
                }
            }
        }
    }

	private void DisableLava()
	{
		foreach (Lava lava in lavas)
		{
			lava.SetEnabled(false);
		}
	}

	private void EnableLava()
	{
		foreach (Lava lava in lavas)
		{
			lava.SetEnabled(true);
		}
	}
}

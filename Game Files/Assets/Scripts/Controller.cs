using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    private float horizontalInput;
    public float speed = 5.0f;
    public SpriteRenderer PlayerSr;
    public GameObject RocketObject;
    [SerializeField]
    private Text topScore;
    public Text highScore;
    public Text midScore;
    private float maxHeight = 0.0f;
    public GameObject bullet;
    public GameObject player;
    public GameObject boss;
    public Button item1, item2, item3;
    public int levelReached;
    public bool isInvis = false, isRocket = false;
    public bool isDead = false, isFalling = true, noLife = false;
    public int coinlevel = 1, shieldlevel = 1, rocketlevel = 1;
    public int coin = 0;
    public int coinBoost = 1;
    public bool isInDanger = false;
    public Text coinText;
    private float _fireRate = 0.65f;
    private float _nextFire = 0.0f;
    int OverAllCurrentMaxScore = 0;


    public GameObject deathScreen, playerScoreScreen, winScreen;
    public Animator anim;
    Color color;

    private void Start()
    {
        color = PlayerSr.material.color;
        int temp1= PlayerPrefs.GetInt("doubleGold", 0);
        int temp2= PlayerPrefs.GetInt("shield", 0);
        int temp3= PlayerPrefs.GetInt("rocket", 0);

        if (temp1 == 0)
        {
            item1.interactable = false;
        }
        if (temp2 == 0)
        {
            item2.interactable = false;
        }

        if (temp3 == 0)
        {
            item3.interactable = false;
        }
    }

    private void FixedUpdate()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            horizontalInput = Input.acceleration.x;
            rb.velocity = new Vector2(horizontalInput * speed * 2, rb.velocity.y);
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }

        if (isRocket)
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 15, 0);
        }

    }

    private void Update()
    {
        float playerHeight = player.transform.position.y;   //Oyuncunun Yüksekliği

        if (playerHeight > maxHeight)   //Oyuncu Yüksekliği oyuncunun oyun içinde ulaştığı max yükseklikten büyükse
        {
            maxHeight = playerHeight;
            topScore.text = "Score: " + Mathf.RoundToInt(maxHeight * 10.0f);    //Sağ üst kösedeki skoru güncelle
        }

        else if (playerHeight < maxHeight - 20.0f && isDead == false)    //Oyuncu Max height den 20 birim aşağı düştüyse
        {
            isDead = true;
            fallState();
        }

        if (rb.velocity.y < -0.1)
        {
            isFalling = true;
            anim.SetBool("isFalling", true);
        }
        else
        {
            isFalling = false;
            anim.SetBool("isFalling", false);
        }


        edgeMove();


        if (!isInDanger)
        {
            PlayerSr.material.color = color;
        }
        if (isInDanger)
        {
            PlayerSr.material.color = Color.red;
            StartCoroutine(DangerCountdown());
        }

        if (!isRocket)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) || Input.touchCount > 0)
            {
                Shoot();
            }
        }
    }


    private void edgeMove()
    {
        if (player.transform.position.x < -2.8f)
        {
            player.transform.position = new Vector2(2.8f, player.transform.position.y);
        }
        if (player.transform.position.x > 2.8f)
        {
            player.transform.position = new Vector2(-2.8f, player.transform.position.y);
        }

    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
            _nextFire = Time.time + _fireRate;

        }
    }
    public void fallState()
    {
        int CurrentMaxScore = Mathf.RoundToInt(maxHeight * 10.0f);
        int OverAllCurrentMaxScore = Mathf.RoundToInt(PlayerPrefs.GetInt("maxScore", 0));

        float oldcoin = PlayerPrefs.GetInt("gold", 0); //Yeni Kazanılan Coinleri Ekle
        PlayerPrefs.SetInt("gold", (int)oldcoin + coin);   //Save new coin value to local

        if (CurrentMaxScore > OverAllCurrentMaxScore)
        {    //Yeni Bir Skor Kırıldıysa   
            PlayerPrefs.SetInt("maxScore", CurrentMaxScore); //Debug.Log("MaxScore:" + MaxScore + " Saved");
        }

        CurrentMaxScore = Mathf.RoundToInt(maxHeight * 10.0f);     //Update
        OverAllCurrentMaxScore = Mathf.RoundToInt(PlayerPrefs.GetInt("maxScore", 0)); //Update

        StartCoroutine(CallUpdate("Update", PlayerPrefs.GetString("username", "0"), PlayerPrefs.GetString("password", "0"), OverAllCurrentMaxScore.ToString(), PlayerPrefs.GetInt("gold", 0).ToString(), PlayerPrefs.GetInt("levelReached", 0).ToString()));


        OverAllCurrentMaxScore = Mathf.RoundToInt(PlayerPrefs.GetInt("maxScore", 0));   //GUNCELLE
        midScore.text = "Your Score: " + CurrentMaxScore;
        highScore.text = "Max Score: " + OverAllCurrentMaxScore;


        playerScoreScreen.SetActive(false);
        Destroy(boss);
        this.gameObject.SetActive(false);
        deathScreen.SetActive(true);
    }


    IEnumerator DangerCountdown()
    {
        yield return new WaitForSeconds(15);
        isInDanger = false;
        PlayerSr.material.color = color;
    }


    public IEnumerator CallUpdate(string request, string username, string password, string ParamMaxScore, string ParamCoin, string level)
    {

        WWWForm form = new WWWForm();

        form.AddField("request", request);
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("level", level);
        form.AddField("MaxScore", ParamMaxScore);
        form.AddField("gold", ParamCoin);

        UnityWebRequest www = UnityWebRequest.Post("http://yeditepe.ga/spacejump/user.php", form);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text);
        }

    }

    public void increaseCoin()
    {
        coin += coinBoost;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinText.text = coin.ToString();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            winScreen.SetActive(true);
            PlayerPrefs.SetInt("levelReached", levelReached);
            StartCoroutine(CallUpdate("Update", PlayerPrefs.GetString("username", "0"), PlayerPrefs.GetString("password", "0"), OverAllCurrentMaxScore.ToString(), PlayerPrefs.GetInt("gold", 0).ToString(), PlayerPrefs.GetInt("levelReached", 0).ToString()));
            boss.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void DoubleCoin()
    {
        int temp;
        coinBoost = 2;
        item1.interactable = false;
        StartCoroutine(powerUp0CDRoutine());
        temp = PlayerPrefs.GetInt("doubleGold", 0);
        temp--;
        PlayerPrefs.SetInt("doubleGold", temp);

    }

    public void Invisible()
    {
        int temp;
        PlayerSr.color = new Color(1f, 1f, 1f, .5f);
        isInvis = true;
        isInDanger = false;
        noLife = false;
        item2.interactable = false;
        StartCoroutine(powerUp1CDRoutine());
        temp = PlayerPrefs.GetInt("shield", 0);
        temp--;
        PlayerPrefs.SetInt("shield", temp);
    }

    public void Rocket()
    {
        int temp;
        PlayerSr.enabled = false;
        RocketObject.SetActive(true);
        isRocket = true;
        noLife = false;
        isInDanger = false;
        item3.interactable = false;
        StartCoroutine(powerUp2CDRoutine());
        temp = PlayerPrefs.GetInt("rocket", 0);
        temp--;
        PlayerPrefs.SetInt("rocket", temp);
    }

    public IEnumerator powerUp0CDRoutine()
    {
        yield return new WaitForSeconds(7.5f + coinlevel);
        coinBoost = 1;
    }

    public IEnumerator powerUp1CDRoutine()
    {
        yield return new WaitForSeconds(7.5f);
        PlayerSr.color = new Color(1f, 1f, 1f, 1f);
        isInvis = false;
        noLife = false;
        isInDanger = false;
    }
    public IEnumerator powerUp2CDRoutine()
    {
        yield return new WaitForSeconds(5.5f);
        PlayerSr.enabled = true;
        RocketObject.SetActive(false);
        isRocket = false;
        noLife = false;
        isInDanger = false;

    }

}

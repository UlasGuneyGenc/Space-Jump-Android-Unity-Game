using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Response
{
    public int success = 0;
    public int gold = 0;
    public int level = 0;
    public int maxScore = 0;
}

public class MainMenu : MonoBehaviour
{
    
    public InputField LoginScreen_Username;
    public InputField LoginScreen_Password;
    public InputField RegisterScreen_Username;
    public InputField RegisterScreen_Password;

    public Text MainMenuText_Username;
    public Text MainMenuText_Coin;
    public Text StoreCurrentCoin;
    public TextMeshProUGUI RankingsText;
    public TextMeshProUGUI Text_Error;

    public TextMeshProUGUI Text_Loading;
    public GameObject LoginScreenMainMenuLayer;
    public GameObject ButtonTryAgain;
    public GameObject ButtonOffline;

    public bool volume = true;

    public void Start() //Giris yapmis mi kontrolu
    {
        //START FONKSIYONU  Login Screen ve  Main Menu için ortak o yüzden name kontrolü var


        if (SceneManager.GetActiveScene().name == "Login Screen")
        {
            if (PlayerPrefs.GetInt("isLoggedIn", 0) == 1) //ADAM GIRIS YAPMISSA
            {
                Text_Loading.transform.gameObject.SetActive(true);              //Waiting Göster
                StartCoroutine(CallUser("Login", PlayerPrefs.GetString("username", "0"), PlayerPrefs.GetString("password", "0")));  //Localdeki bilgiyle güncel bilgiyi iste
            }
            else //GIRIS YAPILMADIYSA
            {
                Text_Loading.transform.gameObject.SetActive(false);             //Waiting Gizle
                LoginScreenMainMenuLayer.transform.gameObject.SetActive(true);  //Login Screen Göster
            }

        }
        else if(SceneManager.GetActiveScene().name == "Main Menu") 
        {
            MainMenuText_Username.text  =   PlayerPrefs.GetString("username", "Guest");
            MainMenuText_Coin.text  =   PlayerPrefs.GetInt("gold", 0).ToString();
        }

    }


    public IEnumerator CallUser(string request, string username, string password)
    {
        WWWForm form = new WWWForm();


        form.AddField("request", request);
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://yeditepe.ga/spacejump/user.php", form);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
            Text_Error.text = "Network Error!";
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text);
        }

        Response Response = new Response();
        JsonUtility.FromJsonOverwrite(www.downloadHandler.text, Response);

        if (request == "Login")
        {
            Debug.Log("Login Response: " + Response.success);
            if (Response.success == 1)
            {    //Giris Basarılı Ise
                PlayerPrefs.SetInt("isLoggedIn", 1);
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);
                PlayerPrefs.SetInt("gold", Response.gold);
                PlayerPrefs.SetInt("levelReached", Response.level);
                PlayerPrefs.SetInt("maxScore", Response.maxScore);
                SceneManager.LoadScene("Main Menu");
            }
            else if (Response.success == -1){
                Text_Error.text = "Username or password Wrong!";
                PlayerPrefs.SetInt("isLoggedIn", 0);
            }
            else if (Response.success == -2)
            {
                Text_Error.text = "Username or Password Wrong!";
            }
            else
            {
                Text_Error.text = "Connection Error!";
                ButtonTryAgain.transform.gameObject.SetActive(true);
                ButtonOffline.transform.gameObject.SetActive(true);
            }

        }
        else if (request == "Register")
        {
            Debug.Log("Register Response: " + Response.success);

            if (Response.success == 1)
            {    //Giris Basarılı Ise
                SceneManager.LoadScene("Login Screen");
            }
            else if (Response.success == -1)
            {
                Text_Error.text = "Username or Password Lenght Should > 4!";
            }
            else
            {
                Text_Error.text = "Connection Error!";
                ButtonTryAgain.transform.gameObject.SetActive(true);
                ButtonOffline.transform.gameObject.SetActive(true);
            }

        }

        if (Response.success != 1) { Text_Error.transform.gameObject.SetActive(true); }

    }


    public void PlayGame()
    {

        SceneManager.LoadScene("Level Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Guest()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Login()
    {
        Text_Error.transform.gameObject.SetActive(false);
        StartCoroutine(CallUser("Login", LoginScreen_Username.text, LoginScreen_Password.text));

    }

    public void Register()
    {
        Text_Error.transform.gameObject.SetActive(false);
        StartCoroutine(CallUser("Register", RegisterScreen_Username.text, RegisterScreen_Password.text));
    }

    public void OfflineMode()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Login Screen");
    }

    public void QuitAccount()
    {
        PlayerPrefs.SetInt("isLoggedIn", 0);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Login Screen");
    }

    public void VolumeButton()
    {
        if (volume)
        {
            AudioListener.pause = true;
            volume = false;
        }
        else
        {
            AudioListener.pause = false;
            volume = true;
        }
    }

    public void UpdateStoreCoin()
    {
        StoreCurrentCoin.text = PlayerPrefs.GetInt("gold", 0).ToString();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            MainMenuText_Coin.text = PlayerPrefs.GetInt("gold", 0).ToString();
        }
    }

    public void UpdateScores()
    {

        StartCoroutine(CallUpdateScores("Scores")); 
        
    }

    public IEnumerator CallUpdateScores(string request)
    {

        WWWForm form = new WWWForm();

        form.AddField("request", request);

        UnityWebRequest www = UnityWebRequest.Post("http://yeditepe.ga/spacejump/user.php", form);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            RankingsText.text = "Score Get Error!";
        }
        else
        {
            RankingsText.text = www.downloadHandler.text;
        }

    }

}

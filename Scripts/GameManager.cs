using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    [Header("Variables")]
    public int keyRed;
    public int keyBlue;
    public int goalRed = 1;
    public int goalBlue = 3;
    public GameObject[] doorsRed;
    public GameObject doorBlue;
    public int waypoint;
    public bool detected;

    [Header("UI")]
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public TextMeshProUGUI txtKeyRed;
    public TextMeshProUGUI txtKeyBlue;
    public TextMeshProUGUI txtWaypoint;
    public TextMeshProUGUI txtDetect;
    public TextMeshProUGUI txtResult;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.inGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.inGame)
        {
            panelGameOver.SetActive(false);
            panelInGame.SetActive(true);

            txtKeyBlue.text = keyBlue.ToString();
            txtKeyRed.text = keyRed.ToString();
            
            txtWaypoint.text = WaypointToString(waypoint);
            txtDetect.text = detected.ToString();

            if (keyRed == goalRed)
            {
                foreach (GameObject door in doorsRed)
                {
                    door.gameObject.SetActive(false);
                }
            }

            if (keyBlue == goalBlue)
            {
                doorBlue.gameObject.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            panelGameOver.SetActive(true);
            panelInGame.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public string WaypointToString(int i)
    {
        switch (i)
        {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            case 4:
                return "E";
            case 5:
                return "F";
            default:
                return null;
        }
    }

    public void Win()
    {
        state = GameState.gameOver;
        txtResult.text = "Victory!";
    }

    public void Lose()
    {
        state = GameState.gameOver;
        txtResult.text = "Try Again!";
    }

    public void _BtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void _BtnQuit()
    {
        Application.Quit();
    }
}

public enum GameState
{
    inGame,
    gameOver
}

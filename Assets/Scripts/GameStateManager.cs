using UnityEngine;
using UnityEngine.Splines;
using ButchersGames;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [Header("Spline")]
    [SerializeField] private SplineAnimate splineAnimate;

    [Header("UI")]
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;

    private PlayerSplineLoader splineLoader;

    private bool gameStarted;
    private bool gameEnded;
    private bool win;
    private bool lose;

    public bool GameStarted => gameStarted;
    public bool GameEnded => gameEnded;
    public bool GetWin => win;
    public bool GetLose => lose;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ResetGameState();
    }

    private void Update()
    {
        if (gameStarted || gameEnded)
            return;

        if (IsScreenPressed())
        {
            StartGame();
        }
    }

    private bool IsScreenPressed()
    {
        // Мобильное устройство
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                return true;
        }

        // Для тестирования в Unity через мышь
        if (Input.GetMouseButtonDown(0))
            return true;

        return false;
    }

    private void StartGame()
    {
        gameStarted = true;

        if (tutorialUI != null)
            tutorialUI.SetActive(false);

        if (splineAnimate != null)
            splineAnimate.Play();

        Debug.Log("GAME START");
    }

    public void Win()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        win = true;

        StopGame();

        if (winUI != null)
            winUI.SetActive(true);

        Debug.Log("WIN");
    }

    public void Lose()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        lose = true;

        StopGame();

        if (loseUI != null)
            loseUI.SetActive(true);

        Debug.Log("LOSE");
    }

    public void NextLevel()
    {
        if (!gameEnded || !win)
            return;

        // Скрываем окно победы
        if (winUI != null)
            winUI.SetActive(false);

        // Загружаем следующий уровень
        if (LevelManager.Default != null)
        {
            LevelManager.Default.NextLevel();
        }
        else
        {
            Debug.LogWarning("LevelManager не найден!");
            return;
        }

        // Находим новый Anchor и его компоненты
        GameObject anchor = GameObject.FindGameObjectWithTag("Anchor");

        if (anchor == null)
        {
            Debug.LogWarning("Объект с тегом Anchor не найден!");
            return;
        }

        splineAnimate = anchor.GetComponent<SplineAnimate>();
        splineLoader = anchor.GetComponent<PlayerSplineLoader>();

        // Находим spline нового уровня
        if (splineLoader != null)
        {
            splineLoader.FindSpline();
        }
        else
        {
            Debug.LogWarning("На Anchor нет PlayerSplineLoader!");
        }

        // Новый уровень должен ждать нажатия игрока
        if (splineAnimate != null)
            splineAnimate.Pause();

        // Сбрасываем состояние игры
        gameStarted = false;
        gameEnded = false;
        win = false;
        lose = false;

        // Показываем tutorial
        if (tutorialUI != null)
            tutorialUI.SetActive(true);

        if (loseUI != null)
            loseUI.SetActive(false);

        Debug.Log("NEXT LEVEL");
    }

    private void ResetGameState()
    {
        gameStarted = false;
        gameEnded = false;
        win = false;
        lose = false;

        if (tutorialUI != null)
            tutorialUI.SetActive(true);

        if (winUI != null)
            winUI.SetActive(false);

        if (loseUI != null)
            loseUI.SetActive(false);

        FindAnchor();
    }

    private void FindAnchor()
    {
        GameObject anchor = GameObject.FindGameObjectWithTag("Anchor");

        if (anchor == null)
        {
            Debug.LogWarning("Объект с тегом Anchor не найден!");
            return;
        }

        splineAnimate = anchor.GetComponent<SplineAnimate>();
        splineLoader = anchor.GetComponent<PlayerSplineLoader>();

        if (splineLoader != null)
        {
            splineLoader.FindSpline();
        }

        if (splineAnimate != null)
            splineAnimate.Pause();
    }

    private void StopGame()
    {
        if (splineAnimate != null)
            splineAnimate.Pause();
    }
}
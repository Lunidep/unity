using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    //public GameObject moneyPrefab;
    public GameObject superEnemyPrefab;
    public GameObject[] meteorPrefabs; // Массив префабов метеоритов

    public float minInstantiateValue;
    public float maxInstantiateValue;
    public float enemyDestoryTime = 10f;
    public float enemyInstantiateSpeed = 6f;

    public float moneyDestoryTime = 10f;
    public float moneyInstantiateSpeed = 12f;

    public float superEnemyDestoryTime = 10f;
    public float superEnemyInstantiateSpeed = 8f;

    public float meteorDestoryTime = 10f;
    public float meteorInstantiateSpeed = 4f; // Скорость появления метеоритов

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject nextButton;
    public GameObject prevButton;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        InvokeRepeating("InstantiateEnemy", 1f, enemyInstantiateSpeed);
        //InvokeRepeating("InstantiateMoney", 1f, moneyInstantiateSpeed);
        InvokeRepeating("InstantiateSuperEnemy", 1f, superEnemyInstantiateSpeed);
        InvokeRepeating("InstantiateMeteor", 1f, meteorInstantiateSpeed); // Запускаем создание метеоритов
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        Vector3 enemypos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 10f);
        GameObject enemy = Instantiate(enemyPrefab, enemypos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyDestoryTime);
    }

    /*void InstantiateMoney()
    {
        Vector3 enemypos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 4.5f);
        GameObject money = Instantiate(moneyPrefab, enemypos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(money, enemyDestoryTime);
    }
    */

    void InstantiateSuperEnemy()
    {
        Vector3 enemypos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 10f);
        GameObject superEnemy = Instantiate(superEnemyPrefab, enemypos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(superEnemy, enemyDestoryTime);
    }

    void InstantiateMeteor()
    {
        if (meteorPrefabs != null && meteorPrefabs.Length > 0)
        {
            // Выбираем случайный метеорит из массива
            int randomIndex = Random.Range(0, meteorPrefabs.Length);
            GameObject randomMeteorPrefab = meteorPrefabs[randomIndex];

            Vector3 meteorPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 10f);
            GameObject meteor = Instantiate(randomMeteorPrefab, meteorPos, Quaternion.Euler(0f, 0f, 180f));
            Destroy(meteor, meteorDestoryTime);
        }
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        nextButton.SetActive(false);
        prevButton.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameCoroutine());
    }

    private IEnumerator RestartGameCoroutine()
    {
        // Ждем 1 секунду перед перезапуском
        yield return new WaitForSeconds(1f);

        // Перезагружаем сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Сбрасываем время
        Time.timeScale = 1f;
    }
}
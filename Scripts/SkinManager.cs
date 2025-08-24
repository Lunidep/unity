using UnityEngine;
using UnityEngine.UI; // Не забудьте эту библиотеку для работы с UI

public class SkinManager : MonoBehaviour
{
    // Синглтон для простого доступа из других скриптов
    public static SkinManager Instance;

    // Массив для хранения всех доступных скинов (спрайтов)
    public Sprite[] shipSkins;

    // Ссылка на компонент SpriteRenderer игрока
    public SpriteRenderer playerSpriteRenderer;

    // Индекс текущего выбранного скина
    private int currentSkinIndex = 0;

    void Awake()
    {
        // Простая реализация синглтона
        if (Instance == null)
        {
            Instance = this;
            // Опционально: DontDestroyOnLoad(gameObject); если хотите сохранять выбор между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Автоматически находим SpriteRenderer игрока, если ссылка не задана в инспекторе
        if (playerSpriteRenderer == null)
        {
            // Ищем по тегу (убедитесь, что ваш игрок имеет тег "Player")
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            }

            // Или находим просто по имени
            // playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        }

        // При старте сразу применяем текущий скин
        ApplySkin(currentSkinIndex);
    }

    // Метод для применения скина по индексу
    public void ApplySkin(int skinIndex)
    {
        // Проверяем, чтобы индекс был в пределах массива
        if (skinIndex >= 0 && skinIndex < shipSkins.Length && shipSkins[skinIndex] != null)
        {
            currentSkinIndex = skinIndex;
            playerSpriteRenderer.sprite = shipSkins[skinIndex];
        }
        else
        {
            Debug.LogWarning("Попытка применить несуществующий скин с индексом: " + skinIndex);
        }
    }

    // Метод для переключения на следующий скин (по кругу)
    public void NextSkin()
    {
        currentSkinIndex = (currentSkinIndex + 1) % shipSkins.Length;
        ApplySkin(currentSkinIndex);
    }

    // Метод для переключения на предыдущий скин (по кругу)
    public void PreviousSkin()
    {
        currentSkinIndex--;
        // Если индекс стал меньше 0, переходим к последнему элементу массива
        if (currentSkinIndex < 0)
        {
            currentSkinIndex = shipSkins.Length - 1;
        }
        ApplySkin(currentSkinIndex);
    }

    // Геттер для получения текущего индекса (может пригодиться для сохранения)
    public int GetCurrentSkinIndex()
    {
        return currentSkinIndex;
    }

    // Метод для прямого выбора скина по кнопке (можно подключить в OnClick UI кнопки)
    public void SelectSkinByIndex(int index)
    {
        ApplySkin(index);
    }
}
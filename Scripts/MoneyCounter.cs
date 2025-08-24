using UnityEngine;
using TMPro; // Добавь эту директиву

public class MoneyCounter : MonoBehaviour
{
    public static int Coins;
    TextMeshProUGUI text; // Измени тип

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>(); // Измени метод
    }

    void Update()
    {
        text.text = Coins.ToString();
    }
}
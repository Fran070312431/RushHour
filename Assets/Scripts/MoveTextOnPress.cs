using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTextOnPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float moveDown = 5f; // Cuánto baja el texto

    private TextMeshProUGUI texto;
    private Vector2 posicionOriginal;

    void Start()
    {
        texto = GetComponentInChildren<TextMeshProUGUI>();
        posicionOriginal = texto.rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Bajar el texto
        texto.rectTransform.anchoredPosition = posicionOriginal + new Vector2(0, -moveDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Subir el texto
        texto.rectTransform.anchoredPosition = posicionOriginal;
    }
}
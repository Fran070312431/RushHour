using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleCar : MonoBehaviour
{
    [Header("Configuración")]
    public bool horizontal = true;
    public int length = 2;
    public bool isPlayerCar = false;

    [Header("Sprite del Vehículo")]
    public Sprite carSprite; // NUEVO: Arrastra tu sprite aquí

    [Header("Posición en Grid")]
    public Vector2Int gridPos = Vector2Int.zero;

    private Camera cam;
    private bool dragging = false;
    private Vector3 dragStartPos;
    private Vector2Int dragStartGridPos;
    private SpriteRenderer spriteRenderer; // CAMBIADO de Renderer a SpriteRenderer
    private Color originalColor;

    void Start()
    {
        cam = Camera.main;

        // Obtener o crear SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // Aplicar sprite personalizado
        if (carSprite != null)
        {
            spriteRenderer.sprite = carSprite;
        }

        // Color
        if (isPlayerCar)
        {
            originalColor = Color.red;
        }
        else
        {
            originalColor = Color.white; // Color original del sprite
        }

        spriteRenderer.color = originalColor;

        // Ajustar tamaño según orientación y longitud
        AdjustScale();

        UpdatePosition();
    }

    void AdjustScale()
    {
        float cellSize = SimpleGrid.Instance.cellSize;
        float padding = 0.05f;

        if (carSprite != null)
        {
            // Obtener tamaño real del sprite
            float spriteWidth = carSprite.bounds.size.x;
            float spriteHeight = carSprite.bounds.size.y;

            if (horizontal)
            {
                // El sprite debe ocupar 'length' celdas horizontalmente
                float targetWidth = length * cellSize - padding;
                float targetHeight = cellSize - padding;

                float scaleX = targetWidth / spriteWidth;
                float scaleY = targetHeight / spriteHeight;

                transform.localScale = new Vector3(scaleX, scaleY, 1);
            }
            else
            {
                // El sprite debe ocupar 'length' celdas verticalmente
                float targetWidth = cellSize - padding;
                float targetHeight = length * cellSize - padding;

                float scaleX = targetWidth / spriteWidth;
                float scaleY = targetHeight / spriteHeight;

                transform.localScale = new Vector3(scaleX, scaleY, 1);
            }
        }
        else
        {
            // Si no hay sprite, usar escala por defecto
            if (horizontal)
                transform.localScale = new Vector3(length - 0.1f, 0.9f, 1);
            else
                transform.localScale = new Vector3(0.9f, length - 0.1f, 1);
        }
    }

    void OnMouseDown()
    {
        dragging = true;
        dragStartPos = transform.position;
        dragStartGridPos = gridPos;
        spriteRenderer.color = originalColor * 1.3f;
    }

    void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 delta = mousePos - dragStartPos;
        float cellSize = SimpleGrid.Instance.cellSize;

        int cellsDelta = horizontal
            ? Mathf.RoundToInt(delta.x / cellSize)
            : Mathf.RoundToInt(delta.y / cellSize);

        if (cellsDelta == 0)
        {
            UpdatePositionSmooth();
            return;
        }

        int step = cellsDelta > 0 ? 1 : -1;
        int steps = Mathf.Abs(cellsDelta);

        Vector2Int testPos = dragStartGridPos;
        Vector2Int lastValidPos = dragStartGridPos;

        for (int i = 0; i < steps; i++)
        {
            if (horizontal)
            {
                testPos.x += step;
            }
            else
            {
                testPos.y += step;
            }

            if (IsValidPosition(testPos))
            {
                lastValidPos = testPos;
            }
            else
            {
                break;
            }
        }

        gridPos = lastValidPos;
        UpdatePositionSmooth();
    }

    void OnMouseUp()
    {
        dragging = false;

        if (gridPos != dragStartGridPos)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMoveSound();
            }
        }

        UpdatePosition();
        spriteRenderer.color = originalColor;
        CheckWin();
    }

    void UpdatePositionSmooth()
    {
        Vector3 targetPos = GetWorldPosFromGrid(gridPos);
        transform.position = targetPos;
    }

    void UpdatePosition()
    {
        Vector3 targetPos = GetWorldPosFromGrid(gridPos);
        transform.position = targetPos;
    }

    Vector3 GetWorldPosFromGrid(Vector2Int gPos)
    {
        Vector3 pos = SimpleGrid.Instance.GetWorldPos(gPos.x, gPos.y);
        float cellSize = SimpleGrid.Instance.cellSize;

        if (horizontal)
            pos.x += (length - 1) * cellSize / 2f;
        else
            pos.y += (length - 1) * cellSize / 2f;

        return pos;
    }

    bool IsValidPosition(Vector2Int pos)
    {
        if (isPlayerCar && horizontal)
        {
            if (pos.y == 3)
            {
                if (pos.x < 0 || pos.x > SimpleGrid.Instance.size)
                    return false;
            }
            else
            {
                if (pos.x < 0 || pos.x + length > SimpleGrid.Instance.size)
                    return false;
            }

            if (pos.y < 0 || pos.y >= SimpleGrid.Instance.size)
                return false;
        }
        else
        {
            if (horizontal)
            {
                if (pos.x < 0 || pos.x + length > SimpleGrid.Instance.size)
                    return false;
                if (pos.y < 0 || pos.y >= SimpleGrid.Instance.size)
                    return false;
            }
            else
            {
                if (pos.x < 0 || pos.x >= SimpleGrid.Instance.size)
                    return false;
                if (pos.y < 0 || pos.y + length > SimpleGrid.Instance.size)
                    return false;
            }
        }

        SimpleCar[] allCars = FindObjectsByType<SimpleCar>(FindObjectsSortMode.None);
        foreach (SimpleCar other in allCars)
        {
            if (other == this) continue;

            if (CheckCollision(pos, other))
                return false;
        }

        return true;
    }

    bool CheckCollision(Vector2Int myPos, SimpleCar other)
    {
        for (int i = 0; i < length; i++)
        {
            Vector2Int myCell = horizontal ?
                new Vector2Int(myPos.x + i, myPos.y) :
                new Vector2Int(myPos.x, myPos.y + i);

            for (int j = 0; j < other.length; j++)
            {
                Vector2Int otherCell = other.horizontal ?
                    new Vector2Int(other.gridPos.x + j, other.gridPos.y) :
                    new Vector2Int(other.gridPos.x, other.gridPos.y + j);

                if (myCell == otherCell)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void CheckWin()
    {
        if (!isPlayerCar) return;
        if (!horizontal) return;

        if (gridPos.x == 6 && gridPos.y == 3)
        {
            Debug.Log("¡¡¡GANASTE!!!");

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayWinSound();
            }

            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowWinPanel();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (SimpleGrid.Instance == null) return;

        Gizmos.color = isPlayerCar ? Color.red : Color.yellow;

        for (int i = 0; i < length; i++)
        {
            Vector2Int cell = horizontal ?
                new Vector2Int(gridPos.x + i, gridPos.y) :
                new Vector2Int(gridPos.x, gridPos.y + i);

            Vector3 cellWorldPos = SimpleGrid.Instance.GetWorldPos(cell.x, cell.y);
            Gizmos.DrawWireCube(cellWorldPos, Vector3.one * SimpleGrid.Instance.cellSize * 0.9f);

            #if UNITY_EDITOR
            UnityEditor.Handles.Label(cellWorldPos, $"{cell.x},{cell.y}");
            #endif
        }
    }
}
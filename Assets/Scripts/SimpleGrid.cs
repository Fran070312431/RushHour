using UnityEngine;

public class SimpleGrid : MonoBehaviour
{
    public static SimpleGrid Instance;
    public int size = 6;
    public float cellSize = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        // Crear celdas
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cell.name = "Cell";
                cell.transform.parent = transform;
                cell.transform.position = GetWorldPos(x, y) + Vector3.forward;

                Color color = (x + y) % 2 == 0 ? Color.white : new Color(0.9f, 0.9f, 0.9f);
                cell.GetComponent<Renderer>().material.color = color;
                Destroy(cell.GetComponent<Collider>());
            }
        }

        // Salida
        GameObject exit = GameObject.CreatePrimitive(PrimitiveType.Quad);
        exit.name = "Exit";
        exit.transform.parent = transform;
        exit.transform.position = GetWorldPos(6, 3);
        exit.transform.localScale = Vector3.one * 0.8f;
        exit.GetComponent<Renderer>().material.color = Color.yellow;
        Destroy(exit.GetComponent<Collider>());
    }

    public Vector3 GetWorldPos(int x, int y)
    {
        float offset = (size - 1) * cellSize / 2f;
        return new Vector3(x * cellSize - offset, y * cellSize - offset, 0);
    }

    public Vector2Int GetGridPos(Vector3 worldPos) // CORREGIDO: era "wolrdPos"
    {
        float offset = (size - 1) * cellSize / 2f;
        int x = Mathf.RoundToInt((worldPos.x + offset) / cellSize);
        int y = Mathf.RoundToInt((worldPos.y + offset) / cellSize);
        return new Vector2Int(Mathf.Clamp(x, 0, size - 1), Mathf.Clamp(y, 0, size - 1));
    }
}
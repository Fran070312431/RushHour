using UnityEngine;

public class SimpleGrid : MonoBehaviour
{
    // Singleton para que los coches puedan consultar la posición del grid fácilmente
    public static SimpleGrid Instance;

    public int size = 6;         // Tamaño del tablero (6x6)
    public float cellSize = 1f;  // Tamaño de cada cuadrado

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Al arrancar creamos visualmente el tablero
        CreateBoard();
    }

    void CreateBoard()
    {
        // Doble bucle para recorrer filas y columnas (X e Y)
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                // Creamos un plano (Quad) para cada celda
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cell.name = "Cell";
                cell.transform.parent = transform;

                // Colocamos la celda en su sitio. Le sumamos "forward" para que esté un pelín atrás
                cell.transform.position = GetWorldPos(x, y) + Vector3.forward;

                // Pintamos el tablero tipo ajedrez alternando dos colores
                Color color = (x + y) % 2 == 0 ? Color.white : new Color(0.9f, 0.9f, 0.9f);
                cell.GetComponent<Renderer>().material.color = color;

                // Quitamos el colisionador a las celdas porque no lo necesitamos (solo es visual)
                Destroy(cell.GetComponent<Collider>());
            }
        }

        // Creamos un cuadrado amarillo fuera del grid para marcar la salida del coche rojo
        GameObject exit = GameObject.CreatePrimitive(PrimitiveType.Quad);
        exit.name = "Exit";
        exit.transform.parent = transform;
        exit.transform.position = GetWorldPos(6, 3); // La salida está en la fila 3, columna 6
        exit.transform.localScale = Vector3.one * 0.8f;
        exit.GetComponent<Renderer>().material.color = Color.yellow;
        Destroy(exit.GetComponent<Collider>());
    }

    // Esta función traduce coordenadas del grid (0,1,2...) a posiciones reales de Unity (metros)
    public Vector3 GetWorldPos(int x, int y)
    {
        // Calculamos un "offset" para que el centro del grid sea el (0,0,0) del mundo
        float offset = (size - 1) * cellSize / 2f;
        return new Vector3(x * cellSize - offset, y * cellSize - offset, 0);
    }

    // Esta función hace lo contrario: nos dice en qué celda estamos según la posición del ratón
    public Vector2Int GetGridPos(Vector3 worldPos)
    {
        float offset = (size - 1) * cellSize / 2f;
        // Usamos RoundToInt para que nos dé el número de la celda más cercana
        int x = Mathf.RoundToInt((worldPos.x + offset) / cellSize);
        int y = Mathf.RoundToInt((worldPos.y + offset) / cellSize);

        // El Clamp asegura que no nos salgamos del tablero (0 a 5)
        return new Vector2Int(Mathf.Clamp(x, 0, size - 1), Mathf.Clamp(y, 0, size - 1));
    }
}
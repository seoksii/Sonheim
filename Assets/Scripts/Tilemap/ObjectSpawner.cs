using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private TileMapGenerator _tileMapGenerator;
    
    [SerializeField] private GameObject[] _objectPrefabs;
    [SerializeField] Transform _objectParents;
    [SerializeField, Range(0, 100)] private int _density; 

    public GameObject[,] MapObjects;
    private void Awake()
    {
        _tileMapGenerator = GetComponent<TileMapGenerator>();
    }

    private void Start()
    {
        SpawnInitialObjects();
    }

    void SpawnInitialObjects()
    {
        Vector2Int size = _tileMapGenerator.size;
        int[,] mapBiomes = _tileMapGenerator.MapBiomes;
        int[,] mapHeights = _tileMapGenerator.MapHeights;
        
        MapObjects = new GameObject[size.x, size.y];
        int[,] objectStatus = new int[size.x, size.y];
        
        System.Random psuedoRandom = new System.Random(_tileMapGenerator.seed.GetHashCode());
        
        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
                if (mapHeights[x, y] == 0)
                    objectStatus[x, y] = psuedoRandom.Next(0, 100) > _density ? 0 : 1;
        
        
        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
                if (objectStatus[x, y] == 1)
                {
                    SpawnObject(_objectPrefabs[mapBiomes[x, y]], x, y);
                }
                else MapObjects[x, y] = null;
        
    }

    public void SpawnObject(GameObject prefab, int gridX, int gridY)
    {
        Grid grid = _tileMapGenerator.MapGrid;
        
        Vector3 pos = grid.CellToWorld(new Vector3Int(gridX, gridY, 0));
        GameObject generated = Instantiate(prefab, pos, Quaternion.identity, _objectParents);
        MapObjects[gridX, gridY] = generated;
    }
}
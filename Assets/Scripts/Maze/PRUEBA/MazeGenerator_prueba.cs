using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;

public class MazeGenerator_prueba : MonoBehaviour
{
    [SerializeField]
    public MazeCell _mazeCellPrefab; // prefab de celda

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        // Instanciar todas las celdas
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity,transform);
                _mazeGrid[x, z].transform.localPosition = new Vector3(x, 0, z);
            }
        }

        GenerateMaze(_mazeGrid[0, 0]);
        GetComponent<NavMeshSurface>().BuildNavMesh();
        
    }

    private void GenerateMaze(MazeCell startCell)
    {
        Stack<MazeCell> cellStack = new Stack<MazeCell>();
        startCell.Visit();
        cellStack.Push(startCell);

        while (cellStack.Count > 0)
        {
            MazeCell current = cellStack.Pop();
            var neighbors = GetUnvisitedCells(current).OrderBy(_ => Random.value).ToList();

            if (neighbors.Count > 0)
            {
                cellStack.Push(current); // volver a apilar la celda actual
                MazeCell next = neighbors[0];
                ClearWalls(current, next);
                next.Visit();
                cellStack.Push(next);
            }
        }
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.localPosition.x;
        int z = (int)currentCell.transform.localPosition.z;

        if (x + 1 < _mazeWidth && !_mazeGrid[x + 1, z].IsVisited)
            yield return _mazeGrid[x + 1, z];
        if (x - 1 >= 0 && !_mazeGrid[x - 1, z].IsVisited)
            yield return _mazeGrid[x - 1, z];
        if (z + 1 < _mazeDepth && !_mazeGrid[x, z + 1].IsVisited)
            yield return _mazeGrid[x, z + 1];
        if (z - 1 >= 0 && !_mazeGrid[x, z - 1].IsVisited)
            yield return _mazeGrid[x, z - 1];
    }

    private void ClearWalls(MazeCell current, MazeCell next)
    {
        if (current.transform.localPosition.x < next.transform.localPosition.x)
        {
            current.ClearRightWall();
            next.ClearLeftWall();
            return;
        }
        if (current.transform.localPosition.x > next.transform.localPosition.x)
        {
            current.ClearLeftWall();
            next.ClearRightWall();
            return;
        }
        if (current.transform.localPosition.z < next.transform.localPosition.z)
        {
            current.ClearFrontWall();
            next.ClearBackWall();
            return;
        }
        if (current.transform.localPosition.z > next.transform.localPosition.z)
        {
            current.ClearBackWall();
            next.ClearFrontWall();
            return;
        }
    }
}

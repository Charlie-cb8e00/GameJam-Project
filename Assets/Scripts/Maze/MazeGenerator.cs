using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab; // prefab de celda

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
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // Generar laberinto desde la esquina inferior izquierda
        GenerateMaze(_mazeGrid[0, 0]);

        
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
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

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
        if (current.transform.position.x < next.transform.position.x)
        {
            current.ClearRightWall();
            next.ClearLeftWall();
            return;
        }
        if (current.transform.position.x > next.transform.position.x)
        {
            current.ClearLeftWall();
            next.ClearRightWall();
            return;
        }
        if (current.transform.position.z < next.transform.position.z)
        {
            current.ClearFrontWall();
            next.ClearBackWall();
            return;
        }
        if (current.transform.position.z > next.transform.position.z)
        {
            current.ClearBackWall();
            next.ClearFrontWall();
            return;
        }
    }
}

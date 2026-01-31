using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell_prueba : MonoBehaviour
{
    [Header("Wall Variants")]
    [SerializeField] private GameObject[] _leftWalls;
    [SerializeField] private GameObject[] _rightWalls;
    [SerializeField] private GameObject[] _frontWalls;
    [SerializeField] private GameObject[] _backWalls;

    [SerializeField] private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }

    private GameObject leftWallActive;
    private GameObject rightWallActive;
    private GameObject frontWallActive;
    private GameObject backWallActive;

    void Awake()
    {
        leftWallActive = ActivateRandomWall(_leftWalls);
        rightWallActive = ActivateRandomWall(_rightWalls);
        frontWallActive = ActivateRandomWall(_frontWalls);
        backWallActive = ActivateRandomWall(_backWalls);
    }

    GameObject ActivateRandomWall(GameObject[] walls)
    {
        if (walls == null || walls.Length == 0)
            return null;

        int index = Random.Range(0, walls.Length);

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].SetActive(i == index);
        }

        return walls[index];
    }

    public void Visit()
    {
        IsVisited = true;
        if (_unvisitedBlock != null)
            _unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall() { if (leftWallActive != null) leftWallActive.SetActive(false); }
    public void ClearRightWall() { if (rightWallActive != null) rightWallActive.SetActive(false); }
    public void ClearFrontWall() { if (frontWallActive != null) frontWallActive.SetActive(false); }
    public void ClearBackWall() { if (backWallActive != null) backWallActive.SetActive(false); }
}
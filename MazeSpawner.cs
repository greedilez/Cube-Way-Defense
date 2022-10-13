using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _mazesAtScene;

    [SerializeField] private bool _debug = false;

    [SerializeField] private NavMeshSurface _surface;

    private void Awake() => TurnRandomMaze(_mazesAtScene, true);

    private void Start() => _surface.BuildNavMesh();

    public void TurnRandomMaze(GameObject[] mazes, bool state) {
        int mazeIndex = GenerateRandomMazeIndex(mazes.Length);
        _mazesAtScene[mazeIndex].SetActive(state);
        if(_debug) Debug.Log($"Maze index: {mazeIndex}, state: {state}");
        TurnOffAnotherMazes(_mazesAtScene[mazeIndex]);
    }

    public void TurnOffAnotherMazes(GameObject currentMaze) {
        for (int i = 0; i < _mazesAtScene.Length; i++) {
            if (_mazesAtScene[i] == currentMaze) continue;
            _mazesAtScene[i].SetActive(false);
        }
    }

    public int GenerateRandomMazeIndex(int maxValue) {
        int targetIndex = RandomInt(maxValue);
        return Mathf.Abs(targetIndex);
    }

    public int RandomInt(int maxValue) => Random.Range(0, maxValue);
}

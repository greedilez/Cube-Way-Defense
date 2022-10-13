using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _redZonePrefab;

    [SerializeField] private Transform[] _redZonePositionsAtStructure;

    private void Start() => SpawnRedZonesAtRandomPlace();

    public void SpawnRedZonesAtRandomPlace() {
        for (int i = 0; i < _redZonePositionsAtStructure.Length; i++) {
            int targetIndex = i;
            Instantiate(_redZonePrefab, _redZonePositionsAtStructure[targetIndex].position, Quaternion.identity);
        }
    }
}

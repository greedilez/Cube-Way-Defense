using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenZoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _greenZonePositionsAtStructure;

    [SerializeField] private GameObject _greenZone;

    private void Start() => SpawnGreenZoneAtRandomPlace();

    public void SpawnGreenZoneAtRandomPlace() {
        int chance = Random.Range(0, 5);
        int targetIndex = (chance >= 3) ? 0 : 1;
        Instantiate(_greenZone, _greenZonePositionsAtStructure[targetIndex].position, Quaternion.identity);
    }
}

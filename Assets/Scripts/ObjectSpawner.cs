using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
  public Transform[] StuffSpawnPoints;
  public GameObject Pickup;
  public GameObject[] Obstacles;
  public bool[] PositionLock;

  public float[] lanes = {-3f, 0f, 3f};

  void Start() {
    bool placeObstacle = Random.Range(0, 10) <= 9f; 
    int obstacleIndex = -1;
    if (placeObstacle) {
      obstacleIndex = Random.Range(1, StuffSpawnPoints.Length);

      int positionIndex = Random.Range(0, Obstacles.Length);

      bool positionLocked = PositionLock[positionIndex];

      Vector3 position = StuffSpawnPoints[obstacleIndex].position + new Vector3(0f, Obstacles[positionIndex].transform.position.y, 0f);

      CreateObject(position, Obstacles[positionIndex], positionLocked);
    }

    for (int i = 0; i < StuffSpawnPoints.Length; i++) {
      if (i == obstacleIndex) continue;
      if (Random.Range(0, 3) == 0) {
        CreateObject(StuffSpawnPoints[i].position, Pickup, false);
      }
    }

  }

  void CreateObject(Vector3 position, GameObject prefab, bool positionLocked) {
    if (!positionLocked) {
      position += new Vector3(lanes[Random.Range(0, lanes.Length)], 0, 0);
    }

    Instantiate(prefab, position, Quaternion.identity);
  }
}
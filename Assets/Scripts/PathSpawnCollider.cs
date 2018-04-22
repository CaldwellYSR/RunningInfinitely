using UnityEngine;

public class PathSpawnCollider : MonoBehaviour {

  public float positionY = 0f;
  public Vector3 PathSpawnPoint;
  public GameObject Path;
  public Transform Parent;

  void OnTriggerEnter(Collider hit) {
    if (hit.gameObject.CompareTag(Constants.PlayerTag)) {
      GameObject path = Instantiate(Path, transform.position + PathSpawnPoint, Quaternion.identity);
      path.transform.parent = Parent;

      GameObject wall_l = path.transform.Find("Wall_L").gameObject;
      GameObject wall_r = path.transform.Find("Wall_R").gameObject;

      Color newColor = new Color(Random.value, Random.value, Random.value);
      wall_l.GetComponent<Renderer>().material.color = newColor;
      wall_r.GetComponent<Renderer>().material.color = newColor;

    }
  }
}
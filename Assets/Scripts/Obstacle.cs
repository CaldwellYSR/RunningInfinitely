using UnityEngine;

public class Obstacle : MonoBehaviour {

  void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == Constants.PlayerTag) {
      GameManager.Instance.GameOver();
    }
  }
}

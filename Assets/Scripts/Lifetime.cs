using UnityEngine;

public class Lifetime : MonoBehaviour {

  public float LifeTime = 10f;

  void Start() {
    Invoke("KillObject", LifeTime);
  }

  void KillObject() {
    if (GameManager.Instance.GameState != GameState.Dead) {
      Destroy(gameObject);
    }
  }
}
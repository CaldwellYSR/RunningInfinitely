using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour {

  [Range(10f, 100f)]
  public float rotateSpeed = 50f;

  [Range(0.5f, 10f)]
  public float bobSpeed = 0.75f;

  [Range(0f, 10f)]
  public float bobDistance = 2f;

  public int ScorePoints = 100;

  void Update() {
    transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);

    Vector3 bobPosition = new Vector3(
      transform.position.x,
      transform.position.y + bobDistance * Mathf.Sin(Time.time * bobSpeed),
      transform.position.z
      );

    transform.position = bobPosition;
  }

  void OnTriggerEnter(Collider col) {
    UIManager.Instance.IncreaseScore(ScorePoints);
    GameObject.Find("ScoreText").GetComponent<Text>().text = UIManager.Instance.UpdatedScoreText();
    Destroy(this.gameObject);
  }
}
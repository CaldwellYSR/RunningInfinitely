using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

  public Text ScoreText;

  private static float score = 0;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      DestroyImmediate(this);
    }
  }

  private static UIManager instance;
  public static UIManager Instance {
    get {
      if (instance == null)
        instance = new UIManager();

      return instance;
    }
  }

  protected UIManager() {
  }

  public void ResetScore() {
    score = 0;
    UpdateScoreText();
  }

  public void SetScore(float value) {
    score = value;
    UpdateScoreText();
  }

  public void IncreaseScore(float value) {
    score += value;
    UpdateScoreText();
  }

  public void UpdateScoreText() {
    GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + score.ToString("n2");
  }
}
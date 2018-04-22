using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

  public Text ScoreText, StatusText;

  private float score = 0;

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

  private void UpdateScoreText() {
    ScoreText.text = score.ToString("n2");
  }

  public void SetStatus(string text) {
    StatusText.text = text;
  }
}
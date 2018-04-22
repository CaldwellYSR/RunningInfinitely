public class UIManager {

  private static float score = 0;

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
  }

  public void SetScore(float value) {
    score = value;
  }

  public void IncreaseScore(float value) {
    score += value;
  }

  public string UpdatedScoreText() {
    return "Score: " + score.ToString();
  }
}
public class GameManager {

  private static GameManager instance;

  public static GameManager Instance {
    get {
      if (instance == null) {
        instance = new GameManager();
      }
      return instance;
    }
  }

  protected GameManager() {
    GameState = GameState.Default;
  }

  public GameState GameState { get; set; }

  public void GameOver() {
    this.GameState = GameState.Dead;
  }

}
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour {

	void Awake () {
    UIManager.Instance.UpdateScoreText();
	}

  public void OnButtonClick() {
    SceneManager.LoadScene("SampleScene");
  }
}

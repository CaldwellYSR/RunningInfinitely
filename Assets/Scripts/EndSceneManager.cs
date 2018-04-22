using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour {

	void Update () {
    GameObject.Find("ScoreText").GetComponent<Text>().text = UIManager.Instance.UpdatedScoreText();
	}

  public void OnButtonClick() {
    SceneManager.LoadScene("SampleScene");
  }
}

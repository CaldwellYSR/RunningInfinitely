using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

  public Text RunningTextObject, InfinitelyTextObject;
  public Button playButton;

  private string runningText = "Running";
  private string infinitelyText = "Infinitely";
  private AudioSource audioSource;

  private int currentPosition = 0;
  private float Delay = 0.20f;

  private float timeOfTravel = .5f; 
  private float currentTime = 0f; 
  private float normalizedValue;
  private RectTransform rectTransform;
  private Vector3 startPosition = new Vector3(0, -650f, 0);
  private Vector3 endPosition = new Vector3(0, -200f, 0);


  void Start() {
    audioSource = GetComponent<AudioSource>();
    rectTransform = playButton.GetComponent<RectTransform>();
    StartCoroutine("WriteRunningText");
  }

  public void StartGame() {
    SceneManager.LoadScene("SampleScene");
  }

  IEnumerator WriteRunningText() { 
    while (currentPosition < runningText.Length) {
      audioSource.Play();
      RunningTextObject.text += runningText[currentPosition++];
      yield return new WaitForSeconds(Delay);
    }
    currentPosition = 0;
    yield return StartCoroutine("Wait");
  }

  IEnumerator Wait() {
    yield return new WaitForSeconds(1f);
    yield return StartCoroutine("WriteInfinitelyText");
  }

  IEnumerator WriteInfinitelyText() { 
    while (currentPosition < infinitelyText.Length) {
      audioSource.Play();
      InfinitelyTextObject.text += infinitelyText[currentPosition++];
      yield return new WaitForSeconds(Delay);
    }
    yield return new WaitForSeconds(1f);
    yield return StartCoroutine("LerpObject");
  }

  IEnumerator LerpObject() {

    while (currentTime <= timeOfTravel) {
      currentTime += Time.deltaTime;
      normalizedValue = currentTime / timeOfTravel; 

      rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, normalizedValue);
      yield return null;
    }

    while (true) {
      yield return StartCoroutine("WiggleWiggleWiggle");
    }
  }

  float currentWiggleTime = 0, wiggleTime = 0.75f;
  float wiggleSpeed = 25f;
  float wiggleDistance = 0.5f;

  IEnumerator WiggleWiggleWiggle() {
    yield return new WaitForSeconds(2f);
    while (currentWiggleTime < wiggleTime) {
      currentWiggleTime += Time.deltaTime;

      playButton.transform.Rotate(0f, 0f, wiggleDistance * Mathf.Cos(currentWiggleTime * wiggleSpeed));
      yield return new WaitForSeconds(0.01f);
    }
    currentWiggleTime = 0f;
  }

}

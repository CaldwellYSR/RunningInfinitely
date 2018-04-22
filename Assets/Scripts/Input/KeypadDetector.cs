using UnityEngine;

public class KeypadDetector : MonoBehaviour, IInputDetector {

  public InputDirection? DetectInputDirection() {

    if (Input.GetButtonDown("Up") || Input.GetButtonDown("Jump")) {
      return InputDirection.Top;
    } else if (Input.GetButtonDown("Down")) {
      return InputDirection.Bottom;
    } else if (Input.GetButtonDown("Right")) {
      return InputDirection.Right;
    } else if (Input.GetButtonDown("Left")) {
      return InputDirection.Left;
    } else {
      return null;
    }

  }
}
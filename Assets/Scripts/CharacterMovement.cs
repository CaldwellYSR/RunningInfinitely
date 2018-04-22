using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {

  public float gravity = 20f;

  public float SideWaysSpeed = 5.0f;

  public float JumpSpeed = 8.0f;
  public float Speed = 6.0f;
  public float SlideSpeed = 5.5f;

  public float slideTimerMax = 2f;

  private Vector3 moveDirection = Vector3.zero;
  private CharacterController controller;
  private Animator anim;

  private int Lane = 0;
  private bool isChangingLane = false;
  private Vector3 locationAfterChangingLane;

  private bool isSliding;
  private float slideTimer;

  private float height;

  private Vector3 sidewaysMovementDistance = Vector3.right * 3f;

  IInputDetector inputDetector = null;

  void Start() {
    this.ResetMoveDirection();

    UIManager.Instance.ResetScore();

    GameManager.Instance.GameState = GameState.Default;

    anim = GetComponent<Animator>();
    controller = GetComponent<CharacterController>();
    inputDetector = GetComponent<IInputDetector>();

    height = controller.height;
  }

  void Update() {
    switch (GameManager.Instance.GameState) {
      case GameState.Default:
        if (Input.GetMouseButtonUp(0)) {
          var instance = GameManager.Instance;
          instance.GameState = GameState.Running;
        }
        break;
      case GameState.Running:
        float h = height;
        Vector3 position = transform.position;
        UIManager.Instance.IncreaseScore(0.1f);
        anim.SetBool(Constants.AnimationStarted, true);

        CheckHeight();

        DetectJumpOrSwipeLeftRight();

        if (isChangingLane) {
          if (Mathf.Abs(transform.position.x - locationAfterChangingLane.x) < 0.1f) {
            isChangingLane = false;
            moveDirection.x = 0;
          }
        }

        if (isSliding) {
          h = 0.5f * height;
          SetSlideMoveDirection();

          slideTimer += Time.deltaTime;
          if (slideTimer > slideTimerMax) {
            anim.SetBool(Constants.AnimationSlide, false);
            isSliding = false;
            ResetMoveDirection();
          }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        var lastHeight = controller.height;
        controller.height = Mathf.Lerp(controller.height, h, 5 * Time.deltaTime);
        position.y += (controller.height - lastHeight) * 0.5f; 
        controller.Move(moveDirection * Time.deltaTime);
        break;

      case GameState.Dead:
        anim.SetBool(Constants.AnimationJump, false);
        anim.SetBool(Constants.AnimationSlide, false);
        anim.SetBool(Constants.AnimationStarted, false);
        StartCoroutine("EndScene");
        break;
      default:
        break;
    }

  }

  private IEnumerator EndScene() {
    yield return new WaitForSeconds(2f); 
    SceneManager.LoadScene("EndScene");
  }

  private void CheckHeight() {
    if (transform.position.y < -10) {
      GameManager.Instance.GameOver();
    }
  }

  private void DetectJumpOrSwipeLeftRight() {
    var inputDirection = inputDetector.DetectInputDirection();

    if (controller.isGrounded && inputDirection.HasValue && inputDirection == InputDirection.Top && !isChangingLane) {
      moveDirection.y = JumpSpeed;
      anim.SetBool(Constants.AnimationJump, true);
      anim.SetBool(Constants.AnimationStarted, false);
    } else if (controller.isGrounded && inputDirection.HasValue && inputDirection == InputDirection.Bottom && !isChangingLane && !isSliding) {
      slideTimer = 0.0f;
      isSliding = true;
      anim.SetBool(Constants.AnimationSlide, true);
      anim.SetBool(Constants.AnimationStarted, false);
    } else if (controller.isGrounded) {
      anim.SetBool(Constants.AnimationJump, false);
      anim.SetBool(Constants.AnimationStarted, true);
    }

    if (controller.isGrounded && inputDirection.HasValue && !isChangingLane) {
      isChangingLane = true;

      if (inputDirection == InputDirection.Left && Lane > Constants.LeftLaneBoundary) {
        --Lane;
        locationAfterChangingLane = transform.position - sidewaysMovementDistance;
        moveDirection.x = -SideWaysSpeed;
      } else if (inputDirection == InputDirection.Right && Lane < Constants.RightLaneBoundary) {
        ++Lane;
        locationAfterChangingLane = transform.position + sidewaysMovementDistance;
        moveDirection.x = SideWaysSpeed;
      }
    }
  }

  private void ResetMoveDirection() {
    moveDirection = transform.forward;
    moveDirection = transform.TransformDirection(moveDirection);
    moveDirection *= Speed;
  }

  private void SetSlideMoveDirection() {
    moveDirection = transform.forward;
    moveDirection = transform.TransformDirection(moveDirection);
    moveDirection *= SlideSpeed;
  }

}

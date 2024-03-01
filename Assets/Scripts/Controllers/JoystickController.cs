using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform joysticOutLine;
    [SerializeField] private RectTransform joysticButton;
    [SerializeField] private float moveFactor;

    private Vector3 move;

    private bool canControlJoystick = false;
    private Vector3 touchPosition;

    // Start is called before the first frame update
    void Start()
    {
        HideJoystick();
    }

    public void TappedOnJoystickZone()
    {

        touchPosition = Input.mousePosition;
        joysticOutLine.position = touchPosition;
        ShowJoystick();
    }

    public void ReleasedToJoystickZone()
    {

        HideJoystick();

    }

    private void ShowJoystick()
    {
        joysticOutLine.gameObject.SetActive(true);
        canControlJoystick = true;
    }

    private void HideJoystick()
    {
        joysticOutLine.gameObject.SetActive(false);
        canControlJoystick = false;
        move = Vector3.zero;
    }

    public void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - touchPosition;

        //float moveMagnitude = direction.magnitude * moveFactor / Screen.width;
        //moveMagnitude = Mathf.Min(moveMagnitude, joysticOutLine.rect.width/2);

        float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
        float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;

        float joysticOutLineHalfWidth = joysticOutLine.rect.width / 2;
        float newWidth = joysticOutLineHalfWidth * canvasYScale;

        moveMagnitude = Mathf.Min(moveMagnitude, newWidth);

        move = direction.normalized * moveMagnitude;

        Vector3 targetPos = touchPosition + move;
        joysticButton.position = targetPos;
    }

    public Vector3 GetMovePosition()
    {
        return move /1.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canControlJoystick)
        {
            ControlJoystick();
        }

    }
}

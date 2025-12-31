using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f; 
        // this initially places the cube gameobject in front of the user three meters
    }

    // Update is called once per frame
    void Update()
    {
        step = 5.0f * Time.deltaTime; // step value to animate the cube

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) centerCube(); // if the user presses the right index trigger, center the cube

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) transform.Rotate(0, 5.0f * step, 0); // rotate the cube left when the user presses the right thumbstick left
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) transform.Rotate(0, -5.0f * step, 0); // rotate the cube right when the user presses the right thumbstick right

        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch); // vibrate the right controller when the user releases the A button
        }

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch); // move the cube to the left controller position when the user holds the left hand trigger
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch); // rotate the cube to the left controller rotation when the user holds the left hand trigger
        }
    }

    // helper function to place and rotate the cube smoothly
    private void centerCube()
    {
        // places the cube gameobject in front of the user, at the center, rotates the cube according to user's headpose

        targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
}

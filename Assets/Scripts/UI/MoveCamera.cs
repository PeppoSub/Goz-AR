using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public int mission_group_index;
    public int[] camera_X_position;
    public Transform camera_transform;
    public float leap_threeshold = 0.05f;

    private void Update()
    {
        move_camera();
    }

    public void move_camera_left()
    { 
        mission_group_index = Mathf.Abs((mission_group_index - 1) % camera_X_position.Length);
    }

    public void move_camera_right()
    {
        mission_group_index = (mission_group_index + 1) % camera_X_position.Length;
    }


    public void move_camera()
    {
        if (camera_transform == null) return;

        // gestiamo il problema degli arrotondamenti   
        Vector3 target_position = new Vector3(camera_X_position[mission_group_index], camera_transform.position.y, camera_transform.position.z);
        if (Mathf.Abs(camera_transform.position.x - target_position.x) < leap_threeshold) return;

        print(Mathf.Abs(camera_transform.position.x - target_position.x));
        camera_transform.position = Vector3.Lerp(camera_transform.position, target_position, Time.deltaTime);
    }

}

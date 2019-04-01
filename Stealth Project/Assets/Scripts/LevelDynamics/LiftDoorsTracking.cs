using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoorsTracking : MonoBehaviour
{
    public float doorSpeed = 7.0f;

    private Transform leftOuterDoor;
    private Transform rightOuterDoor;
    private Transform leftInnerDoor;
    private Transform rightInnerDoor;
    //储存门关闭时的坐标
    private float leftClosedPosx;
    private float rightClosedPosX;

    private void Awake()
    {
        leftOuterDoor = GameObject.Find("door_exit_outer_left_001").transform;
        rightOuterDoor = GameObject.Find("door_exit_outer_right_001").transform;
        leftInnerDoor = GameObject.Find("door_exit_inner_left_001").transform;
        rightInnerDoor = GameObject.Find("door_exit_inner_right_001").transform;

        leftClosedPosx = leftInnerDoor.position.x;
        rightClosedPosX = rightInnerDoor.position.x;
    }

    void MoveDoors(float newLeftXTarget, float newRightXTarget)
    {
        float newX = Mathf.Lerp(leftInnerDoor.position.x, newLeftXTarget, doorSpeed * Time.deltaTime);
        leftInnerDoor.position = new Vector3(newX, leftInnerDoor.position.y, leftInnerDoor.position.z);

        newX = Mathf.Lerp(rightInnerDoor.position.x, newRightXTarget, doorSpeed * Time.deltaTime);
        rightInnerDoor.position = new Vector3(newX, rightInnerDoor.position.y, rightInnerDoor.position.z);
    }

    //开门时候，里门跟着外门坐标走
    public void DoorFollowing()
    {
        MoveDoors(leftOuterDoor.position.x, rightOuterDoor.position.x);
    }

    //关门的时候里门跟着原始关门坐标走
    public void CloseDoors()
    {
        MoveDoors(leftClosedPosx, rightClosedPosX);
    }
}

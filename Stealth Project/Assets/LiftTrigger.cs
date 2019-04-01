using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public float timeToDoorsClose = 2f;
    public float timeToLiftStart = 3f;
    public float timeToEndLevel = 6f;
    public float liftSpeed = 3f;

    private GameObject player;
    private Animator playerAnim;
    private HashIDs hash;
    private CameraMovement cameraMovement;
    private SceneFadeInOut sceneFadeInOut;
    private LiftDoorsTracking liftDoorsTracking;
    private bool playerInLift;
    private float timer;
}

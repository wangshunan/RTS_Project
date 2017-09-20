using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationCanvas : MonoBehaviour {

    [SerializeField]
    Status status;

    [SerializeField]
    Slider hpSlider;

    [SerializeField]
    GameObject parentObject;

    [SerializeField]
    Canvas canvas;

    public Camera rotateCamera;
    private float countDown;
    private const float COUNT_DOWN_MAX = 2;

    private void Awake()
    {
        parentObject = transform.parent.gameObject;   
        status = parentObject.GetComponent<Status>();
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        rotateCamera = Camera.main;
        hpSlider.maxValue = status.hp;
        hpSlider.value = status.hp;
        canvas.enabled = false;
        countDown = 0;
    }

    private void Update()
    {
        LookAtCamera();
        CountDown();
        DrawSlider();

        hpSlider.value = status.hp;
    }

    private void LookAtCamera()
    {
        transform.rotation = rotateCamera.transform.rotation;
    }

    private void CountDown()
    {
        if (hpSlider.value != status.hp)
        {
            countDown = COUNT_DOWN_MAX;
        }

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
    }

    private void DrawSlider()
    {
        if ( countDown > 0 )
        {
            canvas.enabled = true;
        } else {
            canvas.enabled = false;
        }
    }
}
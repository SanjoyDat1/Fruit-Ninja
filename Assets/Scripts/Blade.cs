using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private bool isSlicing;
    private Collider bladeCollider;
    private Camera mainCamera;
    public float minSliceVelocity = 0.0005f;
    private TrailRenderer bladeTrail;
    public float sliceForce = 5f;

    public Vector3 direction;

    private void Awake()
    {
        bladeCollider = GetComponent<Collider>();
        mainCamera = Camera.main;
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startSlicing();
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            stopSlicing();
        }
        else if (isSlicing)
        {
            continueSlicing();
        }
    }

    private void OnEnable()
    {
        stopSlicing();
    }

    private void OnDisable()
    {
        stopSlicing();
    }

    private void startSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        isSlicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();

    }

    private void stopSlicing()
    {
        isSlicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;


    }

    private void continueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        isSlicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;

        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;

    }
}

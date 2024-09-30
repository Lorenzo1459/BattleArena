using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CylinderShrink : MonoBehaviour {
    public float shrinkAmount = 10f; // Amount by which the cylinder shrinks each interval
    public float shrinkInterval = 1f; // Time interval (in seconds) between each shrink
    public Text shrinkTimerText;

    private CapsuleCollider capsuleCollider;
    private float timer;

    void Start() {
        // Get the CapsuleCollider component
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Initialize the timer
        timer = shrinkInterval;

        // Start the shrinking coroutine
        StartCoroutine(ShrinkCoroutine());
    }

    void Update() {
        UpdateTimer();
    }

    IEnumerator ShrinkCoroutine() {
        while (true) {
            yield return new WaitForSeconds(shrinkInterval);
            Shrink();
        }
    }

    void Shrink()
    {
        // Calculate the new scale
        Vector3 newScale = transform.localScale;
        newScale.x -= shrinkAmount;
        newScale.z -= shrinkAmount;

        // Ensure the scale does not go below zero
        if (newScale.x > 0 && newScale.z > 0)
        {
            transform.localScale = newScale;
            UpdateColliderSize();
        }
        else
        {
            // Stop shrinking if the scale reaches zero or below
            StopCoroutine(ShrinkCoroutine());
        }
    }

    void UpdateColliderSize() {
        if (capsuleCollider != null) {
            // Update the collider size to match the object's scale
            capsuleCollider.height = transform.localScale.y;
            capsuleCollider.radius = transform.localScale.x / 2f;
        }
    }

    void UpdateTimer() {
        // Update the timer
        timer -= Time.deltaTime;

        // Ensure the timer does not go below zero
        if (timer < 0) {
            timer = 0;
        }

        // Update the UI text
        if (shrinkTimerText != null) {
            shrinkTimerText.text = $"Arena will be reduced in {Mathf.Ceil(timer)} seconds";
        }        

        // Reset the timer if it reaches zero or below
        if (timer <= 0) {
            timer = shrinkInterval;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;


    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    private void Awake(){
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        Cursor.visible = false; // Hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update(){
        Vector3 mouseWorldPosition = Vector3.zero; 
        Vector2 screenCenterPoint = new Vector2(Screen.width/2f, Screen.height/2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask)){
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if(starterAssetsInputs.aim){
            aimVirtualCamera.gameObject.SetActive(true);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else{
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }

    
}

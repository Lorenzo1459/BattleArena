using StarterAssets;
using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
	public bool canPush = false;
	private Vector3 pushDirection;

	private float timer, currentPushForce;
	[SerializeField]
	private float pushForce;
	
	private CharacterController characterController;

	private ThirdPersonController thirdPersonController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
		if (currentPushForce <= 0)
		{
			canPush = false;
		}

		if (canPush)
		{
            thirdPersonController.canMove = false;
			PushRigidBodies();
		}else{
            thirdPersonController.canMove = true;
		}

	}

	public void Push(Vector3 direction)
	{
		canPush = true;
		currentPushForce = pushForce;
        pushDirection = direction;

    }

	private void PushRigidBodies()
	{
		currentPushForce -= Time.deltaTime;
		characterController.Move(pushDirection * currentPushForce * Time.deltaTime * 5);
		characterController.Move(Vector3.up * currentPushForce * Time.deltaTime * 3);
	}

}
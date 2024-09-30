using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
	public bool canPush = false;
	private Vector3 pushDirection;

	private float timer, currentPushForce;
	[SerializeField]
	private float pushForce;
	
	private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
		if (currentPushForce <= 0)
		{
			canPush = false;
		}

		if (canPush)
		{
			PushRigidBodies();
		}

	}

	public void Push(Vector3 direction, float distanceFactor)
	{
		canPush = true;
		currentPushForce = pushForce;
		currentPushForce -= distanceFactor;
        pushDirection = direction;

    }

	private void PushRigidBodies()
	{
		currentPushForce -= Time.deltaTime;
		characterController.Move(pushDirection * currentPushForce * Time.deltaTime);
	}

}
using UnityEngine;

public class BattleBusController : MonoBehaviour
{
    private Vector3 myTargetPosition;
    private bool myHasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        myHasSpawned = false;

        Vector3 targetDirection = new Vector3(0.0f, transform.position.y, 0.0f) - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));

        float distance = Vector3.Distance(new Vector3(0.0f, transform.position.y, 0.0f), transform.position);
        myTargetPosition = distance * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myHasSpawned)
        {
            Vector3 zero = new Vector3(0.0f, transform.position.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, zero, 2.0f * Time.deltaTime);
            Vector3 targetDirection = zero - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, transform.position.y, targetDirection.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2.0f * Time.deltaTime);

            if (Vector3.Distance(transform.position, zero) < 1.0f)
            {
                FindObjectOfType<LevelManager>().OnSpawn();
                myHasSpawned = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, myTargetPosition) < 1.0f)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, myTargetPosition, 2.0f * Time.deltaTime);
            Vector3 targetDirection = myTargetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2.0f * Time.deltaTime);
        }
    }
}

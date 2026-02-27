using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private List<IAttractable> _objs;

    private void Awake()
    {
        _objs = new List<IAttractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ISeller seller = other.gameObject.GetComponentInParent<ISeller>();

        if (seller != null)
        {
            _objs = seller.Buy();

           /* foreach (IAttractable obj in _objs)
            {
                // obj.Collect();
                obj.TransitToStore();
            }*/
        }
    }

    private void FixedUpdate()
    {
        if (_objs.Count > 0)
        {
            List<IAttractable> objectsToRemove = new List<IAttractable>();


            foreach (var obj in _objs)
            {
                Debug.Log(obj.ToString());
                Move(obj.Transform, transform.position, _moveSpeed, Time.fixedDeltaTime);


                if (IsApproachPosition(transform.position, obj.Transform.position, 0.1f))
                {
                    objectsToRemove.Add(obj);
                    obj.TransitToStore();
                }
            }

            _objs.RemoveAll(obj => objectsToRemove.Contains(obj));
        }
    }

    private bool IsApproachPosition(Vector3 targetPostion, Vector3 objectPosition, float offset)
    {
        return (objectPosition - targetPostion).sqrMagnitude <= offset;
    }

    private void Move(Transform objTransform, Vector3 targetposition, float moveSpeed, float deltatIme)
    {
        objTransform.position = Vector3.MoveTowards(objTransform.position, targetposition, moveSpeed * deltatIme);
    }
}

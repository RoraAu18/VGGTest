using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionController : MonoBehaviour
{
    public List<Collider> currentFrameCollissions = new List<Collider>();
    public List<Collider> lastFrameCollissions = new List<Collider>();
    //Arraly to store
    Collider[] collisions = new Collider[30];
    public Collider theCollider;

    public bool foundOnCurrentCollision = false;
    public LayerMask layerMask;
    public int amountOfObjectsHit = 0;

    public Action<Collider> collisionStay;
    public Action<Collider> collisionEnter;
    public Action<Collider> collisionExit;

    public void Start()
    {
        //TryGetComponent(out theCollider);
    }

    public void Update()
    {
        lastFrameCollissions = currentFrameCollissions.GetRange(0, currentFrameCollissions.Count);
        currentFrameCollissions.Clear();

        int objsHittedAmount = 0;


        if (theCollider is CapsuleCollider myCapsuleCollider)
        {
            objsHittedAmount = Physics.OverlapSphereNonAlloc(transform.position, myCapsuleCollider.radius, collisions, layerMask);
        }

        if (theCollider is BoxCollider myBoxCollider)
        {
            objsHittedAmount = Physics.OverlapBoxNonAlloc(transform.position, myBoxCollider.size / 2, collisions, transform.rotation, layerMask);
        }
        else if (theCollider is CharacterController myCharacterCollider)
        {
            var center = myCharacterCollider.transform.position;
            var point1 = (myCharacterCollider.height / 2) * Vector3.up + center;
            var point0 = (myCharacterCollider.height / 2) * Vector3.down + center;

            objsHittedAmount = Physics.OverlapCapsuleNonAlloc(point0, point1, myCharacterCollider.radius, collisions, layerMask);
        }
        else if (theCollider is SphereCollider sphereCollider)
        {
            var pos = sphereCollider.transform.position;
            var rad = sphereCollider.radius;

            objsHittedAmount = Physics.OverlapSphereNonAlloc(pos, rad, collisions, layerMask);
        }


        for (int i = 0; i < objsHittedAmount; i++)
        {
            var collidedObject = collisions[i];
            if (collidedObject == theCollider) continue;
            currentFrameCollissions.Add(collidedObject);
            var wasCollidingAlready = lastFrameCollissions.Contains(collidedObject);
            if (!wasCollidingAlready)
            {
                collisionEnter?.Invoke(collidedObject);

            }
        }

        for (int i = 0; i < lastFrameCollissions.Count; i++)
        {
            var lastCollision = lastFrameCollissions[i];
            bool foundOnCurrentCollision = false;

            for (int j = 0; j < currentFrameCollissions.Count; j++)
            {
                var currentCollilsion = currentFrameCollissions[j];
                if (currentCollilsion == lastCollision)
                {
                    foundOnCurrentCollision = true;
                    break;
                }
            }
            if (foundOnCurrentCollision)
            {
                //We invoke the lastcollision as it represents the element in the lsit where we were able to measure the status of the collision
                collisionStay?.Invoke(lastCollision);
                //Debug.Log("Collision stay");
            }
            else
            {
                //Same as prev note 
                collisionExit?.Invoke(lastCollision);
                //Debug.Log("Collision Exit");
            }
        }
    }
}

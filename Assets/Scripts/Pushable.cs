using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public void Move(Vector3 movement)
    {
        transform.position += movement;
    }
}

﻿using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter other;

    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        Debug.Log(other.gameObject.activeSelf);
        if (zPos < 0) Teleport(other.transform);
    }

    private void Teleport(Transform obj)
    {
        // Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        obj.position = other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // Rotation
        Quaternion difference = other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        obj.rotation = difference * obj.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = 9;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 8;
    }
}

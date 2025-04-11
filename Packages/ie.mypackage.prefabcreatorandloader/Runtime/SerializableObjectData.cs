using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableObjectData
{
    public string name;
    public string prefabId;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public List<SerializableObjectData> children = new List<SerializableObjectData>();
}

[System.Serializable]
public class SerializableVector3
{
    public float x, y, z;
    public SerializableVector3() { }
    public SerializableVector3(Vector3 v) { x = v.x; y = v.y; z = v.z; }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}

[System.Serializable]
public class SerializableQuaternion
{
    public float x, y, z, w;
    public SerializableQuaternion() { }
    public SerializableQuaternion(Quaternion q) { x = q.x; y = q.y; z = q.z; w = q.w; }
    public Quaternion ToQuaternion() => new Quaternion(x, y, z, w);
}

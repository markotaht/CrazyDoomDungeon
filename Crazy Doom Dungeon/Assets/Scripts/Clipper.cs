using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipper : MonoBehaviour
{
    Matrix4x4 projection;
    [SerializeField]
    Camera mcamera;

    [SerializeField]
    float height = 0.0f;

    [SerializeField]
    public Transform Player;

    void Awake()
    {
        projection = mcamera.projectionMatrix;
    }

    void OnPreRender()
    {
        mcamera.projectionMatrix = projection;
        Matrix4x4 obliqueProjection = projection;

        Vector4 cameraSpaceClipPlane = CameraSpacePlane(mcamera, new Vector3(0.0f, Player.position.y+height, 0.0f), Vector3.up, 1.0f);
        Vector4 clipplane = new Vector4(0, 1, 0, 0);
        CalculateObliqueMatrix(ref obliqueProjection, clipplane);
  //      Debug.Log(cameraSpaceClipPlane);
        mcamera.projectionMatrix = obliqueProjection;
    }

    static Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        Vector3 offsetPos = pos + normal * 0.07f;
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cpos = m.MultiplyPoint(offsetPos);
        Vector3 point = m.inverse.MultiplyPoint(new Vector3(0.0f, 0.0f, 0.0f));
        cpos -= new Vector3(0.0f, point.y, 0.0f);
        Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -cpos.y);
    }

    static Matrix4x4 CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
    {
        Vector4 q = projection.inverse * new Vector4(
            Mathf.Sign(clipPlane.x),
            Mathf.Sign(clipPlane.y),
            1.0f,
            1.0f
        );
        Vector4 c = clipPlane * (2.0F / (Vector4.Dot(clipPlane, q)));
        // third row = clip plane - fourth row
        projection[2] = c.x;
        projection[6] = c.y;
        projection[10] = c.z;
        projection[14] = c.w - 1.0f;
        return projection;
    }
}
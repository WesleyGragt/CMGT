using UnityEditor.ShaderGraph;
using UnityEngine;

public class NormalExercise : MonoBehaviour
{
    /*private Vector3 lookDir = new Vector3(0.6f, 0, 0.8f);
    private Vector3 upDir = new Vector3(0, 1, 0);
    private Vector3 lookDir = new Vector3(0.707f, 0, 0.707f);
    private Vector3 upDir = new Vector3(0, 0, 1);

    Vector3 rightDir;

    private void Start()
    {
        rightDir = Vector3.Cross(upDir, lookDir);
        Debug.Log($"Right Direction: {rightDir}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, lookDir);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, upDir);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rightDir);
    }*/

    /*private Vector3 point1 = new Vector3(1, 0, 0);
    private Vector3 point2 = new Vector3(0, 1, 0);
    private Vector3 point3 = new Vector3(0, 0, 1);

    private Vector3 cross;
    private void Start()
    {
        Vector3 lookDir = point2 - point1;
        Vector3 upDir = point3 - point1;

        cross = Vector3.Cross(lookDir, upDir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point2, point3);
        Gizmos.DrawLine(point3, point1);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(point1, cross);
    }*/


}

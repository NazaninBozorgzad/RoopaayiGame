using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int lineSegmentCount = 20;
    public float timeStep = 0.1f;
    public Rigidbody2D rb;
    public float forceMultiplier = 4f;

    public void ShowTrajectory(Vector2 startPos, Vector2 dragVector)
    {
        Vector2 velocity = -dragVector * forceMultiplier;
        Vector2 currentPosition = startPos;
        Vector2 currentVelocity = velocity;

        lineRenderer.positionCount = lineSegmentCount;

        for (int i = 0; i < lineSegmentCount; i++)
        {
            lineRenderer.SetPosition(i, currentPosition);
            currentVelocity += Physics2D.gravity * timeStep;
            currentPosition += currentVelocity * timeStep;
        }

        lineRenderer.enabled = true;
    }

    public void HideTrajectory()
    {
        lineRenderer.enabled = false;
    }
}

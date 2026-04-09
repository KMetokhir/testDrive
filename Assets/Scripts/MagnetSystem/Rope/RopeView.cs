using UnityEngine;

public class RopeView
{
    private LineRenderer _lineRenderer;
    private int _segments = 20;
    private float _sagAmount = 1f;
    private float _width = 0.2f;

    public RopeView(LineRenderer lineRenderer, int segments, float sagAmount, float width)
    {
        _lineRenderer = lineRenderer;

        _segments = segments;
        _sagAmount = sagAmount;
        _width = width;

        _lineRenderer.positionCount = _segments;
        _lineRenderer.startWidth = _width;
        _lineRenderer.endWidth = _width;
    }

    public void DrawRope(Vector3 start, Vector3 end)
    {

        Vector3 mid = (start + end) / 2f;
        Vector3 sag = mid + Vector3.down * _sagAmount;

        for (int i = 0; i < _segments; i++)
        {
            float t = i / (float)(_segments - 1);
            _lineRenderer.SetPosition(i, GetBezierPoint(t, start, sag, end));
        }
    }

    private Vector3 GetBezierPoint(float t, Vector3 start, Vector3 sag, Vector3 end)
    {
        return Mathf.Pow(1 - t, 2) * start +
               2 * (1 - t) * t * sag +
               Mathf.Pow(t, 2) * end;
    }
}

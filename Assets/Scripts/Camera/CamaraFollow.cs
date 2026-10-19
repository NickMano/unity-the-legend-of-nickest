using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour {

	public float smoothTime = 3f;

    public Transform target;
    public float tLX, tLY, bRX, bRY;

    Vector2 velocity;
    bool hasBounds;

    void Awake()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
    }

	void LateUpdate()
	{
		if (target == null) return;
		float posX = Mathf.Round(
            Mathf.SmoothDamp(transform.position.x,
                target.position.x, ref velocity.x, smoothTime) * 100) / 100;
        float posY = Mathf.Round(
            Mathf.SmoothDamp(transform.position.y,
                target.position.y, ref velocity.y, smoothTime) * 100) / 100;
		transform.position = new Vector3(
			hasBounds ? Mathf.Clamp(posX, tLX, bRX) : posX,
			hasBounds ? Mathf.Clamp(posY, bRY, tLY) : posY,
			transform.position.z);

		if (transform.position.y - 6f > target.transform.position.y)
            target.SendMessage("SeCayo");
    }

    public void SetBound(GameObject map)
    {
        hasBounds = false;
        Camera view = GetComponent<Camera>();
        Bounds bounds = new Bounds();
        if (map != null && view != null)
        {
            foreach (Renderer mapRenderer in map.GetComponentsInChildren<Renderer>())
            {
                if (!mapRenderer.enabled || mapRenderer.bounds.size.x <= 0 ||
                    mapRenderer.bounds.size.y <= 0) continue;

                if (!hasBounds) bounds = mapRenderer.bounds;
                else bounds.Encapsulate(mapRenderer.bounds);
                hasBounds = true;
            }
        }

        if (hasBounds)
        {
            float halfHeight = view.orthographicSize;
            float halfWidth = halfHeight * view.aspect;
            // A map smaller than the viewport stays centered on that axis.
            tLX = Mathf.Min(bounds.min.x + halfWidth, bounds.center.x);
            bRX = Mathf.Max(bounds.max.x - halfWidth, bounds.center.x);
            bRY = Mathf.Min(bounds.min.y + halfHeight, bounds.center.y);
            tLY = Mathf.Max(bounds.max.y - halfHeight, bounds.center.y);
        }

        FastMove();

    }

    public void FastMove()
    {
        velocity = Vector2.zero;
        if (target == null) return;
        transform.position = new Vector3(
            hasBounds ? Mathf.Clamp(target.position.x, tLX, bRX) : target.position.x,
            hasBounds ? Mathf.Clamp(target.position.y, bRY, tLY) : target.position.y,
            transform.position.z
        );
    }
}

using UnityEngine;
using System.Collections;

namespace Footsteps {

	public class CameraFollow : MonoBehaviour {

Transform target;
public Vector3 offset;
 public float followLerpFactor = 1f;

		Transform thisTransform;


		void Start() {
            target = PlayerManager.instance.player.transform;

            if (!target) enabled = false;

			thisTransform = transform;
		}

		void Update() {
			thisTransform.position = Vector3.Lerp(thisTransform.position, target.position + offset, followLerpFactor);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactLinked : MonoBehaviour {

    public ArtifactType artifactType = ArtifactType.Purple;

    // Just came out of watching Infinity War ):
    public void Disintegrate() {
        Destroy(gameObject);
    }
}

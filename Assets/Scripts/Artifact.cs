using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour {

    public ArtifactType type = ArtifactType.Default;
	
    //One time. Upon collision with player
	public void Activate() {
        ArtifactLinked[] linkedObjects = FindObjectsOfType<ArtifactLinked>();
        foreach (ArtifactLinked obj in linkedObjects) {
            if (obj.artifactType == type) {
                obj.Disintegrate();
            }
        }
    }
}

public enum ArtifactType { Default }
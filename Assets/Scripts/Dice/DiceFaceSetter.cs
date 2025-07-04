using UnityEngine;

public class DiceFaceSetter : MonoBehaviour
{
    public MeshRenderer[] faceRenderers = new MeshRenderer[6];

    public void SetFaces(GameObject[] faceAssets)
    {
        for (int i = 0; i < 6; i++)
        {
            if (faceAssets[i] != null && faceRenderers[i] != null)
            {
                // 예: Material 교체
                Material newMat = faceAssets[i].GetComponent<Renderer>().sharedMaterial;
                faceRenderers[i].material = newMat;
            }
        }
    }
}

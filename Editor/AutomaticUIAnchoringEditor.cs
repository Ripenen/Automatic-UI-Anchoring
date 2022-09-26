#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Automatic_UI_Anchoring.Editor
{
    public class AutomaticUIAnchoringEditor : UnityEditor.Editor
    {
        private static void ChangeAnchorsPositions(RectTransform rectTransform, RectTransform parent)
        {
            Undo.RecordObject(rectTransform, "Anchor UI Objects");
            Rect parentRect = parent.rect;
            
            rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x + (rectTransform.offsetMin.x / parentRect.width),
                rectTransform.anchorMin.y + (rectTransform.offsetMin.y / parentRect.height));
            rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x + (rectTransform.offsetMax.x / parentRect.width),
                rectTransform.anchorMax.y + (rectTransform.offsetMax.y / parentRect.height));
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        [MenuItem("Tools/Automatic UI Anchoring/Anchor All UI Objects In Scene _F2")]
        private static void AnchorAllUIObjectsInScene()
        {
            foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var rectTransform in root.GetComponentsInChildren<RectTransform>())
                {
                    if(rectTransform.parent is RectTransform parent)
                        ChangeAnchorsPositions(rectTransform, parent);
                }
            }
        }
    }
}
#endif

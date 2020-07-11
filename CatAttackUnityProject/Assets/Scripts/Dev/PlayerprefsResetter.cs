using UnityEngine;
 
public class PlayerprefsResetter : MonoBehaviour
{
    /// Add a context menu named "Do Something" in the inspector
    /// of the attached script.
    [ContextMenu("Reset Playerprefs")]
    void ResetPlayerprefs()
    {
        Debug.LogWarning("ResettingPlayerprefs");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
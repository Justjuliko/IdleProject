using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    /// <summary>
    /// Opens the specified URL in the default web browser.
    /// </summary>
    /// <param name="url">The URL to open.</param>
    public void OpenLink(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
            Debug.Log($"Opening URL: {url}");
        }
        else
        {
            Debug.LogWarning("The URL is null or empty.");
        }
    }
}

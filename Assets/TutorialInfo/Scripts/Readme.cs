using UnityEngine;

public class Readme : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections;
    public Readme nextReadme;
    public Readme prevReadme;

    // Agrega la propiedad isRoot
    public bool isRoot { get; set; }

    [System.Serializable]
    public class Section
    {
        public string heading;
        public string text;
        public string linkText;
        public string url;
        public string name;
    }
}
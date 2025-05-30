using UnityEngine;

namespace DS.Data.Error
{
    public class DSErrorData
    {
        public Color Color { get; set; }

        public DSErrorData()
        {
            GenerateColor();
        }

        private void GenerateColor()
        {
            Color = new Color32(
                (byte)Random.Range(65, 256),
                (byte)Random.Range(50, 176),
                (byte)Random.Range(50, 176),
                255
            );
        }
    }
}
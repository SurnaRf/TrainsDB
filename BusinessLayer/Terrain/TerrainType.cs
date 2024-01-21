namespace BusinessLayer.Terrain
{
    public enum TerrainType
    {
        Plains   = (1 << 0),
        Forest   = (1 << 1),
        Mountain = (1 << 2),
        Tunnel   = (1 << 3),
        Desert   = (1 << 4),
        Snow     = (1 << 5),
    }
}

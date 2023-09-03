namespace Ships.Enums
{
    /// <summary>
    /// Defines the best sailing point for a ship, these angles are based on the wind direction being 0 degrees.
    /// Reference: https://static.wikia.nocookie.net/sidmeierspirates/images/e/eb/Wind_PointsOfSailing.png/revision/latest?cb=20110418161442
    /// </summary>
    public enum ShipWindDirections
    {
        BeforeTheWind, // 0° to the wind (wind coming in directly from behind the ship)
        RunningBroadReach, // 22.5° to the wind
        BroadReach, // 45° to the wind
        BroadBeamReach, // 67.5° to the wind
        BeamReach, // 90° to the wind (wind coming in directly perpendicular to the ship's heading)
        CloseHauledBeamReach, // 112.5° to the wind
        CloseHauled, // 135° to the wind
        CloseHauledIntoTheEye, // 157.5° to the wind
        IntoTheEye // 180° to the wind (wind coming in directly from the front of the ship)
    }
}

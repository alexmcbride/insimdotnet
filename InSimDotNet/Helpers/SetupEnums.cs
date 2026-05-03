namespace InSimDotNet.Helpers {
    public enum DiffType {
        Open = 0,
        Locked = 1,
        Viscous = 2,
        ClutchPack = 3
    }

    public enum CenterDiffType {
        Open = 0,
        Viscous = 1
    }

    public enum TyreCompound {
        R1 = 0,
        R2 = 1,
        R3 = 2,
        R4 = 3,
        RoadSuper = 4,
        RoadNormal = 5,
        Hybrid = 6,
        Knobbly = 7
    }

    public enum WheelManufacturer {
        CromoPlain = 0,
        Cromo = 1,
        Torro = 2,
        Michelin = 3,
        Evostar = 4,
        Bridgestone = 5,
        Avon = 6
    }

    public static class SetupEnumExtensions {
        public static string ToDisplayName(this DiffType value) {
            switch (value) {
                case DiffType.Open:       return "Open";
                case DiffType.Locked:     return "Locked";
                case DiffType.Viscous:    return "Viscous";
                case DiffType.ClutchPack: return "Clutch Pack";
                default: return "Unknown (" + (int)value + ")";
            }
        }

        public static string ToDisplayName(this CenterDiffType value) {
            switch (value) {
                case CenterDiffType.Open:    return "Open";
                case CenterDiffType.Viscous: return "Viscous";
                default: return "Unknown (" + (int)value + ")";
            }
        }

        public static string ToDisplayName(this TyreCompound value) {
            switch (value) {
                case TyreCompound.R1:         return "R1";
                case TyreCompound.R2:         return "R2";
                case TyreCompound.R3:         return "R3";
                case TyreCompound.R4:         return "R4";
                case TyreCompound.RoadSuper:  return "Road Super";
                case TyreCompound.RoadNormal: return "Road Normal";
                case TyreCompound.Hybrid:     return "Hybrid";
                case TyreCompound.Knobbly:    return "Knobbly";
                default: return "Unknown (" + (int)value + ")";
            }
        }

        public static string ToDisplayName(this WheelManufacturer value) {
            switch (value) {
                case WheelManufacturer.CromoPlain:  return "Cromo Plain";
                case WheelManufacturer.Cromo:       return "Cromo";
                case WheelManufacturer.Torro:       return "Torro";
                case WheelManufacturer.Michelin:    return "Michelin";
                case WheelManufacturer.Evostar:     return "Evostar";
                case WheelManufacturer.Bridgestone: return "Bridgestone";
                case WheelManufacturer.Avon:        return "Avon";
                default: return "Unknown (" + (int)value + ")";
            }
        }
    }
}

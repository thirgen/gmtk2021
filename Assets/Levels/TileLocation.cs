namespace Levels
{
    /// <summary>
    /// See Textures/TilemapLayout.png for visualisation.
    /// <para>Numbers 0-3 indicate corners starting from bottom left, going anti-clockwise.</para>
    /// <para>Note: 'et'/'em'/'eb' (EnclosedTop/etc.) changed to VerticalTop/etc.</para>
    /// </summary>
    public enum TileLocation
    {
        // first row
        TopLeft, TopMiddle, TopRight, VerticalTop,
        TopLeft1, TopMiddle1, TopMiddle0, TopRight0,
        TopMiddle01, Middle02,

        // second row
        MiddleLeft, Middle, MiddleRight, VerticalMiddle,
        MiddleLeft1, Middle1, Middle0, MiddleRight0,
        Middle01, Middle13,

        // third row
        BottomLeft, BottomMiddle, BottomRight, VerticalBottom,
        MiddleLeft2, Middle2, Middle3, MiddleRight3,
        Middle23, Middle023, Middle123,

        // fourth row
        HorizontalLeft, HorizontalMiddle, HorizontalRight, Square,
        BottomLeft2, BottomMiddle2, BottomMiddle3, BottomRight3,
        BottomMiddle23, Middle013, Middle012,

        // fifth row
        MiddleLeft12, Middle12, Middle03, MiddleRight03, Middle0123
    }

    /// <summary>
    /// basics are 4 bit. //todo add corners with additional 4 bits
    /// </summary>
    public enum TileLocationFromAdjacent
    {
        Square = 0b0000,
        HorizontalLeft = 0b0001,
        HorizontalRight = 0b0010,
        HorizontalMiddle = 0b0011,

        VerticalTop = 0b0100,
        TopLeft = 0b0101,
        TopRight = 0b0110,
        TopMiddle = 0b0111,

        VerticalBottom = 0b1000,
        BottomLeft = 0b1001,
        BottomRight = 0b1010,
        BottomMiddle = 0b1011,

        VerticalMiddle = 0b1100,
        MiddleLeft = 0b1101,
        MiddleRight = 0b1110,
        Middle = 0b1111
    }

    public static class TileLocationUtil
    {
        public static TileLocation Convert(this TileLocationFromAdjacent t)
        {
            switch (t)
            {
                case TileLocationFromAdjacent.Square:
                    return TileLocation.Square;
                case TileLocationFromAdjacent.HorizontalLeft:
                    return TileLocation.HorizontalLeft;
                case TileLocationFromAdjacent.HorizontalRight:
                    return TileLocation.HorizontalRight;
                case TileLocationFromAdjacent.HorizontalMiddle:
                    return TileLocation.HorizontalMiddle;

                case TileLocationFromAdjacent.VerticalTop:
                    return TileLocation.VerticalTop;
                case TileLocationFromAdjacent.TopLeft:
                    return TileLocation.TopLeft;
                case TileLocationFromAdjacent.TopRight:
                    return TileLocation.TopRight;
                case TileLocationFromAdjacent.TopMiddle:
                    return TileLocation.TopMiddle;

                case TileLocationFromAdjacent.VerticalBottom:
                    return TileLocation.VerticalBottom;
                case TileLocationFromAdjacent.BottomLeft:
                    return TileLocation.BottomLeft;
                case TileLocationFromAdjacent.BottomRight:
                    return TileLocation.BottomRight;
                case TileLocationFromAdjacent.BottomMiddle:
                    return TileLocation.BottomMiddle;

                case TileLocationFromAdjacent.VerticalMiddle:
                    return TileLocation.VerticalMiddle;
                case TileLocationFromAdjacent.MiddleLeft:
                    return TileLocation.MiddleLeft;
                case TileLocationFromAdjacent.MiddleRight:
                    return TileLocation.MiddleRight;
                case TileLocationFromAdjacent.Middle:
                    return TileLocation.Middle;

                default:
                    throw new System.ArgumentOutOfRangeException(t.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;

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
    
    [Flags]
    public enum TileLocationShit
    {
        None = 0b0000,
        Right = 0b0001,
        Left = 0b0010,
        Down = 0b0100,
        Up = 0b1000,
        Tl = 0b0001_0000, // corner 3
        Tr = 0b0010_0000, // corner 2
        Br = 0b0100_0000, // corner 1
        Bl = 0b1000_0000 // corner 0
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Levels.TileLocationShit;
//using J = Levels.TileLocationShit;

namespace Levels
{
    public static class TileLocationUtil
    {

        public static TileLocation Convert(this TileLocationShit t)
        {
            // square - corners dont matter
            if ((t | None) == None)
                return TileLocation.Square;

            // horizontals - corners dont matter
            if ((t | Right) == Right)
                return TileLocation.HorizontalLeft;
            if ((t | Left) == Left)
                return TileLocation.HorizontalRight;
            if ((t & (Left | Right | Up | Down)) == (Left | Right))
                return TileLocation.HorizontalMiddle;

            // verticals - corners dont matter
            if ((t | Down) == Down)
                return TileLocation.VerticalTop;
            if ((t | Up) == Up)
                return TileLocation.VerticalBottom;
            if ((t & (Up | Down | Left | Right)) == (Up | Down))
                return TileLocation.VerticalMiddle;

            // top (Down = true, Up = false
            if ((t & (Down | Up)) == Down)
            {
                // middle
                if ((t & (Left | Right)) == (Left | Right))
                {
                    // corners
                    if ((t & (Bl | Br)) == (Bl | Br))
                        return TileLocation.TopMiddle01;
                    if ((t & Bl) == Bl)
                        return TileLocation.TopMiddle0;
                    if ((t & Br) == Br)
                        return TileLocation.TopMiddle1;

                    // else no corners
                    return TileLocation.TopMiddle;
                }

                // Left
                if ((t & Right) == Right)
                {
                    // corners
                    if ((t & Br) == Br)
                        return TileLocation.TopLeft1;

                    // else no corners
                    return TileLocation.TopLeft;
                }

                // Right
                if ((t & Left) == Left)
                {
                    // corners
                    if ((t & Bl) == Bl)
                        return TileLocation.TopRight0;

                    // else no corners
                    return TileLocation.TopRight;
                }

            }

            return TileLocation.Middle;


            switch (t)
            {
                /*
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
                    */
                default:
                    throw new System.ArgumentOutOfRangeException(t.ToString());
            }
        }
    }
}

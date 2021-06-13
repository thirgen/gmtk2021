using static Levels.TileLocationFlags;

namespace Levels
{
    public static class TileLocationUtil
    {
        // todo break up into smaller methods
        /// <summary>
        /// Changes the Flag bases <see cref="TileLocationFlags"/> into the
        /// tilesheet-order based <see cref="TileLocation"/>.
        /// </summary>
        public static TileLocation Convert(this TileLocationFlags t)
        {
            TileLocationFlags udlr = Up | Down | Left | Right;
            TileLocationFlags ud = Up | Down;
            TileLocationFlags lr = Left | Right;

            // square - corners dont matter
            if ((t & udlr) == udlr)
                return TileLocation.Square;

            // horizontals - corners dont matter
            if ((t & ud) == ud)
            {
                if ((t & Right) == Right)
                    return TileLocation.HorizontalRight;
                if ((t & Left) == Left)
                    return TileLocation.HorizontalLeft;
                return TileLocation.HorizontalMiddle;
            }

            // verticals - corners dont matter
            if ((t & lr) == lr)
            {
                if ((t & Down) == Down)
                    return TileLocation.VerticalBottom;
                if ((t & Up) == Up)
                    return TileLocation.VerticalTop;
                return TileLocation.VerticalMiddle;
            }

            // top
            if ((t & Up) == Up)
            {
                // Left
                if ((t & Left) == Left)
                {
                    // corners
                    if ((t & Br) == Br)
                        return TileLocation.TopLeft1;

                    // else no corners
                    return TileLocation.TopLeft;
                }

                // Right
                if ((t & Right) == Right)
                {
                    // corners
                    if ((t & Bl) == Bl)
                        return TileLocation.TopRight0;

                    // else no corners
                    return TileLocation.TopRight;
                }

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


            // bottom
            if ((t & Down) == Down)
            {
                // Left
                if ((t & Left) == Left)
                {
                    // corners
                    if ((t & Tr) == Tr)
                        return TileLocation.BottomLeft2;

                    // else no corners
                    return TileLocation.BottomLeft;
                }

                // Right
                if ((t & Right) == Right)
                {
                    // corners
                    if ((t & Tl) == Tl)
                        return TileLocation.BottomRight3;

                    // else no corners
                    return TileLocation.BottomRight;
                }

                // corners
                if ((t & (Tl | Tr)) == (Tl | Tr))
                    return TileLocation.BottomMiddle23;
                if ((t & Tl) == Tl)
                    return TileLocation.BottomMiddle3;
                if ((t & Tr) == Tr)
                    return TileLocation.BottomMiddle2;

                // else no corners
                return TileLocation.BottomMiddle;
            }


            // middle-left
            if ((t & Left) == Left)
            {
                // corners
                if ((t & (Br | Tr)) == (Br | Tr))
                    return TileLocation.MiddleLeft12;
                if ((t & Br) == Br)
                    return TileLocation.MiddleLeft1;
                if ((t & Tr) == Tr)
                    return TileLocation.MiddleLeft2;

                // else no corners
                return TileLocation.MiddleLeft;
            }

            // middle-right
            if ((t & Right) == Right)
            {
                // corners
                if ((t & (Bl | Tl)) == (Bl | Tl))
                    return TileLocation.MiddleRight03;
                if ((t & Bl) == Bl)
                    return TileLocation.MiddleRight0;
                if ((t & Tl) == Tl)
                    return TileLocation.MiddleRight3;

                // else no corners
                return TileLocation.MiddleRight;
            }

            // middle corners

            // bottom left - 0
            if ((t & Bl) == Bl)
            {
                if ((t & (Br | Tr | Tl)) == (Br | Tr | Tl))
                    return TileLocation.Middle0123;
                if ((t & (Tr | Tl)) == (Tr | Tl))
                    return TileLocation.Middle023;
                if ((t & (Br | Tl)) == (Br | Tl))
                    return TileLocation.Middle013;
                if ((t & (Br | Tr)) == (Br | Tr))
                    return TileLocation.Middle012;
                if ((t & Tl) == Tl)
                    return TileLocation.Middle03;
                if ((t & Tr) == Tr)
                    return TileLocation.Middle02;
                if ((t & Br) == Br)
                    return TileLocation.Middle01;

                return TileLocation.Middle0;
            }
            // bottom right - 1
            if ((t & Br) == Br)
            {
                if ((t & (Tr | Tl)) == (Tr | Tl))
                    return TileLocation.Middle123;
                if ((t & Tl) == Tl)
                    return TileLocation.Middle13;
                if ((t & Tr) == Tr)
                    return TileLocation.Middle12;

                return TileLocation.Middle1;
            }
            // top right - 2
            if ((t & Tr) == Tr)
            {
                if ((t & Tl) == Tl)
                    return TileLocation.Middle23;

                return TileLocation.Middle2;
            }
            // top left - 3
            if ((t & Tl) == Tl)
                return TileLocation.Middle3;

            return TileLocation.Middle;
        }
    }
}

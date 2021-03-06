using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightGameInterface.Utils {
    public static class Draw {
        public static Texture2D t;
        public static void DrawLine(SpriteBatch sb,
                                    Vector2 start, Vector2 end, 
                                    Color color, int width = 1) {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(t,
                new Rectangle( // rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    width), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle, //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }

        public static void DrawAroundSprite(SpriteBatch sb, Vector2 pos, Vector2 sprite, int offset = 10) {
            const int lineWidth = 3;

            Vector2 topLeft = new (pos.X - offset, pos.Y - offset);
            Vector2 topRight = new( pos.X + sprite.X + offset , pos.Y - offset);
            Vector2 bottomLeft = new (pos.X - offset, pos.Y + sprite.Y + offset);
            Vector2 bottomRight = new (pos.X + sprite.X + offset, pos.Y + sprite.Y + offset);

            DrawLine(sb, topLeft, topRight, Color.White, lineWidth);
            DrawLine(sb, topRight, bottomRight, Color.White, lineWidth);
            DrawLine(sb, bottomRight, bottomLeft, Color.White, lineWidth);
            DrawLine(sb, bottomLeft, topLeft, Color.White, lineWidth);
            
        }
    }
    
}
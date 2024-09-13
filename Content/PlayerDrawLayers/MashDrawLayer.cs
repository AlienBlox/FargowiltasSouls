// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.MashDrawLayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.PlayerDrawLayers
{
  public class MashDrawLayer : PlayerDrawLayer
  {
    public virtual bool IsHeadLayer => false;

    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return ((Entity) drawInfo.drawPlayer).whoAmI == Main.myPlayer && ((Entity) drawInfo.drawPlayer).active && !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.ghost && (double) drawInfo.shadow == 0.0 && drawInfo.drawPlayer.FargoSouls().Mash;
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.Between();
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      Player drawPlayer = drawInfo.drawPlayer;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/PlayerDrawLayers/DPad", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / 4;
      int num2 = num1 * (int) ((double) Main.GlobalTimeWrappedHourly % 0.5 * 8.0);
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Subtraction((double) drawPlayer.gravDir > 0.0 ? ((Entity) drawPlayer).Bottom : ((Entity) drawPlayer).Top, Main.screenPosition);
      vector2.Y += 48f * drawPlayer.gravDir;
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, vector2, new Rectangle?(rectangle), Color.White, (double) drawPlayer.gravDir < 0.0 ? 3.14159274f : 0.0f, Vector2.op_Division(Utils.Size(rectangle), 2f), 2f, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.RoseTintedVisorDrawLayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.Challengers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.PlayerDrawLayers
{
  public class RoseTintedVisorDrawLayer : PlayerDrawLayer
  {
    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return drawInfo.drawPlayer.HeldItem.type == ModContent.ItemType<RoseTintedVisor>() && !drawInfo.drawPlayer.dead;
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.Between();
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      Player drawPlayer = drawInfo.drawPlayer;
      Vector2 vector2 = Vector2.op_Subtraction((double) drawPlayer.gravDir > 0.0 ? ((Entity) drawPlayer).Top : ((Entity) drawPlayer).Bottom, Main.screenPosition);
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/PlayerDrawLayers/RoseTintedVisorDrawLayer", (AssetRequestMode) 1).Value;
      Rectangle bounds = texture2D.Bounds;
      SpriteEffects spriteEffects = (double) ((Entity) drawPlayer).direction == -(double) drawPlayer.gravDir ? (SpriteEffects) 1 : (SpriteEffects) 0;
      vector2.Y += 7f * drawPlayer.gravDir;
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, vector2, new Rectangle?(bounds), Color.White, (double) drawPlayer.gravDir < 0.0 ? 3.14159274f : 0.0f, Vector2.op_Division(Utils.Size(bounds), 2f), 1f, spriteEffects, 0.0f);
      drawInfo.DrawDataCache.Add(drawData);
    }
  }
}

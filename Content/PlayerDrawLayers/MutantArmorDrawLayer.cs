// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.MutantArmorDrawLayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.PlayerDrawLayers
{
  public class MutantArmorDrawLayer : PlayerDrawLayer
  {
    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return ((Entity) drawInfo.drawPlayer).active && !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.ghost && (double) drawInfo.shadow == 0.0 && drawInfo.drawPlayer.FargoSouls().MutantSetBonusItem != null;
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.Between();
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      if ((double) drawInfo.shadow != 0.0)
        return;
      Player drawPlayer = drawInfo.drawPlayer;
      FargoSoulsPlayer fargoSoulsPlayer = drawPlayer.FargoSouls();
      if (fargoSoulsPlayer.MutantSetBonusItem == null)
        return;
      if (fargoSoulsPlayer.frameCounter % 4 == 0 && ++fargoSoulsPlayer.frameMutantAura >= 19)
        fargoSoulsPlayer.frameMutantAura = 0;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantAura", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / 19;
      int num2 = (int) ((double) drawPlayer.MountedCenter.X - (double) Main.screenPosition.X);
      int num3 = (int) ((double) drawPlayer.MountedCenter.Y - (double) Main.screenPosition.Y - 16.0 * (double) drawPlayer.gravDir);
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, new Vector2((float) num2, (float) num3), new Rectangle?(new Rectangle(0, num1 * fargoSoulsPlayer.frameMutantAura, texture2D.Width, num1)), Color.White, (double) drawPlayer.gravDir < 0.0 ? 3.14159274f : 0.0f, new Vector2((float) texture2D.Width / 2f, (float) num1 / 2f), 1f, ((Entity) drawPlayer).direction < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.SnowstormDrawLayer
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
  public class SnowstormDrawLayer : PlayerDrawLayer
  {
    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return ((Entity) drawInfo.drawPlayer).whoAmI == Main.myPlayer && ((Entity) drawInfo.drawPlayer).active && !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.ghost && (double) drawInfo.shadow == 0.0 && drawInfo.drawPlayer.FargoSouls().SnowVisual;
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
      FargowiltasSouls.FargowiltasSouls instance = FargowiltasSouls.FargowiltasSouls.Instance;
      FargoSoulsPlayer fargoSoulsPlayer = drawPlayer.FargoSouls();
      if (!fargoSoulsPlayer.SnowVisual)
        return;
      if (fargoSoulsPlayer.frameCounter % 5 == 0 && ++fargoSoulsPlayer.frameSnow > 20)
        fargoSoulsPlayer.frameSnow = 1;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Souls/SnowBlizzard", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / 20;
      int num2 = (int) ((double) drawPlayer.MountedCenter.X - (double) Main.screenPosition.X);
      int num3 = (int) ((double) drawPlayer.MountedCenter.Y - (double) Main.screenPosition.Y);
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, new Vector2((float) num2, (float) num3), new Rectangle?(new Rectangle(0, num1 * fargoSoulsPlayer.frameSnow, texture2D.Width, num1)), Lighting.GetColor((int) (((double) drawInfo.Position.X + (double) ((Entity) drawPlayer).width / 2.0) / 16.0), (int) (((double) drawInfo.Position.Y + (double) ((Entity) drawPlayer).height / 2.0) / 16.0)), (double) drawPlayer.gravDir < 0.0 ? 3.14159274f : 0.0f, new Vector2((float) texture2D.Width / 2f, (float) num1 / 2f), 1f, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData);
    }
  }
}

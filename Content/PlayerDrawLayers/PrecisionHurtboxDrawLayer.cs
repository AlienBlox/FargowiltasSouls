// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.PrecisionHurtboxDrawLayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.PlayerDrawLayers
{
  public class PrecisionHurtboxDrawLayer : PlayerDrawLayer
  {
    public virtual bool IsHeadLayer => false;

    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return ((Entity) drawInfo.drawPlayer).whoAmI == Main.myPlayer && ((Entity) drawInfo.drawPlayer).active && !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.ghost && (double) drawInfo.shadow == 0.0 && drawInfo.drawPlayer.FargoSouls().PrecisionSealNoDashNoJump && drawInfo.drawPlayer.HasEffect<PrecisionSealHurtbox>();
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.Between();
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      Player drawPlayer = drawInfo.drawPlayer;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/PlayerDrawLayers/PrecisionHurtboxDrawLayer", (AssetRequestMode) 1).Value;
      Rectangle bounds = texture2D.Bounds;
      float num1 = (float) Main.mouseTextColor / (float) byte.MaxValue;
      float num2 = num1 * num1;
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, Vector2.op_Subtraction(((Entity) drawPlayer).Center, Main.screenPosition), new Rectangle?(bounds), Color.op_Multiply(Color.White, num2), 0.0f, Vector2.op_Division(Utils.Size(bounds), 2f), 1f, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData);
    }
  }
}

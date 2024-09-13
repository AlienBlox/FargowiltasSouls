// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.NinjaSpeedDrawLayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.PlayerDrawLayers
{
  public class NinjaSpeedDrawLayer : PlayerDrawLayer
  {
    public int DrawTime;

    public virtual bool IsHeadLayer => false;

    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      Player drawPlayer = drawInfo.drawPlayer;
      if (drawPlayer == null || !((Entity) drawPlayer).active || drawPlayer.dead || drawPlayer.ghost || ((Entity) drawPlayer).whoAmI != Main.myPlayer || (double) drawInfo.shadow != 0.0)
        return false;
      FargoSoulsPlayer fargoSoulsPlayer = drawPlayer.FargoSouls();
      if (fargoSoulsPlayer == null || !drawPlayer.HasEffect<NinjaEffect>())
        return false;
      float num = fargoSoulsPlayer.ForceEffect<NinjaEnchant>() ? 7f : 4f;
      if ((double) ((Vector2) ref ((Entity) drawPlayer).velocity).Length() < (double) num && this.DrawTime < 15)
        ++this.DrawTime;
      else if (this.DrawTime > 0)
        --this.DrawTime;
      return this.DrawTime > 0;
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.BeforeParent(PlayerDrawLayerLoader.Layers[0]);
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) drawInfo.drawPlayer).Center, Main.screenPosition);
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing", (AssetRequestMode) 1).Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      float num = 0.4f * Math.Min((float) this.DrawTime / 15f, 1f);
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, vector2_1, new Rectangle?(rectangle), Color.op_Multiply(Color.Black, num), 0.0f, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData);
    }
  }
}

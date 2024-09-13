// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.RustrifleReload
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.Challengers;
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
  public class RustrifleReload : PlayerDrawLayer
  {
    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return drawInfo.drawPlayer.FargoSouls().RustRifleReloading && drawInfo.drawPlayer.HeldItem.type == ModContent.ItemType<NavalRustrifle>();
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.Between();
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      if ((double) drawInfo.shadow != 0.0)
        return;
      float num1 = 2f;
      Player drawPlayer = drawInfo.drawPlayer;
      FargoSoulsPlayer fargoSoulsPlayer = drawPlayer.FargoSouls();
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) drawPlayer).Center, Main.screenPosition);
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/PlayerDrawLayers/RustrifleReloadBar", (AssetRequestMode) 1).Value;
      Rectangle bounds1 = texture2D1.Bounds;
      Vector2 vector2_2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 150f), drawPlayer.gravDir), -(double) drawInfo.rotation, new Vector2());
      int num2 = (int) ((double) (bounds1.Width - 16) * (double) num1);
      Vector2 vector2_3 = vector2_2;
      Vector2 vector2_4 = Vector2.op_Addition(vector2_1, vector2_3);
      Vector2 vector2_5 = Vector2.op_Subtraction(vector2_4, Utils.RotatedBy(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) num2), 2f), -(double) drawInfo.rotation, new Vector2()));
      DrawData drawData1;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData1).\u002Ector(texture2D1, vector2_4, new Rectangle?(bounds1), Color.White, (double) drawPlayer.gravDir < 0.0 ? 3.14159274f - drawInfo.rotation : 0.0f - drawInfo.rotation, Vector2.op_Division(Utils.Size(bounds1), 2f), num1, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData1);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/PlayerDrawLayers/RustrifleReloadZone", (AssetRequestMode) 1).Value;
      Rectangle bounds2 = texture2D2.Bounds;
      Vector2 vector2_6 = Vector2.op_Addition(vector2_5, Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) num2), fargoSoulsPlayer.RustRifleReloadZonePos), -(double) drawInfo.rotation, new Vector2()));
      DrawData drawData2;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData2).\u002Ector(texture2D2, vector2_6, new Rectangle?(bounds2), Color.White, (double) drawPlayer.gravDir < 0.0 ? 3.14159274f - drawInfo.rotation : 0.0f - drawInfo.rotation, Vector2.op_Division(Utils.Size(bounds2), 2f), num1, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData2);
      Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/PlayerDrawLayers/RustrifleReloadSlider", (AssetRequestMode) 1).Value;
      Rectangle bounds3 = texture2D3.Bounds;
      float num3 = NavalRustrifle.ReloadProgress(fargoSoulsPlayer.RustRifleTimer);
      Vector2 vector2_7 = Vector2.op_Addition(vector2_5, Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) num2), num3), -(double) drawInfo.rotation, new Vector2()));
      DrawData drawData3;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData3).\u002Ector(texture2D3, vector2_7, new Rectangle?(bounds3), Color.White, (double) drawPlayer.gravDir < 0.0 ? 3.14159274f - drawInfo.rotation : 0.0f - drawInfo.rotation, Vector2.op_Division(Utils.Size(bounds3), 2f), num1, (SpriteEffects) 0, 0.0f);
      drawInfo.DrawDataCache.Add(drawData3);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.PlayerDrawLayers.EternitySoulWingPlayerLayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Drawers;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.PlayerDrawLayers
{
  internal class EternitySoulWingPlayerLayer : PlayerDrawLayer
  {
    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.BeforeParent(Terraria.DataStructures.PlayerDrawLayers.Wings);
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      EternitySoulWingPlayerLayer.DrawWings(ref drawInfo);
    }

    internal static void DrawWings(ref PlayerDrawSet drawinfo)
    {
      if (EternityWingDrawer.DoNotDrawSpecialWings || drawinfo.drawPlayer.dead || drawinfo.hideEntirePlayer || drawinfo.drawPlayer.wings != EternitySoul.WingSlotID)
        return;
      Main.instance.LoadWings(EternitySoul.WingSlotID);
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(drawinfo.Position, Main.screenPosition), new Vector2((float) (((Entity) drawinfo.drawPlayer).width / 2), (float) (((Entity) drawinfo.drawPlayer).height - drawinfo.drawPlayer.bodyFrame.Height / 2))), Vector2.op_Multiply(Vector2.UnitY, 7f)), Vector2.op_Multiply(new Vector2(-9f, -7f), drawinfo.drawPlayer.Directions));
      Texture2D texture2D = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
      int num = 5;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, texture2D.Height / num * drawinfo.drawPlayer.wingFrame, texture2D.Width, texture2D.Height / num);
      DrawData drawData1;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData1).\u002Ector(texture2D, Utils.Floor(vector2), new Rectangle?(rectangle), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, Vector2.op_Multiply(Utils.Size(rectangle), 0.5f), 1f, drawinfo.playerEffect, 0.0f);
      drawData1.shader = drawinfo.cWings;
      DrawData drawData2 = drawData1;
      drawinfo.DrawDataCache.Add(drawData2);
    }
  }
}

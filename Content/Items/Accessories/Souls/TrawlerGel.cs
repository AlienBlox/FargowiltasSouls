// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.TrawlerGel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class TrawlerGel : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TrawlerHeader>();

    public override int ToggleItemType => 4987;

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      if (Main.myPlayer != ((Entity) player).whoAmI)
        return;
      ++player.volatileGelatinCounter;
      if (player.volatileGelatinCounter <= 50)
        return;
      player.volatileGelatinCounter = 0;
      int num1 = 65;
      float num2 = 7f;
      float num3 = 640f;
      NPC npc1 = (NPC) null;
      for (int index = 0; index < 200; ++index)
      {
        NPC npc2 = Main.npc[index];
        if (npc2 != null && ((Entity) npc2).active && npc2.CanBeChasedBy((object) player, false) && Collision.CanHit((Entity) player, (Entity) npc2))
        {
          float num4 = Vector2.Distance(((Entity) npc2).Center, ((Entity) player).Center);
          if ((double) num4 < (double) num3)
          {
            num3 = num4;
            npc1 = npc2;
          }
        }
      }
      if (npc1 == null)
        return;
      Vector2 vector2 = Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) player).Center), Vector2.Zero), 6f);
      vector2.Y -= 2f;
      Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center.X, ((Entity) player).Center.Y, vector2.X, vector2.Y, 937, num1, num2, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}

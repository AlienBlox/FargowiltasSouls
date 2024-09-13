// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MeteorEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MeteorEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<CosmoHeader>();

    public override int ToggleItemType => ModContent.ItemType<MeteorEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      bool flag = player.FargoSouls().ForceEffect<MeteorEnchant>();
      int dmg = flag ? 50 : 20;
      if (fargoSoulsPlayer.MeteorShower)
      {
        if (fargoSoulsPlayer.MeteorTimer % (flag ? 2 : 10) == 0)
        {
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector(((Entity) player).Center.X + Utils.NextFloat(Main.rand, -1000f, 1000f), ((Entity) player).Center.Y - 1000f);
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector(Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, 8f, 12f));
          if (Utils.NextBool(Main.rand))
          {
            List<NPC> list = ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n.CanBeChasedBy((object) null, false) && (double) ((Entity) n).Distance(((Entity) player).Center) < 900.0)).ToList<NPC>();
            if (list.Count > 0)
            {
              NPC npc = list[Main.rand.Next(list.Count)];
              vector2_1.X = ((Entity) npc).Center.X + Utils.NextFloat(Main.rand, -32f, 32f);
              Vector2 vector2_3 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 10f, 30f), ((Entity) npc).velocity);
              vector2_1.X += vector2_3.X;
              Vector2 vector2_4 = Vector2.op_Addition(((Entity) npc).Center, vector2_3);
              if ((double) vector2_1.Y < (double) vector2_4.Y)
              {
                Vector2 vector2_5 = Vector2.op_Multiply(((Vector2) ref vector2_2).Length(), Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_1, vector2_4));
                vector2_2 = Vector2.Lerp(vector2_2, vector2_5, Utils.NextFloat(Main.rand));
              }
            }
          }
          Projectile.NewProjectile(this.GetSource_EffectItem(player), vector2_1, vector2_2, Main.rand.Next(424, 427), FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 0.5f, ((Entity) player).whoAmI, 0.0f, (float) (0.5 + Main.rand.NextDouble() * 0.30000001192092896), 0.0f);
        }
        if (--fargoSoulsPlayer.MeteorTimer > 0)
          return;
        fargoSoulsPlayer.MeteorShower = false;
        fargoSoulsPlayer.MeteorCD = flag ? 240 : 600;
      }
      else
      {
        fargoSoulsPlayer.MeteorTimer = 150 + 450 / (flag ? 1 : 10);
        if (fargoSoulsPlayer.WeaponUseTimer > 0)
        {
          if (--fargoSoulsPlayer.MeteorCD > 0)
            return;
          fargoSoulsPlayer.MeteorShower = true;
        }
        else
        {
          if (fargoSoulsPlayer.MeteorCD >= 150)
            return;
          ++fargoSoulsPlayer.MeteorCD;
        }
      }
    }
  }
}

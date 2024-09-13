// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.PungentEyeballCursor
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class PungentEyeballCursor : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<LumpofFleshHeader>();

    public override int ToggleItemType => ModContent.ItemType<PungentEyeball>();

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.dontTakeDamage && n.lifeMax > 5 && !n.friendly)))
      {
        if ((double) Vector2.Distance(Main.MouseWorld, FargoSoulsUtil.ClosestPointInHitbox(((Entity) npc).Hitbox, Main.MouseWorld)) < 80.0)
          npc.AddBuff(ModContent.BuffType<PungentGazeBuff>(), 2, true);
      }
      for (int index = 0; index < 32; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(Main.MouseWorld, Utils.NextVector2CircularEdge(Main.rand, 80f, 80f));
        Dust dust1 = Main.dust[Dust.NewDust(vector2, 0, 0, 90, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust1.scale = 0.5f;
        dust1.velocity = Vector2.Zero;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, dust1.position)), Utils.NextFloat(Main.rand, 5f)));
          Dust dust3 = dust1;
          dust3.position = Vector2.op_Addition(dust3.position, Vector2.op_Multiply(dust1.velocity, 5f));
        }
        dust1.noGravity = true;
      }
    }
  }
}

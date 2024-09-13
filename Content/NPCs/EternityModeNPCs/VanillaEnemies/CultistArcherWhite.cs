// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.CultistArcherWhite
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class CultistArcherWhite : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(380);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.chaseable = true;
      npc.lavaImmune = false;
      npc.value = (float) Item.buyPrice(0, 1, 0, 0);
      npc.lifeMax *= 2;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Utils.NextBool(Main.rand, 3) || !NPC.downedGolemBoss || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, Main.rand.Next(2, 10));
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[1] <= 0.0)
        return;
      if ((double) npc.ai[1] == 41.0)
        npc.ai[1] = 39f;
      if ((double) npc.ai[1] <= 10.0 || (double) npc.ai[1] >= 40.0 || (double) npc.ai[1] % 10.0 != 5.0 || !FargoSoulsUtil.HostCheck)
        return;
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
      vector2.Y -= Math.Abs(vector2.X) * 0.1f;
      vector2.X += (float) Main.rand.Next(-20, 21);
      vector2.Y += (float) Main.rand.Next(-20, 21);
      ((Vector2) ref vector2).Normalize();
      vector2 = Vector2.op_Multiply(vector2, 12f);
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<CultistArrow>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.77777779f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}

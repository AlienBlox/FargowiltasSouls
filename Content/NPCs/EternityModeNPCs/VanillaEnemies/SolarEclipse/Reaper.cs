// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SolarEclipse.Reaper
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SolarEclipse
{
  public class Reaper : EModeNPCBehaviour
  {
    public int DashTimer;
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(253);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      EModeGlobalNPC.Aura(npc, 40f, false, 199, new Color(), ModContent.BuffType<MarkedforDeathBuff>(), 163);
      if (++this.DashTimer >= 420)
      {
        this.DashTimer = 0;
        npc.TargetClosest(true);
        ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center)), 12f);
        this.AttackTimer = 90;
      }
      if (this.AttackTimer < 0 || --this.AttackTimer % 10 != 0)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 274, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 900, true, false);
      target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<UnluckyBuff>(), 1800, true, false);
    }
  }
}

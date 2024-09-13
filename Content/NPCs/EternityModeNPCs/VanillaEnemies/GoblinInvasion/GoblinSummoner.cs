// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion.GoblinSummoner
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion
{
  public class GoblinSummoner : EModeNPCBehaviour
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(471);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer <= 180)
        return;
      this.AttackTimer = 0;
      SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      if (!npc.HasPlayerTarget || !FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(180f, Utils.RotatedByRandom(Utils.RotatedBy(Vector2.UnitX, 2.0943951606750488 * (double) index, new Vector2()), 0.78539818525314331)));
        float rotation = Utils.ToRotation(((Entity) Main.player[npc.target]).DirectionFrom(vector2));
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<ShadowflamePortal>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, rotation, 0.0f, 0.0f);
      }
    }

    public override void ModifyHitByAnything(
      NPC npc,
      Player player,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByAnything(npc, player, ref modifiers);
      if (!Utils.NextBool(Main.rand, 3) || !FargoSoulsUtil.HostCheck || !FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), -5f), ModContent.ProjectileType<GoblinSpikyBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 50; ++index)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, new Vector2((float) Main.rand.Next(-500, 501) / 100f, (float) Main.rand.Next(-1000, 1) / 100f), ModContent.ProjectileType<GoblinSpikyBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.5f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}

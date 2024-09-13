// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Nymph
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class Nymph : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(195, 196);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lavaImmune = true;
      if (!Main.hardMode)
        return;
      npc.lifeMax *= 4;
      npc.damage *= 2;
      npc.defense *= 2;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[31] = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.type != 196)
        return;
      npc.knockBackResist = 0.0f;
      EModeGlobalNPC.Aura(npc, 250f, ModContent.BuffType<LovestruckBuff>(), true, 242, new Color());
      if (--this.Counter >= 0)
        return;
      this.Counter = 300;
      if (!FargoSoulsUtil.HostCheck || !npc.HasPlayerTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) >= 1000.0)
        return;
      Vector2 vector2 = Vector2.op_Multiply(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), 10f);
      for (int index = -3; index < 3; ++index)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, Math.PI / 7.0 * (double) index, new Vector2()), ModContent.ProjectileType<FakeHeart2>(), 20, 0.0f, Main.myPlayer, 30f, (float) (90 + 10 * index), 0.0f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      npc.life += ((Player.HurtInfo) ref hurtInfo).Damage * 2;
      if (npc.life > npc.lifeMax)
        npc.life = npc.lifeMax;
      CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, ((Player.HurtInfo) ref hurtInfo).Damage * 2, false, false);
      npc.netUpdate = true;
    }

    public override void ModifyHitByAnything(
      NPC npc,
      Player player,
      ref NPC.HitModifiers modifiers)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Nymph.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new Nymph.\u003C\u003Ec__DisplayClass6_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.npc = npc;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.player = player;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      base.ModifyHitByAnything(cDisplayClass60.npc, cDisplayClass60.player, ref modifiers);
      // ISSUE: reference to a compiler-generated field
      if (!cDisplayClass60.player.loveStruck)
        return;
      // ISSUE: method pointer
      ((NPC.HitModifiers) ref modifiers).ModifyHitInfo += new NPC.HitModifiers.HitInfoModifier((object) cDisplayClass60, __methodptr(\u003CModifyHitByAnything\u003Eb__0));
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.BrainIllusion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  [AutoloadBossHead]
  public class BrainIllusion : ModNPC
  {
    public virtual void SetStaticDefaults() => Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 160;
      ((Entity) this.NPC).height = 110;
      this.NPC.damage = 0;
      this.NPC.defense = 9999;
      this.NPC.lifeMax = 9999;
      this.NPC.dontTakeDamage = true;
      this.NPC.hide = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit9);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.damage = 0;
      this.NPC.lifeMax = 9999;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.NPC.ai[0], 266);
      if (npc == null)
      {
        this.NPC.life = 0;
        this.NPC.HitEffect(0, 10.0, new bool?());
        this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
        ((Entity) this.NPC).active = false;
      }
      else
      {
        this.NPC.target = npc.target;
        if (this.NPC.HasPlayerTarget)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).Center, ((Entity) npc).Center);
          ((Entity) this.NPC).Center = ((Entity) Main.player[this.NPC.target]).Center;
          ((Entity) this.NPC).position.X += vector2.X * this.NPC.ai[1];
          ((Entity) this.NPC).position.Y += vector2.Y * this.NPC.ai[2];
        }
        else
          ((Entity) this.NPC).Center = ((Entity) npc).Center;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(20, 120, true, false);
      target.AddBuff(22, 120, true, false);
      target.AddBuff(30, 120, true, false);
      target.AddBuff(32, 120, true, false);
      target.AddBuff(33, 120, true, false);
      target.AddBuff(36, 120, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
        Main.dust[index2].scale += 0.5f;
      }
    }

    public virtual bool CheckActive() => false;

    public virtual bool CheckDead() => false;

    public virtual void BossHeadSlot(ref int index)
    {
      index = -1;
      base.BossHeadSlot(ref index);
    }
  }
}

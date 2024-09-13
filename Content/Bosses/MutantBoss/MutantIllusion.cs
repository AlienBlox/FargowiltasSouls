// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantIllusion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  [AutoloadBossHead]
  public class MutantIllusion : ModNPC
  {
    public virtual string Texture
    {
      get
      {
        return "FargowiltasSouls/Content/Bosses/MutantBoss/MutantBoss" + FargoSoulsUtil.TryAprilFoolsTexture;
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 4;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 12);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<MutantNibbleBuff>();
      int num8 = num7 + 1;
      span[num8] = ModContent.BuffType<OceanicMaulBuff>();
      int num9 = num8 + 1;
      span[num9] = ModContent.BuffType<LightningRodBuff>();
      int num10 = num9 + 1;
      span[num10] = ModContent.BuffType<SadismBuff>();
      int num11 = num10 + 1;
      span[num11] = ModContent.BuffType<GodEaterBuff>();
      int num12 = num11 + 1;
      span[num12] = ModContent.BuffType<TimeFrozenBuff>();
      int num13 = num12 + 1;
      npc.AddDebuffImmunities(debuffs);
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 34;
      ((Entity) this.NPC).height = 50;
      this.NPC.damage = 360;
      this.NPC.defense = 400;
      this.NPC.lifeMax = 7000000;
      this.NPC.dontTakeDamage = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit57);
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
      this.NPC.damage = (int) ((double) this.NPC.damage * 0.5);
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * 0.5 * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => false;

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.NPC.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if (npc == null || (double) npc.ai[0] < 18.0 || (double) npc.ai[0] > 19.0 || npc.life <= 1)
      {
        this.NPC.life = 0;
        this.NPC.HitEffect(0, 10.0, new bool?());
        this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
        ((Entity) this.NPC).active = false;
        for (int index1 = 0; index1 < 40; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
          Main.dust[index2].scale += 0.5f;
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 229, 0.0f, 0.0f, 0, new Color(), 2f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].noLight = true;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
        }
      }
      else
      {
        this.NPC.target = npc.target;
        this.NPC.damage = npc.damage;
        this.NPC.defDamage = npc.damage;
        this.NPC.frame.Y = npc.frame.Y;
        if (this.NPC.HasValidTarget)
        {
          Vector2 center = ((Entity) Main.player[npc.target]).Center;
          Vector2 vector2 = Vector2.op_Subtraction(center, ((Entity) npc).Center);
          ((Entity) this.NPC).Center = center;
          ((Entity) this.NPC).position.X += vector2.X * this.NPC.ai[1];
          ((Entity) this.NPC).position.Y += vector2.Y * this.NPC.ai[2];
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).position.X < (double) ((Entity) Main.player[this.NPC.target]).position.X ? 1 : -1;
        }
        else
          ((Entity) this.NPC).Center = ((Entity) npc).Center;
        if ((double) --this.NPC.ai[3] == 0.0)
        {
          int num = (double) this.NPC.ai[1] >= 0.0 ? ((double) this.NPC.ai[2] >= 0.0 ? 2 : 1) : 0;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, -5f), ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(npc.damage, 1.33333337f), 0.0f, Main.myPlayer, (float) num, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if (!Main.getGoodWorld || (double) ++this.NPC.localAI[0] <= 6.0)
          return;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.AI();
      }
    }

    public virtual bool CheckActive() => false;

    public virtual bool PreKill() => false;

    public virtual void FindFrame(int frameHeight)
    {
    }

    public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
    {
    }
  }
}

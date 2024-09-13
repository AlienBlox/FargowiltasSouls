// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.CreeperGutted
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class CreeperGutted : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 3;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.ImmuneToAllBuffs[this.Type] = true;
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[266], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary.GuttedCreeper")
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 30;
      ((Entity) this.NPC).height = 30;
      this.NPC.damage = 20;
      this.NPC.defense = 0;
      this.NPC.lifeMax = 30;
      this.NPC.friendly = true;
      this.NPC.dontCountMe = true;
      this.NPC.netAlways = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit9);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.8f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
    }

    public virtual void AI()
    {
      if ((double) this.NPC.localAI[0] == 0.0)
      {
        this.NPC.localAI[0] = 1f;
        this.NPC.lifeMax *= (int) this.NPC.ai[2];
        this.NPC.defDamage *= (int) this.NPC.ai[2];
        this.NPC.defDefense *= (int) this.NPC.ai[2];
        this.NPC.life = this.NPC.lifeMax;
      }
      this.NPC.damage = this.NPC.defDamage;
      this.NPC.defense = this.NPC.defDefense;
      Player player = Main.player[(int) this.NPC.ai[0]];
      if ((((Entity) player).whoAmI != Main.myPlayer || !player.HasEffect<GuttedHeartMinions>()) && (!((Entity) player).active || player.dead))
      {
        int whoAmI = ((Entity) this.NPC).whoAmI;
        this.NPC.SimpleStrikeNPC(this.NPC.lifeMax * 2, 0, false, 0.0f, (DamageClass) null, false, 0.0f, false);
        if (!FargoSoulsUtil.HostCheck)
          return;
        NetMessage.SendData(23, -1, -1, (NetworkText) null, whoAmI, 9999f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        Vector2 vector2 = Utils.RotatedBy(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center), (double) this.NPC.ai[3], new Vector2());
        float num = ((Vector2) ref vector2).Length();
        if ((double) num > 1000.0)
        {
          ((Entity) this.NPC).Center = ((Entity) player).Center;
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), 8f);
        }
        else if ((double) num > 40.0)
        {
          vector2 = Vector2.op_Division(vector2, 10f);
          ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, 15f), vector2), 16f);
        }
        else if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() < 8.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 1.05f);
        }
        if ((double) this.NPC.ai[1]++ > 52.0)
        {
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[3] = MathHelper.ToRadians(Utils.NextFloat(Main.rand, -15f, 15f));
          if (((Entity) player).whoAmI == Main.myPlayer && !player.HasEffect<GuttedHeartMinions>())
          {
            int whoAmI = ((Entity) this.NPC).whoAmI;
            this.NPC.SimpleStrikeNPC(this.NPC.lifeMax * 2, 0, false, 0.0f, (DamageClass) null, false, 0.0f, false);
            if (!FargoSoulsUtil.HostCheck)
              return;
            NetMessage.SendData(23, -1, -1, (NetworkText) null, whoAmI, 9999f, 0.0f, 0.0f, 0, 0, 0);
            return;
          }
        }
        NPC npc1 = this.NPC;
        ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition));
        foreach (NPC npc2 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && (double) n.ai[0] == (double) this.NPC.ai[0] && n.type == this.NPC.type && ((Entity) n).whoAmI != ((Entity) this.NPC).whoAmI && (double) ((Entity) this.NPC).Distance(((Entity) n).Center) < (double) ((Entity) this.NPC).width)))
        {
          ((Entity) this.NPC).velocity.X += (float) (0.05000000074505806 * ((double) ((Entity) this.NPC).Center.X < (double) ((Entity) npc2).Center.X ? -1.0 : 1.0));
          ((Entity) this.NPC).velocity.Y += (float) (0.05000000074505806 * ((double) ((Entity) this.NPC).Center.Y < (double) ((Entity) npc2).Center.Y ? -1.0 : 1.0));
          ((Entity) npc2).velocity.X += (float) (0.05000000074505806 * ((double) ((Entity) npc2).Center.X < (double) ((Entity) this.NPC).Center.X ? -1.0 : 1.0));
          ((Entity) npc2).velocity.Y += (float) (0.05000000074505806 * ((double) ((Entity) npc2).Center.Y < (double) ((Entity) this.NPC).Center.Y ? -1.0 : 1.0));
        }
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      if ((double) this.NPC.ai[2] <= 1.0)
        this.NPC.frame.Y = 0;
      else if ((double) this.NPC.ai[2] <= 2.0)
        this.NPC.frame.Y = frameHeight;
      else
        this.NPC.frame.Y = frameHeight * 2;
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 3f);
    }

    public virtual bool? CanBeHitByProjectile(Projectile projectile)
    {
      switch (projectile.type)
      {
        case 17:
        case 31:
        case 39:
        case 40:
        case 56:
        case 67:
        case 71:
        case 179:
        case 241:
          if ((double) ((Entity) projectile).velocity.X == 0.0)
            return new bool?(false);
          break;
        case 318:
          return new bool?(false);
      }
      return new bool?();
    }

    public virtual void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
      if (!FargoSoulsUtil.CanDeleteProjectile(projectile))
        return;
      projectile.timeLeft = 0;
      projectile.FargoSouls().canHurt = false;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      int num = (double) this.NPC.ai[2] > 1.0 ? 1 : 0;
      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), this.NPC.damage, 6f, (int) this.NPC.ai[0], (float) num, 0.0f, 0.0f);
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
        Main.dust[index2].scale += 0.5f;
      }
    }

    public virtual bool CheckActive() => false;

    public virtual bool PreKill() => false;
  }
}

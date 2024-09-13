// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.LesserSquirrel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class LesserSquirrel : ModNPC
  {
    public int counter;
    private bool spawnedByP1;

    public virtual string Texture => "FargowiltasSouls/Content/NPCs/Critters/TophatSquirrelCritter";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 6;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<TimberChampion>()], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 50;
      ((Entity) this.NPC).height = 32;
      this.NPC.damage = 0;
      this.NPC.defense = 0;
      this.NPC.lifeMax = 1800;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.value = 0.0f;
      this.NPC.knockBackResist = 0.1f;
      this.AnimationType = 299;
      this.NPC.aiStyle = 7;
      this.AIType = 299;
      if (WorldSavingSystem.EternityMode)
      {
        this.NPC.aiStyle = 41;
        this.AIType = 174;
      }
      this.NPC.dontTakeDamage = true;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (((IEnumerable<NPC>) Main.npc).FirstOrDefault<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<TimberChampion>())) == null)
        return;
      this.spawnedByP1 = true;
    }

    public virtual void AI()
    {
      if ((double) ((Entity) this.NPC).velocity.Y == 0.0 && !((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p =>
      {
        if (!((Entity) p).active)
          return false;
        return p.type == ModContent.ProjectileType<TimberTree>() || p.type == ModContent.ProjectileType<TimberTreeAcorn>();
      })))
        this.NPC.dontTakeDamage = false;
      if (++this.counter > 900)
        this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      if (this.NPC.dontTakeDamage || !WorldSavingSystem.EternityMode)
        return;
      Vector2 position = ((Entity) this.NPC).position;
      position.X += ((Entity) this.NPC).velocity.X * 1.5f;
      if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
        position.Y += ((Entity) this.NPC).velocity.Y;
      if (Collision.SolidTiles(position, ((Entity) this.NPC).width, ((Entity) this.NPC).height))
        return;
      ((Entity) this.NPC).position = position;
    }

    public virtual bool CheckDead()
    {
      if (FargoSoulsUtil.HostCheck)
      {
        int closest = (int) Player.FindClosest(((Entity) this.NPC).Center, 0, 0);
        NPC npc = ((IEnumerable<NPC>) Main.npc).FirstOrDefault<NPC>((Func<NPC, bool>) (n =>
        {
          if (!((Entity) n).active)
            return false;
          return n.type == ModContent.NPCType<TimberChampion>() || n.type == ModContent.NPCType<TimberChampionHead>();
        }));
        if (closest != -1 && npc != null)
        {
          bool flag = true;
          if (this.spawnedByP1)
            flag = NPC.AnyNPCs(ModContent.NPCType<TimberChampion>());
          if (flag)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(6f, Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 0.78539818525314331))), 873, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) npc.target, Utils.NextFloat(Main.rand), 0.0f);
        }
      }
      return true;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 0; index < 20; ++index)
        Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, (float) hit.HitDirection, -1f, 0, new Color(), 1f);
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 5f);
        int index5 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 6, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index6 = 0; index6 < 4; ++index6)
      {
        int index7 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index7];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
        Gore gore2 = Main.gore[index7];
        gore2.velocity = Vector2.op_Addition(gore2.velocity, Utils.RotatedBy(new Vector2(1f, 1f), 1.5707963705062866 * (double) index6, new Vector2()));
      }
      if (Main.dedServ)
        return;
      int index8 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 6f), Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 0.78539818525314331))), ModContent.Find<ModGore>(((ModType) this).Mod.Name, Utils.NextBool(Main.rand) ? "TrojanSquirrelGore2" : "TrojanSquirrelGore2_2").Type, this.NPC.scale);
      Main.gore[index8].rotation = Utils.NextFloat(Main.rand, 6.28318548f);
    }
  }
}

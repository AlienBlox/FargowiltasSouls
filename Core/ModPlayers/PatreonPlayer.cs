// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ModPlayers.PatreonPlayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Patreon.ParadoxWolf;
using FargowiltasSouls.Content.Patreon.Potato;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Core.ModPlayers
{
  public class PatreonPlayer : ModPlayer
  {
    public bool Gittle;
    public bool RoombaPet;
    public bool Sasha;
    public bool FishMinion;
    public bool CompOrb;
    public int CompOrbDrainCooldown;
    public bool ManliestDove;
    public bool DovePet;
    public bool Cat;
    public bool KingSlimeMinion;
    public bool WolfDashing;
    public bool PiranhaPlantMode;
    public bool JojoTheGamer;
    public bool PrimeMinion;
    public bool Crimetroid;
    public bool ROB;
    public bool ChibiiRemii;
    public bool Northstrider;
    public bool RazorContainer;
    public static readonly SoundStyle RazorContainerTink;
    private int razorCD;

    public virtual void SaveData(TagCompound tag)
    {
      base.SaveData(tag);
      string str = "PatreonSaves" + this.Player.name;
      List<string> stringList = new List<string>();
      if (this.PiranhaPlantMode)
        stringList.Add("PiranhaPlantMode");
      tag.Add(str, (object) stringList);
    }

    public virtual void LoadData(TagCompound tag)
    {
      base.LoadData(tag);
      string str = "PatreonSaves" + this.Player.name;
      this.PiranhaPlantMode = tag.GetList<string>(str).Contains("PiranhaPlantMode");
    }

    public virtual void ResetEffects()
    {
      this.Gittle = false;
      this.RoombaPet = false;
      this.Sasha = false;
      this.FishMinion = false;
      this.CompOrb = false;
      this.ManliestDove = false;
      this.DovePet = false;
      this.Cat = false;
      this.KingSlimeMinion = false;
      this.WolfDashing = false;
      this.JojoTheGamer = false;
      this.Crimetroid = false;
      this.ROB = false;
      this.PrimeMinion = false;
      this.ChibiiRemii = false;
      this.Northstrider = false;
      this.RazorContainer = false;
    }

    public virtual void OnEnterWorld()
    {
      if (!this.Gittle && !this.Sasha && !this.ManliestDove && !this.Cat && !this.JojoTheGamer && !this.Northstrider)
        return;
      Main.NewText(Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.PatreonNameEffect") + ", " + this.Player.name + "!", byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    public virtual void PostUpdateMiscEffects()
    {
      if (this.CompOrbDrainCooldown > 0)
        --this.CompOrbDrainCooldown;
      if (this.Player.name == "iverhcamer")
      {
        this.Gittle = true;
        this.Player.pickSpeed -= 0.15f;
        Lighting.AddLight(((Entity) this.Player).Center, 0.8f, 0.8f, 0.0f);
      }
      if (this.Player.name == "Sasha")
      {
        this.Sasha = true;
        this.Player.lavaImmune = true;
        this.Player.fireWalk = true;
        this.Player.buffImmune[24] = true;
        this.Player.buffImmune[39] = true;
        this.Player.buffImmune[67] = true;
      }
      if (this.Player.name == "Dove")
        this.ManliestDove = true;
      if (this.Player.name == "cat")
      {
        this.Cat = true;
        if (NPC.downedMoonlord)
          this.Player.maxMinions += 4;
        else if (Main.hardMode)
          this.Player.maxMinions += 2;
        ref StatModifier local = ref this.Player.GetDamage(DamageClass.Summon);
        local = StatModifier.op_Addition(local, (float) this.Player.maxMinions * 0.5f);
      }
      if (this.Player.name == "VirtualDefender")
        this.JojoTheGamer = true;
      if (this.Player.name == "Northstrider")
      {
        this.Northstrider = true;
        this.Player.wingsLogic = 3;
        this.Player.wings = 3;
        this.Player.wingTimeMax = 100;
        this.Player.wingAccRunSpeed = 9f;
        this.Player.wingRunAccelerationMult = 9f;
      }
      if (!this.CompOrb || this.Player.itemAnimation <= 0)
        return;
      this.Player.manaRegenDelay = this.Player.maxRegenDelay;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Gittle)
      {
        if (Utils.NextBool(Main.rand, 10))
        {
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            NPC npc = Main.npc[index];
            if ((double) Vector2.Distance(((Entity) target).Center, ((Entity) npc).Center) < 50.0)
              npc.AddBuff(70, 300, false);
          }
        }
        Mod mod;
        if (Terraria.ModLoader.ModLoader.TryGetMod("CalamityMod", ref mod))
          target.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      }
      if (!this.CompOrb || this.CompOrbDrainCooldown > 0)
        return;
      this.CompOrbDrainCooldown = 15;
      if (!this.Player.CheckMana(10, true, false))
        return;
      this.Player.manaRegenDelay = this.Player.maxRegenDelay;
    }

    public virtual void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
    {
      if (!this.CompOrb || item.DamageType == DamageClass.Magic || item.DamageType == DamageClass.Summon)
        return;
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, 1.25f);
      if (this.Player.manaSick)
      {
        ref StatModifier local2 = ref modifiers.FinalDamage;
        local2 = StatModifier.op_Multiply(local2, this.Player.manaSickReduction);
      }
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) target).position.X, ((Entity) target).position.Y), ((Entity) target).width, ((Entity) target).height, 15, (float) (-(double) ((Entity) target).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) target).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(new Vector2(((Entity) target).Center.X, ((Entity) target).Center.Y), ((Entity) target).width, ((Entity) target).height, 15, (float) (-(double) ((Entity) target).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) target).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual void ModifyHitNPCWithProj(
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      if (!this.CompOrb || proj.DamageType == DamageClass.Magic || proj.DamageType == DamageClass.Summon)
        return;
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, 1.25f);
      if (this.Player.manaSick)
      {
        ref StatModifier local2 = ref modifiers.FinalDamage;
        local2 = StatModifier.op_Multiply(local2, this.Player.manaSickReduction);
      }
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) target).position.X, ((Entity) target).position.Y), ((Entity) target).width, ((Entity) target).height, 15, (float) (-(double) ((Entity) target).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) target).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(new Vector2(((Entity) target).Center.X, ((Entity) target).Center.Y), ((Entity) target).width, ((Entity) target).height, 15, (float) (-(double) ((Entity) target).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) target).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo) => this.OnHitByEither();

    public virtual void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
      this.OnHitByEither();
    }

    private void OnHitByEither()
    {
      if (!this.PiranhaPlantMode)
        return;
      int index = Main.rand.Next(FargowiltasSouls.FargowiltasSouls.DebuffIDs.Count);
      this.Player.AddBuff(FargowiltasSouls.FargowiltasSouls.DebuffIDs[index], 180, true, false);
    }

    public virtual void Kill(
      double damage,
      int hitDirection,
      bool pvp,
      PlayerDeathReason damageSource)
    {
      Entity entity;
      if (!SoulConfig.Instance.PatreonWolf || !damageSource.TryGetCausingEntity(ref entity) || !(entity is NPC npc) || !((Entity) npc).active || npc.type != 155)
        return;
      Item.NewItem(((Entity) this.Player).GetSource_Death((string) null), ((Entity) this.Player).Hitbox, ModContent.ItemType<ParadoxWolfSoul>(), 1, false, 0, false, false);
    }

    public virtual void HideDrawLayers(PlayerDrawSet drawInfo)
    {
      base.HideDrawLayers(drawInfo);
      if (!this.WolfDashing)
        return;
      drawInfo.DrawDataCache.Clear();
    }

    public virtual void FrameEffects()
    {
    }

    public virtual void MeleeEffects(Item item, Rectangle hitbox)
    {
      if (!this.RazorContainer || --this.razorCD > 0)
        return;
      for (int index1 = 0; index1 < Main.projectile.Length; ++index1)
      {
        Projectile projectile = Main.projectile[index1];
        if (projectile.TypeAlive<RazorBlade>() && (double) Utils.Distance(hitbox, ((Entity) projectile).Center) < 100.0 && ((Entity) this.Player).whoAmI == projectile.owner)
        {
          Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Player).Center)), 30f);
          for (int index2 = 0; index2 < 3; ++index2)
          {
            Vector2 worldPosition = Vector2.op_Addition(((Entity) projectile).Center, Utils.NextVector2Circular(Main.rand, (float) (((Entity) projectile).width / 2), (float) (((Entity) projectile).height / 2)));
            new SparkParticle(worldPosition, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(worldPosition, ((Entity) projectile).Center)), Utils.NextFloat(Main.rand, 2f, 5f)), Color.Lerp(Color.Orange, Color.Red, Utils.NextFloat(Main.rand)), 0.2f, 20).Spawn();
          }
          SoundStyle razorContainerTink = PatreonPlayer.RazorContainerTink;
          ((SoundStyle) ref razorContainerTink).Volume = 0.25f;
          SoundEngine.PlaySound(ref razorContainerTink, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
          ((Entity) projectile).velocity = vector2;
          projectile.ai[0] = 1f;
          projectile.ai[1] = 0.0f;
          projectile.ResetLocalNPCHitImmunity();
          projectile.netUpdate = true;
          NetMessage.SendData(27, -1, -1, (NetworkText) null, ((Entity) projectile).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      this.razorCD = 30;
    }

    static PatreonPlayer()
    {
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/RazorTink", (SoundType) 0);
      ((SoundStyle) ref soundStyle).PitchVariance = 0.25f;
      PatreonPlayer.RazorContainerTink = soundStyle;
    }
  }
}

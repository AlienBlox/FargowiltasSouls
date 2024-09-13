// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Globals.FargoSoulsGlobalNPC
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.NPCs;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Content.Items.Weapons.Misc;
using FargowiltasSouls.Content.NPCs.Critters;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Globals
{
  public class FargoSoulsGlobalNPC : GlobalNPC
  {
    public static int boss = -1;
    public int originalDefense;
    public bool BrokenArmor;
    public bool CanHordeSplit = true;
    public bool FirstTick;
    public bool OriPoison;
    public bool SBleed;
    public bool Rotting;
    public bool LeadPoison;
    public bool Needled;
    public bool SolarFlare;
    public bool TimeFrozen;
    public bool HellFire;
    public bool HellFireMarked;
    public bool Corrupted;
    public bool CorruptedForce;
    public bool Infested;
    public int MaxInfestTime;
    public float InfestedDust;
    public bool Electrified;
    public bool CurseoftheMoon;
    public int lightningRodTimer;
    public bool Sadism;
    public bool OceanicMaul;
    public bool MutantNibble;
    public int LifePrevious = -1;
    public bool GodEater;
    public bool Suffocation;
    public int SuffocationTimer;
    public bool FlamesoftheUniverse;
    public bool Lethargic;
    public int LethargicCounter;
    public bool Sublimation;
    public bool SnowChilled;
    public int SnowChilledTimer;
    public int EbonCorruptionTimer;
    public bool Chilled;
    public bool Smite;
    public bool MoltenAmplify;
    public bool Anticoagulation;
    public bool BloodDrinker;
    public bool MagicalCurse;
    public int NecroDamage;
    public bool PungentGazeWasApplied;
    public int PungentGazeTime;
    public int GrazeCD;
    private static HashSet<int> RareNPCs = new HashSet<int>();
    private bool lootMultiplierCheck;

    public virtual bool InstancePerEntity => true;

    public virtual void Unload()
    {
      ((ModType) this).Unload();
      FargoSoulsGlobalNPC.RareNPCs = (HashSet<int>) null;
    }

    public virtual void ResetEffects(NPC npc)
    {
      this.BrokenArmor = false;
      this.TimeFrozen = false;
      this.SBleed = false;
      this.Rotting = false;
      this.LeadPoison = false;
      this.SolarFlare = false;
      this.HellFire = false;
      this.HellFireMarked = false;
      this.Corrupted = false;
      this.CorruptedForce = false;
      this.OriPoison = false;
      this.Infested = false;
      this.Electrified = false;
      this.CurseoftheMoon = false;
      this.Sadism = false;
      this.OceanicMaul = false;
      this.MutantNibble = false;
      this.GodEater = false;
      this.Suffocation = false;
      this.Sublimation = false;
      this.Chilled = false;
      this.Smite = false;
      this.MoltenAmplify = false;
      this.Anticoagulation = false;
      this.BloodDrinker = false;
      this.FlamesoftheUniverse = false;
      this.MagicalCurse = false;
      this.PungentGazeTime = 0;
    }

    public virtual void SetStaticDefaults()
    {
      ModBuff modBuff;
      if (!ModContent.TryFind<ModBuff>("CalamityMod", "MiracleBlight", ref modBuff))
        return;
      foreach (ModNPC modNpc in ((ModType) this).Mod.GetContent<ModNPC>())
        NPCID.Sets.SpecificDebuffImmunity[modNpc.Type][modBuff.Type] = new bool?(true);
    }

    public virtual void SetDefaults(NPC npc)
    {
      if (npc.rarity <= 0 || FargoSoulsGlobalNPC.RareNPCs.Contains(npc.type))
        return;
      FargoSoulsGlobalNPC.RareNPCs.Add(npc.type);
    }

    public virtual bool PreAI(NPC npc)
    {
      if (npc.boss || npc.type == 13)
        FargoSoulsGlobalNPC.boss = ((Entity) npc).whoAmI;
      if (!Luminance.Common.Utilities.Utilities.AnyBosses())
        FargoSoulsGlobalNPC.boss = -1;
      bool flag = base.PreAI(npc);
      if (this.TimeFrozen)
      {
        ((Entity) npc).position = ((Entity) npc).oldPosition;
        npc.frameCounter = 0.0;
        flag = false;
      }
      if (!this.FirstTick)
      {
        this.originalDefense = npc.defense;
        this.FirstTick = true;
      }
      if (this.Lethargic && ++this.LethargicCounter > 3)
      {
        this.LethargicCounter = 0;
        flag = false;
      }
      if (!npc.HasBuff<CorruptingBuff>())
        this.EbonCorruptionTimer -= Math.Min(3, this.EbonCorruptionTimer);
      if (this.SnowChilled)
      {
        --this.SnowChilledTimer;
        if (this.SnowChilledTimer <= 0)
          this.SnowChilled = false;
        if (this.SnowChilledTimer % 2 == 1)
        {
          ((Entity) npc).position = ((Entity) npc).oldPosition;
          flag = false;
        }
      }
      return flag;
    }

    public virtual void PostAI(NPC npc)
    {
      if (this.BrokenArmor)
        npc.defense = this.originalDefense - 10;
      if (this.Sublimation)
        npc.defense = this.originalDefense - 15;
      if (this.SnowChilled)
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 76, ((Entity) npc).velocity.X, ((Entity) npc).velocity.Y, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
      }
      this.SuffocationTimer += this.Suffocation ? 1 : -3;
      if (this.SuffocationTimer < 0)
        this.SuffocationTimer = 0;
      if (npc.friendly || npc.damage <= 0 || !((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead)
        return;
      if (--this.GrazeCD < 0)
        this.GrazeCD = 6;
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.realLife, Array.Empty<int>());
      FargoSoulsGlobalNPC fargoSoulsGlobalNpc = npc1 != null ? npc1.FargoSouls() : npc.FargoSouls();
      if (fargoSoulsGlobalNpc.GrazeCD != 0)
        return;
      FargoSoulsPlayer fargoPlayer = Main.LocalPlayer.FargoSouls();
      if (!fargoPlayer.Graze || Main.LocalPlayer.immune || Main.LocalPlayer.hurtCooldowns[0] > 0 || Main.LocalPlayer.hurtCooldowns[1] > 0)
        return;
      Vector2 vector2 = FargoSoulsUtil.ClosestPointInHitbox(((Entity) npc).Hitbox, ((Entity) Main.LocalPlayer).Center);
      int num = -1;
      if ((double) ((Entity) Main.LocalPlayer).Distance(vector2) >= (double) fargoPlayer.GrazeRadius || !NPCLoader.CanHitPlayer(npc, Main.LocalPlayer, ref num) || npc.ModNPC != null && !npc.ModNPC.CanHitPlayer(Main.LocalPlayer, ref num) || !npc.noTileCollide && !Collision.CanHitLine(vector2, 0, 0, ((Entity) Main.LocalPlayer).Center, 0, 0))
        return;
      fargoSoulsGlobalNpc.GrazeCD = 30;
      if (fargoPlayer.DeviGraze)
        SparklingAdoration.OnGraze(fargoPlayer, npc.damage);
      if (!fargoPlayer.CirnoGraze)
        return;
      IceQueensCrown.OnGraze(fargoPlayer, npc.damage);
    }

    public virtual void DrawEffects(NPC npc, ref Color drawColor)
    {
      if (this.LeadPoison && Main.rand.Next(4) < 3)
      {
        int index = Dust.NewDust(new Vector2(((Entity) npc).position.X - 2f, ((Entity) npc).position.Y - 2f), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 82, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust1 = Main.dust[index];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1.8f);
        Dust dust2 = Main.dust[index];
        dust2.velocity.Y -= 0.5f;
        if (Utils.NextBool(Main.rand, 4))
        {
          dust2.noGravity = false;
          dust2.scale *= 0.5f;
        }
      }
      if ((this.Corrupted || this.CorruptedForce) && Main.rand.Next(8) < 9)
      {
        int index = Dust.NewDust(new Vector2(((Entity) npc).position.X - 2f, ((Entity) npc).position.Y - 2f), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 27, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.Y -= 10f;
      }
      if (this.OriPoison && Main.rand.Next(4) < 3)
      {
        int index = Dust.NewDust(new Vector2(((Entity) npc).position.X - 2f, ((Entity) npc).position.Y - 2f), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 242, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust3 = Main.dust[index];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 1.8f);
        Dust dust4 = Main.dust[index];
        dust4.velocity.Y -= 0.5f;
        if (Utils.NextBool(Main.rand, 4))
        {
          dust4.noGravity = false;
          dust4.scale *= 0.5f;
        }
      }
      if (this.MagicalCurse && Utils.NextBool(Main.rand, 4))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, Utils.NextBool(Main.rand) ? 107 : 157, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.2f);
        Main.dust[index].scale = 1.5f;
      }
      if (this.Sublimation && Utils.NextBool(Main.rand, 4))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 263, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(220, (int) byte.MaxValue, 220), 2.5f);
        --Main.dust[index].velocity.Y;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        Main.dust[index].noGravity = true;
      }
      if (this.HellFire && Main.rand.Next(4) < 3)
      {
        int index = Dust.NewDust(new Vector2(((Entity) npc).position.X - 2f, ((Entity) npc).position.Y - 2f), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 259, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
        Dust dust = Main.dust[index];
        dust.velocity.Y -= 0.5f;
        if (Utils.NextBool(Main.rand, 4))
        {
          dust.noGravity = false;
          dust.scale *= 0.5f;
        }
      }
      if (this.HellFireMarked && Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 259, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 4f);
        Main.dust[index].noGravity = true;
        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
        Dust dust5 = Main.dust[index];
        dust5.velocity.Y -= 0.5f;
        if (Utils.NextBool(Main.rand, 4))
        {
          dust5.noGravity = false;
          dust5.scale *= 0.5f;
        }
        Dust dust6 = dust5;
        dust6.velocity = Vector2.op_Multiply(dust6.velocity, 3f);
      }
      if (this.SBleed && Main.rand.Next(4) < 3)
      {
        int index = Dust.NewDust(new Vector2(((Entity) npc).position.X - 2f, ((Entity) npc).position.Y - 2f), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 5, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
        Dust dust = Main.dust[index];
        dust.velocity.Y -= 0.5f;
        if (Utils.NextBool(Main.rand, 4))
        {
          dust.noGravity = false;
          dust.scale *= 0.5f;
        }
      }
      if (this.Suffocation)
        drawColor = Colors.RarityPurple;
      if (this.Electrified)
      {
        if (Main.rand.Next(4) < 3)
        {
          int index = Dust.NewDust(new Vector2(((Entity) npc).position.X - 2f, ((Entity) npc).position.Y - 2f), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 229, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
          if (Utils.NextBool(Main.rand, 3))
          {
            Main.dust[index].noGravity = false;
            Main.dust[index].scale *= 0.5f;
          }
        }
        Lighting.AddLight((int) ((Entity) npc).Center.X / 16, (int) ((Entity) npc).Center.Y / 16, 0.3f, 0.8f, 1.1f);
      }
      if (this.CurseoftheMoon)
      {
        int index1 = Dust.NewDust(((Entity) npc).Center, 0, 0, 229, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
        Main.dust[index1].noGravity = true;
        Dust dust7 = Main.dust[index1];
        dust7.velocity = Vector2.op_Multiply(dust7.velocity, 3f);
        Main.dust[index1].scale += 0.5f;
        if (Main.rand.Next(4) < 3)
        {
          int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 229, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          --Main.dust[index2].velocity.Y;
          Dust dust8 = Main.dust[index2];
          dust8.velocity = Vector2.op_Multiply(dust8.velocity, 2f);
        }
      }
      if (this.Sadism && Utils.NextBool(Main.rand, 7))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 156, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, Color.White, 4f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
      }
      if (this.GodEater)
      {
        if (Utils.NextBool(Main.rand, 7))
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) npc).position, new Vector2(2f, 2f)), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 86, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, Color.White, 4f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.2f);
          Main.dust[index].velocity.Y -= 0.15f;
        }
        Lighting.AddLight(((Entity) npc).position, 0.15f, 0.03f, 0.09f);
      }
      if (this.Chilled)
      {
        int index3 = Dust.NewDust(((Entity) npc).Center, 0, 0, 15, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
        Main.dust[index3].noGravity = true;
        Dust dust9 = Main.dust[index3];
        dust9.velocity = Vector2.op_Multiply(dust9.velocity, 3f);
        Main.dust[index3].scale += 0.5f;
        if (Main.rand.Next(4) < 3)
        {
          int index4 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 15, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index4].noGravity = true;
          --Main.dust[index4].velocity.Y;
          Dust dust10 = Main.dust[index4];
          dust10.velocity = Vector2.op_Multiply(dust10.velocity, 2f);
        }
      }
      if (this.FlamesoftheUniverse && !Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 203, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5, 0), 2.5f);
        --Main.dust[index].velocity.Y;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        Main.dust[index].noGravity = true;
      }
      if (this.Smite && !Utils.NextBool(Main.rand, 4))
      {
        Color discoColor = Main.DiscoColor;
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 91, 0.0f, 0.0f, 100, discoColor, 2.5f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        Main.dust[index].noGravity = true;
      }
      if (this.Anticoagulation && !Utils.NextBool(Main.rand, 4))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        ++Main.dust[index].scale;
      }
      if (this.BloodDrinker && !Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 235, ((Entity) npc).velocity.X * 0.2f, ((Entity) npc).velocity.Y * 0.2f, 0, Color.White, 2.5f);
        Main.dust[index].noGravity = true;
      }
      if (this.PungentGazeTime <= 0 || !Utils.NextBool(Main.rand))
        return;
      float num = (float) this.PungentGazeTime / 300f;
      int index5 = Dust.NewDust(((Entity) npc).Center, 0, 0, 90, ((Entity) npc).velocity.X * 0.2f, ((Entity) npc).velocity.Y * 0.2f, 0, Color.White, 1f);
      Main.dust[index5].scale = MathHelper.Lerp(0.5f, 3f, num);
      Dust dust11 = Main.dust[index5];
      dust11.velocity = Vector2.op_Multiply(dust11.velocity, Main.dust[index5].scale);
      Main.dust[index5].noGravity = true;
    }

    public virtual void PostDraw(
      NPC npc,
      SpriteBatch spriteBatch,
      Vector2 screenPos,
      Color drawColor)
    {
      int num1 = 0;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (projectile.TypeAlive<BaronTuskShrapnel>() && projectile.owner == Main.myPlayer && Luminance.Common.Utilities.Utilities.As<BaronTuskShrapnel>(projectile).EmbeddedNPC == npc)
          ++num1;
      }
      if (num1 >= 15)
      {
        Texture2D texture2D = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing", (AssetRequestMode) 1));
        Rectangle bounds = texture2D.Bounds;
        Vector2 vector2 = Vector2.op_Division(Utils.Size(bounds), 2f);
        Color red = Color.Red;
        float num2 = npc.scale / 3f;
        spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) npc).Center, Main.screenPosition), new Vector2(0.0f, npc.gfxOffY)), new Rectangle?(bounds), red, npc.rotation, vector2, num2, (SpriteEffects) 0, 0.0f);
      }
      base.PostDraw(npc, spriteBatch, screenPos, drawColor);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      if (!this.Chilled)
        return new Color?();
      drawColor = Color.LightBlue;
      return new Color?(drawColor);
    }

    public virtual void UpdateLifeRegen(NPC npc, ref int damage)
    {
      FargoSoulsPlayer modPlayer = Main.player[Main.myPlayer].FargoSouls();
      if (this.Rotting)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 100;
        if (damage < 5)
          damage = 5;
      }
      if (this.LeadPoison)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        int num = npc.type == 14 ? 4 : 20;
        ModNPC modNpc;
        if (Terraria.ModLoader.ModLoader.HasMod("CalamityMod") && ModContent.TryFind<ModNPC>("CalamityMod", "DesertScourgeBody", ref modNpc) && npc.type == modNpc.Type)
          num = 4;
        if (((IEnumerable<Player>) Main.player).Any<Player>((Func<Player, bool>) (p =>
        {
          if (p.Alive() && p.HasEffect<LeadEffect>())
          {
            FargoSoulsPlayer fargoSoulsPlayer = p.FargoSouls();
            switch (fargoSoulsPlayer)
            {
              case null:
              case null:
                break;
              default:
                return fargoSoulsPlayer.ForceEffect<LeadEnchant>();
            }
          }
          return false;
        })))
          num *= 3;
        npc.lifeRegen -= num;
      }
      if (this.SolarFlare)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 100;
        if (damage < 10)
          damage = 10;
      }
      if (this.HellFire)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        int num1 = this.HellFireMarked ? 5 : 1;
        npc.lifeRegen -= 200 * num1;
        int num2 = 50 * num1;
        if (damage < num2)
          damage = num2;
      }
      if (this.Sublimation)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 50;
        if (damage < 5)
          damage = 5;
      }
      if (this.OriPoison)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 40;
        if (damage < 4)
          damage = 4;
      }
      if (this.Infested)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= this.InfestedExtraDot(npc);
        if (damage < 8)
          damage = 8;
      }
      else
        this.MaxInfestTime = 0;
      if (this.Electrified)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 4;
        if (Vector2.op_Inequality(((Entity) npc).velocity, Vector2.Zero))
          npc.lifeRegen -= 16;
        if (((Entity) npc).wet)
          npc.lifeRegen -= 16;
        if (damage < 4)
          damage = 4;
      }
      if (this.CurseoftheMoon)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 24;
        if (damage < 6)
          damage = 6;
      }
      if (this.OceanicMaul)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 48;
        if (damage < 12)
          damage = 12;
      }
      if (this.Sadism)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 306;
        if (damage < 70)
          damage = 70;
      }
      if (this.MutantNibble)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        if (npc.lifeRegenCount > 0)
          npc.lifeRegenCount = 0;
        if (npc.life > 0 && this.LifePrevious > 0)
        {
          if (npc.life > this.LifePrevious)
            npc.life = this.LifePrevious;
          else
            this.LifePrevious = npc.life;
        }
      }
      else
        this.LifePrevious = npc.life;
      if (this.GodEater)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 4200;
        if (damage < 777)
          damage = 777;
      }
      if (this.Suffocation)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= (int) (40.0 * (double) Math.Min(1f, (float) (1.0 * (double) this.SuffocationTimer / 480.0)));
        if (damage < 5)
          damage = 5;
      }
      if (this.FlamesoftheUniverse)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 79;
        if (damage < 20)
          damage = 20;
      }
      if (this.Anticoagulation)
      {
        if (npc.lifeRegen > 0)
          npc.lifeRegen = 0;
        npc.lifeRegen -= 16;
        if (damage < 6)
          damage = 6;
      }
      if (modPlayer.Player.HasEffect<OrichalcumEffect>() && npc.lifeRegen < 0)
        OrichalcumEffect.OriDotModifier(npc, modPlayer, ref damage);
      if (this.MagicalCurse && npc.lifeRegen < 0)
      {
        npc.lifeRegen *= 2;
        damage *= 2;
      }
      if (!this.TimeFrozen || npc.life != 1 || npc.lifeRegen >= 0)
        return;
      npc.lifeRegen = 0;
    }

    private int InfestedExtraDot(NPC npc)
    {
      int buffIndex = npc.FindBuffIndex(ModContent.BuffType<InfestedBuff>());
      if (buffIndex == -1)
        return 0;
      int num1 = npc.buffTime[buffIndex];
      if (this.MaxInfestTime <= 0)
        this.MaxInfestTime = num1;
      float num2 = (float) (this.MaxInfestTime - num1) / 30f;
      int num3 = (int) ((double) num2 * (double) num2 + 8.0);
      this.InfestedDust = (float) ((double) num2 / 15.0 + 0.5);
      if ((double) this.InfestedDust <= 5.0)
        return num3;
      this.InfestedDust = 5f;
      return num3;
    }

    public virtual void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
      if (player.FargoSouls().Bloodthirsty)
      {
        spawnRate = (int) ((double) spawnRate * 0.01);
        maxSpawns *= 3;
      }
      if (!player.HasEffect<SinisterIconEffect>())
        return;
      spawnRate /= 2;
      maxSpawns *= 2;
    }

    public virtual void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
      if (spawnInfo.Player.FargoSouls().PungentEyeball)
      {
        foreach (KeyValuePair<int, float> keyValuePair in (IEnumerable<KeyValuePair<int, float>>) pool)
        {
          if (FargoSoulsGlobalNPC.RareNPCs.Contains(keyValuePair.Key))
            pool[keyValuePair.Key] = keyValuePair.Value * 5f;
        }
      }
      int spawnTileY = spawnInfo.SpawnTileY;
      if (!(Main.dayTime & ((double) spawnTileY < Main.worldSurface && !spawnInfo.Sky)) || !spawnInfo.PlayerInTown || !FargowiltasSouls.FargowiltasSouls.NoBiome(spawnInfo) || !FargowiltasSouls.FargowiltasSouls.NoZone(spawnInfo))
        return;
      pool[ModContent.NPCType<TophatSquirrelCritter>()] = 0.03f;
    }

    private static int[] IllegalLootMultiplierNPCs
    {
      get => new int[4]{ 551, 14, 13, 15 };
    }

    public virtual void OnKill(NPC npc)
    {
      Player player = Main.player[npc.lastInteraction];
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      if (player.HasEffect<NecroEffect>() && !npc.boss)
        NecroEffect.NecroSpawnGraveEnemy(npc, player, modPlayer);
      if (!this.lootMultiplierCheck)
      {
        this.lootMultiplierCheck = true;
        if (player.HasEffect<SinisterIconDropsEffect>() && !npc.boss && !((IEnumerable<int>) FargoSoulsGlobalNPC.IllegalLootMultiplierNPCs).Contains<int>(npc.type))
          npc.NPCLoot();
        if (player.FargoSouls().PlatinumEffect != null && !npc.boss && Utils.NextBool(Main.rand, player.FargoSouls().ForceEffect(new int?(player.FargoSouls().PlatinumEffect.type)) ? 3 : 5) && !((IEnumerable<int>) FargoSoulsGlobalNPC.IllegalLootMultiplierNPCs).Contains<int>(npc.type))
        {
          int num = 5;
          npc.extraValue /= num;
          for (int index = 0; index < num - 1; ++index)
            npc.NPCLoot();
        }
      }
      if (!npc.boss || WorldSavingSystem.DownedAnyBoss)
        return;
      WorldSavingSystem.DownedAnyBoss = true;
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      switch (npc.type)
      {
        case 4:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<LeashOfCthulhu>()));
          break;
        case 13:
        case 14:
        case 15:
          LeadingConditionRule leadingConditionRule1 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.LegacyHack_IsABoss());
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, BossDrop(ModContent.ItemType<EaterLauncherJr>()), false);
          ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule1);
          break;
        case 35:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<BoneZone>()));
          break;
        case 50:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<SlimeKingsSlasher>()));
          break;
        case 113:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<FleshHand>()));
          break;
        case 125:
        case 126:
          LeadingConditionRule leadingConditionRule2 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.MissingTwin());
          Chains.OnSuccess((IItemDropRule) leadingConditionRule2, BossDrop(ModContent.ItemType<TwinRangs>()), false);
          ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule2);
          break;
        case (int) sbyte.MaxValue:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<RefractorBlaster>()));
          break;
        case 134:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<DestroyerGun>()));
          break;
        case 222:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<TheSmallSting>()));
          break;
        case 245:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<RockSlide>()));
          break;
        case 262:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<Dicer>()));
          break;
        case 266:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<BrainStaff>()));
          break;
        case 370:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<FishStick>()));
          break;
        case 398:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<MoonBow>()));
          break;
        case 476:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.OneFromOptions(1, new int[3]
          {
            ModContent.ItemType<Vineslinger>(),
            ModContent.ItemType<Mahoguny>(),
            ModContent.ItemType<OvergrownKey>()
          }));
          break;
        case 551:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<DragonBreath>()));
          break;
        case 636:
          ((NPCLoot) ref npcLoot).Add(BossDrop(ModContent.ItemType<PrismaRegalia>()));
          break;
      }

      static IItemDropRule BossDrop(int item)
      {
        return (IItemDropRule) new DropBasedOnEMode(ItemDropRule.Common(item, 3, 1, 1), ItemDropRule.Common(item, 10, 1, 1));
      }
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (this.TimeFrozen)
      {
        npc.life = 1;
        return false;
      }
      Player player = FargoSoulsUtil.PlayerExists(npc.lastInteraction);
      if (player == null)
        return base.CheckDead(npc);
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      if (player.HasEffect<WoodCompletionEffect>())
        WoodCompletionEffect.WoodCheckDead(modPlayer, npc);
      if (this.Needled && npc.lifeMax > 1 && npc.lifeMax != int.MaxValue)
        CactusEffect.CactusProc(npc, player);
      return base.CheckDead(npc);
    }

    public virtual void OnHitByItem(
      NPC npc,
      Player player,
      Item item,
      NPC.HitInfo hit,
      int damageDone)
    {
      this.OnHitByEither(npc, player, damageDone);
    }

    public virtual void OnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
      this.OnHitByEither(npc, Main.player[projectile.owner], damageDone);
    }

    public void OnHitByEither(NPC npc, Player player, int damageDone)
    {
      if (this.Anticoagulation && ((Entity) player).whoAmI == Main.myPlayer)
      {
        int index = ModContent.ProjectileType<Bloodshed>();
        if (Utils.NextBool(Main.rand, player.ownedProjectileCounts[index] + 2))
          Projectile.NewProjectile(((Entity) npc).GetSource_OnHurt((Entity) player, (string) null), ((Entity) npc).Center, Utils.NextVector2Circular(Main.rand, 12f, 12f), index, 0, 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
      }
      if (damageDone <= 0 || !player.HasEffect<NecroEffect>() || !npc.boss)
        return;
      NecroEffect.NecroSpawnGraveBoss(this, npc, player, damageDone);
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return !this.TimeFrozen;
    }

    public virtual void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
    {
      Main.player[Main.myPlayer].FargoSouls();
      if (target.type != ModContent.NPCType<CreeperGutted>())
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 20f);
    }

    public virtual bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
      return this.TimeFrozen && npc.life == 1 ? new bool?(false) : new bool?();
    }

    public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
      return this.TimeFrozen && npc.life == 1 ? new bool?(false) : new bool?();
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      FargoSoulsPlayer fargoSoulsPlayer = Main.player[Main.myPlayer].FargoSouls();
      if (this.Corrupted)
      {
        ref AddableFloat local = ref modifiers.ArmorPenetration;
        local = AddableFloat.op_Addition(local, 10f);
      }
      if (this.CorruptedForce)
      {
        ref AddableFloat local = ref modifiers.ArmorPenetration;
        local = AddableFloat.op_Addition(local, 40f);
      }
      if (this.OceanicMaul)
      {
        ref AddableFloat local = ref modifiers.ArmorPenetration;
        local = AddableFloat.op_Addition(local, 20f);
      }
      if (this.CurseoftheMoon)
      {
        ref AddableFloat local = ref modifiers.ArmorPenetration;
        local = AddableFloat.op_Addition(local, 10f);
      }
      if (this.Rotting)
      {
        ref AddableFloat local = ref modifiers.ArmorPenetration;
        local = AddableFloat.op_Addition(local, 10f);
      }
      if (this.Smite)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 1.2f);
      }
      if (this.MoltenAmplify)
      {
        float num = 1.2f;
        if (fargoSoulsPlayer.ForceEffect<MoltenEnchant>())
          num = 1.3f;
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, num);
      }
      if (this.PungentGazeTime > 0)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, (float) (1.0 + 0.15000000596046448 * (double) this.PungentGazeTime / 300.0));
      }
      if (!fargoSoulsPlayer.DeviGraze)
        return;
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, 1f + (float) fargoSoulsPlayer.DeviGrazeBonus);
    }

    public virtual void ModifyShop(NPCShop shop)
    {
      if (((AbstractNPCShop) shop).NpcType != ModContent.NPCType<Deviantt>())
        return;
      NPCShop npcShop = shop;
      Item obj = new Item(ModContent.ItemType<EternityAdvisor>(), 1, 0);
      obj.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray = Array.Empty<Condition>();
      npcShop.Add(obj, conditionArray);
    }

    public virtual void ModifyActiveShop(NPC npc, string shopName, Item[] items)
    {
      if (!Main.player[Main.myPlayer].FargoSouls().WoodEnchantDiscount)
        return;
      WoodEnchant.WoodDiscount(items);
    }

    public virtual void SetupTravelShop(int[] shop, ref int nextSlot)
    {
      if (!Main.hardMode || Main.moonPhase != 0)
        return;
      shop[nextSlot] = ModContent.ItemType<MechLure>();
      ++nextSlot;
    }
  }
}

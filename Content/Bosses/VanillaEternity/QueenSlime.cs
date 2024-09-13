// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.QueenSlime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class QueenSlime : EModeNPCBehaviour
  {
    public int StompTimer;
    public int StompCounter;
    public int RainTimer;
    public int SpikeCounter;
    public float StompVelocityX;
    public float StompVelocityY;
    public bool SpawnedMinions1;
    public bool SpawnedMinions2;
    public bool GelatinSubjectDR;
    public int RainDirection;
    public bool DroppedSummon;
    private const float StompTravelTime = 60f;
    private const float StompGravity = 1.6f;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(657);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.StompTimer);
      binaryWriter.Write7BitEncodedInt(this.StompCounter);
      binaryWriter.Write7BitEncodedInt(this.RainTimer);
      binaryWriter.Write(this.StompVelocityX);
      binaryWriter.Write(this.StompVelocityY);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.StompTimer = binaryReader.Read7BitEncodedInt();
      this.StompCounter = binaryReader.Read7BitEncodedInt();
      this.RainTimer = binaryReader.Read7BitEncodedInt();
      this.StompVelocityX = binaryReader.ReadSingle();
      this.StompVelocityY = binaryReader.ReadSingle();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.25, (MidpointRounding) 0);
      this.StompTimer = -360;
    }

    private bool Stompy(NPC npc)
    {
      if (this.StompTimer == 0)
      {
        this.StompTimer = 1;
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 114f, 0.0f);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
        return false;
      }
      if (this.StompTimer > 0 && this.StompTimer < 30)
      {
        ++this.StompTimer;
        npc.rotation = 0.0f;
        if (QueenSlime.NPCInAnyTiles(npc))
          ((Entity) npc).position.Y -= 16f;
        return false;
      }
      if (this.StompTimer == 30)
      {
        if (!npc.HasValidTarget)
          npc.TargetClosest(false);
        if (npc.HasValidTarget && this.StompCounter++ < 3)
        {
          ++this.StompTimer;
          npc.ai[1] = 1f;
          Vector2 center = ((Entity) Main.player[npc.target]).Center;
          for (int index = 0; index < 3; ++index)
          {
            Tile tileSafely = Framing.GetTileSafely(center);
            if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
              center.Y += 16f;
            else
              break;
          }
          center.Y -= 42f;
          Vector2 vector2 = Vector2.op_Subtraction(center, ((Entity) npc).Bottom);
          if (this.StompCounter == 1 || this.StompCounter == 2)
            vector2.X += 300f * (float) Math.Sign(((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X);
          float num = 60f;
          if (this.StompCounter < 0)
            num /= 2f;
          vector2.X /= num;
          vector2.Y = (float) ((double) vector2.Y / (double) num - 0.800000011920929 * (double) num);
          this.StompVelocityX = vector2.X;
          this.StompVelocityY = vector2.Y;
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          return false;
        }
        this.StompCounter = 0;
        this.StompTimer = -360;
        ((Entity) npc).velocity.X = 0.0f;
        npc.ai[1] = 2000f;
        npc.ai[2] = 1f;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      else if (this.StompTimer > 30)
      {
        npc.rotation = 0.0f;
        npc.noTileCollide = true;
        float num1 = 60f;
        if (this.StompCounter < 0)
          num1 /= 2f;
        if ((double) ++this.StompTimer > (double) num1 + 30.0)
        {
          npc.noTileCollide = false;
          if ((double) ((Entity) npc).velocity.Y == 0.0 || QueenSlime.NPCInAnyTiles(npc) || (double) this.StompTimer >= (double) num1 * 2.0 + 25.0)
          {
            ((Entity) npc).velocity = Vector2.Zero;
            this.StompTimer = !WorldSavingSystem.MasochistModeReal ? 15 : 25;
            if (npc.DeathSound.HasValue)
            {
              SoundStyle soundStyle = npc.DeathSound.Value;
              SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            }
            if (FargoSoulsUtil.HostCheck)
            {
              int num2 = !WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld ? 0 : FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 922, num2, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              for (int index1 = -1; index1 <= 1; index1 += 2)
              {
                Vector2 vector2_1 = Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians(Utils.NextFloat(Main.rand, 10f) * (float) index1), new Vector2());
                for (int index2 = 0; index2 < 12; ++index2)
                {
                  Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 5f, 15f) * (float) index1, Utils.RotatedBy(vector2_1, 0.052359879016876221 * (double) index2 * (double) -index1, new Vector2())), WorldSavingSystem.MasochistModeReal ? 2f : 1.5f);
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, 920, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
            return false;
          }
        }
        float num3 = this.StompVelocityY - ((Entity) npc).velocity.Y;
        if ((double) num3 > 1.6000000238418579)
          ((Entity) npc).position.Y += num3;
        ((Entity) npc).velocity.X = this.StompVelocityX;
        ((Entity) npc).velocity.Y = this.StompVelocityY;
        this.StompVelocityY += 1.6f;
        return false;
      }
      return true;
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.queenSlimeBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return true;
      TrySpawnMinions(ref this.SpawnedMinions1, 0.75);
      TrySpawnMinions(ref this.SpawnedMinions2, 0.25);
      this.GelatinSubjectDR = NPC.AnyNPCs(ModContent.NPCType<GelatinSubject>());
      npc.HitSound = new SoundStyle?(this.GelatinSubjectDR ? SoundID.Item27 : SoundID.NPCHit1);
      if ((double) npc.ai[0] == 5.0)
      {
        if (NPC.AnyNPCs(ModContent.NPCType<GelatinSubject>()))
          npc.ai[1] -= 0.5f;
        if ((double) npc.ai[1] == 45.0 && --this.SpikeCounter < 0)
        {
          this.SpikeCounter = 4;
          EModeNPCBehaviour.NetSync(npc);
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 center = ((Entity) Main.player[npc.target]).Center;
            for (int index = 0; index < 50; ++index)
            {
              Tile tileSafely = Framing.GetTileSafely(center);
              if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
                center.Y += 16f;
              else
                break;
            }
            center.Y -= 21f;
            for (int index = -5; index <= 5; ++index)
            {
              Vector2 vector2_1 = center;
              vector2_1.X += (float) (330 * index);
              float ai0 = 60f + (float) Main.rand.Next(30);
              float ai1 = 0.4f;
              Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) npc).Center);
              vector2_2.X /= ai0;
              vector2_2.Y = (float) ((double) vector2_2.Y / (double) ai0 - 0.5 * (double) ai1 * (double) ai0);
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, ModContent.NPCType<GelatinSlime>(), ((Entity) npc).whoAmI, ai0, ai1, vector2_2.X, vector2_2.Y, npc.target, new Vector2());
            }
          }
        }
      }
      if (npc.life > npc.lifeMax / 2)
      {
        if (this.StompTimer > 0 || (double) npc.ai[0] == 0.0 && (double) ((Entity) npc).velocity.Y == 0.0)
        {
          if (this.StompTimer < 0)
            ++this.StompTimer;
          else
            npc.ai[0] = 4f;
          if (!this.Stompy(npc))
            return false;
        }
      }
      else
      {
        npc.defense = npc.defDefense / 2;
        if (this.RainTimer < 0)
          ++this.RainTimer;
        if (this.RainTimer <= 0 && this.StompTimer < 0)
          ++this.StompTimer;
        if ((double) npc.ai[0] == 0.0)
        {
          if (this.RainTimer == 0)
          {
            if ((double) ((Entity) npc).velocity.Y < 0.0)
              ((Entity) npc).position.Y += ((Entity) npc).velocity.Y;
            --npc.ai[1];
            if (npc.HasValidTarget && (double) Math.Abs(((Entity) npc).Center.Y - (((Entity) Main.player[npc.target]).Center.Y - 250f)) < 32.0)
            {
              this.RainTimer = 1;
              EModeNPCBehaviour.NetSync(npc);
              npc.netUpdate = true;
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, -16f, 0.0f);
            }
          }
          else if (this.RainTimer > 0)
          {
            ((Entity) npc).velocity.X *= 0.9f;
            --npc.ai[1];
            ++this.RainTimer;
            int num = this.RainTimer - 45 - 45;
            if (num < 0)
              num = 0;
            if (this.RainTimer == 45)
              this.RainDirection = Math.Sign(((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X);
            if (this.RainTimer > 45 && this.RainTimer < 525 && this.RainTimer % 5 == 0)
            {
              Vector2 vector2_3;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_3).\u002Ector(((Entity) npc).Center.X, Math.Min(((Entity) npc).Center.Y, ((Entity) Main.player[npc.target]).Center.Y));
              vector2_3.X += 200f * (float) this.RainDirection * (float) Math.Sin(Math.PI / 240.0 * (double) num * 1.5);
              vector2_3.Y -= 500f;
              for (int index = -4; index <= 4; ++index)
              {
                Vector2 vector2_4 = Vector2.op_Addition(vector2_3, Utils.NextVector2Circular(Main.rand, 32f, 32f));
                vector2_4.X += (float) (330 * index);
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_4, Vector2.op_Multiply(8f, Vector2.UnitY), 920, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            bool flag = this.RainTimer > 615;
            if ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 1200.0)
            {
              flag = true;
              this.StompTimer = 0;
              this.StompCounter = -3;
            }
            if (!npc.HasValidTarget)
            {
              npc.TargetClosest(false);
              if (!npc.HasValidTarget)
                flag = true;
            }
            if (flag)
            {
              this.RainTimer = -1000;
              npc.netUpdate = true;
              EModeNPCBehaviour.NetSync(npc);
              if (this.StompTimer == 0)
              {
                npc.ai[0] = 4f;
                npc.ai[1] = 0.0f;
              }
            }
          }
          else
            ++npc.ai[1];
        }
        else if ((double) npc.ai[0] == 4.0)
        {
          if (!this.Stompy(npc))
            return false;
          if (!WorldSavingSystem.MasochistModeReal)
          {
            if ((double) npc.ai[1] == 0.0)
              SoundEngine.PlaySound(ref npc.DeathSound, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            if ((double) npc.ai[1] < 70.0)
            {
              float num = (float) (1.0 - (double) npc.ai[1] / 70.0);
              ((Entity) npc).position.Y -= ((Entity) npc).velocity.Y * num;
            }
          }
        }
        else if ((double) npc.ai[0] == 5.0 && npc.HasValidTarget && (double) ((Entity) npc).Bottom.Y > (double) ((Entity) Main.player[npc.target]).Top.Y - 80.0 && (double) ((Entity) npc).velocity.Y > -8.0)
          ((Entity) npc).velocity.Y -= 0.8f;
      }
      EModeUtils.DropSummon(npc, "JellyCrystal", NPC.downedQueenSlime, ref this.DroppedSummon, Main.hardMode);
      return true;

      void TrySpawnMinions(ref bool check, double threshold)
      {
        if (check || (double) npc.life >= (double) npc.lifeMax * threshold)
          return;
        check = true;
        FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.GelatinSubjects", new Color(175, 75, (int) byte.MaxValue));
        for (int index = 0; index < 7; ++index)
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, ModContent.NPCType<GelatinSubject>(), ((Entity) npc).whoAmI, target: npc.target, velocity: Vector2.op_Multiply(Utils.NextFloat(Main.rand, 8f), Utils.RotatedByRandom(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), 1.5707963705062866)));
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
      }
    }

    private static bool NPCInAnyTiles(NPC npc)
    {
      bool flag = false;
      for (int index = 0; index < ((Entity) npc).width; index += 16)
      {
        for (float num = (float) (((Entity) npc).height / 2); (double) num < (double) ((Entity) npc).height; num += 16f)
        {
          Tile tileSafely = Framing.GetTileSafely((int) ((double) ((Entity) npc).position.X + (double) index) / 16, (int) ((double) ((Entity) npc).position.Y + (double) num) / 16);
          if (((Tile) ref tileSafely).HasUnactuatedTile && (Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType]))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (npc.life < npc.lifeMax / 2)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.8f);
      }
      if (this.GelatinSubjectDR)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.25f);
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(137, 240, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 38);
      EModeNPCBehaviour.LoadGore(recolor, 1258);
      EModeNPCBehaviour.LoadGore(recolor, 1259);
      EModeNPCBehaviour.LoadExtra(recolor, 177);
      EModeNPCBehaviour.LoadExtra(recolor, 180);
      EModeNPCBehaviour.LoadExtra(recolor, 185);
    }
  }
}

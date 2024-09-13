// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Hungry2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Hungry2 : Hungry
  {
    private int baseWidth;
    private int baseHeight;

    public override void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.minion = false;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.baseWidth = ((Entity) this.Projectile).width = 24;
      this.baseHeight = ((Entity) this.Projectile).height = 24;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[0] != 0.0);

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.scale = 1f;
      }
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        Player player = Main.player[this.Projectile.owner];
        if (((Entity) player).active && !player.dead && player.channel && player.HeldItem.type == ModContent.ItemType<FleshCannon>() && player.CheckMana(player.HeldItem.mana, false, false))
        {
          this.Projectile.damage = player.GetWeaponDamage(player.HeldItem, false);
          this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
          this.Projectile.knockBack = player.GetWeaponKnockback(player.HeldItem, player.HeldItem.knockBack);
          if ((double) this.Projectile.scale < 5.0)
          {
            this.Projectile.scale *= 1.008f;
            if ((double) this.Projectile.scale >= 5.0)
            {
              for (int index3 = 0; index3 < 42; ++index3)
              {
                Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 18f), (double) (index3 - 17) * 6.2831854820251465 / 42.0, new Vector2()), ((Entity) this.Projectile).Center);
                Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
                int index4 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 5f);
                Main.dust[index4].noGravity = true;
                Main.dust[index4].scale = 5f;
                Main.dust[index4].velocity = vector2_2;
              }
            }
          }
          this.Projectile.rotation = player.itemRotation;
          if (((Entity) player).direction < 0)
            this.Projectile.rotation += 3.14159274f;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), ((Vector2) ref ((Entity) this.Projectile).velocity).Length());
          float num = (float) (60.0 + 40.0 * (double) this.Projectile.scale / 5.0);
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(num * player.HeldItem.scale, Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2())));
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
          this.Projectile.timeLeft = 240;
        }
        else
        {
          SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.ai[0] = 1f;
          this.Projectile.penetrate = 1;
          this.Projectile.maxPenetrate = 1;
          this.Projectile.netUpdate = true;
          return;
        }
      }
      else
      {
        if (!this.Projectile.tileCollide && !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
          this.Projectile.tileCollide = true;
        this.Projectile.ignoreWater = false;
        ++this.Projectile.ai[0];
        if ((double) this.Projectile.ai[0] > 5.0)
        {
          this.Projectile.ai[0] = 5f;
          NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 600f, true), Array.Empty<int>());
          if (npc.Alive())
            ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 60f), 0.1f);
          else if (this.Projectile.timeLeft > 120)
            this.Projectile.timeLeft = 120;
        }
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      }
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = (int) ((double) this.baseWidth * (double) this.Projectile.scale);
      ((Entity) this.Projectile).height = (int) ((double) this.baseHeight * (double) this.Projectile.scale);
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      this.Projectile.rotation += 1.57079637f;
      this.Projectile.damage = (int) ((double) this.Projectile.ai[1] * (double) this.Projectile.scale);
      if ((double) this.Projectile.scale >= 2.5)
        return;
      this.Projectile.damage /= 2;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if ((double) this.Projectile.scale < 5.0)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 1.5f);
      ((NPC.HitModifiers) ref modifiers).SetCrit();
    }

    public override void OnKill(int timeleft)
    {
      if ((double) this.Projectile.scale >= 5.0)
      {
        for (int index1 = 0; index1 < 40; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), Utils.NextFloat(Main.rand, 3f, 6f));
          if (Utils.NextBool(Main.rand, 3))
            Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 12f, 24f));
          Main.dust[index2].position = ((Entity) this.Projectile).Center;
        }
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index5 = 0; index5 < 20; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index6].noGravity = true;
        Dust dust1 = Main.dust[index6];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index7];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      float num = 0.5f;
      for (int index8 = 0; index8 < 4; ++index8)
      {
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index9];
        gore.velocity = Vector2.op_Multiply(gore.velocity, num);
        ++Main.gore[index9].velocity.X;
        ++Main.gore[index9].velocity.Y;
      }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = -oldVelocity.X;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
        ((Entity) this.Projectile).velocity.Y = -oldVelocity.Y;
      return false;
    }
  }
}

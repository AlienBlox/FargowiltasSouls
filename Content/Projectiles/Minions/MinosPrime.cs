// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.MinosPrime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class MinosPrime : ModProjectile
  {
    private Vector2 mousePos;
    private int syncTimer;
    private float Wobble;
    public int ColorTimer;

    public ref float Timer => ref this.Projectile.ai[0];

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 9;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 84;
      ((Entity) this.Projectile).height = 98;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual bool? CanHitNPC(NPC target) => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).active && !player.dead && player.HasEffect<PrimeSoulEffect>())
        this.Projectile.timeLeft = 2;
      this.SyncMouse(player);
      this.Wobble = 20f * (float) Math.Sin(6.2831854820251465 * ((double) this.Timer / 60.0));
      this.Movement(player);
    }

    public void SyncMouse(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      this.mousePos = Main.MouseWorld;
      if (++this.syncTimer <= 20)
        return;
      this.syncTimer = 0;
      this.Projectile.netUpdate = true;
    }

    public void Movement(Player player)
    {
      int num1 = Math.Sign(this.mousePos.X - ((Entity) player).Center.X);
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = -num1;
      Vector2 vector2_1 = Vector2.op_Subtraction(Vector2.op_Addition(this.mousePos, Vector2.op_Multiply(Vector2.UnitX, (float) (num1 * 225))), ((Entity) this.Projectile).Center);
      float num2 = ((Vector2) ref vector2_1).Length();
      float num3 = 38f;
      float num4 = 15f;
      ((Vector2) ref vector2_1).Normalize();
      Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, num3);
      ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num4 - 1f), vector2_2), num4);
      if ((double) num2 == 0.0)
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      if ((double) num2 < (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length())
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num2);
      if (!Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero) || (double) num2 <= 10.0)
        return;
      ((Entity) this.Projectile).velocity.X = -0.15f;
      ((Entity) this.Projectile).velocity.Y = -0.05f;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = lightColor;
      Player player = Main.player[this.Projectile.owner];
      if (player.Alive())
      {
        Color? color2 = this.GetColor(player);
        if (color2.HasValue)
          color1 = color2.Value;
      }
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 3)
      {
        Color color3 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), Vector2.op_Multiply(Vector2.UnitY, this.Wobble)), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num3, vector2, this.Projectile.scale, this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), Vector2.op_Multiply(Vector2.UnitY, this.Wobble)), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, this.Projectile.rotation, vector2, this.Projectile.scale, this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      return false;
    }

    public Color? GetColor(Player player)
    {
      ++this.ColorTimer;
      List<Color> colorList = new List<Color>();
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.MeleeSoul)
        colorList.Add(BerserkerSoul.ItemColor);
      if (fargoSoulsPlayer.RangedSoul)
        colorList.Add(SnipersSoul.ItemColor);
      if (fargoSoulsPlayer.MagicSoul)
        colorList.Add(ArchWizardsSoul.ItemColor);
      if (fargoSoulsPlayer.SummonSoul)
        colorList.Add(ConjuristsSoul.ItemColor);
      if (fargoSoulsPlayer.WorldShaperSoul)
        colorList.Add(WorldShaperSoul.ItemColor);
      if (fargoSoulsPlayer.MasochistSoul)
        colorList.Add(MasochistSoul.ItemColor);
      if (fargoSoulsPlayer.SupersonicSoul)
        colorList.Add(SupersonicSoul.ItemColor);
      if (fargoSoulsPlayer.ColossusSoul)
        colorList.Add(ColossusSoul.ItemColor);
      if (fargoSoulsPlayer.FlightMasterySoul)
        colorList.Add(FlightMasterySoul.ItemColor);
      if (fargoSoulsPlayer.FishSoul2)
        colorList.Add(TrawlerSoul.ItemColor);
      int count = colorList.Count;
      if (count == 0)
        return new Color?();
      if ((double) this.ColorTimer >= 300.0 * (double) count)
        this.ColorTimer = 1;
      float num = (float) ((double) this.ColorTimer % 300.0 / 300.0);
      int index1 = (int) Math.Floor((double) this.ColorTimer / 300.0);
      if (count <= 1)
        return new Color?(colorList[index1]);
      int index2 = index1 + 1;
      if (index2 >= count)
        index2 = 0;
      return new Color?(Color.Lerp(colorList[index1], colorList[index2], num));
    }
  }
}

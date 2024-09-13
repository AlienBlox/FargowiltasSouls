// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomSword
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomSword : AbomSpecialDeathray
  {
    public int counter;
    public bool spawnedHandle;

    public AbomSword()
      : base(300)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.extraUpdates = 1;
      this.Projectile.netImportant = true;
    }

    public override void AI()
    {
      base.AI();
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
        {
          SoundStyle soundStyle1;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle1).\u002Ector("FargowiltasSouls/Assets/Sounds/StyxGazer", (SoundType) 0);
          ((SoundStyle) ref soundStyle1).Volume = 1.5f;
          SoundEngine.PlaySound(ref soundStyle1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          SoundStyle soundStyle2 = new SoundStyle("FargowiltasSouls/Assets/Sounds/RetinazerDeathray", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle2, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        float num1 = 1f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) (Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * (double) num1 * 6.0);
          if ((double) this.Projectile.scale > (double) num1)
            this.Projectile.scale = num1;
          float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          if ((Vector2.op_Inequality(((Entity) npc).velocity, Vector2.Zero) || (double) npc.ai[0] == 19.0) && (double) npc.ai[0] != 20.0)
            rotation += this.Projectile.ai[0] / (float) this.Projectile.MaxUpdates;
          this.Projectile.rotation = rotation - 1.57079637f;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation);
          float length = 3f;
          int width = ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
          {
            Vector2 vector2_1 = nullable.Value;
          }
          float[] numArray = new float[(int) length];
          for (int index = 0; index < numArray.Length; ++index)
            numArray[index] = 3000f;
          float num2 = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num2 += numArray[index];
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
          if ((double) this.Projectile.localAI[0] % 2.0 == 0.0)
          {
            if (Vector2.op_Inequality(((Entity) npc).velocity, Vector2.Zero) && --this.counter < 0)
            {
              this.counter = 5;
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2_2 = ((Entity) this.Projectile).Center;
                Vector2 vector2_3 = Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 2.0 * (double) Math.Sign(this.Projectile.ai[0]), new Vector2());
                for (int index = 1; index <= 15; ++index)
                {
                  vector2_2 = Vector2.op_Addition(vector2_2, Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3000f), 15f));
                  Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), vector2_2, vector2_3, ModContent.ProjectileType<AbomSickle2>(), this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
                }
              }
            }
            for (int index1 = 0; index1 < 15; ++index1)
            {
              int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand, 2000f))), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
          }
          if (this.spawnedHandle)
            return;
          this.spawnedHandle = true;
          if (!FargoSoulsUtil.HostCheck)
            return;
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<AbomSwordHandle>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 1.57079637f, (float) this.Projectile.identity, 0.0f);
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<AbomSwordHandle>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -1.57079637f, (float) this.Projectile.identity, 0.0f);
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      ((Entity) target).velocity.X = (double) ((Entity) target).Center.X < (double) ((Entity) Main.npc[(int) this.Projectile.ai[1]]).Center.X ? -15f : 15f;
      ((Entity) target).velocity.Y = -10f;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) target).Center, Utils.NextVector2Circular(Main.rand, 100f, 100f)), Vector2.Zero, ModContent.ProjectileType<AbomBlast>(), 0, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
        target.AddBuff(67, 180, true, false);
      }
      target.AddBuff(195, 600, true, false);
      target.AddBuff(196, 600, true, false);
    }

    public float WidthFunction(float _)
    {
      return (float) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale * 2.0);
    }

    public static Color ColorFunction(float _) => new Color(253, 254, 32, 100);

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.WillBigDeathray");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance));
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(center, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      Color black = Color.Black;
      shader.TrySetParameter("mainColor", (object) black);
      ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak", (AssetRequestMode) 2).Value.SetTexture1();
      for (int index1 = 0; index1 < 2; ++index1)
      {
        // ISSUE: method pointer
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        PrimitiveSettings primitiveSettings = new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), AbomSword.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (AbomSword.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?());
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, primitiveSettings, new int?(30));
        for (int index2 = 0; index2 < vector2Array.Length / 2; ++index2)
        {
          Vector2 vector2_2 = vector2Array[index2];
          int index3 = vector2Array.Length - 1 - index2;
          vector2Array[index2] = vector2Array[index3];
          vector2Array[index3] = vector2_2;
        }
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, primitiveSettings, new int?(30));
      }
      return false;
    }
  }
}

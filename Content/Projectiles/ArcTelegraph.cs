// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ArcTelegraph
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Lifelight;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class ArcTelegraph : ModProjectile
  {
    private int npc;

    public ref float Timer => ref this.Projectile.ai[0];

    public ref float ArcAngle => ref this.Projectile.ai[1];

    public ref float Width => ref this.Projectile.ai[2];

    public virtual string Texture => "Terraria/Images/Extra_" + (short) 27.ToString();

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 10000;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      ((Entity) this.Projectile).width = 1;
      ((Entity) this.Projectile).height = 1;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(this.npc);
      writer.Write(this.Projectile.localAI[1]);
      writer.Write(this.Projectile.localAI[2]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.npc = reader.Read7BitEncodedInt();
      this.Projectile.localAI[1] = reader.ReadSingle();
      this.Projectile.localAI[2] = reader.ReadSingle();
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity) || entity.type != ModContent.NPCType<LifeChallenger>())
        return;
      this.npc = ((Entity) entity).whoAmI;
      this.Projectile.localAI[1] = MathHelper.WrapAngle(Utils.ToRotation(((Entity) this.Projectile).velocity) - Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[entity.target]).Center, ((Entity) entity).Center)));
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.npc, Array.Empty<int>());
      if (npc != null)
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center), (double) this.Projectile.localAI[1], new Vector2()));
      }
      ++this.Timer;
    }

    public virtual bool ShouldUpdatePosition() => false;

    public float WidthFunction(float progress) => this.Width;

    public Color ColorFunction(float progress)
    {
      return Color.Lerp(Color.Transparent, Color.DeepSkyBlue, Math.Min(this.Timer / 30f, Math.Min((float) this.Projectile.timeLeft / 15f, 1f)));
    }

    public static Matrix GetWorldViewProjectionMatrixIdioticVertexShaderBoilerplate()
    {
      Matrix lookAt = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up);
      Viewport viewport1 = Main.graphics.GraphicsDevice.Viewport;
      double num1 = (double) (((Viewport) ref viewport1).Width / 2);
      viewport1 = Main.graphics.GraphicsDevice.Viewport;
      double num2 = (double) (((Viewport) ref viewport1).Height / -2);
      Matrix translation = Matrix.CreateTranslation((float) num1, (float) num2, 0.0f);
      Matrix matrix = Matrix.op_Multiply(Matrix.op_Multiply(Matrix.op_Multiply(lookAt, translation), Matrix.CreateRotationZ(3.14159274f)), Matrix.CreateScale(Main.GameViewMatrix.Zoom.X, Main.GameViewMatrix.Zoom.Y, 1f));
      Viewport viewport2 = Main.graphics.GraphicsDevice.Viewport;
      double width = (double) ((Viewport) ref viewport2).Width;
      viewport2 = Main.graphics.GraphicsDevice.Viewport;
      double height = (double) ((Viewport) ref viewport2).Height;
      Matrix orthographic = Matrix.CreateOrthographic((float) width, (float) height, 0.0f, 1000f);
      return Matrix.op_Multiply(matrix, orthographic);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Vertex_ArcTelegraph");
      ModContent.Request<Texture2D>("Terraria/Images/Extra_193", (AssetRequestMode) 2).Value.SetTexture1();
      shader.TrySetParameter("mainColor", (object) Color.Lerp(Color.DeepSkyBlue, Color.SlateBlue, 0.7f));
      shader.Apply("AutoloadPass");
      VertexStrip vertexStrip = new VertexStrip();
      List<Vector2> vector2List = new List<Vector2>();
      List<float> floatList = new List<float>();
      float num1 = this.Projectile.rotation - this.ArcAngle * 0.5f;
      for (float num2 = 0.0f; (double) num2 < 1.0; num2 += 0.005f)
      {
        float num3 = num1 + this.ArcAngle * num2;
        vector2List.Add(Vector2.op_Addition(Vector2.op_Multiply(Utils.ToRotationVector2(num3), this.Width), ((Entity) this.Projectile).Center));
        floatList.Add(num3 + 1.57079637f);
      }
      // ISSUE: method pointer
      // ISSUE: method pointer
      vertexStrip.PrepareStrip(vector2List.ToArray(), floatList.ToArray(), new VertexStrip.StripColorFunction((object) this, __methodptr(ColorFunction)), new VertexStrip.StripHalfWidthFunction((object) this, __methodptr(WidthFunction)), Vector2.op_UnaryNegation(Main.screenPosition), new int?(), true);
      vertexStrip.DrawTrail();
      Main.spriteBatch.ExitShaderRegion();
      return false;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?(false);
    }
  }
}

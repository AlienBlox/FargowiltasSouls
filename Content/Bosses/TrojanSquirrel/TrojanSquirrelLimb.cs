// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrelLimb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public abstract class TrojanSquirrelLimb : TrojanSquirrelPart
  {
    protected NPC body;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.hide = true;
    }

    public virtual void DrawBehind(int index) => Main.instance.DrawCacheNPCProjectiles.Add(index);

    public virtual void OnSpawn(IEntitySource source)
    {
      base.OnSpawn(source);
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity))
        return;
      this.body = entity;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
      base.SendExtraAI(writer);
      writer.Write(this.body != null ? ((Entity) this.body).whoAmI : -1);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
      base.ReceiveExtraAI(reader);
      this.body = FargoSoulsUtil.NPCExists(reader.ReadInt32(), Array.Empty<int>());
    }

    public virtual bool PreAI()
    {
      if (this.body != null)
        this.body = FargoSoulsUtil.NPCExists(((Entity) this.body).whoAmI, new int[1]
        {
          ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>()
        });
      if (this.body != null)
        return base.PreAI();
      if (FargoSoulsUtil.HostCheck)
      {
        this.NPC.life = 0;
        if (Main.netMode == 2)
          NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        ((Entity) this.NPC).active = false;
      }
      return false;
    }

    public virtual bool CheckActive() => false;

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npcLoot);
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.DropNothing());
    }

    public virtual void FindFrame(int frameHeight)
    {
      base.FindFrame(frameHeight);
      if (this.body == null)
        return;
      this.NPC.frame = this.body.frame;
    }

    public bool Trail => (double) this.body.ai[0] == 0.0 && (double) this.body.localAI[0] > 0.0;

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (this.body == null)
        return base.PreDraw(spriteBatch, screenPos, drawColor);
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = ((Entity) this.NPC).direction < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      if (this.Trail)
      {
        for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.Type]; ++index)
        {
          float num = this.NPC.oldRot[index];
          Vector2 vector2_2 = Vector2.op_Addition(this.body.oldPos[index], Vector2.op_Division(((Entity) this.body).Size, 2f));
          DrawData drawData;
          // ISSUE: explicit constructor call
          ((DrawData) ref drawData).\u002Ector(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, screenPos), new Vector2(0.0f, this.NPC.gfxOffY - 53f * this.body.scale)), new Rectangle?(frame), Color.op_Multiply(alpha, 0.5f / (float) index), num, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
          GameShaders.Misc["LCWingShader"].UseColor(Color.Blue).UseSecondaryColor(Color.Black);
          GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
          ((DrawData) ref drawData).Draw(spriteBatch);
        }
      }
      Vector2 center = ((Entity) this.body).Center;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY - 53f * this.body.scale)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
